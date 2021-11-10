using System;
using System.Collections.Generic;
using System.Linq;
using UnicdaPlatform.Controllers.Inteface;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models;

namespace UnicdaPlatform.Controllers.Transactions
{
    public class TransactionController : MainIntefaces
    {
        public bool Delete(ApplicationDbContext context, int Id)
        {
            try
            {
                if (Id != 0)
                {
                    var data = (Models.Trans.Transactions)Get(context, Id);
                    context.Transaction.Update(data);
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
                return context.Transaction.Any(a => a.Id == Id);
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
                var trans = context.Transaction.Where(a => a.Id == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new Models.Trans.Transactions();
            }
        }

        public object Get(ApplicationDbContext context, string Id)
        {
            try
            {
                var trans = context.Transaction.Where(a => a.ReceiptId == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new Models.Trans.Transactions();
            }
        }

        public List<Models.Trans.Transactions> GetList(ApplicationDbContext context)
        {
            try
            {
                return context.Transaction.ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<Models.Trans.Transactions>();
            }
        }

        public int Save(ApplicationDbContext context, object data)
        {
            try
            {
                var dataSave = (Models.Trans.Transactions)data;
                if (dataSave.Id == 0)
                    context.Transaction.Add(dataSave);
                else
                    context.Transaction.Update(dataSave);

                return context.SaveChanges();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return 0;
            }
        }
        public List<Models.Trans.Transactions> GetTransactionLines(ApplicationDbContext context, DateTime dateStart, 
                                                         DateTime dateEnd, string receiptId = "") 
        {
            
            return new List<Models.Trans.Transactions>();
            
        }
    }
}

