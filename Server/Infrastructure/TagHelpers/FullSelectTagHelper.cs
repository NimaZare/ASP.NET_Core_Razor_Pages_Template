namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: Constants.TagHelper.FullSelect, TagStructure = TagStructure.WithoutEndTag)]
public class FullSelectTagHelper : TagHelper
{
	public FullSelectTagHelper(IHtmlGenerator generator) : base()
	{
		Generator = generator;
	}

	private IHtmlGenerator Generator { get; }

	[ViewContext]
	[HtmlAttributeNotBound]
	public ViewContext? ViewContext { get; set; }

	[HtmlAttributeName(name: "asp-for")]
	public ModelExpression? For { get; set; }

	[HtmlAttributeName(name: "asp-items")]
	public IEnumerable<SelectListItem>? Items { get; set; }

	[HtmlAttributeName(name: "asp-option-label")]
	public string? OptionLabel { get; set; }

	public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		var div = new TagBuilder(tagName: "div");
		div.AddCssClass(value: "mb-3");
		var label = await Utility.GenerateLabelAsync(generator: Generator, viewContext: ViewContext, @for: For);
		div.InnerHtml.AppendHtml(encoded: label);
		var select = await Utility.GenerateSelectAsync(generator: Generator, viewContext: ViewContext, @for: For, selectList: Items);
		div.InnerHtml.AppendHtml(encoded: select);
		var validationMessage = await Utility.GenerateValidationMessageAsync(generator: Generator, viewContext: ViewContext, @for: For);
		div.InnerHtml.AppendHtml(encoded: validationMessage);

		output.TagName = null;
		output.TagMode = TagMode.StartTagAndEndTag;
		output.Content.SetHtmlContent(htmlContent: div);
	}
}
