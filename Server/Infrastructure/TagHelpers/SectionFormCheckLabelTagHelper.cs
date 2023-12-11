namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: "label", ParentTag = "section-form-check")]
public class SectionFormCheckLabelTagHelper : LabelTagHelper
{
	public SectionFormCheckLabelTagHelper(IHtmlGenerator generator) : base(generator)
	{
	}

	public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		Utility.CreateOrMergeAttribute(name: "class", content: "form-check-label", output: output);

		return base.ProcessAsync(context, output);
	}
}
