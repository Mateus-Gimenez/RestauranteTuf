using System.ComponentModel.DataAnnotations;


namespace GuardFood.Core.Entities
{
    public class GuardFoodCommon
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool Ativo { get; set; } = true;
        public DateTime Inclusao { get; set; } = DateTime.Now;
        public DateTime Alteracao { get; set; } = DateTime.Now;
    }
}
