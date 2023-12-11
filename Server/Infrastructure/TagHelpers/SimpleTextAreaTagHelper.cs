namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: Constants.TagHelper.TextArea, TagStructure = TagStructure.WithoutEndTag)]
public class SimpleTextAreaTagHelper : TextAreaTagHelper
{
	public SimpleTextAreaTagHelper(IHtmlGenerator generator) : base(generator: generator)
	{
	}

	public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		Utility.CreateOrMergeAttribute(name: "class", content: "form-control", output: output);

		return base.ProcessAsync(context, output);
	}
}
