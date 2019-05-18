using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csi.Data
{
    [Obsolete]
    [Table("Organization")]
    public class Organization 
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime RegisteredDate { get; set; }

        

    }
}
