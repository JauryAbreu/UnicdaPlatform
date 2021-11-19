using System;
using System.Collections.Generic;
using System.Linq;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.CarrierSubjects;

namespace UnicdaPlatform.Controllers.CarrierSubject
{
    public class CarrierUserTeacherPensumController
    {
        public bool Exist(ApplicationDbContext context, int Id)
        {
            try
            {
                return context.CarrierUserTeacherPensum.Any(a => a.Id == Id);
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
                var trans = context.CarrierUserTeacherPensum.Where(a => a.Id == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new CarrierUserTeacherPensum();
            }
        }

        public object Get(ApplicationDbContext context, string CareerId, string CareerPensumId, string SessionCode, int PeriodCycle, int PeriodYear)
        {
            try
            {
                var trans = context.CarrierUserTeacherPensum.Where(a => a.UserId == CareerId && a.CareerPensumId == CareerPensumId &&
                                                                        a.SessionCode == SessionCode && a.PeriodCycle == PeriodCycle &&
                                                                        a.PeriodYear == PeriodYear).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new CarrierUserTeacherPensum();
            }
        }

        public List<CarrierUserTeacherPensum> GetList(ApplicationDbContext context)
        {
            try
            {
                return context.CarrierUserTeacherPensum.ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<CarrierUserTeacherPensum>();
            }
        }

        public int Save(ApplicationDbContext context, object data)
        {
            try
            {
                var dataSave = (CarrierUserTeacherPensum)data;
                if (dataSave.Id == 0)
                    context.CarrierUserTeacherPensum.Add(dataSave);
                else
                    context.CarrierUserTeacherPensum.Update(dataSave);

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
