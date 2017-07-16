using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_My_Revised.Models
{
    public class EmpMod
    {
        public static int Empid()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            int numIterations = 0;
            numIterations = rand.Next(1, 1000);
            return numIterations;
        }


        public int EmpId = Empid();
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact_number { get; set; }
        public string Pan { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public DateTime Date_of_birth { get; set; }
        public int Salary { get; set; }
        public bool Status { get; set; }
        public DateTime Last_Updated { get; set; }
    }
}