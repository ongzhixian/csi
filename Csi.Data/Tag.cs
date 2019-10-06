using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csi.Data
{
    [Table("tag")]
    public class Tag //: BaseEntity, IUserAudit
    {
        [Key]
        [Column("id")]
        public uint Id { get; set; }

        [DataType(DataType.Text)]
        [Required]
        [StringLength(int.MaxValue, MinimumLength=3, ErrorMessage="Text must have a minimum length of 3 characters.")]
        [Column("text")]
        public string Text { get; set; }
        
        // Constructors

        public Tag(string text) : this()
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text), "Text cannot be null.");
            }

            if ((string.IsNullOrWhiteSpace(text)) || (text.Length < 3))
            {
                throw new ArgumentOutOfRangeException(nameof(text), "Text too short");
            }

            this.Text = text;
        }

        public Tag()
        {
        }

    }
}
