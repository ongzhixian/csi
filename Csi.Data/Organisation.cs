using System;
using System.ComponentModel.DataAnnotations;

namespace Csi.Data
{
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
