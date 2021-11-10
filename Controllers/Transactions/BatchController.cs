using System;
using System.Collections.Generic;
using System.Linq;
using UnicdaPlatform.Controllers.Inteface;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.Trans;

namespace UnicdaPlatform.Controllers.Transactions
{
    public class BatchController : MainIntefaces
    {
        public bool Delete(ApplicationDbContext context, int Id)
        {
            try
            {
                if (Id != 0)
                {
                    var data = (Batch)Get(context, Id);
                    context.Batch.Update(data);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return false;
            }
        }

        public bool Exist(ApplicationDbContext context, int Id)
        {
            try
            {
                return context.Batch.Any(a => a.Id == Id);
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
                var trans = context.Batch.Where(a => a.Id == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new Batch();
            }
        }

        public object Get(ApplicationDbContext context, string Id)
        {
            try
            {
                var trans = context.Batch.Where(a => a.BatchId == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new Batch();
            }
        }

        public List<Batch> GetList(ApplicationDbContext context)
        {
            try
            {
                return context.Batch.ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<Batch>();
            }
        }

        public int Save(ApplicationDbContext context, object data)
        {
            try
            {
                var dataSave = (Batch)data;
                if (dataSave.Id == 0)
                    context.Batch.Add(dataSave);
                else
                    context.Batch.Update(dataSave);

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

