namespace AFS.NET_developer_test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addErrorModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TranslationModels", "Error_code", c => c.Int(nullable: false));
            AddColumn("dbo.TranslationModels", "Error_message", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TranslationModels", "Error_message");
            DropColumn("dbo.TranslationModels", "Error_code");
        }
    }
}
