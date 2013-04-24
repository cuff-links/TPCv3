using System.Collections.Generic;
using System.Data.Common;
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
            get{
                return _context.Posts;
            }
        }

        #endregion

        #region Public Methods and Operators

        public int AddPost(Post post){
            using (DbTransaction tran = _context.Database.Connection.BeginTransaction()){
                _context.Posts.Add(post);
                _context.SaveChanges();
                tran.Commit();
                return post.Id;
            }
        }

        public IList<Category> Categories(){
            List<Category> categoryList = _context.Categories.OrderBy(c => c.Name).ToList();
            return categoryList;
        }

        public Category Category(string categorySlug){
            Category category =
                _context.Posts.Select(c => c.Category).FirstOrDefault(c => c.UrlSlug.Equals(categorySlug));
            return category;
        }

        public Post Post(int month, int year, string titleSlug){
            Post post =
                _context.Posts.Include(c => c.Category).Include(t => t.Tags).FirstOrDefault(
                    p => p.PostedOn.Year == year && p.PostedOn.Month == month && p.UrlSlug.Equals(titleSlug));
            return post;
        }

        public IQueryable<Post> Posts(int pagesToSkip, int pageSize){
            IQueryable<Post> posts =
                _context.Posts.Include(c => c.Category).Include(t => t.Tags).Where(p => p.Published).OrderByDescending(
                    p => p.PostedOn).Skip(pagesToSkip * pageSize).Take(pageSize);
            return posts;
        }

        public IQueryable<Post> Posts(int pageNo, int pageSize, string sortColumn, bool sortByAscending){
            IQueryable<Post> query;

            switch (sortColumn){
                case "Title":
                    if (sortByAscending){
                        query =
                            _context.Posts.Include(c => c.Category).Include(t => t.Tags).OrderBy(p => p.Title).Skip(
                                pageNo * pageSize).Take(pageSize);
                    }
                    else{
                        query =
                            _context.Posts.Include(c => c.Category).Include(t => t.Tags).Skip(pageNo * pageSize).Take(
                                pageSize);
                    }
                    break;
                case "Published":
                    if (sortByAscending){
                        query =
                            _context.Posts.Include(c => c.Category).Include(t => t.Tags).Skip(pageNo * pageSize).Take(
                                pageSize);
                    }
                    else{
                        query =
                            _context.Posts.Include(c => c.Category).Include(t => t.Tags).OrderByDescending(
                                p => p.Published).Skip(pageNo * pageSize).Take(pageSize);
                    }
                    break;
                case "PostedOn":
                    if (sortByAscending){
                        query =
                            _context.Posts.Include(c => c.Category).Include(t => t.Tags).OrderBy(p => p.PostedOn).Skip(
                                pageNo * pageSize).Take(pageSize);
                    }
                    else{
                        query =
                            _context.Posts.Include(c => c.Category).Include(t => t.Tags).OrderByDescending(
                                p => p.PostedOn).Skip(pageNo * pageSize).Take(pageSize);
                    }
                    break;
                case "Modified":
                    if (sortByAscending){
                        query =
                            _context.Posts.Include(c => c.Category).Include(t => t.Tags).OrderBy(p => p.Modified).Skip(
                                pageNo * pageSize).Take(pageSize);
                    }
                    else{
                        query =
                            _context.Posts.Include(c => c.Category).Include(t => t.Tags).OrderByDescending(
                                p => p.Modified).Skip(pageNo * pageSize).Take(pageSize);
                    }
                    break;
                case "Category":
                    if (sortByAscending){
                        query =
                            _context.Posts.Include(c => c.Category).Include(t => t.Tags).OrderBy(p => p.Category.Name).
                                Skip(pageNo * pageSize).Take(pageSize);
                    }
                    else{
                        query =
                            _context.Posts.Include(c => c.Category).Include(t => t.Tags).OrderByDescending(
                                p => p.Category.Name).Skip(pageNo * pageSize).Take(pageSize);
                    }
                    break;
                default:
                    query =
                        _context.Posts.Include(c => c.Category).Include(t => t.Tags).OrderByDescending(p => p.PostedOn).
                            Skip(pageNo * pageSize).Take(pageSize);
                    break;
            }

            return query;
        }

        public IQueryable<Post> PostsForCategory(string categorySlug, int pagesToSkip, int pageSize){
            IQueryable<Post> posts =
                _context.Posts.Include(c => c.Category).Include(t => t.Tags).Where(
                    p => p.Published && p.Category.UrlSlug.Equals(categorySlug)).OrderByDescending(p => p.PostedOn).Skip
                    (pagesToSkip * pageSize).Take(pageSize);
            return posts;
        }

        public IQueryable<Post> PostsForSearch(string search, int pagesToSkip, int pageSize){
            IQueryable<Post> posts =
                _context.Posts.Include(c => c.Category).Include(t => t.Tags).Where(
                    p =>
                    p.Published
                    &&
                    (p.Category.UrlSlug.Contains(search) || p.Category.Name.Contains(search)
                     ||
                     p.Tags.Any(
                         tags =>
                         tags.Name.Equals(search) || p.ShortDescription.Contains(search)
                         || p.Description.Contains(search)))).OrderByDescending(p => p.PostedOn).Skip(
                             pagesToSkip * pageSize).Take(pageSize);
            return posts;
        }

        public IQueryable<Post> PostsForTags(string tagSlug, int pagesToSkip, int pageSize){
            IQueryable<Post> posts =
                _context.Posts.Include(c => c.Category).Include(t => t.Tags).Where(
                    p => p.Published && p.Tags.Any(tag => tag.UrlSlug.Equals(tagSlug))).OrderByDescending(
                        p => p.PostedOn).Skip(pagesToSkip * pageSize).Take(pageSize);
            return posts;
        }

        public Tag Tag(string tagSlug){
            Tag tag = _context.Tags.FirstOrDefault(t => t.UrlSlug.Equals(tagSlug));
            return tag;
        }

        public IList<Tag> Tags(){
            List<Tag> tagList = _context.Tags.OrderBy(t => t.Name).ToList();
            return tagList;
        }

        public int TotalPosts(bool checkIsPublished = true){
            int countPosts = _context.Posts.Count(p => checkIsPublished || p.Published);
            return countPosts;
        }

        public int TotalPostsForCategory(string categorySlug){
            int postCount = _context.Posts.Count(p => p.Published && p.Category.UrlSlug.Equals(categorySlug));
            return postCount;
        }

        public int TotalPostsForSearch(string search){
            int postCount =
                _context.Posts.Count(
                    p =>
                    p.Published
                    &&
                    (p.Category.UrlSlug.Contains(search) || p.Category.Name.Equals(search)
                     || p.Tags.Any(tags => tags.Name.Equals(search))));
            return postCount;
        }

        public int TotalPostsForTag(string tagSlug){
            int postCount = _context.Posts.Count(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)));
            return postCount;
        }

        #endregion
    }
}