using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigurationsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmetSubjects_Departments_DID",
                table: "DepartmetSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmetSubjects_Subjects_SubID",
                table: "DepartmetSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Students_StudID",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Subjects_SubID",
                table: "StudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubjects_SubID",
                table: "StudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmetSubjects",
                table: "DepartmetSubjects");

            migrationBuilder.DropIndex(
                name: "IX_DepartmetSubjects_SubID",
                table: "DepartmetSubjects");

            migrationBuilder.DropColumn(
                name: "StudSubID",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "DeptSubID",
                table: "DepartmetSubjects");

            migrationBuilder.RenameColumn(
                name: "SubID",
                table: "StudentSubjects",
                newName: "SubId");

            migrationBuilder.RenameColumn(
                name: "StudID",
                table: "StudentSubjects",
                newName: "StudId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubjects_StudID",
                table: "StudentSubjects",
                newName: "IX_StudentSubjects_StudId");

            migrationBuilder.RenameColumn(
                name: "SubID",
                table: "DepartmetSubjects",
                newName: "SubId");

            migrationBuilder.RenameColumn(
                name: "DID",
                table: "DepartmetSubjects",
                newName: "DepId");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmetSubjects_DID",
                table: "DepartmetSubjects",
                newName: "IX_DepartmetSubjects_DepId");

            migrationBuilder.AddColumn<int>(
                name: "InsManager",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects",
                columns: new[] { "SubId", "StudId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmetSubjects",
                table: "DepartmetSubjects",
                columns: new[] { "SubId", "DepId" });

            migrationBuilder.CreateTable(
                name: "Instructor",
                columns: table => new
                {
                    InsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupervisorId = table.Column<int>(type: "int", nullable: true),
                    DID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor", x => x.InsId);
                    table.ForeignKey(
                        name: "FK_Instructor_Departments_DID",
                        column: x => x.DID,
                        principalTable: "Departments",
                        principalColumn: "DID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instructor_Instructor_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Instructor",
                        principalColumn: "InsId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstructorSubject",
                columns: table => new
                {
                    InsId = table.Column<int>(type: "int", nullable: false),
                    SubId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorSubject", x => new { x.SubId, x.InsId });
                    table.ForeignKey(
                        name: "FK_InstructorSubject_Instructor_InsId",
                        column: x => x.InsId,
                        principalTable: "Instructor",
                        principalColumn: "InsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorSubject_Subjects_SubId",
                        column: x => x.SubId,
                        principalTable: "Subjects",
                        principalColumn: "SubID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_InsManager",
                table: "Departments",
                column: "InsManager",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_DID",
                table: "Instructor",
                column: "DID");

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_SupervisorId",
                table: "Instructor",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorSubject_InsId",
                table: "InstructorSubject",
                column: "InsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Instructor_InsManager",
                table: "Departments",
                column: "InsManager",
                principalTable: "Instructor",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmetSubjects_Departments_DepId",
                table: "DepartmetSubjects",
                column: "DepId",
                principalTable: "Departments",
                principalColumn: "DID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmetSubjects_Subjects_SubId",
                table: "DepartmetSubjects",
                column: "SubId",
                principalTable: "Subjects",
                principalColumn: "SubID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Students_StudId",
                table: "StudentSubjects",
                column: "StudId",
                principalTable: "Students",
                principalColumn: "StudID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Subjects_SubId",
                table: "StudentSubjects",
                column: "SubId",
                principalTable: "Subjects",
                principalColumn: "SubID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Instructor_InsManager",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmetSubjects_Departments_DepId",
                table: "DepartmetSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmetSubjects_Subjects_SubId",
                table: "DepartmetSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Students_StudId",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Subjects_SubId",
                table: "StudentSubjects");

            migrationBuilder.DropTable(
                name: "InstructorSubject");

            migrationBuilder.DropTable(
                name: "Instructor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmetSubjects",
                table: "DepartmetSubjects");

            migrationBuilder.DropIndex(
                name: "IX_Departments_InsManager",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "InsManager",
                table: "Departments");

            migrationBuilder.RenameColumn(
                name: "StudId",
                table: "StudentSubjects",
                newName: "StudID");

            migrationBuilder.RenameColumn(
                name: "SubId",
                table: "StudentSubjects",
                newName: "SubID");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSubjects_StudId",
                table: "StudentSubjects",
                newName: "IX_StudentSubjects_StudID");

            migrationBuilder.RenameColumn(
                name: "SubId",
                table: "DepartmetSubjects",
                newName: "SubID");

            migrationBuilder.RenameColumn(
                name: "DepId",
                table: "DepartmetSubjects",
                newName: "DID");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmetSubjects_DepId",
                table: "DepartmetSubjects",
                newName: "IX_DepartmetSubjects_DID");

            migrationBuilder.AddColumn<int>(
                name: "StudSubID",
                table: "StudentSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "DeptSubID",
                table: "DepartmetSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects",
                column: "StudSubID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmetSubjects",
                table: "DepartmetSubjects",
                column: "DeptSubID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_SubID",
                table: "StudentSubjects",
                column: "SubID");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmetSubjects_SubID",
                table: "DepartmetSubjects",
                column: "SubID");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmetSubjects_Departments_DID",
                table: "DepartmetSubjects",
                column: "DID",
                principalTable: "Departments",
                principalColumn: "DID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmetSubjects_Subjects_SubID",
                table: "DepartmetSubjects",
                column: "SubID",
                principalTable: "Subjects",
                principalColumn: "SubID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Students_StudID",
                table: "StudentSubjects",
                column: "StudID",
                principalTable: "Students",
                principalColumn: "StudID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Subjects_SubID",
                table: "StudentSubjects",
                column: "SubID",
                principalTable: "Subjects",
                principalColumn: "SubID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
