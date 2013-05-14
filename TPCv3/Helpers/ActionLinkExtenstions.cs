﻿using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using TPCv3.Domain.Entities;

namespace TPCv3.Helpers{
    public static class ActionLinkExtensions{
        public static MvcHtmlString PostLink(this HtmlHelper helper, Post post){
            return helper.ActionLink(
                post.Title,
                "Post",
                "Blog",
                new {year = post.PostedOn.Year, month = post.PostedOn.Month, title = post.UrlSlug},
                new
                    {
                        title = post.Title,
                        @class = "postTitle"
                    });
        }

        public static MvcHtmlString CategoryLink(this HtmlHelper helper, Category category){
            return helper.ActionLink(
                category.Name,
                "Category",
                "Blog",
                new {selectedCategory = category.UrlSlug},
                new
                    {
                        title = String.Format("See all posts in the {0} category", category.Name),
                        @class = "categoryLink"
                    });
        }

        public static MvcHtmlString TagLink(this HtmlHelper helper, Tag tag){
            return helper.ActionLink(
                tag.Name,
                "Tag",
                "Blog",
                new {tag = tag.UrlSlug},
                new
                    {
                        title = String.Format("See all posts tagged {0}", tag.Name),
                        @class = "tagLink"
                    });
        }
    }
}