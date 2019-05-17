using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csi.Data
{
    [Table("Project")]
    public class Project : Entity<Guid>
    {
        
        
        //[Key]
        //public Guid Id { get; set; }

        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RegisteredDate { get; set; }

        // Navigation

        public Guid? OrganizationId { get; set; }

        [ForeignKey("OrganizationId")]
        public Organization Organization { get; set; }

        public Project()
        {
        }

        public Project(string name) : this(name, DateTime.UtcNow)
        {
        }

        public Project(string name, DateTime registeredDate)
        {
            this.Name = name;
            this.RegisteredDate = registeredDate;
        }

    }
}
