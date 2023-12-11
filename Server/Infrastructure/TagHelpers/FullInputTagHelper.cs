namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: Constants.TagHelper.FullInput, TagStructure = TagStructure.WithoutEndTag)]
public class FullInputTagHelper : TagHelper
{
	public FullInputTagHelper(IHtmlGenerator generator) : base()
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
		var label = await Utility.GenerateLabelAsync(generator: Generator, viewContext: ViewContext, @for: For);
		div.InnerHtml.AppendHtml(encoded: label);
		string? dirString = null;
		var dirAttribute = output.Attributes["dir"];

		if (dirAttribute != null)
		{
			var dirValue = dirAttribute.Value;

			if (dirValue != null)
			{
				dirString = dirValue.ToString().Replace(oldValue: "{", newValue: string.Empty).Replace(oldValue: "}", newValue: string.Empty);
			}
		}

		var textBox = await Utility.GenerateTextBoxAsync(generator: Generator, viewContext: ViewContext, @for: For, dir: dirString);
		div.InnerHtml.AppendHtml(encoded: textBox);
		var validationMessage = await Utility.GenerateValidationMessageAsync(generator: Generator, viewContext: ViewContext, @for: For);
		div.InnerHtml.AppendHtml(encoded: validationMessage);

		output.TagName = null;
		output.TagMode = TagMode.StartTagAndEndTag;
		output.Content.SetHtmlContent(htmlContent: div);
	}
}
