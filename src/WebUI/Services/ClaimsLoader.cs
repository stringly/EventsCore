﻿using EventsCore.Application.Common.Interfaces;
using EventsCore.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventsCore.WebUI.Services
{
    public class ClaimsLoader : IClaimsTransformation
    {
        private readonly IEventsCoreDbContext _context;
        public ClaimsLoader(IEventsCoreDbContext context)
        {
            _context = context;
        }
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var identity = principal.Identities.FirstOrDefault(x => x.IsAuthenticated);
            if (identity == null) return principal;
            var user = identity.Name;
            if (user == null) return principal;
            if (principal.Identity is ClaimsIdentity)
            {
                string logonName = user.Split('\\')[1];
                User dbUser = await _context.Users
                    .Include(x => x.Rank)
                    .Include(x => x.Roles)
                        .ThenInclude(x => x.UserRoleType)
                    .FirstOrDefaultAsync(x => x.LDAPName == logonName);
                if (dbUser != null)
                {
                    var ci = (ClaimsIdentity)principal.Identity;
                    // TODO: Uncomment and update below when User Roles are implemented
                    foreach (UserRole ur in dbUser.Roles)
                    {
                        var c = new Claim(ci.RoleClaimType, ur.UserRoleType.Name);
                        ci.AddClaim(c);
                        //if (ur.RoleType.RoleTypeName == "ComponentAdmin")
                        //{
                        //    int memberParentComponentId = dbUser.Position.ParentComponent.ComponentId;
                        //    // TODO: Repo method to get tree of componentIds for the user's parent component
                        //    List<ComponentSelectListItem> canEditComponents = _unitOfWork.Components.GetChildComponentsForComponentId(memberParentComponentId);
                        //    var d = new Claim("CanEditComponents", JsonConvert.SerializeObject(canEditComponents));
                        //    ci.AddClaim(d);
                        //    List<MemberSelectListItem> canEditMembers = _unitOfWork.Members.GetMembersUserCanEdit(memberParentComponentId);
                        //    var e = new Claim("CanEditUsers", JsonConvert.SerializeObject(canEditMembers));
                        //    ci.AddClaim(e);
                        //    List<PositionSelectListItem> canEditPositions = _unitOfWork.Positions.GetPositionsUserCanEdit(memberParentComponentId);
                        //    var f = new Claim("CanEditPositions", JsonConvert.SerializeObject(canEditPositions));
                        //    ci.AddClaim(f);
                        //}
                    }
                    ci.AddClaim(new Claim(ClaimTypes.GivenName, dbUser.NameFactory.First));
                    ci.AddClaim(new Claim(ClaimTypes.Surname, dbUser.NameFactory.Last));                    
                    ci.AddClaim(new Claim("UserId", dbUser.Id.ToString(), ClaimValueTypes.Integer32));
                    ci.AddClaim(new Claim("GivenName", dbUser.NameFactory.First));
                    ci.AddClaim(new Claim("DisplayNameShort", $"{dbUser.Rank.Abbreviation} {dbUser.NameFactory.Last}"));
                    ci.AddClaim(new Claim("DisplayName", dbUser.DisplayName));
                    ci.AddClaim(new Claim(ClaimTypes.WindowsAccountName, dbUser.LDAPName));
                }
                else
                {
                    var ci = (ClaimsIdentity)principal.Identity;
                    ci.AddClaim(new Claim("DisplayName", "Guest"));
                    ci.AddClaim(new Claim("UserId", "0", ClaimValueTypes.Integer32));
                    ci.AddClaim(new Claim("GivenName", "Guest"));
                    ci.AddClaim(new Claim("DisplayNameShort", "Guest"));
                    //ci.AddClaim(new Claim(ci.RoleClaimType, "Guest"));
                    ci.AddClaim(new Claim("LDAPName", logonName));
                }
            }
            return principal;
        }

    }
}
