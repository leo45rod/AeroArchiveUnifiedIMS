using SQLite;
using System;

namespace AeroArchiveUnifiedIMS
{
    public class Employee
    {
        [PrimaryKey, AutoIncrement]
        
        public int ID { get; set; }
        
        public String employeeName { get; set; }
        
        public String role { get; set; }
        
        public int employeeID { get; set; }

        public String accessPass { get; set; }
    }
}
