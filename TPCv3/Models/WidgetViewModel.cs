using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPCv3.Domain.Abstract;
using TPCv3.Domain.Entities;

namespace TPCv3.Models{
    public class WidgetViewModel{
        private readonly IBlogRepository _repository;

        public WidgetViewModel(IBlogRepository repository){
            _repository = repository;
            Categories = _repository.Categories();
            Tags = _repository.Tags();
            LatestPosts = _repository.Posts(0, 10).ToList();
        }

        public IList<Category> Categories { get; private set; }
        public IList<Tag> Tags { get; private set; }
        public IList<Post> LatestPosts { get; private set; }
    }
}