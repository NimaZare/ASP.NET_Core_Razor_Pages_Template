namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: "section-form-footer", ParentTag = "section-form", TagStructure = TagStructure.NormalOrSelfClosing)]
public class SectionFormFooterTagHelper : TagHelper
{
	public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		var originalContents = await output.GetChildContentAsync();

		var horizontalRule = new TagBuilder(tagName: "hr")
		{
			TagRenderMode = TagRenderMode.SelfClosing
		};

		horizontalRule.AddCssClass(value: "mt-4");
		var body = new TagBuilder("div");
		body.AddCssClass(value: "mb-3");
		body.AddCssClass(value: "text-center");
		body.InnerHtml.AppendHtml(content: originalContents);

		output.TagName = null;
		output.TagMode = TagMode.StartTagAndEndTag;
		output.PreElement.AppendHtml(htmlContent: horizontalRule);
		output.Content.SetHtmlContent(htmlContent: body);
	}
}
