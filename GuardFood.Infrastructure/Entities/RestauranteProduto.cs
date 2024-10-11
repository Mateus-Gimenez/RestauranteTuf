using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Entities
{
    [Table("RestauranteProduto")]
    public class RestauranteProduto : GuardFoodCommon
    {
        [ForeignKey("Produto")]
        public Guid ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }

        [ForeignKey("Restaurante")]
        public Guid RestauranteId { get; set; }
        public virtual Restaurante Restaurante { get; set; }
    }
}
