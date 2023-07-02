using QLBH_WebManage.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBH_WebManage.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string id = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJVc2VySUQiOiI2ZTRlNGI1ZS1kNTJlLTQ3ZTMtODlmZC0yNzlkZDAwMWMwMzAiLCJqdGkiOiIwNTlhYjM0My02Nzc2LTQ4MTctOWZkZC05YzYxOTU4OGNiNzYiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiVXNlciIsIkFkbWluIl0sImV4cCI6MTY4ODI0MTcxMSwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo0MjAwIn0.QZuFSxGjz7p7q8Fpagu-dJQq9sdRsCaJ2wUN_CUC8Tg";
            ViewBag.ID = TokenDecode.GetID(id);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}