﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Entities
{
    [Table("Produto")]
    public class Produto : GuardFoodCommon
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }
        
        public decimal Valor { get; set; }

        [ForeignKey("ProdutoCategoria")]
        public Guid ProdutoCategoriaId { get; set; }
        public virtual ProdutoCategoria ProdutoCategoria { get; set; }

        [NotMapped]
        public string ValorMonetario
        {
            get
            {
                return Valor.ToString("C", new System.Globalization.CultureInfo("pt-BR"));
            }
        }
    }
}
