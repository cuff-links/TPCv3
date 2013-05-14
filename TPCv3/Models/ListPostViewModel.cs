using System.Linq;
using TPCv3.Domain.Abstract;
using TPCv3.Domain.Entities;

namespace TPCv3.Models{
    public class ListPostViewModel{
        #region Constants and Fields

        private const int itemsPerPage = 2;
        private readonly IBlogRepository _blogRepository;

        #endregion

        #region Constructors and Destructors

        public ListPostViewModel(IBlogRepository blogRepository, int pageNo){
            _blogRepository = blogRepository;
            Posts = _blogRepository.Posts(pageNo - 1, itemsPerPage);
            TotalPosts = _blogRepository.TotalPosts();
            PagingInfo = new PagingInfo
                             {CurrentPage = pageNo, ItemsPerPage = itemsPerPage, TotalItems = TotalPosts};
        }

        public ListPostViewModel(IBlogRepository blogRepository, string text, string type, int pageNo){
            _blogRepository = blogRepository;
            switch (type)
            {
                case "Category":
                    {
                        Posts = _blogRepository.PostsForCategory(text, pageNo - 1, itemsPerPage);
                        TotalPosts = _blogRepository.TotalPostsForCategory(text);
                        Category = _blogRepository.Category(text);
                        PagingInfo = new PagingInfo
                                         {
                                             CurrentPage = pageNo,
                                             ItemsPerPage = itemsPerPage,
                                             TotalItems = TotalPosts,
                                             CurrentCategory = text
                                         };
                        break;
                    }
                case "Tag":
                    {
                        Posts = _blogRepository.PostsForTags(text, pageNo - 1, itemsPerPage);
                        TotalPosts = _blogRepository.TotalPostsForTag(text);
                        Tag = _blogRepository.Tag(text);
                        CurrentTag = Tag.Name;
                        PagingInfo = new PagingInfo
                                         {
                                             CurrentPage = pageNo,
                                             ItemsPerPage = itemsPerPage,
                                             TotalItems = TotalPosts,
                                             CurrentTag = text
                                         };
                        break;
                    }
                default:
                    {
                        Posts = _blogRepository.PostsForSearch(text, pageNo - 1, itemsPerPage);
                        TotalPosts = _blogRepository.TotalPostsForSearch(text);
                        PagingInfo = new PagingInfo
                                         {
                                             CurrentPage = pageNo,
                                             ItemsPerPage = itemsPerPage,
                                             TotalItems = TotalPosts,
                                             CurrentCategory = text
                                         };
                        break;
                    }
            }
        }

        #endregion

        #region Public Properties

        public Category Category { get; private set; }

        public string CurrentCategory { get; set; }

        public string CurrentTag { get; set; }

        public PagingInfo PagingInfo { get; private set; }

        public IQueryable<Post> Posts { get; set; }

        public Tag Tag { get; private set; }

        public int TotalPosts { get; private set; }

        public string Search { get; private set; }

        #endregion
    }
}