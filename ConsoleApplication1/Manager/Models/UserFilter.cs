using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Manager.Models
{
    public class UserFilter:EasyRepo.IFilter
    {
        public string Id { get; set; }
        public string Nominativo { get; set; }
        

    }
}
