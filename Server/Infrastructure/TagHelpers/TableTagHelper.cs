namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: "table", ParentTag = "section-table", TagStructure = TagStructure.NormalOrSelfClosing)]
public class TableTagHelper : TagHelper
{
	public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		var originalContents = (await output.GetChildContentAsync()).GetContent();

		originalContents = originalContents.Replace("thead", "thead class=\"table-primary text-center\"");
		originalContents = originalContents.Replace("tfooter", "tfooter class=\"table-secondary\"");
		
		output.TagName = "table";
		output.TagMode = TagMode.StartTagAndEndTag;
		output.Attributes.SetAttribute(name: "class", value: "table table-bordered table-sm table-striped table-hover align-items-center");
		output.Content.SetHtmlContent(encoded: originalContents);
	}
}
