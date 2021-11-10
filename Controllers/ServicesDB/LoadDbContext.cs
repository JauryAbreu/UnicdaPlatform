using UnicdaPlatform.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnicdaPlatform.Controllers.ServicesDB
{
    public class LoadDbContext :ILoadDbContext
    {
        public ApplicationDbContext _context;
        public LoadDbContext(ApplicationDbContext context) 
        {
            _context = context;
        }

        public ApplicationDbContext GetConnection()
        {
            return _context;
        }
    }
}
