namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: "button-reset", ParentTag = "section-form-buttons", TagStructure = TagStructure.WithoutEndTag)]
public class ButtonResetTagHelper : TagHelper
{
	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		var icon = Utility.GetIconReset();
		var body = new TagBuilder(tagName: "button");

		body.Attributes.Add(key: "type", value: "reset");
		body.AddCssClass(value: "btn");
		body.AddCssClass(value: "btn-secondary");
		body.InnerHtml.AppendHtml(content: icon);
		body.InnerHtml.Append(unencoded: Resources.ButtonCaptions.Reset);

		output.TagName = null;
		output.TagMode = TagMode.StartTagAndEndTag;
		output.Content.SetHtmlContent(htmlContent: body);
	}
}
