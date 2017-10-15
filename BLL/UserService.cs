using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserService
    {
        public string GetUser()
        {
            return JsonConvert.SerializeObject(new { id = 1, name = "wucong" });
        }
    }
}
