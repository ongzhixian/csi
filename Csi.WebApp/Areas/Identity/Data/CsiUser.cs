using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csi.WebApp.Data
{
    // Add profile data for application users by adding properties to the CsiUser class
    public class CsiUser : IdentityUser
    {
        [Column(TypeName = "bit(1)")]
        public override bool EmailConfirmed { get; set; }

        [Column(TypeName = "bit(1)")]
        public override bool PhoneNumberConfirmed { get; set; }

        [Column(TypeName = "bit(1)")]
        public override bool TwoFactorEnabled { get; set; }

        [Column(TypeName = "bit(1)")]
        public override bool LockoutEnabled { get; set; }

    }
}
