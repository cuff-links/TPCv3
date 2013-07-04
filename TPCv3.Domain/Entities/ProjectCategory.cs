using System.ComponentModel.DataAnnotations;

namespace TPCv3.Domain.Entities{
    public class ProjectCategory{
        public virtual string Description { get; set; }

        [Key]
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }
    }
}