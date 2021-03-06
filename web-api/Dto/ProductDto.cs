﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web.api.Dto
{
    public class ProductDto
    {
        public ProductDto()
        {
            Materials = new List<Material>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public ICollection<Material> Materials { get; set; }

        public int MaterialCount => Materials.Count;
    }

    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ProductCreation
    {
        [Display(Name = "产品名称")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "{0}的长度应该不小于{2}, 不大于{1}")]
        public string Name { get; set; }

        [Display(Name = "价格")]
        [Range(0, Double.MaxValue, ErrorMessage = "{0}的值必须大于{1}")]
        public float Price { get; set; }
    }


    public class ProductModification
    {
        [Display(Name = "产品名称")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "{0}的长度应该不小于{2}, 不大于{1}")]
        public string Name { get; set; }

        [Display(Name = "价格")]
        [Range(0, Double.MaxValue, ErrorMessage = "{0}的值必须大于{1}")]
        public float Price { get; set; }
        
    }

    

}
