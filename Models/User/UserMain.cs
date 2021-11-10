using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnicdaPlatform.Models.User
{
    public class UserMain
    {
        public bool userExist { get; set; }
        public string error { get; set; }
        public User user { get; set; }
        public UserGroup userGroup { get; set; }
        //public Permission permission { get; set; }
        public List<GroupPermission> groupPermission { get; set; }
    }
}
