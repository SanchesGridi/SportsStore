using System;
using System.Text;
using System.Web.Mvc;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString GeneratePageLinks(this HtmlHelper htmlHelper, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            var stringBuilder = new StringBuilder();

            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                var tagBuilder = new TagBuilder("a");
                tagBuilder.MergeAttribute("href", pageUrl?.Invoke(i));
                tagBuilder.InnerHtml = i.ToString();

                if (i == pagingInfo.CurrentPage)
                {
                    tagBuilder.AddCssClass("selected");
                    tagBuilder.AddCssClass("btn-primary");
                }

                tagBuilder.AddCssClass("btn btn-default");

                stringBuilder.Append(tagBuilder.ToString());
            }

            return MvcHtmlString.Create(stringBuilder.ToString());
        }
    }
}