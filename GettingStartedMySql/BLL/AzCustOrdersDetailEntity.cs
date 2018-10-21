using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SqlRepoEx.Core.CustomAttribute;

// AzCustOrdersDetail有返回结果存储过程业务类
namespace DemoTools.BLL.DemoNorthwind
{
    /// <summary>
    /// AzCustOrdersDetail 业务类
    /// </summary>

    [Table("custordersdetail")]
    public sealed class AzCustOrdersDetail
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public int? Discount { get; set; }
        public decimal? ExtendedPrice { get; set; }
        [Key]
        public int OrderID { get; set; }

    }
}