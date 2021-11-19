using System;
using System.Collections.Generic;
using System.Linq;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.CarrierSubjects;

namespace UnicdaPlatform.Controllers.CarrierSubject
{
    public class CarrierUserPensumController
    {
        public bool Exist(ApplicationDbContext context, int Id)
        {
            try
            {
                return context.CarrierUserPensum.Any(a => a.Id == Id);
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
                var trans = context.CarrierUserPensum.Where(a => a.Id == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new CarrierUserPensum();
            }
        }

        public object Get(ApplicationDbContext context, string CareerId, string CareerPensumId, string SessionCode, int PeriodCycle, int PeriodYear)
        {
            try
            {
                var trans = context.CarrierUserPensum.Where(a => a.UserId == CareerId && a.CareerPensumId == CareerPensumId &&
                                                                        a.SessionCode == SessionCode && a.PeriodCycle == PeriodCycle &&
                                                                        a.PeriodYear == PeriodYear).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new CarrierUserPensum();
            }
        }

        public List<CarrierUserPensum> GetList(ApplicationDbContext context)
        {
            try
            {
                return context.CarrierUserPensum.ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<CarrierUserPensum>();
            }
        }

        public int Save(ApplicationDbContext context, object data)
        {
            try
            {
                var dataSave = (CarrierUserPensum)data;
                if (dataSave.Id == 0)
                    context.CarrierUserPensum.Add(dataSave);
                else
                    context.CarrierUserPensum.Update(dataSave);

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
