using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csi.Data
{
    [Table("Project")]
    public class Project : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [DataType(DataType.Text)]
        [Required]
        [StringLength(int.MaxValue, MinimumLength=3, ErrorMessage="Name must have a minimum length of 3 characters.")]
        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RegisteredDate { get; set; }

        // Navigation

        // public Guid? OrganizationId { get; set; }

        // [ForeignKey("OrganizationId")]
        // public Organization Organization { get; set; }

        // Constructors

        public Project(string name) : this(name, DateTime.UtcNow)
        {
        }

        public Project(string name, DateTime registeredDate) : this()
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name), "Project name cannot be null.");
            }

            if ((string.IsNullOrWhiteSpace(name)) || (name.Length < 3))
            {
                throw new ArgumentOutOfRangeException(nameof(name), "Project name too short");
            }

            this.Name = name;
            this.RegisteredDate = registeredDate;
        }

        public Project()
        {
            this.Id = Guid.NewGuid();
        }

    }
}
