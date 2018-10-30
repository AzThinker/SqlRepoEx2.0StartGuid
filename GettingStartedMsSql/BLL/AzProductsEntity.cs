using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SqlRepoEx.Core.CustomAttribute;

// 产品 业务类
namespace DemoTools.BLL.DemoNorthwind
{
    [Table("Products")]
    /// <summary>
    /// 产品 业务类
    /// </summary>
    public sealed class AzProducts
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       // [IdentityField]
        [Key]
       // [KeyField]
        public int ProductID { get; set; }

        [Column("ProductName")]
        public string ProductName2 { get; set; }
        public int? SupplierID { get; set; }

        [NotMapped]
        //[NonDatabaseField]
        [Column("CompanyName")]
        public string Supplier { get; set; }
        public int? CategoryID { get; set; }
        [NotMapped]
        public string CategoryName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

    }
}