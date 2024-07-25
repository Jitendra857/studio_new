using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soc_Management_Web.Models
{
	public class Webcategory
	{
		public long Id { get; set; }
		public long Pid { get; set; }
		public string Catname { get; set; }
		public List<SelectListItem> parentCat { get; set; }
		public string filename { get; set; }
		public string Description { get; set; }
		public string VideoURL { get; set; }

		public string path { get; set; }
		public IFormFile file { get; set; }
	}

	public class Webcategorydetails
	{
		public long Id { get; set; }
		public long catid { get; set; }
		public long Subcatid { get; set; }
		public string Catname { get; set; }
		public string tittle { get; set; }
		public string videopath { get; set; }
		public string filename { get; set; }
		public string description { get; set; }
		public string iamgepath { get; set; }
		public List<SelectListItem> Caltlst { get; set; }
		public List<SelectListItem> subCaltlst { get; set; }

		public IFormFile? file { get; set; }
		public List<IFormFile> bannerfile { get; set; }
		public string Filepath { get; internal set; }
	}
	public class MenuBanner
	{
		public long Id { get; set; }
		public int MenuType { get; set; }
		public IFormFile file { get; set; }
		List<string> lstMenu = new List<string>();
	}
}
