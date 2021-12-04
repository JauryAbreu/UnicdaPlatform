using System;
using System.Collections.Generic;
using System.Linq;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.Request;

namespace UnicdaPlatform.Controllers.Request
{
    public class RequestUserMatterController
    {
        public bool Exist(ApplicationDbContext context, int Id)
        {
            try
            {
                return context.RequestUserMatter.Any(a => a.Id == Id);
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
                var trans = context.RequestUserMatter.Where(a => a.Id == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new RequestUserMatter();
            }
        }

        public object Get(ApplicationDbContext context, string UserId, int CareerPensumId, int SessionCode, int PeriodCycle, int PeriodYear)
        {
            try
            {
                var trans = context.RequestUserMatter.Where(a => a.UserId == UserId && a.CareerPensumId == CareerPensumId &&
                                                                a.SessionCode == SessionCode && a.PeriodCycle == PeriodCycle &&
                                                                a.PeriodYear == PeriodYear).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new RequestUserMatter();
            }
        }

        public List<RequestUserMatter> GetList(ApplicationDbContext context, string UserId)
        {
            try
            {
                return context.RequestUserMatter.Where(a => a.UserId == UserId && a.Status == 1).ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<RequestUserMatter>();
            }
        }

        public List<RequestUserMatter> GetList(ApplicationDbContext context)
        {
            try
            {
                return context.RequestUserMatter.ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<RequestUserMatter>();
            }
        }

        public int Save(ApplicationDbContext context, object data)
        {
            try
            {
                var dataSave = (RequestUserMatter)data;
                if (dataSave.Id == 0)
                    context.RequestUserMatter.Add(dataSave);
                else
                    context.RequestUserMatter.Update(dataSave);

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
