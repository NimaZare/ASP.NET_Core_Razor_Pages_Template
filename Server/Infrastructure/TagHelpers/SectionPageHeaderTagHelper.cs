namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: "section-page-header", TagStructure = TagStructure.NormalOrSelfClosing)]
public class SectionPageHeaderTagHelper : TagHelper
{
	public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		var originalContents = await output.GetChildContentAsync();

		var horizontalRule = new TagBuilder(tagName: "hr")
		{
			TagRenderMode = TagRenderMode.SelfClosing
		};

		horizontalRule.AddCssClass(value: "mt-4");

		var body = new TagBuilder(tagName: "h3");

		body.AddCssClass(value: "mb-3");
		body.AddCssClass(value: "text-center");

		body.InnerHtml.AppendHtml(content: originalContents);

		output.TagName = null;
		output.TagMode = TagMode.StartTagAndEndTag;
		output.Content.SetHtmlContent(htmlContent: body);
	}
}
