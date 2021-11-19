using System;
using System.Collections.Generic;
using System.Linq;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.Request;

namespace UnicdaPlatform.Controllers.Request
{
    public class RequestUserChangeCarrierController
    {
        public bool Exist(ApplicationDbContext context, int Id)
        {
            try
            {
                return context.RequestUserChangeCarrier.Any(a => a.Id == Id);
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
                var trans = context.RequestUserChangeCarrier.Where(a => a.Id == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new RequestUserChangeCarrier();
            }
        }

        public object Get(ApplicationDbContext context, string UserId)
        {
            try
            {
                var trans = context.RequestUserChangeCarrier.Where(a => a.UserId == UserId).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new RequestUserChangeCarrier();
            }
        }

        public List<RequestUserChangeCarrier> GetList(ApplicationDbContext context)
        {
            try
            {
                return context.RequestUserChangeCarrier.ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<RequestUserChangeCarrier>();
            }
        }

        public List<RequestUserChangeCarrier> GetList(ApplicationDbContext context, string UserId)
        {
            try
            {
                return context.RequestUserChangeCarrier.Where(a => a.UserId == UserId).ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<RequestUserChangeCarrier>();
            }
        }

        public int Save(ApplicationDbContext context, object data)
        {
            try
            {
                var dataSave = (RequestUserChangeCarrier)data;
                if (dataSave.Id == 0)
                    context.RequestUserChangeCarrier.Add(dataSave);
                else
                    context.RequestUserChangeCarrier.Update(dataSave);

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
