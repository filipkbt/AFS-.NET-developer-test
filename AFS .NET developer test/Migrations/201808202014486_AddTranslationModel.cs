namespace AFS.NET_developer_test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTranslationModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TranslationModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Translated = c.String(),
                        Text = c.String(),
                        TranslationType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TranslationModels");
        }
    }
}
