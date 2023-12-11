namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: "table-actions", ParentTag = "tr", TagStructure = TagStructure.NormalOrSelfClosing)]
public class TableActionsTagHelper : TagHelper
{
	public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		var originalContents = await output.GetChildContentAsync();

		var body = new TagBuilder(tagName: "td");
		body.AddCssClass(value: "text-center");

		body.InnerHtml.AppendHtml(content: originalContents);

		output.TagName = null;
		output.TagMode = TagMode.StartTagAndEndTag;
		output.Content.SetHtmlContent(htmlContent: body);
	}
}
