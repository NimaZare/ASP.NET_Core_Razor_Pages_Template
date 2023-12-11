namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: "section-form-check", ParentTag = "fieldset", TagStructure = TagStructure.NormalOrSelfClosing)]
public class SectionFormCheckTagHelper : TagHelper
{
	public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		var originalContents = await output.GetChildContentAsync();
		var div = new TagBuilder(tagName: "div");
		div.AddCssClass(value: "form-check");
		div.InnerHtml.AppendHtml(content: originalContents);
		var body = new TagBuilder(tagName: "div");
		body.AddCssClass(value: "mb-3");
		body.InnerHtml.AppendHtml(content: div);

		output.TagName = null;
		output.TagMode = TagMode.StartTagAndEndTag;
		output.Content.SetHtmlContent(htmlContent: body);
	}
}
