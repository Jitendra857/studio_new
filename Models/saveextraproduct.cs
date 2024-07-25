using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soc_Management_Web.Models
{
    public class saveextraproduct
    {
        public int orderid { get; set; }
        public int jobid { get; set; }
        public string pname { get; set; }
        public long qty { get; set; }
        public string remarks { get; set; }
    }
    public class saveextraproducthead
    {
        public long id { get; set; }
        public List<saveextraproduct>? produsct { get; set; }
    }
}
