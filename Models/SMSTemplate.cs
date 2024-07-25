using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soc_Management_Web.Models
{
	public class SMSTemplate
	{
		public int sl { get; set; }
		public string customer { get; set; }
		public string mobile { get; set; }
		public string category { get; set; }
		public long Id { get; set; }
		public string types { get; set; }
	}
}
