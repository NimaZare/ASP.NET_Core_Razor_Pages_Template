namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: "section-page-actions", TagStructure = TagStructure.NormalOrSelfClosing)]
public class SectionPageActionsTagHelper : TagHelper
{
	public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		var originalContents = await output.GetChildContentAsync();
		var divCol = new TagBuilder("div");
		divCol.AddCssClass(value: "col");

		divCol.InnerHtml.AppendHtml(content: originalContents);
		
		var divRow = new TagBuilder("div");
		divRow.AddCssClass(value: "row");
		divRow.AddCssClass(value: "mb-3");

		divRow.InnerHtml.AppendHtml(content: divCol);

		output.TagName = null;
		output.TagMode = TagMode.StartTagAndEndTag;
		output.Content.SetHtmlContent(htmlContent: divRow);
	}
}
