namespace EnglishLearningTools.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldsToUserLists : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserLists", "ListName", c => c.String());
            AddColumn("dbo.UserLists", "Word", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserLists", "Word");
            DropColumn("dbo.UserLists", "ListName");
        }
    }
}
