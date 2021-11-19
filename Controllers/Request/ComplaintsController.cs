using System;
using System.Collections.Generic;
using System.Linq;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.Request;

namespace UnicdaPlatform.Controllers.Request
{
    public class ComplaintsController
    {
        public bool Exist(ApplicationDbContext context, int Id)
        {
            try
            {
                return context.Complaints.Any(a => a.Id == Id);
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
                var trans = context.Complaints.Where(a => a.Id == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new Complaints();
            }
        }

        public object Get(ApplicationDbContext context, string UserId)
        {
            try
            {
                var trans = context.Complaints.Where(a => a.UserId == UserId).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new Complaints();
            }
        }

        public List<Complaints> GetList(ApplicationDbContext context, string UserId)
        {
            try
            {
                return context.Complaints.Where(a => a.UserId == UserId).ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<Complaints>();
            }
        }

        public List<Complaints> GetList(ApplicationDbContext context)
        {
            try
            {
                return context.Complaints.ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<Complaints>();
            }
        }

        public int Save(ApplicationDbContext context, object data)
        {
            try
            {
                var dataSave = (Complaints)data;
                if (dataSave.Id == 0)
                    context.Complaints.Add(dataSave);
                else
                    context.Complaints.Update(dataSave);

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
