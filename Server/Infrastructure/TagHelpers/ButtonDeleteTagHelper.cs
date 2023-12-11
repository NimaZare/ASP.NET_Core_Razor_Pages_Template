namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: "button-delete", ParentTag = "section-form-buttons", TagStructure = TagStructure.WithoutEndTag)]
public class ButtonDeleteTagHelper : TagHelper
{
	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		var icon = Utility.GetIconDelete();
		var body = new TagBuilder(tagName: "button");

		body.Attributes.Add(key: "type", value: "submit");
		body.AddCssClass(value: "btn");
		body.AddCssClass(value: "btn-danger");
		body.InnerHtml.AppendHtml(content: icon);
		body.InnerHtml.Append(unencoded: Resources.ButtonCaptions.Delete);

		output.TagName = null;
		output.TagMode = TagMode.StartTagAndEndTag;
		output.Content.SetHtmlContent(htmlContent: body);
	}
}
