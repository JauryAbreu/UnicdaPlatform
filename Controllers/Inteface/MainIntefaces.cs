using UnicdaPlatform.Data;
using System.Collections.Generic;
using System;

namespace UnicdaPlatform.Controllers.Inteface
{
    public interface MainIntefaces
    {
        public object Get(ApplicationDbContext context, int Id);
        //public List<object> GetList(ApplicationDbContext context);
        public bool Exist(ApplicationDbContext context, int Id);
        public int Save(ApplicationDbContext context, object data);
        public bool Delete(ApplicationDbContext context, int Id);
    }
}
