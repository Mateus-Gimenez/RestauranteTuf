using GuardFood.Core.Entities;
using GuardFood.Core.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Context
{
    public class GFContext : IdentityDbContext<Usuario>
    {
        public GFContext(DbContextOptions<GFContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Restaurante> Restaurantes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<RestauranteProduto> RestauranteProdutos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Identity
            base.OnModelCreating(builder);
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                // Replace table names
                if (entity.GetTableName() == "AspNetUsers")
                {
                    entity.SetTableName("Usuario");
                }
                if (entity.GetTableName() == "AspNetUserClaims")
                {
                    entity.SetTableName("UsuarioAcesso");
                }
                if (entity.GetTableName() == "AspNetRoleClaims")
                {
                    entity.SetTableName("FuncaoAcesso");
                }
                if (entity.GetTableName() == "AspNetUserLogins")
                {
                    entity.SetTableName("UsuarioLogin");
                }
                if (entity.GetTableName() == "AspNetRoles")
                {
                    entity.SetTableName("Funcao");
                }
                if (entity.GetTableName() == "AspNetUserRoles")
                {
                    entity.SetTableName("UsuarioFuncao");
                }
                if (entity.GetTableName() == "AspNetUserTokens")
                {
                    entity.SetTableName("UsuarioToken");
                }
            }

            #endregion
        }
    }
}
