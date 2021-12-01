using System;
using System.Collections.Generic;
using System.Linq;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.CareerSubjects;

namespace UnicdaPlatform.Controllers.CareerSubject
{
    public class CareerController
    {
        public bool Exist(ApplicationDbContext context, int Id)
        {
            try
            {
                return context.Career.Any(a => a.Id == Id);
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
                var trans = context.Career.Where(a => a.Id == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new Career();
            }
        }

        public object Get(ApplicationDbContext context, string CareerId)
        {
            try
            {
                var trans = context.Career.Where(a => a.CareerId == CareerId).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new Career();
            }
        }

        public List<Career> GetList(ApplicationDbContext context)
        {
            try
            {
                return context.Career.ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<Career>();
            }
        }

        public int Save(ApplicationDbContext context, object data)
        {
            try
            {
                var dataSave = (Career)data;
                if (dataSave.Id == 0)
                    context.Career.Add(dataSave);
                else
                    context.Career.Update(dataSave);

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
