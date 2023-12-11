namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: Constants.TagHelper.ReadOnlyTextArea, TagStructure = TagStructure.WithoutEndTag)]
public class ReadOnlyTextAreaTagHelper : TagHelper
{
	public ReadOnlyTextAreaTagHelper(IHtmlGenerator generator) : base()
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
		var textBoxElement = await CreateTextAreaElementAsync();
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

	private async Task<string> CreateTextAreaElementAsync()
	{
		TagBuilder tagBuilder;

		tagBuilder = Generator.GenerateTextArea(viewContext: ViewContext,
			modelExplorer: For.ModelExplorer,
			expression: For.Name, rows: 3, columns: 60, htmlAttributes: null);

		tagBuilder.AddCssClass(value: "form-control");
		tagBuilder.Attributes.Add(key: "disabled", value: null);
		var writer = new StringWriter();
		tagBuilder.WriteTo(writer: writer, encoder: NullHtmlEncoder.Default);
		var result = writer.ToString();
		await writer.DisposeAsync();

		return result;
	}
}
