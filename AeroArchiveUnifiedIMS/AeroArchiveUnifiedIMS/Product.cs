using SQLite;
using System;

namespace AeroArchiveUnifiedIMS
{
    [Table("Product")]
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [Unique]
        public String ProductCategory { get; set; }
        
        public String ProductID { get; set; }
        
        public int WarrantyStatus { get; set; }
    }
}
