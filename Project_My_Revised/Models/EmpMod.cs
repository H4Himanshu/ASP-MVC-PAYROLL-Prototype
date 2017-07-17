using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

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
        
        [Required]    
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Contact_number { get; set; }
        [Required]
        public string Pan { get; set; }
        [Required]
        public DateTime Start_date { get; set; }
        public DateTime? End_date { get; set; }
        [Required]
        public DateTime Date_of_birth { get; set; }
        [Required]
        public int Salary { get; set; }
        public bool Status { get; set; }
        public DateTime Last_Updated { get; set; }

        public EmpMod()
        {
             DateTime? End_date=null;
        }

    }

}