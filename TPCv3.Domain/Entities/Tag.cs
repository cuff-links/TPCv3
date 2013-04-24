﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace TPCv3.Domain.Entities{
    public class Tag{
        #region Public Properties

        public virtual string Description { get; set; }

        [Key]
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Post> Posts { get; set; }

        public virtual string UrlSlug { get; set; }

        #endregion
    }
}