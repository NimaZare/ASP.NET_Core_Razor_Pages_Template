﻿namespace ViewModels.Pages.Account.Users;

public class ChangePasswordViewModel
{
	public string? OldPassword { get; set; }

	public string? NewPassword { get; set; }

	public string? ConfirmNewPassword { get; set; }
}
