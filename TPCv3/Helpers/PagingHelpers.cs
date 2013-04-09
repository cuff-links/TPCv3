using System;
using System.Text;
using System.Web.Mvc;
using TPCv3.Models;

namespace TPCv3.Helpers{
    public static class PagingHelpers{
        #region Public Methods and Operators

        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl){
            var result = new StringBuilder();
            for (var i = 1; i <= pagingInfo.TotalPages; i++){
                var tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = Convert.ToString(i);
                if (i == pagingInfo.CurrentPage){
                    tag.AddCssClass("selected_paginator");
                }
                result.Append(tag);
            }
            return MvcHtmlString.Create(result.ToString());
        }

        #endregion
    }
}