using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicdaPlatform.Controllers.Inteface;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.Fiscal;

namespace UnicdaPlatform.Controllers.Fiscal
{
    public class NcfHistoryController
    {
        public bool Exist(ApplicationDbContext context, int Id)
        {
            try
            {
                return context.NcfHistory.Any(a => a.Id == Id);
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
                var trans = context.NcfHistory.Where(a => a.Id == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new NcfHistory();
            }
        }

        public object Get(ApplicationDbContext context, string Id)
        {
            try
            {
                var trans = context.NcfHistory.Where(a => a.ReceiptId == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new NcfHistory();
            }
        }

        public List<NcfHistory> GetList(ApplicationDbContext context)
        {
            try
            {
                return context.NcfHistory.ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<NcfHistory>();
            }
        }

        public int Save(ApplicationDbContext context, object data)
        {
            try
            {
                var dataSave = (NcfHistory)data;
                if (dataSave.Id == 0)
                    context.NcfHistory.Add(dataSave);
                else
                    context.NcfHistory.Update(dataSave);

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
