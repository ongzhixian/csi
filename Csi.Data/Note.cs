using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csi.Data
{
    [Table("note")]
    public class Note //: BaseEntity, IUserAudit
    {
        [Key]
        [Column("id")]
        public uint Id { get; set; }

        [DataType(DataType.Text)]
        [Required]
        [StringLength(int.MaxValue, MinimumLength=3, ErrorMessage="Text must have a minimum length of 3 characters.")]
        [Column("title")]
        public string Title { get; set; }

        [DataType(DataType.Text)]
        [Required]
        [Column("title")]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        [Column("upd_dt")]
        public DateTime? Upd_Dt { get; set; }

        // Constructors

        public Note(string title) : this()
        {
            if (title == null)
            {
                throw new ArgumentNullException(nameof(title), "Title cannot be null.");
            }

            if ((string.IsNullOrWhiteSpace(title)) || (title.Length < 3))
            {
                throw new ArgumentOutOfRangeException(nameof(title), "Title too short");
            }

            this.Title = title;
        }

        public Note()
        {
        }

    }
}
