namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: "span", ParentTag = "section-form-field")]
public class SectionFormFieldValidationMessageTagHelper : ValidationMessageTagHelper
{
	public SectionFormFieldValidationMessageTagHelper(IHtmlGenerator generator) : base(generator)
	{
	}

	public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		Utility.CreateOrMergeAttribute(name: "class", content: "text-danger", output: output);

		return base.ProcessAsync(context, output);
	}
}
