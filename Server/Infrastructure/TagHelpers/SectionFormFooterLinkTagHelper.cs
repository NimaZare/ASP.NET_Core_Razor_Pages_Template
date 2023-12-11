namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: "a", ParentTag = "section-form-footer", TagStructure = TagStructure.NormalOrSelfClosing)]
public class SectionFormFooterLinkTagHelper : TagHelper
{
	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		output.Attributes.SetAttribute(name: "class", value: "text-decoration-none");
	}
}
