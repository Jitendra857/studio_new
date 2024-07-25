using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Soc_Management_Web.Classes
{
    public  class WatsappNotification
    {
		//sMobile=send to,sMessage=Message,filnm=fileurl,pdffilenm=filename
		public  WhatAppAPIResponse SendWhatAppMessage(string sMobile, string sMessage, string filnm, string pdffilenm)
		{
			try
			{
				WhatAppAPIResponse apiResponse = new WhatAppAPIResponse();
				if (sMobile.Length == 10)
				{
					sMobile = "91" + sMobile;
					string result = "";
					WebRequest request = null;
					HttpWebResponse response = null;

					try
					{
						string resultMsg = "";
						string whatid = "", instantid = "", tokenid = "", whatsapi = "", url = "";
						WebRequest requestFile = null;
						HttpWebResponse responseFile = null;
						//if (filnm != "")
						//{
							whatid = "CLNEXT";
							//instantid = "660CF2D261E1B";
							//tokenid = "660cf1bf4819e";
							//whatsapi = "https://wa-smart.cloud/api/send?";

						instantid = "665080D5CE42F";
						tokenid = "660cf1bf4819e";
						whatsapi = "https://smartappcloud.in/api/send?";
						url = "http://broker.pioerp.com/PDF/";
					
						  //with file
						  //string sFileAPI = whatsapi + "number=" + sMobile + "&type=media&message=" + sMessage + "&media_url=" + url + pdffilenm + "&filename=" + pdffilenm + "&instance_id=" + instantid + "&access_token=" + tokenid;

						//without file
						string sFileAPI = whatsapi + "number=" + sMobile + "&type=text&message=" + sMessage + "&instance_id=" + instantid + "&access_token=" + tokenid + "";

							requestFile = WebRequest.Create(sFileAPI);
							

							responseFile = (HttpWebResponse)requestFile.GetResponse();
							Stream streamF = responseFile.GetResponseStream();
							Encoding ecF = System.Text.Encoding.GetEncoding("utf-8");
							StreamReader readerF = new System.IO.StreamReader(streamF, ecF);
							resultMsg = readerF.ReadToEnd();
							readerF.Close();
							streamF.Close();
							if (!string.IsNullOrEmpty(resultMsg))
							{
								if (resultMsg.Contains("success"))
								{
									apiResponse.status = "success";
									apiResponse.message = "WhatsApp send successfully";
								}
								//apiResponse = JsonConvert.DeserializeObject<WhatAppAPIResponse>(resultMsg);
								return apiResponse;
							}
						//}
						//return Json(new { result = true, maxVno = 1 });
						apiResponse.status = "success";
						apiResponse.message = "WhatsApp send successfully";
						return apiResponse;
					}
					catch (Exception exp)
					{
						apiResponse.status = "error";
						apiResponse.message = exp.ToString();
						return apiResponse;
					}
				}
				apiResponse.status = "error";
				apiResponse.message = "Please enter 10 digit number";
				return apiResponse;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
