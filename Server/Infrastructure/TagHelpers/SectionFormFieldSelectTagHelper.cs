namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: "select", ParentTag = "section-form-field")]
public class SectionFormFieldSelectTagHelper : LabelTagHelper
{
	public SectionFormFieldSelectTagHelper(IHtmlGenerator generator) : base(generator)
	{
	}

	public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		Utility.CreateOrMergeAttribute(name: "class", content: "form-select", output: output);

		return base.ProcessAsync(context, output);
	}
}
