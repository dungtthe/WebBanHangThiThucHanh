﻿using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanHang.Models
{

    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        [MinLength(5)]
        [DisplayName("Tên sản phẩm")]
        public string ?Name { get; set; }
        [Required]
        [DisplayName("Giá")]
        public int? Price { get; set; }

        public string ?Images {  get; set; }

        [Required]
        [DisplayName("Loại sản phẩm")]
        public int CategoryId {  get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category ?Category { get; set; }
    }
}