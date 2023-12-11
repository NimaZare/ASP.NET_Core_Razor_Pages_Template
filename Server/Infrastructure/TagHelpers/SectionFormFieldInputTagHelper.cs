namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: "input", ParentTag = "section-form-field")]
public class SectionFormFieldInputTagHelper : InputTagHelper
{
	public SectionFormFieldInputTagHelper(IHtmlGenerator generator) : base(generator)
	{
	}

	public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		Utility.CreateOrMergeAttribute(name: "class", content: "form-control", output: output);

		return base.ProcessAsync(context, output);
	}
}
