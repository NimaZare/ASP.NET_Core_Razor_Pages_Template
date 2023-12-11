namespace Infrastructure;

public static class HtmlHelpers
{
	public static IHtmlContent NZLibDisplayString(this IHtmlHelper html, string? value)
	{
		if (string.IsNullOrWhiteSpace(value: value))
		{
			return html.Raw(value: Constants.Format.NullValue);
		}

		var result = NZLib.Utility.FixText(text: value);

		return html.Raw(value: result);
	}

	public static IHtmlContent NZLibDisplayInteger(this IHtmlHelper html, long? value)
	{
		if (value.HasValue == false)
		{
			return html.Raw(value: Constants.Format.NullValue);
		}

		var result = value.Value.ToString(format: Constants.Format.Integer);
		result = Convert.DigitsToUnicode(value: result);

		return html.Raw(value: result);
	}

	public static IHtmlContent NZLibDisplayRowNumberWithTd(this IHtmlHelper html, long? value)
	{
		var td = new TagBuilder(tagName: "td");
		td.AddCssClass(value: "text-center");
		var innerHtml = NZLibDisplayInteger(html: html, value: value);
		td.InnerHtml.AppendHtml(content: innerHtml);

		return td;
	}

	public static IHtmlContent NZLibDisplayBoolean(this IHtmlHelper html, bool? value)
	{
		ArgumentNullException.ThrowIfNull(html);

		var div = new TagBuilder(tagName: "div");
		var input = new TagBuilder(tagName: "input");
		input.Attributes.Add(key: "type", value: "checkbox");
		input.Attributes.Add(key: "disabled", value: "disabled");

		if (value.HasValue && value.Value)
		{
			input.Attributes.Add(key: "checked", value: "checked");
		}

		div.InnerHtml.AppendHtml(content: input);

		return div;
	}

	public static IHtmlContent NZLibDisplayStringWithTd(this IHtmlHelper html, string? value)
	{
		var td = new TagBuilder(tagName: "td");
		var innerHtml = NZLibDisplayString(html: html, value: value);
		td.InnerHtml.AppendHtml(content: innerHtml);

		return td;
	}

	public static IHtmlContent NZLibDisplayBooleanWithTd(this IHtmlHelper html, bool? value)
	{
		var td = new TagBuilder(tagName: "td");
		td.AddCssClass(value: "text-center");
		var innerHtml = NZLibDisplayBoolean(html: html, value: value);
		td.InnerHtml.AppendHtml(content: innerHtml);

		return td;
	}

	public static IHtmlContent NZLibDisplayIntegerWithTd(this IHtmlHelper html, long? value)
	{
		var td = new TagBuilder(tagName: "td");
		td.Attributes.Add(key: "dir", value: "ltr");
		var innerHtml = NZLibDisplayInteger(html: html, value: value);
		td.InnerHtml.AppendHtml(content: innerHtml);

		return td;
	}

	public static IHtmlContent NZLibDisplayDateTime(this IHtmlHelper html, DateTime? value)
	{
		if (value.HasValue == false)
		{
			return html.Raw(value: Constants.Format.NullValue);
		}

		var result = value.Value.ToString(format: Constants.Format.DateTime);
		result = Convert.DigitsToUnicode(value: result);

		return html.Raw(value: result);
	}

	public static IHtmlContent NZLibDisplayDateTimeWithTd(this IHtmlHelper html, DateTime? value)
	{
		var td = new TagBuilder(tagName: "td");
		td.Attributes.Add(key: "dir", value: "ltr");
		var innerHtml = NZLibDisplayDateTime(html: html, value: value);
		td.InnerHtml.AppendHtml(content: innerHtml);

		return td;
	}

	public static IHtmlContent NZLibGetLinkCaptionForList(this IHtmlHelper html)
	{
		ArgumentNullException.ThrowIfNull(html);

		var icon = TagHelpers.Utility.GetIconList();
		var span = new TagBuilder(tagName: "span");
		span.AddCssClass(value: "mx-1");
		span.InnerHtml.Append(unencoded: "[");
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.AppendHtml(content: icon);
		span.InnerHtml.Append(unencoded: Resources.ButtonCaptions.BackToList);
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.Append(unencoded: "]");

		return span;
	}

	public static IHtmlContent NZLibGetLinkCaptionForDetails(this IHtmlHelper html)
	{
		ArgumentNullException.ThrowIfNull(html);

		var icon = TagHelpers.Utility.GetIconDetails();
		var span = new TagBuilder(tagName: "span");
		span.AddCssClass(value: "mx-1");
		span.InnerHtml.Append(unencoded: "[");
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.AppendHtml(content: icon);
		span.InnerHtml.Append(unencoded: Resources.ButtonCaptions.Details);
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.Append(unencoded: "]");

		return span;
	}

	public static IHtmlContent NZLibGetLinkCaptionForCreate(this IHtmlHelper html)
	{
		ArgumentNullException.ThrowIfNull(html);

		var icon = TagHelpers.Utility.GetIconCreate();
		var span = new TagBuilder(tagName: "span");
		span.InnerHtml.AppendHtml(content: icon);
		span.InnerHtml.Append(unencoded: Resources.ButtonCaptions.Create);

		return span;
	}

	public static IHtmlContent NZLibGetLinkCaptionForUpdate(this IHtmlHelper html)
	{
		ArgumentNullException.ThrowIfNull(html);

		var icon = TagHelpers.Utility.GetIconUpdate();
		var span = new TagBuilder(tagName: "span");
		span.AddCssClass(value: "mx-1");
		span.InnerHtml.Append(unencoded: "[");
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.AppendHtml(content: icon);
		span.InnerHtml.Append(unencoded: Resources.ButtonCaptions.Update);
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.Append(unencoded: "]");

		return span;
	}

	public static IHtmlContent NZLibGetLinkCaptionForUpdateInformationAgain(this IHtmlHelper html)
	{
		ArgumentNullException.ThrowIfNull(html);

		var icon = TagHelpers.Utility.GetIconReset();
		var span = new TagBuilder(tagName: "span");
		span.AddCssClass(value: "mx-1");
		span.InnerHtml.Append(unencoded: "[");
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.AppendHtml(content: icon);
		span.InnerHtml.Append(unencoded: Resources.ButtonCaptions.UpdateInformationsAgain);
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.Append(unencoded: "]");

		return span;
	}

	public static IHtmlContent NZLibGetLinkCaptionForDelete(this IHtmlHelper html)
	{
		ArgumentNullException.ThrowIfNull(html);

		var icon = TagHelpers.Utility.GetIconDelete();
		var span = new TagBuilder(tagName: "span");
		span.AddCssClass(value: "mx-1");
		span.InnerHtml.Append(unencoded: "[");
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.AppendHtml(content: icon);
		span.InnerHtml.Append(unencoded: Resources.ButtonCaptions.Delete);
		span.InnerHtml.Append(unencoded: " ");
		span.InnerHtml.Append(unencoded: "]");

		return span;
	}

	public static IHtmlContent NZLibGetIconDetails(this IHtmlHelper html)
	{
		ArgumentNullException.ThrowIfNull(html);
		var icon = TagHelpers.Utility.GetIconDetails();

		return icon;
	}

	public static IHtmlContent NZLibGetIconCreate(this IHtmlHelper html)
	{
		ArgumentNullException.ThrowIfNull(html);
		var icon = TagHelpers.Utility.GetIconCreate();

		return icon;
	}

	public static IHtmlContent NZLibGetIconUpdate(this IHtmlHelper html)
	{
		ArgumentNullException.ThrowIfNull(html);
		var icon = TagHelpers.Utility.GetIconUpdate();

		return icon;
	}

	public static IHtmlContent NZLibGetIconDelete(this IHtmlHelper html)
	{
		ArgumentNullException.ThrowIfNull(html);
		var icon = TagHelpers.Utility.GetIconDelete();

		return icon;
	}

	public static IHtmlContent NZLibGetIconCustom(this IHtmlHelper html, string iconName)
	{
		ArgumentNullException.ThrowIfNull(html);
		var icon = TagHelpers.Utility.GetIconCustom(iconName: iconName);

		return icon;
	}
}
