//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DemoTY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Cart
    {
        [Key]
        public int CartId { get; set; }
        [ForeignKey("ProductId")]
        public Nullable<int> ProductId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string FreeDelivery { get; set; }
        public Nullable<decimal> Price { get; set; }
    
        public virtual MenuItem MenuItem { get; set; }
    }
}