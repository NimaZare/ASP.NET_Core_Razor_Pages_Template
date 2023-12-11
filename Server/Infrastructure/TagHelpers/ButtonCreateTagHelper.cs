namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: "button-create", ParentTag = "section-form-buttons", TagStructure = TagStructure.WithoutEndTag)]
public class ButtonCreateTagHelper : TagHelper
{
	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		var icon = Utility.GetIconCreate();
		var body = new TagBuilder(tagName: "button");

		body.Attributes.Add(key: "type", value: "submit");

		body.AddCssClass(value: "btn");
		body.AddCssClass(value: "btn-primary");

		body.InnerHtml.AppendHtml(content: icon);
		body.InnerHtml.Append(unencoded: Resources.ButtonCaptions.Create);

		output.TagName = null;
		output.TagMode = TagMode.StartTagAndEndTag;
		output.Content.SetHtmlContent(htmlContent: body);
	}
}
