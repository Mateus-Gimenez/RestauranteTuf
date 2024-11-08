using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Entities
{
    [Table("Mesa")]
    public class Mesa : GuardFoodCommon
    {
        [MaxLength(5)]
        public string Codigo { get; set; }

        [NotMapped]
        public string? Base64QrCode { get; set; }
    }
}