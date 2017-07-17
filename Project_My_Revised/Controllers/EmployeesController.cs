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
            string constr = ConfigurationManager.ConnectionStrings["getConn"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT  EmpId, Emp_Name, Salary , Start_date , End_date , CASE WHEN (Employees.End_Date is null)  THEN DATEDIFF(DATE_ADD(Start_Date, INTERVAL 30 DAY), Start_Date) * Salary/30 ELSE DATEDIFF(End_Date, Start_Date) * Salary/30  END AS Total_Salary From Employees group by EmpId ;"))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt); 

                            //Build the CSV file data as a Comma separated string.
                            string csv = string.Empty;

                            foreach (DataColumn column in dt.Columns)
                            {
                                //Add the Header row for CSV file.
                                csv += column.ColumnName + ',';
                            }

                            //Add new line.
                            csv += "\r\n";

                            foreach (DataRow row in dt.Rows)
                            {
                                foreach (DataColumn column in dt.Columns)
                                {
                                    //Add the Data rows.
                                    csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                                }

                                //Add new line.
                                csv += "\r\n";
                            }

                            //Download the CSV file.
                            Response.Clear();
                            Response.Buffer = true;
                            Response.AddHeader("content-disposition", "attachment;filename=SqlExport.csv");
                            Response.Charset = "";
                            Response.ContentType = "application/text";

                            Response.Output.Write(csv);
                            Response.Flush();
                            Response.End();
                            return RedirectToAction("GetAllEmpDetails");
                            
                        }
                    }
                }
            }
        }
    }
}
