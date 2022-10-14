using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4.Migrations
{
    public partial class spcall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE getstudentsbydept
	-- Add the parameters for the stored procedure here
	@deptname nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select s.id as Id, s.name as Name, s.Email as Email, s.Address as Address, d.DepartmentName as DepartmentName,d.Location as DepartmentLocation, d.Id as DepartmentId  from students s join Departments d on s.departmentid = d.id
	where d.DepartmentName = @deptname

END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop proc getstudentsbydept");
        }
    }
}
