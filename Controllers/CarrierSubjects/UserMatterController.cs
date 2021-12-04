using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UnicdaPlatform.Data;
using UnicdaPlatform.Models.CareerSubjects;

namespace UnicdaPlatform.Controllers.CareerSubject
{
    public class UserMatterController
    {
        public bool Exist(ApplicationDbContext context, int Id)
        {
            try
            {
                return context.UserMatter.Any(a => a.Id == Id);
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
                var trans = context.UserMatter.Where(a => a.Id == Id).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new UserMatter();
            }
        }

        public object Get(ApplicationDbContext context, string CareerId, int CareerPensumId)
        {
            try
            {
                var trans = context.UserMatter.Where(a => a.UserId == CareerId && a.CareerPensumId == CareerPensumId).First();

                return trans;
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new UserMatter();
            }
        }

        public List<UserMatter> GetList(ApplicationDbContext context)
        {
            try
            {
                return context.UserMatter.ToList();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return new List<UserMatter>();
            }
        }

        public int Save(ApplicationDbContext context, object data)
        {
            try
            {
                var dataSave = (UserMatter)data;
                if (dataSave.Id == 0)
                    context.UserMatter.Add(dataSave);
                else
                    context.UserMatter.Update(dataSave);

                return context.SaveChanges();
            }
            catch (Exception ex)
            {
                TXTSaveLog.WriteToTxtFile(ex.Message);
                return 0;
            }
        }

        public List<MatterInProgress> GetMatterInProgress(ApplicationDbContext context, string userId, int status)
        {
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UserId", userId));
                parameters.Add(new SqlParameter("@status", status));

               List<MatterInProgress> lines = context.MatterInProgress.FromSqlRaw("usp_MatterInProgress @UserId, @status", parameters.ToArray()).ToList();

                return lines;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<MatterInProgress>();
            }
        }

        public List<MatterToRemove> GetAvailableMatterToRemove(ApplicationDbContext context, string userId)
        {
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UserId", userId));

                List<MatterToRemove> lines = context.MatterToRemove.FromSqlRaw("usp_AvailableMatterToRemove @UserId", parameters.ToArray()).ToList();

                return lines;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<MatterToRemove>();
            }
        }

        public int GetCurrentPeriod()
        {
            if (DateTime.Now.Month >= 1 && DateTime.Now.Month <= 4) 
            {
                return 1;
            }
            else if (DateTime.Now.Month >= 5 && DateTime.Now.Month <= 8)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
    }
}
