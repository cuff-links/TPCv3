using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TPCv3.Domain.Abstract;
using TPCv3.Domain.Entities;

namespace TPCv3.Domain.Concrete{
    public class EfBlogRepository : IBlogRepository{
        #region Constants and Fields

        private readonly EfDbContext _context = new EfDbContext();

        #endregion

        #region Public Properties

        public IEnumerable<Post> AllPosts{
            get { return _context.Posts; }
        }

        #endregion

        #region Public Methods and Operators

        public Category Category(string categorySlug){
            var category = _context.Posts.Select(c => c.Category).FirstOrDefault(c => c.UrlSlug.Equals(categorySlug));
            return category;
        }

        public IQueryable<Post> Posts(int pagesToSkip, int pageSize){
            var posts = _context.Posts.Include(c => c.Category)
                                .Include(t => t.Tags)
                                .Where(p => p.Published)
                                .OrderByDescending(p => p.PostedOn)
                                .Skip(pagesToSkip*pageSize).Take(pageSize);
            return posts;
        }

        public IQueryable<Post> PostsForCategory(string categorySlug, int pagesToSkip, int pageSize){
            var posts = _context.Posts.Include(c => c.Category).Include(t => t.Tags).Where(
                p => p.Published && p.Category.UrlSlug.Equals(categorySlug)).OrderByDescending(p => p.PostedOn).Skip
                (pagesToSkip*pageSize).Take(pageSize);
            return posts;
        }

        public int TotalPosts(){
            var postCount = _context.Posts.Count(p => p.Published);
            return postCount;
        }

        public int TotalPostsForCategory(string categorySlug){
            var postCount = _context.Posts.Count(p => p.Published && p.Category.UrlSlug.Equals(categorySlug));
            return postCount;
        }

        #endregion

        public IQueryable<Post> PostsForTags(string tagSlug, int pagesToSkip, int pageSize){
            var posts = _context.Posts.Include(c => c.Category).Include(t => t.Tags).Where(
                p => p.Published && p.Tags.Any(tag => tag.UrlSlug.Equals(tagSlug)))
                                .OrderByDescending(p => p.PostedOn)
                                .Skip
                (pagesToSkip*pageSize).Take(pageSize);
            return posts;
        }

        public int TotalPostsForTag(string tagSlug){
            var postCount = _context.Posts.Count(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)));
            return postCount;
        }

        public Tag Tag(string tagSlug){
            var tag = _context.Tags.FirstOrDefault(t => t.UrlSlug.Equals(tagSlug));
            return tag;
        }


        public IQueryable<Post> PostsForSearch(string search, int pagesToSkip, int pageSize){
            var posts = _context.Posts
                                .Include(c => c.Category)
                                .Include(t => t.Tags)
                                .Where(
                                    p =>
                                    p.Published &&
                                    (p.Category.UrlSlug.Contains(search) || p.Category.Name.Contains(search) ||
                                     p.Tags.Any(tags => tags.Name.Equals(search) || p.ShortDescription.Contains(search)
                                                        || p.Description.Contains(search))))
                                .OrderByDescending(p => p.PostedOn)
                                .Skip(pagesToSkip*pageSize).Take(pageSize);
            return posts;
        }

        public int TotalPostsForSearch(string search){
            var postCount = _context.Posts
                                    .Count(p => p.Published &&
                                                (p.Category.UrlSlug.Contains(search) || p.Category.Name.Equals(search) ||
                                                 p.Tags.Any(tags => tags.Name.Equals(search))));
            return postCount;
        }


        public Post Post(int month, int year, string titleSlug){
            var post = _context.Posts
                               .Include(c => c.Category)
                               .Include(t => t.Tags)
                               .FirstOrDefault(
                                   p =>
                                   p.PostedOn.Year == year && p.PostedOn.Month == month && p.UrlSlug.Equals(titleSlug));
            return post;
        }


        public IList<Category> Categories(){
            var categoryList = _context.Categories.OrderBy(c => c.Name).ToList();
            return categoryList;
        }


        public IList<Tag> Tags(){
            var tagList = _context.Tags.OrderBy(t => t.Name).ToList();
            return tagList;
        }
    }
}