namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: "textarea", ParentTag = "section-form-field")]
public class SectionFormFieldTextArea : TextAreaTagHelper
{
	public SectionFormFieldTextArea(IHtmlGenerator generator) : base(generator)
	{
	}

	public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		Utility.CreateOrMergeAttribute(name: "class", content: "form-control", output: output);
		Utility.CreateOrMergeAttribute(name: "class", content: "tinymce", output: output);

		return base.ProcessAsync(context, output);
	}
}
