namespace OptionsWebSite.Migrations.DiplomaMigrations
{
    using DiplomaDataModel.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DiplomaDataModel.Models.DiplomaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\DiplomaMigrations";
        }
        private List<Option> getOption() {
            List<Option> options = new List<Option>()
            {
                new Option { Title = "Data Communications", IsActive = true },
                new Option { Title = "Client Server", IsActive = true },
                new Option { Title = "Digital Processing", IsActive = true },
                new Option { Title = "Information Systems", IsActive = true },
                new Option { Title = "Database", IsActive = false },
                new Option { Title = "Web & Mobile", IsActive = true },
                new Option { Title = "Tech Pro", IsActive = false }
            };
            return options;
        }
        private List<YearTerm> getTerms()
        {
            List<YearTerm> terms = new List<YearTerm>()
            {
                new YearTerm { Year = 2015, Term = 20, IsDefault = false },
                new YearTerm { Year = 2015, Term = 30, IsDefault = false },
                new YearTerm { Year = 2016, Term = 10, IsDefault = false },
                new YearTerm { Year = 2016, Term = 30, IsDefault = true }
            };
            return terms;
        }
        protected override void Seed(DiplomaDataModel.Models.DiplomaContext context)
        {
            //  This method will be called after migrating to the latest version.

            //Add or Update Options
            context.Options.AddOrUpdate(o => o.Title, getOption().ToArray());
            context.SaveChanges();

            //Add or Update terms
            context.YearTerms.AddOrUpdate(t => new { t.Year, t.Term }, getTerms().ToArray());
            context.SaveChanges();

        }
    }
}
