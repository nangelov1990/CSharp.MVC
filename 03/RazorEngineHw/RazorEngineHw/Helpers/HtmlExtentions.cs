namespace RazorEngineHw.Helpers
{
    using System.Web.Mvc;

    public static class HtmlExtentions
    {
        private static readonly string _youtubeUrl = "https://youtube.com/embed/";

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

        public static MvcHtmlString Youtube(
            this HtmlHelper helper,
            string videoId,
            int width = 600,
            int height = 400,
            bool allowfullscreen = true)
        {
            var fullSource = $"{_youtubeUrl}{videoId}";
            TagBuilder builder = new TagBuilder("iframe");
            builder.MergeAttribute("src", fullSource);
            builder.MergeAttribute("width", $"{width}");
            builder.MergeAttribute("height", $"{height}");
            builder.MergeAttribute("frameborder", "0");
            if (allowfullscreen == true)
            {
                builder.MergeAttribute("allowfullscreen", "allowfullscreen");
            }

            MvcHtmlString htmlString = new MvcHtmlString(builder.ToString(TagRenderMode.Normal));

            return htmlString;
        }
    }
}
