using System;
using System.Collections.Generic;
using System.Linq;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.CarrierSubjects;

namespace UnicdaPlatform.Controllers.CarrierSubjects
{
    public class CarrierPensumController
    {
        public bool Exist(ApplicationDbContext context, int Id)
        {
            try
            {
                return context.CarrierPensum.Any(a => a.Id == Id);
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
                var trans = context.CarrierPensum.Where(a => a.Id == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new CarrierPensum();
            }
        }

        public object Get(ApplicationDbContext context, string CareerId, string CareerPensumId)
        {
            try
            {
                var trans = context.CarrierPensum.Where(a => a.CareerId == CareerId && a.CareerPensumId == CareerPensumId).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new CarrierPensum();
            }
        }

        public List<CarrierPensum> GetList(ApplicationDbContext context)
        {
            try
            {
                return context.CarrierPensum.ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<CarrierPensum>();
            }
        }

        public int Save(ApplicationDbContext context, object data)
        {
            try
            {
                var dataSave = (CarrierPensum)data;
                if (dataSave.Id == 0)
                    context.CarrierPensum.Add(dataSave);
                else
                    context.CarrierPensum.Update(dataSave);

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
