namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: "a", ParentTag = "section-page-actions", TagStructure = TagStructure.NormalOrSelfClosing)]
public class SectionPageActionsLink : TagHelper
{
	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		output.Attributes.SetAttribute(name: "class", value: "btn btn-primary");
	}
}
