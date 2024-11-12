using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Entities
{
    [Table("Pedido")]
    public class Pedido : GuardFoodCommon
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Codigo { get; set; }

        public string NomeCliente { get; set; }

        public string Telefone { get; set; }

        [ForeignKey("Mesa")]
        public Guid MesaId { get; set; }
        public Mesa Mesa { get; set; }

        public StatusPedido Status { get; set; }

        public enum StatusPedido
        {
            [Description("Pedido Recebido na Cozinha")]
            Recebido = 0,
            [Description("Pedido sendo Preparado")]
            EmAndamento = 1,
            [Description("Pedido Concluído")]
            Concluido = 2,
            [Description("Pedido Cancelado")]
            Cancelado = 3,
            [Description("Pedido Entregue")]
            Entregue = 4,
        }

        [NotMapped]
        public string StatusDescricao
        {
            get
            {
                return Common.GetEnumDescription(Status);
            }
        }

        [NotMapped]
        public string CodigoFormatado
        {
            get
            {
                return Codigo.ToString("00000");
            }
        }

        [NotMapped]
        public string CorStatus
        {
            get
            {
                switch (Status)
                {
                    case StatusPedido.Recebido: return "var(--rz-base-700)";
                    case StatusPedido.EmAndamento: return "var(--rz-warning)";
                    case StatusPedido.Concluido: return "var(--rz-info)";
                    case StatusPedido.Cancelado: return "var(--rz-danger)";
                    case StatusPedido.Entregue: return "var(--rz-success)";
                    default: return "#ccc";
                }
            }
        }

        [NotMapped]
        public string TelefoneFormatado
        {
            get
            {
                try
                {
                    if(Telefone?.Length != 11)
                    {
                        return Telefone;
                    }

                    return Convert.ToUInt64(Telefone).ToString(@"(00) 00000-0000");
                }
                catch (Exception)
                {
                    return Telefone;
                }
            }
        }
    }
}