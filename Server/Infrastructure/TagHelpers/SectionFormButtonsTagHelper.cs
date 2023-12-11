namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: "section-form-buttons", ParentTag = "section-form", TagStructure = TagStructure.NormalOrSelfClosing)]
public class SectionFormButtonsTagHelper : TagHelper
{
	public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		var originalContents = await output.GetChildContentAsync();
		var body = new TagBuilder("div");
		body.AddCssClass(value: "mb-3");
		body.InnerHtml.AppendHtml(content: originalContents);

		output.TagName = null;
		output.TagMode = TagMode.StartTagAndEndTag;
		output.Content.SetHtmlContent(htmlContent: body);
	}
}
