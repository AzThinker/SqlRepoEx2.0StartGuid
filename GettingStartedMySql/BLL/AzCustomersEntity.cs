using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SqlRepoEx.Core.CustomAttribute;

// 客户 业务类
namespace DemoTools.BLL.DemoNorthwind
{
    [Table("Customers")]
    /// <summary>
    /// 客户 业务类
    /// </summary>
    public sealed class AzCustomers
    {
        [Key]
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

    }
}