namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: "a", ParentTag = "table-actions", TagStructure = TagStructure.NormalOrSelfClosing)]
public class TableActionsLinkTagHelper : TagHelper
{
	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		output.Attributes.SetAttribute(name: "class", value: "text-decoration-none");
	}
}
