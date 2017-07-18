using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_My_Revised.Repository;
using Project_My_Revised.Models;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using ClosedXML.Excel;
using System.IO;

namespace Project_My_Revised.Controllers
{
    public class EmployeesController : Controller
    {
        //
        // GET: /Employees/

    /*    public ActionResult Index()
        {
            return View();
        }
     */
        public ActionResult GetAllEmpDetails()
        {
            EmpRepository EmpRepo = new EmpRepository();
            ModelState.Clear();
            return View(EmpRepo.GetAllEmployees());
        }
        public ActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee(EmpMod Emp)
        {
            try
            {
               
                    EmpRepository EmpRepo = new EmpRepository();
                    if (EmpRepo.AddEmployee(Emp))
                    {
                        ViewBag.Message = "Employee details added Successfully";
                     
                    }
                

                return RedirectToAction("GetAllEmpDetails");
            }
            catch
            {
                return View();                
            }
        }

        public ActionResult EditEmpDetails(int id)
        {
            EmpRepository EmpRepo = new EmpRepository();

            return View(EmpRepo.GetAllEmployees().Find(Emp => Emp.EmpId == id));
        }

        [HttpPost]
        public ActionResult EditEmpDetails(int id, EmpMod obj)
        {
            try
            {
                EmpMod emp=new EmpMod();
                EmpRepository EmpRepo = new EmpRepository();
                EmpRepo.UpdateEmployees(obj,id);

                return RedirectToAction("GetAllEmpDetails");
            }
            catch {
                return View();
            }

        }

        public ActionResult DeleteEmp(int id)
        {
            try
            {
                EmpRepository EmpRepo = new EmpRepository();
                if (EmpRepo.DeleteEmployees(id))
                {
                    ViewBag.AlertMsg = "Employee Details Deleted Successfully";
                    ViewBag.message.cssClass = "alert alert-danger";
                }

                return RedirectToAction("GetAllEmpDetails");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult ExportCSV()
        {
            String constring = ConfigurationManager.ConnectionStrings["getConn"].ConnectionString;
            MySqlConnection con = new MySqlConnection(constring);
            string query = "SELECT  EmpId, Emp_Name, Salary , Start_date , End_date , CASE WHEN (Employees.End_Date is null)  THEN DATEDIFF(DATE_ADD(Start_Date, INTERVAL 30 DAY), Start_Date) * Salary/30 ELSE DATEDIFF(End_Date, Start_Date) * Salary/30  END AS Total_Salary From Employees group by EmpId ;";
            DataTable dt = new DataTable();
            dt.TableName = "Employee";
            con.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(query, con);
            da.Fill(dt);
            con.Close();

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= EmployeeReport.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return RedirectToAction("GetAllEmpDetails");
        }  
    }
}
