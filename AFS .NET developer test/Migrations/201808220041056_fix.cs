namespace AFS.NET_developer_test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.contents",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        translated = c.String(),
                        text = c.String(),
                        translation = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TranslationModels", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.errors",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        code = c.Int(nullable: false),
                        message = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TranslationModels", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.successes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        total = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TranslationModels", t => t.Id)
                .Index(t => t.Id);
            
            DropColumn("dbo.TranslationModels", "Success_total");
            DropColumn("dbo.TranslationModels", "Contents_translated");
            DropColumn("dbo.TranslationModels", "Contents_text");
            DropColumn("dbo.TranslationModels", "Contents_translation");
            DropColumn("dbo.TranslationModels", "Error_code");
            DropColumn("dbo.TranslationModels", "Error_message");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TranslationModels", "Error_message", c => c.String());
            AddColumn("dbo.TranslationModels", "Error_code", c => c.Int(nullable: false));
            AddColumn("dbo.TranslationModels", "Contents_translation", c => c.String());
            AddColumn("dbo.TranslationModels", "Contents_text", c => c.String());
            AddColumn("dbo.TranslationModels", "Contents_translated", c => c.String());
            AddColumn("dbo.TranslationModels", "Success_total", c => c.Int(nullable: false));
            DropForeignKey("dbo.contents", "Id", "dbo.TranslationModels");
            DropForeignKey("dbo.successes", "Id", "dbo.TranslationModels");
            DropForeignKey("dbo.errors", "Id", "dbo.TranslationModels");
            DropIndex("dbo.successes", new[] { "Id" });
            DropIndex("dbo.errors", new[] { "Id" });
            DropIndex("dbo.contents", new[] { "Id" });
            DropTable("dbo.successes");
            DropTable("dbo.errors");
            DropTable("dbo.contents");
        }
    }
}
