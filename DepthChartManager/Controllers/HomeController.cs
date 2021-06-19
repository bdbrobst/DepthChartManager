using DepthChartManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace DepthChartManager.Controllers
{
    public class HomeController : Controller
    {
        SqlCommand command = new SqlCommand();
        SqlDataReader dataReader;
        SqlConnection connection = new SqlConnection();

        List<PlayerInfo> rosterInfo = new List<PlayerInfo>();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            connection.ConnectionString = DepthChartManager.Properties.Resources.ConnectionString;
        }

        public IActionResult Index()
        {
            FetchData();
            return View(rosterInfo);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        private void FetchData()
        {
            if(rosterInfo.Count > 0)
            {
                rosterInfo.Clear();
            }
            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT TOP (5) [playerID],[cfbdapiID],[firstName],[lastName],[jerseyNumber],[position],[pos_rank],[height],[weight],[yearsExperience],[isActive] FROM [DepthChartManager].[dbo].[Player]";
                dataReader = command.ExecuteReader();
                while(dataReader.Read())
                {
                    Trace.WriteLine(dataReader["lastName"].ToString());
                    rosterInfo.Add(new PlayerInfo() { PlayerID = (int)dataReader["playerID"],
                                                      CfbdapiID = (int)dataReader["cfbdapiID"],
                                                      FirstName = dataReader["firstName"].ToString(),
                                                      LastName = dataReader["lastName"].ToString(),
                                                      JerseyNumber = (int)dataReader["jerseyNumber"],
                                                      Position = dataReader["position"].ToString(),
                                                      PosRank = dataReader["pos_rank"] as int? ?? default,
                                                      Height = (int)dataReader["height"],
                                                      Weight = (int)dataReader["weight"],
                                                      YearsExperience = dataReader["yearsExperience"] as int? ?? default,
                                                      IsActive = dataReader["isActive"] as int? ?? default
                    });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
