using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.User;

namespace UnicdaPlatform.Controllers.Users
{
    public class UserMainController
    {
        public UserMain Get(ApplicationDbContext context, string Id)
        {
            UserMain user = new UserMain()
            {
                userExist = false,
                error = string.Empty
            };

            try
            {
                if (!string.IsNullOrEmpty(Id))
                {
                    var _user = context.User.Where(a=>a.MasterId == Id).FirstOrDefault();
                    if (_user != null)
                    {
                        user.user = _user;
                        user.userGroup = context.UserGroup.Where(a => a.Id == _user.GroupId).FirstOrDefault();
                        user.groupPermission = context.GroupPermission.Where(a => a.GroupId == user.userGroup.GroupId).ToList();
                    }
                }

                return user;
            }
            catch (Exception ex)
            {
                return user;
            }
        }

        public Tuple<bool, string, string, int, string> GetUserDate(ApplicationDbContext _context,
                                                        IdentityUser user,
                                                        string permission = "")
        {
            try
            {
                var userData = Get(_context, user.Id);
                if (userData == null)
                    return Tuple.Create(false, string.Empty, string.Empty, 0, string.Empty);

                if (userData.user == null)
                    return Tuple.Create(false, string.Empty, string.Empty, 0, string.Empty);

                string username = string.Format("{0} {1}", userData.user.FirstName, userData.user.LastName);

                if (userData.groupPermission.Count > 0)
                {
                    GroupPermission hasPermission = new GroupPermission();
                    if (!string.IsNullOrEmpty(permission))
                        hasPermission = userData.groupPermission.Where(a => a.PermissionId == permission).First();

                    var comp = _context.Company.Where(a => a.CompanyId == userData.user.CompanyId).First();

                    if (hasPermission != null)
                        return Tuple.Create(true, username, comp.Logo, comp.Id, comp.CompanyName);
                    else
                        return Tuple.Create(false, string.Empty, string.Empty, 0, string.Empty);
                }
                else
                    return Tuple.Create(false, string.Empty, string.Empty, 0, string.Empty);
            }
            catch
            {
                return Tuple.Create(false, string.Empty, string.Empty, 0, string.Empty);
            }
        }
    }
}
