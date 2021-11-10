using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.Fiscal;

namespace UnicdaPlatform.Controllers.Fiscal
{
    public class NcfTypeController
    {

        public object Get(ApplicationDbContext context, int Id)
        {
            try
            {
                var trans = context.NcfType.Where(a => a.NcfId == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new NcfType();
            }
        }

        public List<NcfType> GetList(ApplicationDbContext context)
        {
            try
            {
                return context.NcfType.ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<NcfType>();
            }
        }
    }
}
