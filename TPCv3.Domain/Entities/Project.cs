using System.ComponentModel.DataAnnotations;

namespace TPCv3.Domain.Entities{
    public class Project{
        #region Public Properties

        public virtual ProjectCategory Category { get; set; }

        public string Code { get; set; }

        public bool Completed { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public bool IsDemo { get; set; }

        public string Name { get; set; }

        [Key]
        public int ProjectId { get; set; }

        public string SourceUrl { get; set; }

        public string ThumbUrl { get; set; }

        #endregion
    }
}