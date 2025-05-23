using FluentMigrator;

namespace Devzteps_API.Data.Migrations
{
    [Migration(202505231200)]
    public class CreateTodoItemTable : Migration
    {
        public override void Up()
        {
            Create.Table("TodoItems")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("IsComplete").AsBoolean().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("TodoItems");
        }
    }
}
