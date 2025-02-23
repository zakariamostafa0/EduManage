using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigrationWithFluentApi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_Instructor_Departments_DID",
                table: "Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Instructor_SupervisorId",
                table: "Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_InstructorSubject_Instructor_InsId",
                table: "InstructorSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_InstructorSubject_Subjects_SubId",
                table: "InstructorSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Students_StudId",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Subjects_SubId",
                table: "StudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstructorSubject",
                table: "InstructorSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instructor",
                table: "Instructor");

            migrationBuilder.RenameTable(
                name: "InstructorSubject",
                newName: "InstructorSubjects");

            migrationBuilder.RenameTable(
                name: "Instructor",
                newName: "Instructors");

            migrationBuilder.RenameIndex(
                name: "IX_InstructorSubject_InsId",
                table: "InstructorSubjects",
                newName: "IX_InstructorSubjects_InsId");

            migrationBuilder.RenameIndex(
                name: "IX_Instructor_SupervisorId",
                table: "Instructors",
                newName: "IX_Instructors_SupervisorId");

            migrationBuilder.RenameIndex(
                name: "IX_Instructor_DID",
                table: "Instructors",
                newName: "IX_Instructors_DID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstructorSubjects",
                table: "InstructorSubjects",
                columns: new[] { "SubId", "InsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instructors",
                table: "Instructors",
                column: "InsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Instructors_InsManager",
                table: "Departments",
                column: "InsManager",
                principalTable: "Instructors",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmetSubjects_Departments_DepId",
                table: "DepartmetSubjects",
                column: "DepId",
                principalTable: "Departments",
                principalColumn: "DID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmetSubjects_Subjects_SubId",
                table: "DepartmetSubjects",
                column: "SubId",
                principalTable: "Subjects",
                principalColumn: "SubID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Departments_DID",
                table: "Instructors",
                column: "DID",
                principalTable: "Departments",
                principalColumn: "DID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Instructors_SupervisorId",
                table: "Instructors",
                column: "SupervisorId",
                principalTable: "Instructors",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstructorSubjects_Instructors_InsId",
                table: "InstructorSubjects",
                column: "InsId",
                principalTable: "Instructors",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstructorSubjects_Subjects_SubId",
                table: "InstructorSubjects",
                column: "SubId",
                principalTable: "Subjects",
                principalColumn: "SubID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DID",
                table: "Students",
                column: "DID",
                principalTable: "Departments",
                principalColumn: "DID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Students_StudId",
                table: "StudentSubjects",
                column: "StudId",
                principalTable: "Students",
                principalColumn: "StudID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubjects_Subjects_SubId",
                table: "StudentSubjects",
                column: "SubId",
                principalTable: "Subjects",
                principalColumn: "SubID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Instructors_InsManager",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmetSubjects_Departments_DepId",
                table: "DepartmetSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmetSubjects_Subjects_SubId",
                table: "DepartmetSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Departments_DID",
                table: "Instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Instructors_SupervisorId",
                table: "Instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_InstructorSubjects_Instructors_InsId",
                table: "InstructorSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_InstructorSubjects_Subjects_SubId",
                table: "InstructorSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Students_StudId",
                table: "StudentSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubjects_Subjects_SubId",
                table: "StudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstructorSubjects",
                table: "InstructorSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instructors",
                table: "Instructors");

            migrationBuilder.RenameTable(
                name: "InstructorSubjects",
                newName: "InstructorSubject");

            migrationBuilder.RenameTable(
                name: "Instructors",
                newName: "Instructor");

            migrationBuilder.RenameIndex(
                name: "IX_InstructorSubjects_InsId",
                table: "InstructorSubject",
                newName: "IX_InstructorSubject_InsId");

            migrationBuilder.RenameIndex(
                name: "IX_Instructors_SupervisorId",
                table: "Instructor",
                newName: "IX_Instructor_SupervisorId");

            migrationBuilder.RenameIndex(
                name: "IX_Instructors_DID",
                table: "Instructor",
                newName: "IX_Instructor_DID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstructorSubject",
                table: "InstructorSubject",
                columns: new[] { "SubId", "InsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instructor",
                table: "Instructor",
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
                name: "FK_Instructor_Departments_DID",
                table: "Instructor",
                column: "DID",
                principalTable: "Departments",
                principalColumn: "DID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Instructor_SupervisorId",
                table: "Instructor",
                column: "SupervisorId",
                principalTable: "Instructor",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstructorSubject_Instructor_InsId",
                table: "InstructorSubject",
                column: "InsId",
                principalTable: "Instructor",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstructorSubject_Subjects_SubId",
                table: "InstructorSubject",
                column: "SubId",
                principalTable: "Subjects",
                principalColumn: "SubID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DID",
                table: "Students",
                column: "DID",
                principalTable: "Departments",
                principalColumn: "DID");

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
    }
}
