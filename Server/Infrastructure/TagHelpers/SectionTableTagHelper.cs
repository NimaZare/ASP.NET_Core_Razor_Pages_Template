﻿namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: "section-table", TagStructure = TagStructure.NormalOrSelfClosing)]
public class SectionTableTagHelper : TagHelper
{
	public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		var originalContents = await output.GetChildContentAsync();

		var divCol = new TagBuilder("div");

		divCol.AddCssClass(value: "col");
		divCol.AddCssClass(value: "table-responsive");

		divCol.InnerHtml.AppendHtml(content: originalContents);

		var divRow = new TagBuilder("div");

		divRow.AddCssClass(value: "row");

		divRow.InnerHtml.AppendHtml(content: divCol);

		output.TagName = null;
		output.TagMode = TagMode.StartTagAndEndTag;
		output.Content.SetHtmlContent(htmlContent: divRow);
	}
}
