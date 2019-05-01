using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csi.WebApp.Data
{
    [Table("CsiUsers", Schema = "csi")]
    public partial class CsiUsers
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        [Column(TypeName = "bit(1)")]
        public short EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        [Column(TypeName = "bit(1)")]
        public short PhoneNumberConfirmed { get; set; }
        [Column(TypeName = "bit(1)")]
        public short TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        [Column(TypeName = "bit(1)")]
        public short LockoutEnabled { get; set; }
        [Column(TypeName = "int(11)")]
        public int AccessFailedCount { get; set; }
    }
}
