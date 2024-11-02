using GuardFood.Core.Entities;
using GuardFood.Core.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GuardFood.Core.Context
{
    public class GFContext : IdentityDbContext<Usuario>
    {
        public GFContext(DbContextOptions<GFContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Restaurante> Restaurantes { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoProduto> PedidoProdutos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ProdutoCategoria> ProdutoCategorias { get; set; }
        public DbSet<Midia> Midias { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Ignorar colunas autoincrement
            // Pega todas as classes do sistema
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                // Busca todas as propriedades com a configurao DatabaseGeneratedOption.Identity
                // Configura todas as propriedades para serem ignoradas
                foreach (var property in entity.GetProperties())
                {
                    if (property.PropertyInfo != null && Attribute.IsDefined(property.PropertyInfo, typeof(DatabaseGeneratedAttribute)))
                    {
                        var databaseGeneratedAttribute = property.PropertyInfo.GetCustomAttribute<DatabaseGeneratedAttribute>();

                        if (databaseGeneratedAttribute.DatabaseGeneratedOption == DatabaseGeneratedOption.Identity)
                        {
                            // Configura a propriedade para ser ignorada
                            property.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
                        }
                    }
                }
            }
            #endregion

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
