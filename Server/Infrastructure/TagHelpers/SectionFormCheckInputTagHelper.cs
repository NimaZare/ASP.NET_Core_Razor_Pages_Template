namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: "input", ParentTag = "section-form-check")]
public class SectionFormCheckInputTagHelper : InputTagHelper
{
	public SectionFormCheckInputTagHelper(IHtmlGenerator generator) : base(generator)
	{
	}

	public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		Utility.CreateOrMergeAttribute(name: "class", content: "form-check-input", output: output);

		return base.ProcessAsync(context, output);
	}
}
