using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csi.Data
{
    [Table("Project")]
    public class Project : BaseEntity, IUserAudit
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [DataType(DataType.Text)]
        [Required]
        [StringLength(int.MaxValue, MinimumLength=3, ErrorMessage="Name must have a minimum length of 3 characters.")]
        [Column("name")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("status")]
        public byte Status { get; set; }

        // IUserAudit fields

        [Column("cre_by")]
        public string Cre_By { get; set; }

        [DataType(DataType.DateTime)]
        [Column("cre_dt")]
        public DateTime Cre_Dt { get; set; }

        [Column("upd_by")]
        public string Upd_By { get; set; }

        [DataType(DataType.DateTime)]
        [Column("upd_dt")]
        public DateTime? Upd_Dt { get; set; }

        [Column("del_by")]
        public string Del_By { get; set; }

        [DataType(DataType.DateTime)]
        [Column("del_dt")]
        public DateTime? Del_Dt { get; set; }


        // Navigation

        // public Guid? OrganizationId { get; set; }

        // [ForeignKey("OrganizationId")]
        // public Organization Organization { get; set; }

        // Constructors
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
            //this.RegisteredDate = registeredDate;
        }

        public Project()
        {
            this.Id = Guid.NewGuid();
        }

    }
}
