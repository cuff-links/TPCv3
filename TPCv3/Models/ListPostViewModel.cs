using System.Linq;
using TPCv3.Domain.Abstract;
using TPCv3.Domain.Entities;

namespace TPCv3.Models{
    public class ListPostViewModel{
        #region Constants and Fields

        private readonly IBlogRepository _blogRepository;

        private int itemsPerPage = 10;

        #endregion

        #region Constructors and Destructors

        public ListPostViewModel(IBlogRepository blogRepository, int pageNo){
            this._blogRepository = blogRepository;
            this.Posts = this._blogRepository.Posts(pageNo - 1, this.itemsPerPage);
            this.TotalPosts = this._blogRepository.TotalPosts();
            this.PagingInfo = new PagingInfo
                                  {CurrentPage = pageNo, ItemsPerPage = this.itemsPerPage, TotalItems = this.TotalPosts};
        }

        public ListPostViewModel(IBlogRepository blogRepository, string text, string type, int pageNo){
            this._blogRepository = blogRepository;
            switch (type){
                case "Category":
                    {
                        this.Posts = this._blogRepository.PostsForCategory(text, pageNo - 1, this.itemsPerPage);
                        this.TotalPosts = this._blogRepository.TotalPostsForCategory(text);
                        this.Category = this._blogRepository.Category(text);
                        this.PagingInfo = new PagingInfo
                                              {
                                                  CurrentPage = pageNo,
                                                  ItemsPerPage = this.itemsPerPage,
                                                  TotalItems = this.TotalPosts,
                                                  CurrentCategory = text
                                              };
                        break;
                    }
                case "Tag":
                    {
                        this.Posts = this._blogRepository.PostsForTags(text, pageNo - 1, this.itemsPerPage);
                        this.TotalPosts = this._blogRepository.TotalPostsForTag(text);
                        this.Tag = this._blogRepository.Tag(text);
                        CurrentTag = Tag.Name;
                        this.PagingInfo = new PagingInfo
                                              {
                                                  CurrentPage = pageNo,
                                                  ItemsPerPage = this.itemsPerPage,
                                                  TotalItems = this.TotalPosts,
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

        public IQueryable<Post> Posts { get; private set; }

        public Tag Tag { get; private set; }

        public int TotalPosts { get; private set; }

        public string Search { get; private set; }

        #endregion
    }
}