using System;
using System.Collections.Generic;
using System.Linq;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.CareerSubjects;

namespace UnicdaPlatform.Controllers.CareerSubject
{
    public class CareerUserPensumController
    {
        public bool Exist(ApplicationDbContext context, int Id)
        {
            try
            {
                return context.CareerUserPensum.Any(a => a.Id == Id);
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
                var trans = context.CareerUserPensum.Where(a => a.Id == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new CareerUserPensum();
            }
        }

        public object Get(ApplicationDbContext context, string CareerId, int CareerPensumId, string SessionCode, int PeriodCycle, int PeriodYear)
        {
            try
            {
                var trans = context.CareerUserPensum.Where(a => a.UserId == CareerId && a.CareerPensumId == CareerPensumId &&
                                                                        a.SessionCode == SessionCode && a.PeriodCycle == PeriodCycle &&
                                                                        a.PeriodYear == PeriodYear).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new CareerUserPensum();
            }
        }

        public List<CareerUserPensum> GetList(ApplicationDbContext context)
        {
            try
            {
                return context.CareerUserPensum.ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<CareerUserPensum>();
            }
        }

        public int Save(ApplicationDbContext context, object data)
        {
            try
            {
                var dataSave = (CareerUserPensum)data;
                if (dataSave.Id == 0)
                    context.CareerUserPensum.Add(dataSave);
                else
                    context.CareerUserPensum.Update(dataSave);

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
