using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicdaPlatform.Controllers.Inteface;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.Fiscal;

namespace UnicdaPlatform.Controllers.Fiscal
{
    public class NcfSequenceDetailController : MainIntefaces
    {
        public bool Delete(ApplicationDbContext context, int Id)
        {
            try
            {
                if (Id != 0)
                {
                    var data = (NcfSequenceDetail)Get(context, Id);
                    context.NcfSequenceDetail.Remove(data);
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
                return context.NcfSequenceDetail.Any(a => a.Id == Id);
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
                var trans = context.NcfSequenceDetail.Where(a => a.Id == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new NcfSequenceDetail();
            }
        }

        public object Get(ApplicationDbContext context, string Id)
        {
            try
            {
                var trans = context.NcfSequenceDetail.Where(a => a.MasterId == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new NcfSequenceDetail();
            }
        }

        public List<NcfSequenceDetail> GetList(ApplicationDbContext context, int Id)
        {
            try
            {
                return context.NcfSequenceDetail.Where(a=> a.NcfId == Id).ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<NcfSequenceDetail>();
            }
        }

        public int Save(ApplicationDbContext context, object data)
        {
            try
            {
                var dataSave = (NcfSequenceDetail)data;
                if (dataSave.Id == 0)
                    context.NcfSequenceDetail.Add(dataSave);
                else
                    context.NcfSequenceDetail.Update(dataSave);

                return context.SaveChanges();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return 0;
            }
        }

        public NcfSequenceDetail GetNcfStatus(ApplicationDbContext context, string companyId, int ncfId, int process) 
        {
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@_COMPANYID", companyId));
                parameters.Add(new SqlParameter("@_NCFID", ncfId));
                parameters.Add(new SqlParameter("@_PROCESS", process));

                var lines = context.NcfSequenceDetail.FromSqlRaw("usp_ApplyNCFToUse @_COMPANYID, @_NCFID, @_PROCESS", parameters.ToArray()).ToList();

                return lines.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new NcfSequenceDetail();
            }


        }

        public string ValidateNCFSequence(ApplicationDbContext context, string companyId, int ncfId)
        {
            try
            {
                NcfSequenceDetail nCF = GetNcfStatus(context, companyId, ncfId, 0);

                if (nCF.SeqStatus == 2)
                {
                    return "Ultima secuencia de NCF utilizada...";
                }
                else if (nCF.SeqStatus == 3)
                {
                    return "Fecha de secuencia  NCF vencida por rango de fecha...";
                }
                else if (string.IsNullOrEmpty(nCF.Serie))
                {
                    return "No tiene secuencia disponible de NCF...";
                }
                else if (nCF.SeqEnd < nCF.SeqNext)
                {
                    return "Ultimo NCF utilizado - Debe agregar una sequencia de NCF (" + nCF.DGIIDescription + "): \n"
                                        + nCF.SeqNext + " de " + nCF.SeqEnd + " - Caja. ";
                }
                else if (ValidateNCFInUse(nCF.SeqNext, nCF.SeqEnd))
                {
                    return "Debe agregar una sequencia de NCF - Sencuencias NCF disponibles ("
                                   + nCF.DGIIDescription + "): " + Math.Abs(nCF.SeqEnd - nCF.SeqNext) + " Disponibles. ";
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return "No tiene secuencia disponible de NCF... \r\n" + ex.Message;
            }
        }

        private bool ValidateNCFInUse(int InUse, int Top)
        {
            int StartAlet = 9;
            int IntervalAlert = 3;

            if (Top - StartAlet == InUse)
                return true;
            else if (Top - StartAlet < InUse)
            {
                int intervalo = StartAlet / IntervalAlert;

                for (int i = 0; i <= intervalo; i++)
                {
                    if ((i * IntervalAlert) == (Top - InUse))
                        return true;
                }
            }
            return false;
        }
    }
}
