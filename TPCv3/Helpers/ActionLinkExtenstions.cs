using System;
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
                        title = String.Format("Read full article: " + post.Title),
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
                        title = String.Format("See all posts about {0}.", category.Name),
                        @class = "categoryLink"
                    });
        }

        public static MvcHtmlString ProjectCategoryLink(this HtmlHelper helper, ProjectCategory projectCategory){
            return helper.ActionLink(
                projectCategory.Name,
                "ProjectCategory",
                "Home",
                new {selectedProjectCategory = projectCategory.Name},
                new
                    {
                        title = projectCategory.Description,
                        @class = "projectCategoryLink"
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
                        title = String.Format("See all posts that mention {0}.", tag.Name),
                        @class = "tagLink"
                    });
        }

        public static string GetMonthName(this HtmlHelper helper, int monthNumber){
            switch (monthNumber)
            {
                case 1:
                    {
                        return "JAN";
                    }
                case 2:
                    {
                        return "FEB";
                    }
                case 3:
                    {
                        return "MAR";
                    }
                case 4:
                    {
                        return "APR";
                    }
                case 5:
                    {
                        return "MAY";
                    }
                case 6:
                    {
                        return "JUN";
                    }
                case 7:
                    {
                        return "JUL";
                    }
                case 8:
                    {
                        return "AUG";
                    }
                case 9:
                    {
                        return "SEP";
                    }
                case 10:
                    {
                        return "OCT";
                    }
                case 11:
                    {
                        return "NOV";
                    }
                default:
                    {
                        return "DEC";
                    }
            }
        }
    }
}