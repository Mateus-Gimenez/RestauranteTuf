﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Entities
{
    [Table("ProdutoCategoria")]
    public class ProdutoCategoria : GuardFoodCommon
    {
        public string Nome { get; set; }
    }
}
