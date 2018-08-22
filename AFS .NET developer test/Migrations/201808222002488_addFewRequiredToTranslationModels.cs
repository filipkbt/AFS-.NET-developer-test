namespace AFS.NET_developer_test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFewRequiredToTranslationModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.contents", "text", c => c.String(nullable: false));
            AlterColumn("dbo.contents", "translation", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.contents", "translation", c => c.String());
            AlterColumn("dbo.contents", "text", c => c.String());
        }
    }
}
