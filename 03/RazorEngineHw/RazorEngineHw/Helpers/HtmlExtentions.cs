namespace RazorEngineHw.Helpers
{
    using System.Web.Mvc;

    public static class HtmlExtentions
    {
        public static MvcHtmlString Image(
            this HtmlHelper helper,
            string imageUrl,
            string alt = null,
            string width = "150px",
            string height = "150px")
        {
            TagBuilder builder = new TagBuilder("img");
            builder.MergeAttribute("src", imageUrl);
            builder.MergeAttribute("width", width);
            builder.MergeAttribute("height", height);
            if (alt != null)
            {
                builder.MergeAttribute("alt", alt);
            }

            MvcHtmlString htmlString = new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));

            return htmlString;
        }
    }
}
