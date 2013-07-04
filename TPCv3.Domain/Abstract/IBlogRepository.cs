using System.Collections.Generic;
using System.Linq;
using TPCv3.Domain.Entities;

namespace TPCv3.Domain.Abstract{
    public interface IBlogRepository{
        #region Public Properties

        IEnumerable<Post> AllPosts { get; }

        #endregion

        #region Public Methods and Operators

        int AddPost(Post post);

        int AddTag(Tag tag);

        int AddCategory(Category category);

        void EditCategory(Category category);

        void EditTag(Tag tag);

        void EditPost(Post post);

        void DeletePost(int id);

        void DeleteTag(int id);

        void DeleteCategory(int id);

        Category Category(int id);

        Tag Tag(int id);

        IList<Category> Categories();

        Category Category(string categorySlug);

        Post Post(int year, int month, string titleSlug);

        Post PostForId(int id);

        IQueryable<Post> Posts(int pagesToSkip, int pageSize);

        IQueryable<Post> Posts(int pageNo, int pageSize, string sortColumn, bool sortByAscending);

        IQueryable<Post> PostsForCategory(string categorySlug, int pagesToSkip, int pageSize);

        IQueryable<Post> PostsForSearch(string search, int pagesToSkip, int pageSize);

        IQueryable<Post> PostsForTags(string tagSlug, int pagesToSkip, int pageSize);

        Tag Tag(string tagSlug);

        IList<Tag> Tags();

        int TotalPosts(bool checkIsPublished = true);

        int TotalPostsForCategory(string categorySlug);

        int TotalPostsForSearch(string search);

        int TotalPostsForTag(string tagSlug);

        #endregion
    }
}