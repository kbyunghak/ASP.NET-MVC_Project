namespace OptionsWebSite.Migrations.DiplomaMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedNullOptions : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Choices", "YearTermId", "dbo.YearTerms");
            DropIndex("dbo.Choices", new[] { "FirstChoiceOptionId" });
            DropIndex("dbo.Choices", new[] { "SecondChoiceOptionId" });
            DropIndex("dbo.Choices", new[] { "ThirdChoiceOptionId" });
            DropIndex("dbo.Choices", new[] { "FourthChoiceOptionId" });
            AlterColumn("dbo.Choices", "FirstChoiceOptionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Choices", "SecondChoiceOptionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Choices", "ThirdChoiceOptionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Choices", "FourthChoiceOptionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Choices", "FirstChoiceOptionId");
            CreateIndex("dbo.Choices", "SecondChoiceOptionId");
            CreateIndex("dbo.Choices", "ThirdChoiceOptionId");
            CreateIndex("dbo.Choices", "FourthChoiceOptionId");
            AddForeignKey("dbo.Choices", "YearTermId", "dbo.YearTerms", "YearTermId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Choices", "YearTermId", "dbo.YearTerms");
            DropIndex("dbo.Choices", new[] { "FourthChoiceOptionId" });
            DropIndex("dbo.Choices", new[] { "ThirdChoiceOptionId" });
            DropIndex("dbo.Choices", new[] { "SecondChoiceOptionId" });
            DropIndex("dbo.Choices", new[] { "FirstChoiceOptionId" });
            AlterColumn("dbo.Choices", "FourthChoiceOptionId", c => c.Int());
            AlterColumn("dbo.Choices", "ThirdChoiceOptionId", c => c.Int());
            AlterColumn("dbo.Choices", "SecondChoiceOptionId", c => c.Int());
            AlterColumn("dbo.Choices", "FirstChoiceOptionId", c => c.Int());
            CreateIndex("dbo.Choices", "FourthChoiceOptionId");
            CreateIndex("dbo.Choices", "ThirdChoiceOptionId");
            CreateIndex("dbo.Choices", "SecondChoiceOptionId");
            CreateIndex("dbo.Choices", "FirstChoiceOptionId");
            AddForeignKey("dbo.Choices", "YearTermId", "dbo.YearTerms", "YearTermId", cascadeDelete: true);
        }
    }
}
