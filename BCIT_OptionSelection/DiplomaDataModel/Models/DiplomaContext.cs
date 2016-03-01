using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel.Models
{
    public class DiplomaContext : DbContext
    {
        public DiplomaContext() 
            : base("name = DefaultConnection")
        {           
        }

        public DbSet<YearTerm> YearTerms { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Choice> Choices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
