using System;
using System.Collections.Generic;
using System.Linq;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.CarrierSubjects;

namespace UnicdaPlatform.Controllers.CarrierSubject
{
    public class CarrierController
    {
        public bool Exist(ApplicationDbContext context, int Id)
        {
            try
            {
                return context.Carrier.Any(a => a.Id == Id);
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
                var trans = context.Carrier.Where(a => a.Id == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new Carrier();
            }
        }

        public object Get(ApplicationDbContext context, string CareerId)
        {
            try
            {
                var trans = context.Carrier.Where(a => a.CareerId == CareerId).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new Carrier();
            }
        }

        public List<Carrier> GetList(ApplicationDbContext context)
        {
            try
            {
                return context.Carrier.ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<Carrier>();
            }
        }

        public int Save(ApplicationDbContext context, object data)
        {
            try
            {
                var dataSave = (Carrier)data;
                if (dataSave.Id == 0)
                    context.Carrier.Add(dataSave);
                else
                    context.Carrier.Update(dataSave);

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
