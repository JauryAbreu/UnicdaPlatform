using System;
using System.Collections.Generic;
using System.Linq;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.CareerSubjects;

namespace UnicdaPlatform.Controllers.CareerSubjects
{
    public class CareerPensumController
    {
        public bool Exist(ApplicationDbContext context, int Id)
        {
            try
            {
                return context.CareerPensum.Any(a => a.Id == Id);
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
                var trans = context.CareerPensum.Where(a => a.Id == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new CareerPensum();
            }
        }

        public object Get(ApplicationDbContext context, string CareerId, string MatterId)
        {
            try
            {
                var trans = context.CareerPensum.Where(a => a.CareerId == CareerId && a.MatterId == MatterId).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new CareerPensum();
            }
        }

        public List<CareerPensum> GetList(ApplicationDbContext context)
        {
            try
            {
                return context.CareerPensum.ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<CareerPensum>();
            }
        }

        public int Save(ApplicationDbContext context, object data)
        {
            try
            {
                var dataSave = (CareerPensum)data;
                if (dataSave.Id == 0)
                    context.CareerPensum.Add(dataSave);
                else
                    context.CareerPensum.Update(dataSave);

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
