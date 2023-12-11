namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: Constants.TagHelper.ReadOnlyInput, TagStructure = TagStructure.WithoutEndTag)]
public class ReadOnlyInputTagHelper : TagHelper
{
	public ReadOnlyInputTagHelper(IHtmlGenerator generator) : base()
	{
		Generator = generator;
	}

	private IHtmlGenerator Generator { get; }

	[ViewContext]
	[HtmlAttributeNotBound]
	public ViewContext? ViewContext { get; set; }

	[HtmlAttributeName(name: "asp-for")]
	public ModelExpression? For { get; set; }

	public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		var div = new TagBuilder(tagName: "div");
		div.AddCssClass(value: "mb-3");
		var labelElement = await CreateLabelElementAsync();
		div.InnerHtml.AppendHtml(encoded: labelElement);

		string? dirString = null;
		var dirAttribute = output.Attributes["dir"];

		if (dirAttribute != null)
		{
			var dirValue = dirAttribute.Value;

			if (dirValue != null)
			{
				dirString = dirValue.ToString().Replace(oldValue: "{", newValue: string.Empty).Replace(oldValue: "}", newValue: string.Empty);
			}
		}

		var textBoxElement = await CreateTextBoxElementAsync(dir: dirString);
		div.InnerHtml.AppendHtml(encoded: textBoxElement);

		output.TagName = null;
		output.TagMode = TagMode.StartTagAndEndTag;
		output.Content.SetHtmlContent(htmlContent: div);
	}

	private async Task<string> CreateLabelElementAsync()
	{
		var tagBuilder = Generator.GenerateLabel(viewContext: ViewContext,
			modelExplorer: For.ModelExplorer, expression: For.Name, labelText: null,
			htmlAttributes: new { @class = "form-label" });

		var writer = new StringWriter();
		tagBuilder.WriteTo(writer: writer, encoder: NullHtmlEncoder.Default);

		var result = writer.ToString();
		await writer.DisposeAsync();

		return result;
	}

	private async Task<string> CreateTextBoxElementAsync(string? dir)
	{
		TagBuilder tagBuilder;

		bool leftToRight = false;
		bool hasBeenCustomized = false;
		object formattedValue = For.Model;

		if (For.ModelExplorer.ModelType == typeof(Guid))
		{
			leftToRight = true;
			hasBeenCustomized = true;
		}

		if ((For.ModelExplorer.ModelType == typeof(Int16))
			||
			(For.ModelExplorer.ModelType == typeof(Int32))
			||
			(For.ModelExplorer.ModelType == typeof(Int64)))
		{
			leftToRight = true;
			hasBeenCustomized = true;

			if (formattedValue == null)
			{
				formattedValue = Constants.Format.NullValue;
			}
			else
			{
				var valueInteger = System.Convert.ToInt64(value: formattedValue);
				formattedValue = valueInteger.ToString(format: Constants.Format.Integer);
				formattedValue = Convert.DigitsToUnicode(value: formattedValue);
			}
		}

		if (For.ModelExplorer.ModelType == typeof(DateTime))
		{
			leftToRight = true;
			hasBeenCustomized = true;

			if (formattedValue == null)
			{
				formattedValue = Constants.Format.NullValue;
			}
			else
			{
				var valueDateTime = (DateTime)formattedValue;
				formattedValue = valueDateTime.ToString(format: Constants.Format.DateTime);
				formattedValue = Convert.DigitsToUnicode(value: formattedValue);
			}
		}

		if (hasBeenCustomized)
		{
			tagBuilder = Generator.GenerateTextBox(viewContext: ViewContext,
				modelExplorer: For.ModelExplorer,
				expression: For.Name, value: formattedValue,
				format: null, htmlAttributes: null);
		}
		else
		{
			tagBuilder = Generator.GenerateTextBox(viewContext: ViewContext,
				modelExplorer: For.ModelExplorer,
				expression: For.Name, value: For.Model,
				format: null, htmlAttributes: null);
		}

		tagBuilder.AddCssClass(value: "form-control");
		tagBuilder.Attributes.Add(key: "disabled", value: null);

		if (leftToRight)
		{
			tagBuilder.AddCssClass(value: "ltr");
		}
		else
		{
			if (string.IsNullOrWhiteSpace(value: dir) == false)
			{
				tagBuilder.Attributes.Add(key: "dir", value: dir);
			}
		}

		var writer = new StringWriter();
		tagBuilder.WriteTo(writer: writer, encoder: NullHtmlEncoder.Default);
		var result = writer.ToString();
		await writer.DisposeAsync();

		return result;
	}
}
