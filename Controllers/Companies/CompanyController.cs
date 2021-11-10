using System.Collections.Generic;
using UnicdaPlatform.Controllers.Inteface;
using UnicdaPlatform.Data;
using System;
using System.Linq;
using UnicdaPlatform.Models.Company;

namespace UnicdaPlatform.Controllers.Companies
{
    public class CompanyController : MainIntefaces
    {
        public bool Delete(ApplicationDbContext context, int Id)
        {
            try
            {
                if (Id != 0)
                {
                    var data = (Company)Get(context, Id);
                    data.Deleted = true;
                    context.Company.Update(data);
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
                return context.Company.Any(a => a.Id == Id);
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
                var trans = context.Company.Where(a => a.Id == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new Company();
            }
        }
        public object Get(ApplicationDbContext context, string Id)
        {
            try
            {
                if (string.IsNullOrEmpty(Id))
                    return context.Company.FirstOrDefault();
                else
                    return context.Company.Where(a => a.CompanyId == Id).First();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new Company();
            }
        }
        public List<Company> GetList(ApplicationDbContext context)
        {
             try
            {
                return context.Company.ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<Company>();
            }
        }

        public int Save(ApplicationDbContext context, object data)
        {
            try
            {
                var dataSave = (Company)data;
                if (dataSave.Id == 0)
                    context.Company.Add(dataSave);
                else
                    context.Company.Update(dataSave);
                
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
