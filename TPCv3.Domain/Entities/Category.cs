using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TPCv3.Domain.Entities{
    public class Category{
        #region Public Properties

        public virtual string Description { get; set; }

        [Key]
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Name: Field is required")]
        [StringLength(500, ErrorMessage = "Name: Length should not exceed 500 characters")]
        public virtual string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Post> Posts { get; set; }

        [Required(ErrorMessage = "UrlSlug: Field is required")]
        [StringLength(500, ErrorMessage = "UrlSlug: Length should not exceed 500 characters")]
        public virtual string UrlSlug { get; set; }

        #endregion
    }
}