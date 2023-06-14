using SQLite;
using System;

namespace AeroArchiveUnifiedIMS
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]

        public int ID { get; set; }
        
        public String productID { get; set; }
        
        public int warrantyStatus { get; set; }
    }
}
