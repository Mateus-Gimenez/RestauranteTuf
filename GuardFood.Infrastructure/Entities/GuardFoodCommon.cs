using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuardFood.Core.Entities
{
    public class GuardFoodCommon
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool Ativo { get; set; } = true;
        public DateTime Inclusao { get; set; } = DateTime.Now;
        public DateTime Alteracao { get; set; } = DateTime.Now;

        [ForeignKey("Restaurante")]
        public Guid RestauranteId { get; set; }
        public virtual Restaurante Restaurante { get; set; }
    }
}
