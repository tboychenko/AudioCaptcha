using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SoundTest.Controllers
{
    public class HomeController : Controller
    {
        Dictionary<int, string> audioDict = new Dictionary<int,string> {
            {0, "/media/Muse-Bliss.mp3"},
            {1, "/media/Libertango.mp3"}
        };
        Random rand = new Random();

        //
        // GET: /Home/
        public ActionResult Index()
        {
            int curNumber = rand.Next(0, 2);
            string audioPath = "";
            audioDict.TryGetValue(curNumber, out audioPath);
            @ViewBag.audioPath = audioPath;
            var data = Encoding.Unicode.GetBytes(curNumber.ToString());
            @ViewBag.numberHash = Encoding.Default.GetString(MD5.Create().ComputeHash(data));
            return View();
        }

        public ActionResult Check(string answer, string numberHash)
        {
            try {
                var data = Encoding.Unicode.GetBytes(answer);
                var ans = Encoding.Default.GetString(MD5.Create().ComputeHash(data));
                if (ans.Equals(numberHash)) {
                    return Json(new { success = true });
                }
            }
            catch { }
            return Json(new { success = false });
        }
	}
}