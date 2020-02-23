using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.Extensions.Logging;

namespace Csi.LookMeUp.Models
{
    public interface IClaimsIdentityUser
    {
        string DisplayName { get; set; }
        string AuthenticationType { get; set; }
    }

    public class ClaimsIdentityUser : IClaimsIdentityUser
    {
        private ClaimsIdentity claimsIdentity { get; set; }
        private IEnumerable<Claim> claimList { get; set; }

        public string AuthenticationType { get; set; }

        
        public string NameId { get; set; }
        public string DisplayName { get; set; }

        public ClaimsIdentityUser(IIdentity identity)
        {
            Claim claim = null;

            claimsIdentity = identity as ClaimsIdentity;
            if (claimsIdentity == null)
                throw new ArgumentException("identity is not of type ClaimsIdentity", "identity");

            if (identity.IsAuthenticated)
            {
                claimList = claimsIdentity.Claims;

                // Mapped the claims to the fields that we are handling here

                AuthenticationType = claimsIdentity.AuthenticationType;

                // We might to provide different mapping functions depending on the authentication type

                claim = claimList.Where(r => r.Type == ClaimTypes.GivenName).FirstOrDefault();
                if (claim != null)
                {
                    DisplayName = claim.Value;
                }
                claim = claimList.Where(r => r.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
                if (claim != null)
                {
                    NameId = claim.Value;
                }


            }
        }

    }
}
