namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: Constants.TagHelper.FullCheckBox, TagStructure = TagStructure.WithoutEndTag)]
public class FullCheckBoxTagHelper : TagHelper
{
	public FullCheckBoxTagHelper(IHtmlGenerator generator) : base()
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
		var innerDiv = new TagBuilder(tagName: "div");
		innerDiv.AddCssClass(value: "form-check");
		div.InnerHtml.AppendHtml(content: innerDiv);
		var checkBox = await Utility.GenerateCheckBoxAsync(generator: Generator, viewContext: ViewContext, @for: For);
		innerDiv.InnerHtml.AppendHtml(encoded: checkBox);
		var label = await Utility.GenerateLabelAsync(generator: Generator, viewContext: ViewContext, @for: For, cssClass: "form-check-label");
		innerDiv.InnerHtml.AppendHtml(encoded: label);

		output.TagName = null;
		output.TagMode = TagMode.StartTagAndEndTag;
		output.Content.SetHtmlContent(htmlContent: div);
	}
}
