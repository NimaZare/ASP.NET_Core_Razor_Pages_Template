namespace Infrastructure.TagHelpers;

[HtmlTargetElement(tag: Constants.TagHelper.Label, TagStructure = TagStructure.WithoutEndTag)]
public class SimpleLabelTagHelper : LabelTagHelper
{
	public SimpleLabelTagHelper(IHtmlGenerator generator) : base(generator)
	{
	}

	public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		Utility.CreateOrMergeAttribute(name: "class", content: "form-label", output: output);

		return base.ProcessAsync(context, output);
	}
}
