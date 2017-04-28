namespace RazorEngineHw.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Text;
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

        public static MvcHtmlString Table<T>(
            this HtmlHelper helper,
            IEnumerable<T> models,
            params string[] cssClasses)
        {
            TagBuilder table = new TagBuilder("table");
            StringBuilder tableInnerHtml = new StringBuilder();
            string[] propertyNames = typeof(T).GetProperties().Select(prop => prop.Name).ToArray();
            foreach (var cssClass in cssClasses)
            {
                table.AddCssClass(cssClass);
            }

            TagBuilder tableHeaderRow = new TagBuilder("tr");
            StringBuilder tableHeaderInnerHtml = new StringBuilder();
            foreach (string propertyName in propertyNames)
            {
                var attributes = typeof(T).GetProperty(propertyName).GetCustomAttributes();
                foreach (Attribute attribute in attributes)
                {
                    HiddenInputAttribute hiddenInputAttr = attribute as HiddenInputAttribute;
                    if (hiddenInputAttr?.DisplayValue == false)
                    {
                        continue;
                    }

                    TagBuilder tableData = new TagBuilder("th");
                    if (attribute is DisplayNameAttribute)
                    {
                        DisplayNameAttribute displayNameAttr = attribute as DisplayNameAttribute;
                        tableData.InnerHtml = displayNameAttr.DisplayName;
                    }
                    else
                    {
                        tableData.InnerHtml = propertyName;
                    }

                    tableHeaderInnerHtml.Append(tableData);
                }
            }
            tableHeaderRow.InnerHtml = tableHeaderInnerHtml.ToString();
            tableInnerHtml.Append(tableHeaderRow);

            foreach (var model in models)
            {
                TagBuilder tableRow = new TagBuilder("tr");
                StringBuilder tableRowInnerHtml = new StringBuilder();
                foreach (string propertyName in propertyNames)
                {
                    var attributes = typeof(T).GetProperty(propertyName).GetCustomAttributes();
                    foreach (Attribute attribute in attributes)
                    {
                        HiddenInputAttribute hiddenInputAttr = attribute as HiddenInputAttribute;
                        if (hiddenInputAttr?.DisplayValue == false)
                        {
                            continue;
                        }

                        TagBuilder tableData = new TagBuilder("td");
                        var propertyValue = typeof(T).GetProperty(propertyName).GetValue(model);
                        if (attribute is DisplayFormatAttribute &&
                            propertyValue == null)
                        {
                            DisplayFormatAttribute displayFormatAttr = attribute as DisplayFormatAttribute;
                            tableData.InnerHtml = displayFormatAttr.NullDisplayText;
                        }
                        else
                        {
                            tableData.InnerHtml = propertyValue.ToString();
                        }

                        tableRowInnerHtml.Append(tableData);
                    }
                }

                tableRow.InnerHtml = tableRowInnerHtml.ToString();
                tableInnerHtml.Append(tableRow);
            }

            table.InnerHtml = tableInnerHtml.ToString();
            MvcHtmlString htmlString = new MvcHtmlString(table.ToString(TagRenderMode.Normal));

            return htmlString;
        }
    }
}
