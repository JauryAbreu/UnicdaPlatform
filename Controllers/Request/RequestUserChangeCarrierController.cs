using System;
using System.Collections.Generic;
using System.Linq;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.Request;

namespace UnicdaPlatform.Controllers.Request
{
    public class RequestUserChangeCareerController
    {
        public bool Exist(ApplicationDbContext context, int Id)
        {
            try
            {
                return context.RequestUserChangeCareer.Any(a => a.Id == Id);
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
                var trans = context.RequestUserChangeCareer.Where(a => a.Id == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new RequestUserChangeCareer();
            }
        }

        public object Get(ApplicationDbContext context, string UserId)
        {
            try
            {
                var trans = context.RequestUserChangeCareer.Where(a => a.UserId == UserId).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new RequestUserChangeCareer();
            }
        }

        public List<RequestUserChangeCareer> GetList(ApplicationDbContext context)
        {
            try
            {
                return context.RequestUserChangeCareer.ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<RequestUserChangeCareer>();
            }
        }

        public List<RequestUserChangeCareer> GetList(ApplicationDbContext context, string UserId)
        {
            try
            {
                return context.RequestUserChangeCareer.Where(a => a.UserId == UserId).ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<RequestUserChangeCareer>();
            }
        }

        public int Save(ApplicationDbContext context, object data)
        {
            try
            {
                var dataSave = (RequestUserChangeCareer)data;
                if (dataSave.Id == 0)
                    context.RequestUserChangeCareer.Add(dataSave);
                else
                    context.RequestUserChangeCareer.Update(dataSave);

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
