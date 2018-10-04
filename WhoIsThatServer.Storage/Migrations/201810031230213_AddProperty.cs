namespace WhoIsThatServer.Storage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using WhoIsThatServer.Storage.Controllers;
    using WhoIsThatServer.Storage.Helpers;
    using WhoIsThatServer.Storage.Models;

    public partial class AddProperty : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DatabaseImageElements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageName = c.String(),
                        ImageContentUri = c.String(),
                        PersonFirstName = c.String(),
                        PersonLastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.DatabaseImageElements");
        }
    }
}
