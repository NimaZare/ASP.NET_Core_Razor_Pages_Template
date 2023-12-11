namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: "label", ParentTag = "section-form-field")]
public class SectionFormFieldLabelTagHelper : LabelTagHelper
{
	public SectionFormFieldLabelTagHelper(IHtmlGenerator generator) : base(generator)
	{
	}

	public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		Utility.CreateOrMergeAttribute(name: "class", content: "form-label", output: output);

		return base.ProcessAsync(context, output);
	}
}
