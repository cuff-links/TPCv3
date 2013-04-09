using System.Collections.Generic;
using System.Linq;
using TPCv3.Domain.Entities;

namespace TPCv3.Domain.Abstract{
    public interface IBlogRepository{
        #region Public Properties

        IEnumerable<Post> AllPosts { get; }

        #endregion

        #region Public Methods and Operators

        IList<Category> Categories();

        IList<Tag> Tags();

        Category Category(string categorySlug);

        IQueryable<Post> Posts(int pagesToSkip, int pageSize);

        IQueryable<Post> PostsForCategory(string categorySlug, int pagesToSkip, int pageSize);

        IQueryable<Post> PostsForSearch(string search, int pagesToSkip, int pageSize);

        IQueryable<Post> PostsForTags(string tagSlug, int pagesToSkip, int pageSize);

        Post Post(int year, int month, string titleSlug);

        Tag Tag(string tagSlug);

        int TotalPosts();

        int TotalPostsForCategory(string categorySlug);

        int TotalPostsForSearch(string search);

        int TotalPostsForTag(string tagSlug);

        #endregion
    }
}