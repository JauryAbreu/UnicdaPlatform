using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicdaPlatform.Controllers.Inteface;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.User;

namespace UnicdaPlatform.Controllers.Users
{
    public class UserController
    {
        public bool Delete(ApplicationDbContext context, int Id)
        {
            try
            {
                if (Id != 0)
                {
                    var data = (User)Get(context, Id);
                    context.User.Update(data);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return false;
            }
        }

        public bool Exist(ApplicationDbContext context, int Id)
        {
            try
            {
                return context.User.Any(a => a.Id == Id);
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return false;
            }
        }

        public object Get(ApplicationDbContext context, int Id)
        {
            try
            {
                var trans = context.User.Where(a => a.Id == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new User();
            }
        }

        public object Get(ApplicationDbContext context, string Id)
        {
            try
            {
                var trans = context.User.Where(a => a.MasterId == Id || a.UserId == Id || a.Email == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return null;
            }
        }

        public List<User> GetList(ApplicationDbContext context)
        {
            try
            {
                return context.User.ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<User>();
            }
        }

        public int Save(ApplicationDbContext context, object data, UserManager<IdentityUser> userManager)
        {
            try
            {
                var dataSave = (User)data;
                if (dataSave.Id == 0)
                {
                    if (RegisterUser(context, dataSave, userManager))
                    {
                        var userInfo = userManager.FindByNameAsync(dataSave.Email).Result;
                        dataSave.MasterId = userInfo.Id;
                        context.User.Add(dataSave);
                    }
                    else
                        return 0;
                }
                else
                    context.User.Update(dataSave);

                return context.SaveChanges();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return 0;
            }
        }

        private bool RegisterUser(ApplicationDbContext context, User userData, UserManager<IdentityUser> userManager)
        {
            try
            {
                var user = new IdentityUser { UserName = userData.Email, Email = userData.Email };
                var result = userManager.CreateAsync(user, "123456789M@$t3r").Result;

                if (result.Succeeded)
                {

                    var userInfo = userManager.FindByNameAsync(user.Email).Result;
                    context.Database.EnsureCreated();

                    var code = userManager.GenerateEmailConfirmationTokenAsync(user).Result;
                    var result2 = userManager.ConfirmEmailAsync(user, code).Result;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
