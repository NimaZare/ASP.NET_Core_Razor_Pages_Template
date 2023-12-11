﻿namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: Constants.TagHelper.ReadOnlyCheckBox, TagStructure = TagStructure.WithoutEndTag)]
public class ReadOnlyCheckBoxTagHelper : TagHelper
{
	public ReadOnlyCheckBoxTagHelper(IHtmlGenerator generator) : base()
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
		var innerDiv = new TagBuilder(tagName: "div");
		innerDiv.AddCssClass(value: "form-check");
		div.InnerHtml.AppendHtml(content: innerDiv);
		var textBoxElement = await CreateCheckBoxElementAsync();
		innerDiv.InnerHtml.AppendHtml(encoded: textBoxElement);
		var labelElement = await CreateLabelElementAsync();
		innerDiv.InnerHtml.AppendHtml(encoded: labelElement);

		output.TagName = null;
		output.TagMode = TagMode.StartTagAndEndTag;
		output.Content.SetHtmlContent(htmlContent: div);
	}

	private async Task<string> CreateLabelElementAsync()
	{
		var tagBuilder = Generator.GenerateLabel(viewContext: ViewContext,
			modelExplorer: For.ModelExplorer, expression: For.Name, labelText: null,
			htmlAttributes: new { @class = "form-check-label" });

		var writer = new StringWriter();
		tagBuilder.WriteTo(writer: writer, encoder: NullHtmlEncoder.Default);
		var result = writer.ToString();
		await writer.DisposeAsync();

		return result;
	}

	private async Task<string> CreateCheckBoxElementAsync()
	{
		TagBuilder tagBuilder;
		bool? isChecked = null;

		if (For.Model != null)
		{
			isChecked = System.Convert.ToBoolean(value: For.Model);
		}

		tagBuilder = Generator.GenerateCheckBox(viewContext: ViewContext,
			modelExplorer: For.ModelExplorer,
			expression: For.Name, isChecked: isChecked, htmlAttributes: null);

		tagBuilder.AddCssClass(value: "form-check-input");
		tagBuilder.Attributes.Add(key: "disabled", value: null);
		var writer = new StringWriter();
		tagBuilder.WriteTo(writer: writer, encoder: NullHtmlEncoder.Default);

		var result = writer.ToString();
		await writer.DisposeAsync();

		return result;
	}
}
