using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TPCv3.Domain.Entities{
    public class Post{
        #region Public Properties

        public virtual Category Category { get; set; }

        public virtual string Description { get; set; }

        [Key]
        public virtual int Id { get; set; }

        public virtual string Meta { get; set; }

        public virtual DateTime? Modified { get; set; }

        public virtual DateTime PostedOn { get; set; }

        public virtual bool Published { get; set; }

        public virtual string ShortDescription { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual string Title { get; set; }

        public virtual string UrlSlug { get; set; }

        #endregion
    }
}