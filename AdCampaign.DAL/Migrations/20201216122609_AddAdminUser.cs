using Microsoft.EntityFrameworkCore.Migrations;

namespace AdCampaign.DAL.Migrations
{
    public partial class AddAdminUser : Migration
    {
        private const string AdminEmail = "admin@test.local";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //pass 123
            var passHash = "AQAAAAEAACcQAAAAEARrosQCg7gFF+CSyF6cwG+cndz6T4iilxzphTpzl07R11gHa/FreSUUrqjKPkHXKA==";
            var sql = @$"
            INSERT INTO ""Users"" (""Email"",""Name"",""PasswordHash"",""Phone"") 
                        values ('{AdminEmail}', 'admin', '{passHash}', '+79999999999')";
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"Delete Users Where Email = '{AdminEmail}'");
        }
    }
}
