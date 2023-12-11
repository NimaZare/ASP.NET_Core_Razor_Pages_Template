namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: "section-form-field", ParentTag = "fieldset", TagStructure = TagStructure.NormalOrSelfClosing)]
public class SectionFormFieldTagHelper : TagHelper
{
	public SectionFormFieldTagHelper() : base()
	{
	}

	public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		var originalContents = await output.GetChildContentAsync();
		var body = new TagBuilder(tagName: "div");
		body.AddCssClass(value: "mb-3");
		body.InnerHtml.AppendHtml(content: originalContents);

		output.TagName = null;
		output.TagMode = TagMode.StartTagAndEndTag;
		output.Content.SetHtmlContent(htmlContent: body);
	}
}
