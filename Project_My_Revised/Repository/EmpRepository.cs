using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Project_My_Revised.Models;
using System.Data;
using MySql.Data.MySqlClient;

namespace Project_My_Revised.Repository
{
    public class EmpRepository
    {
        private MySqlConnection con;

        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["getConn"].ToString();
            con = new MySqlConnection(constr); 
        }

        public bool AddEmployee(EmpMod obj)
        {
            connection();
            MySqlCommand com = new MySqlCommand("AddNewEmpDetails", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("E_Emp_ID", obj.EmpId);
            com.Parameters.AddWithValue("E_Emp_Name", obj.Name);
            com.Parameters.AddWithValue("E_Address", obj.Address);
            com.Parameters.AddWithValue("E_Contact", obj.Contact_number);
            com.Parameters.AddWithValue("E_PAN", obj.Pan);
            com.Parameters.AddWithValue("E_Start_Date", obj.Start_date);
            com.Parameters.AddWithValue("E_End_Date", obj.End_date);
            com.Parameters.AddWithValue("E_Date_Of_Birth", obj.Date_of_birth);
            com.Parameters.AddWithValue("E_Salary", obj.Salary);
            com.Parameters.AddWithValue("E_Emp_Status", obj.Status);
            com.Parameters.AddWithValue("E_Last_Updated", obj.Last_Updated);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<EmpMod> GetAllEmployees()
        {
            connection();
            List<EmpMod> EmpList = new List<EmpMod>();

            MySqlCommand com = new MySqlCommand("GetEmployees", con);
            com.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter da = new MySqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                EmpList.Add(
                    new EmpMod
                    {
                        EmpId = Convert.ToInt32(dr["EmpId"]),
                        Name = Convert.ToString(dr["Emp_Name"]),
                        Address = Convert.ToString(dr["Address"]),
                        Contact_number = Convert.ToString(dr["Contact"]),
                        Pan = Convert.ToString(dr["PAN"]),
                        Start_date = Convert.ToDateTime(dr["Start_Date"]),
                        End_date = Convert.ToDateTime(dr["End_Date"]),
                        Date_of_birth = Convert.ToDateTime(dr["Date_of_Birth"]),
                        Salary = Convert.ToInt32(dr["Salary"]),
                        Status = Convert.ToBoolean(dr["Emp_Status"]),
                        Last_Updated = Convert.ToDateTime(dr["Last_Updated"])
                        
                    }
                );
            }

            return EmpList;
        }

        public bool UpdateEmployees(EmpMod obj, int id)
        {
            connection();
            MySqlCommand com = new MySqlCommand("UpdateEmpDetails", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("E_Emp_ID",id );
            com.Parameters.AddWithValue("E_Emp_Name", obj.Name);
            com.Parameters.AddWithValue("E_Address", obj.Address);
            com.Parameters.AddWithValue("E_Contact", obj.Contact_number);
            com.Parameters.AddWithValue("E_PAN", obj.Pan);
            com.Parameters.AddWithValue("E_Start_Date", obj.Start_date);
            com.Parameters.AddWithValue("E_End_Date", obj.End_date);
            com.Parameters.AddWithValue("E_Date_Of_Birth", obj.Date_of_birth);
            com.Parameters.AddWithValue("E_Salary", obj.Salary);
            com.Parameters.AddWithValue("E_Emp_Status", obj.Status);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;   
            }
        }

        public bool DeleteEmployees(int id)
        {
            connection();
            MySqlCommand com = new MySqlCommand("DeleteEmpById", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("E_Emp_ID", id);

            con.Open(); 
            int i= com.ExecuteNonQuery();
            con.Close();
            if(i>=1)
            {   
                return true;
            }
            else
            {
                return false;   
            }
        }

    }
}