using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.User;

namespace UnicdaPlatform.Controllers.Users
{
    public class GroupPermissionController
    {
        public bool Delete(ApplicationDbContext context, int Id)
        {
            try
            {
                if (Id != 0)
                {
                    var data = (GroupPermission)Get(context, Id);
                    context.GroupPermission.Update(data);
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
                return context.GroupPermission.Any(a => a.Id == Id);
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
                var trans = context.GroupPermission.Where(a => a.Id == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new GroupPermission();
            }
        }

        public object Get(ApplicationDbContext context, string GroupId, string PermissionId)
        {
            try
            {
                var trans = context.GroupPermission.Where(a => a.GroupId == GroupId && a.PermissionId == PermissionId).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new GroupPermission();
            }
        }

        public List<GroupPermission> GetList(ApplicationDbContext context)
        {
            try
            {
                return context.GroupPermission.ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<GroupPermission>();
            }
        }
        public List<GroupPermission> GetList(ApplicationDbContext context, int groupId)
        {
            try
            {
                var userGroup = (UserGroup)new UserGroupController().Get(context, groupId);
                return context.GroupPermission.Where(a=>a.GroupId == userGroup.GroupId).ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<GroupPermission>();
            }
        }
        public List<GroupPermission> GetList(ApplicationDbContext context, string groupId)
        {
            try
            {
                return context.GroupPermission.Where(a => a.GroupId == groupId).ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<GroupPermission>();
            }
        }

        public int Save(ApplicationDbContext context, object data)
        {
            try
            {
                var dataSave = (GroupPermission)data;
                if (dataSave.Id == 0)
                    context.GroupPermission.Add(dataSave);
                else
                    context.GroupPermission.Update(dataSave);

                return context.SaveChanges();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return 0;
            }
        }

        public int Save(ApplicationDbContext context, List<GroupPermission> collection)
        {
            try
            {
                var lines = GetList(context, collection.FirstOrDefault().GroupId);
                foreach (var item in lines)
                    context.GroupPermission.Remove(item);
                context.SaveChanges();

                foreach (var item in collection)
                    context.GroupPermission.Add(item);
                
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
