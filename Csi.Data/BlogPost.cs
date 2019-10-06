using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csi.Data
{
    [Table("blog_post")]
    public class BlogPost //: BaseEntity, IUserAudit
    {
        [Key]
        [Column("id")]
        public uint Id { get; set; }

        [DataType(DataType.Text)]
        [Required]
        [StringLength(int.MaxValue, MinimumLength=3, ErrorMessage="Text must have a minimum length of 3 characters.")]
        [Column("title")]
        public string Title { get; set; }
        
        // Constructors

        public BlogPost(string title) : this()
        {
            if (title == null)
            {
                throw new ArgumentNullException(nameof(title), "Text cannot be null.");
            }

            if ((string.IsNullOrWhiteSpace(title)) || (title.Length < 3))
            {
                throw new ArgumentOutOfRangeException(nameof(title), "Text too short");
            }

            this.Title = title;
        }

        public BlogPost()
        {
        }

    }
}
