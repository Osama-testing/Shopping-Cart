using Shopping_Cart_in_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Shopping_Cart_in_MVC.Controllers
{
    public class AddToCartController : Controller
    {
        public ActionResult AddCart(int productid,int qty)
        {
            ViewBag.productid = productid;
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("sno");
            dt.Columns.Add("productid");
            dt.Columns.Add("productname");
            dt.Columns.Add("price");
            dt.Columns.Add("qty");

            if (Session["Buyitems"] == null)
                    {
                        dr = dt.NewRow();
                        String mycon = "Data Source=172.16.14.150;Initial Catalog=Osama_Testing;Persist Security Info=True;User ID=qaserver;Password=apple123!@#";
                        SqlConnection scon = new SqlConnection(mycon);
                        String myquery = "select * from ProductTable where ProductID=" + productid;
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = myquery;
                        cmd.Connection = scon;
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        dr["sno"] = 1;
                        dr["ProductID"] = ds.Tables[0].Rows[0]["ProductID"].ToString();
                        dr["ProductName"] = ds.Tables[0].Rows[0]["ProductName"].ToString();
                        int i2 = Convert.ToInt32(ds.Tables[0].Rows[0]["Price"]);
                        int SumOfPrice = i2 * qty;
                        dr["Price"] = SumOfPrice;
                        dr["qty"] = qty;
                         dt.Rows.Add(dr);                    
                        Session["buyitems"] = dt;
                        ViewBag.cartnumber = dt.Rows.Count.ToString();
                

            }
            else
                    {

                        dt = (DataTable)Session["buyitems"];
                        int sr;
                        sr = dt.Rows.Count;
                        dr = dt.NewRow();
                        String mycon = "Data Source=172.16.14.150;Initial Catalog=Osama_Testing;Persist Security Info=True;User ID=qaserver;Password=apple123!@#";
                        SqlConnection scon = new SqlConnection(mycon);
                        String myquery = "select * from ProductTable where ProductID=" + productid;
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = myquery;
                        cmd.Connection = scon;
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        dr["sno"] = sr + 1;
                        dr["ProductID"] = ds.Tables[0].Rows[0]["ProductID"].ToString();
                        dr["ProductName"] = ds.Tables[0].Rows[0]["ProductName"].ToString();
                        int i2 = Convert.ToInt32(ds.Tables[0].Rows[0]["Price"]);
                         int SumOfPrice = i2 * qty;
                        dr["Price"] = SumOfPrice;
                       dr["qty"] = qty;

                         dt.Rows.Add(dr);                       
                        Session["buyitems"] = dt;


                    }
            return Content("<img src='https://img.icons8.com/ios/30/4a90e2/shopping-cart.png' /> <b>(" + dt.Rows.Count.ToString()+ ")</b>");

        }

      
    }
}