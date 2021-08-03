using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopping_Cart_in_MVC.Controllers
{
    public class ShowCartController : Controller
    {
        // GET: ShowCart
        public ActionResult Index()
        {

            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["buyitems"];
            if (dt1 != null)
            {

                ViewBag.cartnumber = dt1.Rows.Count.ToString();
               // DataTable table = dataSet.Tables["YourTableName"];

                // Declare an object variable.
                var result = dt1.AsEnumerable().Sum(x => Convert.ToInt32(x["Price"]));

                ViewBag.AllPrices = result;
                //int sum = Convert.ToInt32(dt1.Compute("SUM(price)", string.Empty));


            }
            else
            {
                ViewBag.cartnumber = "0";
            }
            return View(dt1);
        }
        public ActionResult RemoveProduct(string sno)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["buyitems"];
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                int sr;
                int sr1;
                sr1 = Convert.ToInt32(sno);
                sr = Convert.ToInt32(dt.Rows[i]["sno"].ToString());


                if (sr == sr1)
                {
                    dt.Rows[i].Delete();
                    dt.AcceptChanges();
                    TempData["msg"] = "Selected Product Has Been Removed";
                    //Label1.Text = "Item Has Been Deleted From Shopping Cart";
                    break;

                }
            }

            for (int i = 1; i <= dt.Rows.Count; i++)
            {
                dt.Rows[i - 1]["sno"] = i;
                dt.AcceptChanges();
            }

            Session["buyitems"] = dt;
            return RedirectToAction("Index", "ShowCart");
        }

    }
}