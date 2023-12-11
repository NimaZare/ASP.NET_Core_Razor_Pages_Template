namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: Constants.TagHelper.Input, TagStructure = TagStructure.WithoutEndTag)]
public class SimpleInputTagHelper : InputTagHelper
{
	public SimpleInputTagHelper(IHtmlGenerator generator) : base(generator: generator)
	{
	}

	public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		Utility.CreateOrMergeAttribute(name: "class", content: "form-control", output: output);

		return base.ProcessAsync(context, output);
	}
}
