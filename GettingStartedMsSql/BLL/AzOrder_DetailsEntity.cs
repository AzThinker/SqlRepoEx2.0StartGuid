using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SqlRepoEx.Core.CustomAttribute;

// 订单明细 业务类
namespace DemoTools.BLL.DemoNorthwind
{
    [Table("Order Details")]
    /// <summary>
    /// 订单明细 业务类
    /// </summary>
    public sealed class AzOrder_Details
    {
        [Key]
        public int OrderID { get; set; }
        public int ProductID { get; set; }

        [NotMapped]
        public string ProductName { get; set; }

        [NotMapped]
        public int OrderCount { get; set; }

        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }

        [NotMapped]
        public int QuantityAvg { get; set; }
        public float Discount { get; set; }

    }
}