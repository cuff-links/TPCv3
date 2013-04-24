using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TPCv3.Domain.Entities{
    public class Post{
        #region Public Properties

        public virtual Category Category { get; set; }

        [Required(ErrorMessage = "Description: Field is required")]
        public virtual string Description { get; set; }

        [Key]
        [Required(ErrorMessage = "Id: Field is required")]
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Meta: Field is required")]
        [StringLength(1000, ErrorMessage = "Meta: Length should not exceed 1000 characters")]
        public virtual string Meta { get; set; }

        public virtual DateTime? Modified { get; set; }

        [Required(ErrorMessage = "PostedOn: Field is required")]
        public virtual DateTime PostedOn { get; set; }

        public virtual bool Published { get; set; }

        [Required(ErrorMessage = "ShortDescription: Field is required")]
        public virtual string ShortDescription { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        [Required(ErrorMessage = "Title: Field is required")]
        [StringLength(500, ErrorMessage = "Title: Length should not exceed 500 characters")]
        public virtual string Title { get; set; }

        [Required(ErrorMessage = "UrlSlug: Field is required")]
        [StringLength(1000, ErrorMessage = "UrlSlug: UrlSlug should not exceed 50 characters")]
        public virtual string UrlSlug { get; set; }

        #endregion
    }
}