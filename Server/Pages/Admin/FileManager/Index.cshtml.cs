namespace Server.Pages.Admin.FileManager;

[Authorize(Roles = Constants.Role.Admin)]
public class IndexModel : Infrastructure.BasePageModel
{
	public IndexModel(IHostEnvironment hostEnvironment,
		Infrastructure.Settings.ApplicationSettings applicationSettings) : base()
	{
		HostEnvironment = hostEnvironment;
		ApplicationSettings = applicationSettings;
		PageAddress = "/Admin/FileManager/Index";
		PhysicalRootPath = Path.Combine(HostEnvironment.ContentRootPath, "wwwroot");

		Files = new List<FileInfo>();
		Directories = new List<DirectoryInfo>();
	}

	public string PageAddress { get; }

	public string PhysicalRootPath { get; }

	public IHostEnvironment HostEnvironment { get; }

	public Infrastructure.Settings.ApplicationSettings ApplicationSettings { get; }

	public string? CurrentPath { get; set; }

	public string? PhysicalCurrentPath { get; set; }

	public IList<FileInfo> Files { get; set; }

	public IList<DirectoryInfo> Directories { get; set; }


	public void OnGet(string? path)
	{
		try
		{
			CheckPathAndSetCurrentPath(path: path);
			SetDirectoriesAndFiles();
		}
		catch (Exception ex)
		{
			AddToastError(message: ex.Message);
		}
	}


	public void OnPostDeleteItems(string? path, IList<string>? items)
	{
		try
		{
			CheckPathAndSetCurrentPath(path: path);

			if (ApplicationSettings.FileManagerSettings.DeleteItemsEnabled == false)
			{
				SetDirectoriesAndFiles();
				return;
			}

			if (items == null || items.Count == 0)
			{
				var errorMessage = "You did not select any files or folders for deleting!";
				AddToastError(message: errorMessage);
				SetDirectoriesAndFiles();
				return;
			}

			foreach (var item in items)
			{
				try
				{
					var physicalItemPath = Path.Combine(PhysicalCurrentPath, item);

					if (Directory.Exists(path: physicalItemPath))
					{
						Directory.Delete(path: physicalItemPath, recursive: true);
						var successMessage = $"The directory ({item}) deleted successfully.";

						AddToastSuccess(message: successMessage);
					}
					else
					{
						if (System.IO.File.Exists(path: physicalItemPath))
						{
							GC.Collect();
							GC.WaitForPendingFinalizers();

							System.IO.File.Delete(path: physicalItemPath);
							var successMessage = $"The file ({item}) deleted successfully.";

							AddToastSuccess(message: successMessage);
						}
					}
				}
				catch (Exception ex)
				{
					AddToastError(message: ex.Message);
				}
			}

			SetDirectoriesAndFiles();
		}
		catch (Exception ex)
		{
			AddToastError(message: ex.Message);
		}
	}

	public void OnPostCreateDirectory(string? path, string? directoryName)
	{
		try
		{
			CheckPathAndSetCurrentPath(path: path);

			if (ApplicationSettings.FileManagerSettings.CreateDirectoryEnabled == false)
			{
				SetDirectoriesAndFiles();
				return;
			}

			if (string.IsNullOrWhiteSpace(directoryName))
			{
				SetDirectoriesAndFiles();
				return;
			}

			directoryName = directoryName.Replace(" ", string.Empty);

			var physicalPath = Path.Combine(PhysicalCurrentPath, directoryName);

			if (Directory.Exists(path: physicalPath))
			{
				var errorMessage = $"The [{directoryName}] folder already exists!";
				AddPageError(message: errorMessage);
				SetDirectoriesAndFiles();
				return;
			}

			Directory.CreateDirectory(path: physicalPath);

			var successMessage = $"The [{directoryName}] folder has been created successfully.";

			AddToastSuccess(message: successMessage);

			SetDirectoriesAndFiles();
		}
		catch (Exception ex)
		{
			AddToastError(message: ex.Message);
		}
	}


	public async Task OnPostUploadFilesAsync(string? path, List<IFormFile> files)
	{
		try
		{
			CheckPathAndSetCurrentPath(path: path);

			if (ApplicationSettings.FileManagerSettings.UploadFilesEnabled == false)
			{
				SetDirectoriesAndFiles();
				return;
			}

			if (files == null || files.Count == 0)
			{
				var errorMessage = "You did not specify any files for uploading!";

				AddToastError(message: errorMessage);

				return;
			}

			foreach (var file in files)
			{
				await CheckFileValidationAndSaveAsync(overwriteIfFileExists: true, file: file);
			}

			SetDirectoriesAndFiles();
		}
		catch (Exception ex)
		{
			AddToastError(message: ex.Message);
		}
	}


	private async Task<bool> CheckFileValidationAndSaveAsync(bool overwriteIfFileExists, IFormFile? file)
	{
		var result = CheckFileValidation(file: file);

		if (result == false)
		{
			return false;
		}

		var fileName = file!.FileName.Trim().Replace(" ", "_");
		var physicalPathName = Path.Combine(PhysicalCurrentPath, fileName);

		if (overwriteIfFileExists == false)
		{
			if (System.IO.File.Exists(path: physicalPathName))
			{
				var errorMessage = string.Format("File '{0}' already exists!", fileName);

				AddToastError(message: errorMessage);

				return false;
			}
		}

		using (var stream = System.IO.File.Create(path: physicalPathName))
		{
			await file.CopyToAsync(target: stream);
			await stream.FlushAsync();
			stream.Close();
		}

		if (string.Compare(file.FileName, fileName, ignoreCase: true) == 0)
		{
			var successMessage = string.Format("File '{0}' uploaded successfully.", fileName);

			AddToastSuccess(message: successMessage);
		}
		else
		{
			var successMessage = string.Format("File '{0}' with the name of '{1}' uploaded successfully.", file.FileName, fileName);

			AddToastSuccess(message: successMessage);
		}

		return true;
	}


	private bool CheckFileValidation(IFormFile? file)
	{
		if (file == null)
		{
			var errorMessage = "You did not specify any files for uploading!";

			AddToastError(message: errorMessage);

			return false;
		}

		if (file.Length == 0)
		{
			var errorMessage = $"The file ({file.FileName}) did not uploaded successfully!";

			AddToastError(message: errorMessage);

			return false;
		}

		var fileExtension = Path.GetExtension(path: file.FileName)?.ToLower();

		if (fileExtension == null)
		{
			var errorMessage = $"The file ({file.FileName}) does not have any extension!";

			AddToastError(message: errorMessage);

			return false;
		}

		var permittedFileExtensions = ApplicationSettings.FileManagerSettings.PermittedFileExtensions.ToList();

		if (permittedFileExtensions.Contains(item: fileExtension) == false)
		{
			var errorMessage = $"The file ({file.FileName}) does not have a valid extension!";

			AddToastError(message: errorMessage);

			return false;
		}

		return true;
	}

	public void CheckPathAndSetCurrentPath(string? path)
	{
		string fixedPath = "/";

		if (string.IsNullOrWhiteSpace(path) == false)
		{
			fixedPath = path.Replace("\\", "/");

			if (fixedPath.StartsWith("/") == false)
			{
				fixedPath = $"/{fixedPath}";
			}

			if (fixedPath.EndsWith("/") == false)
			{
				fixedPath = $"{fixedPath}/";
			}

			while (fixedPath.Contains("//"))
			{
				fixedPath = fixedPath.Replace("//", "/");
			}
		}

		CurrentPath = fixedPath;

		PhysicalCurrentPath = $"{PhysicalRootPath}{CurrentPath}".Replace("/", "\\");

		if (Directory.Exists(path: PhysicalCurrentPath) == false)
		{
			CurrentPath = "/";
			PhysicalCurrentPath = PhysicalRootPath;
		}
	}


	public void SetDirectoriesAndFiles()
	{
		if (string.IsNullOrWhiteSpace(PhysicalCurrentPath) || Directory.Exists(PhysicalCurrentPath) == false)
		{
			Files = new List<FileInfo>();
			Directories = new List<DirectoryInfo>();
			return;
		}

		var directoryInfo = new DirectoryInfo(path: PhysicalCurrentPath);

		Files = directoryInfo.GetFiles()
			.OrderBy(current => current.Extension)
			.ThenBy(current => current.Name)
			.ToList();

		Directories = directoryInfo.GetDirectories()
			.OrderBy(current => current.Name)
			.ToList();
	}
}
