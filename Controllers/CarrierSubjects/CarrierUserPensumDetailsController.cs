using System;
using System.Collections.Generic;
using System.Linq;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.CareerSubjects;

namespace UnicdaPlatform.Controllers.CareerSubject
{
    public class CareerUserPensumDetailsController
    {
        public bool Exist(ApplicationDbContext context, int Id)
        {
            try
            {
                return context.CareerUserPensumDetails.Any(a => a.Id == Id);
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return false;
            }
        }

        public object Get(ApplicationDbContext context, int CareerUserPensumId)
        {
            try
            {
                var trans = context.CareerUserPensumDetails.Where(a => a.CareerUserPensumId == CareerUserPensumId).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new CareerUserPensumDetails();
            }
        }

        public List<CareerUserPensumDetails> GetList(ApplicationDbContext context)
        {
            try
            {
                return context.CareerUserPensumDetails.ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<CareerUserPensumDetails>();
            }
        }

        public int Save(ApplicationDbContext context, object data)
        {
            try
            {
                var dataSave = (CareerUserPensumDetails)data;
                if (dataSave.Id == 0)
                    context.CareerUserPensumDetails.Add(dataSave);
                else
                    context.CareerUserPensumDetails.Update(dataSave);

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
