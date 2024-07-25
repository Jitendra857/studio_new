using Microsoft.Extensions.Hosting;
using PIOAccount.Classes;
using Soc_Management_Web.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Soc_Management_Web.Common
{
    public class MyBackgroundService : BackgroundService
	{
		public MyBackgroundService()
		{

		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				SendTimelyNotification();
				await Task.Delay(TimeSpan.FromMinutes(180), stoppingToken); // Wait for 5 minutes
			}
			
		}

      
        public void SendTimelyNotification()
		{
			DbConnection ObjDBConnection = new DbConnection();
			SqlParameter[] sqlParameters = new SqlParameter[6];
			sqlParameters[0] = new SqlParameter("@id", 0);
			sqlParameters[1] = new SqlParameter("@tosend", "");
			sqlParameters[2] = new SqlParameter("@self", 0);
			sqlParameters[3] = new SqlParameter("@types", "");
			sqlParameters[4] = new SqlParameter("@data", "");
			sqlParameters[5] = new SqlParameter("@mode", 2);
			DataTable DtEmp = ObjDBConnection.CallStoreProcedure("Sendmessagesave", sqlParameters);
			WatsappNotification watsapp = new WatsappNotification();
			if (DtEmp.Rows.Count > 0)
			{
				for (int i = 0; i < DtEmp.Rows.Count; i++)
				{
					var datas = watsapp.SendWhatAppMessage(DtEmp.Rows[i]["AccMob"].ToString(), DtEmp.Rows[i]["Message"].ToString(), "", "");
				}

			}


		}


	}
}
