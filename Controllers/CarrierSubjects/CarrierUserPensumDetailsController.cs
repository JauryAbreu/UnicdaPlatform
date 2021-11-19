using System;
using System.Collections.Generic;
using System.Linq;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.CarrierSubjects;

namespace UnicdaPlatform.Controllers.CarrierSubject
{
    public class CarrierUserPensumDetailsController
    {
        public bool Exist(ApplicationDbContext context, int Id)
        {
            try
            {
                return context.CarrierUserPensumDetails.Any(a => a.Id == Id);
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
                var trans = context.CarrierUserPensumDetails.Where(a => a.Id == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new CarrierUserPensumDetails();
            }
        }

        public object Get(ApplicationDbContext context, string CareerId, string CareerPensumId, string SessionCode, int PeriodCycle, int PeriodYear)
        {
            try
            {
                var trans = context.CarrierUserPensumDetails.Where(a => a.UserId == CareerId && a.CareerPensumId == CareerPensumId &&
                                                                        a.SessionCode == SessionCode && a.PeriodCycle == PeriodCycle &&
                                                                        a.PeriodYear == PeriodYear).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new CarrierUserPensumDetails();
            }
        }

        public List<CarrierUserPensumDetails> GetList(ApplicationDbContext context)
        {
            try
            {
                return context.CarrierUserPensumDetails.ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<CarrierUserPensumDetails>();
            }
        }

        public int Save(ApplicationDbContext context, object data)
        {
            try
            {
                var dataSave = (CarrierUserPensumDetails)data;
                if (dataSave.Id == 0)
                    context.CarrierUserPensumDetails.Add(dataSave);
                else
                    context.CarrierUserPensumDetails.Update(dataSave);

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
