namespace AFS.NET_developer_test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditTranslationAddSuccessAndContents : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TranslationModels", "Success_total", c => c.Int(nullable: false));
            AddColumn("dbo.TranslationModels", "Contents_translated", c => c.String());
            AddColumn("dbo.TranslationModels", "Contents_text", c => c.String());
            AddColumn("dbo.TranslationModels", "Contents_translation", c => c.String());
            AddColumn("dbo.TranslationModels", "StatusCode", c => c.String());
            AddColumn("dbo.TranslationModels", "IsSuccessStatusCode", c => c.Boolean(nullable: false));
            AddColumn("dbo.TranslationModels", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.TranslationModels", "Translated");
            DropColumn("dbo.TranslationModels", "Text");
            DropColumn("dbo.TranslationModels", "TranslationType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TranslationModels", "TranslationType", c => c.String());
            AddColumn("dbo.TranslationModels", "Text", c => c.String());
            AddColumn("dbo.TranslationModels", "Translated", c => c.String());
            DropColumn("dbo.TranslationModels", "Date");
            DropColumn("dbo.TranslationModels", "IsSuccessStatusCode");
            DropColumn("dbo.TranslationModels", "StatusCode");
            DropColumn("dbo.TranslationModels", "Contents_translation");
            DropColumn("dbo.TranslationModels", "Contents_text");
            DropColumn("dbo.TranslationModels", "Contents_translated");
            DropColumn("dbo.TranslationModels", "Success_total");
        }
    }
}
