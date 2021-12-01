using System;
using System.Collections.Generic;
using System.Linq;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.CareerSubjects;

namespace UnicdaPlatform.Controllers.CareerSubject
{
    public class CareerUserTeacherPensumController
    {
        public bool Exist(ApplicationDbContext context, int Id)
        {
            try
            {
                return context.CareerUserTeacherPensum.Any(a => a.Id == Id);
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
                var trans = context.CareerUserTeacherPensum.Where(a => a.Id == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new CareerUserTeacherPensum();
            }
        }

        public object Get(ApplicationDbContext context, string CareerId, int CareerPensumId, string SessionCode, int PeriodCycle, int PeriodYear)
        {
            try
            {
                var trans = context.CareerUserTeacherPensum.Where(a => a.UserId == CareerId && a.CareerPensumId == CareerPensumId &&
                                                                        a.SessionCode == SessionCode && a.PeriodCycle == PeriodCycle &&
                                                                        a.PeriodYear == PeriodYear).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new CareerUserTeacherPensum();
            }
        }

        public List<CareerUserTeacherPensum> GetList(ApplicationDbContext context)
        {
            try
            {
                return context.CareerUserTeacherPensum.ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<CareerUserTeacherPensum>();
            }
        }

        public int Save(ApplicationDbContext context, object data)
        {
            try
            {
                var dataSave = (CareerUserTeacherPensum)data;
                if (dataSave.Id == 0)
                    context.CareerUserTeacherPensum.Add(dataSave);
                else
                    context.CareerUserTeacherPensum.Update(dataSave);

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
