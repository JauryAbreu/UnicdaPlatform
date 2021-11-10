using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.User;

namespace UnicdaPlatform.Controllers.Users
{
    public class UserGroupController
    {
        public bool Delete(ApplicationDbContext context, int Id)
        {
            try
            {
                if (Id != 0)
                {
                    var data = (UserGroup)Get(context, Id);
                    context.UserGroup.Update(data);
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
                return context.UserGroup.Any(a => a.Id == Id);
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
                var trans = context.UserGroup.Where(a => a.Id == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new UserGroup();
            }
        }

        public object Get(ApplicationDbContext context, string Id)
        {
            try
            {
                var trans = context.UserGroup.Where(a => a.GroupId == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new UserGroup();
            }
        }

        public List<UserGroup> GetList(ApplicationDbContext context)
        {
            try
            {
                return context.UserGroup.ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<UserGroup>();
            }
        }

        public int Save(ApplicationDbContext context, object data)
        {
            try
            {
                var dataSave = (UserGroup)data;
                if (dataSave.Id == 0)
                    context.UserGroup.Add(dataSave);
                else
                    context.UserGroup.Update(dataSave);

                return context.SaveChanges();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return 0;
            }
        }
    }
}
