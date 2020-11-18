using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ControleEstoque.Web.Models
{
    public class CidadeMap : EntityTypeConfiguration<CidadeModel> //classe base de mapeamento 
    {
        public CidadeMap()
        {
            ToTable("cidade"); //nome da tabela

            HasKey(x => x.Id); //primary key
            //hascloumn eu passo o nome do campo no banco
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Nome).HasColumnName("nome").HasMaxLength(30).IsRequired();
            Property(x => x.Ativo).HasColumnName("ativo").IsRequired();

            Property(x => x.IdEstado).HasColumnName("id_estado").IsRequired();
            //associação com tabela estado
            HasRequired(x => x.Estado).WithMany().HasForeignKey(x => x.IdEstado).WillCascadeOnDelete(false);
        }
    }
}