using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SqlRepoEx.Core.CustomAttribute;

// 分类 业务类
namespace DemoTools.BLL.DemoNorthwind
{
    [Table("Categories")]
 
    /// <summary>
    /// 分类 业务类
    /// </summary>
    public sealed class AzCategories
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

    }
}