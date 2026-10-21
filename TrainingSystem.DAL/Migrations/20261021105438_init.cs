using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    City = table.Column<string>(type: "varchar(100)", nullable: true),
                    Adress = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", nullable: true),
                    DurationInMonth = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPrograms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Intake",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrainingProgramId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intake", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Intake_TrainingPrograms_TrainingProgramId",
                        column: x => x.TrainingProgramId,
                        principalTable: "TrainingPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(250)", nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", nullable: false),
                    IntakeId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tracks_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tracks_Intake_IntakeId",
                        column: x => x.IntakeId,
                        principalTable: "Intake",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fname = table.Column<string>(type: "varchar(100)", nullable: false),
                    Mname = table.Column<string>(type: "varchar(100)", nullable: false),
                    Lname = table.Column<string>(type: "varchar(100)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrackId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrackCourses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    TrackId = table.Column<int>(type: "int", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackCourses", x => new { x.TrackId, x.CourseId });
                    table.CheckConstraint("HoursBiggerZero", "[Hours]>0");
                    table.ForeignKey(
                        name: "FK_TrackCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrackCourses_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Id", "Adress", "City", "Name" },
                values: new object[,]
                {
                    { 1, "KM 28 Cairo-Alexandria Desert Road", "Giza", "Smart Village" },
                    { 2, "Nasr City", "Cairo", "National Telecommunication Institute" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Fundamentals of C#", "C# Programming" },
                    { 2, "SQL Server and EF Core", "Database Fundamentals" },
                    { 3, "Python basics for data science", "Python for AI" }
                });

            migrationBuilder.InsertData(
                table: "TrainingPrograms",
                columns: new[] { "Id", "Description", "DurationInMonth", "Name" },
                values: new object[,]
                {
                    { 1, "Web development using .NET and Angular", 4, "Full Stack Development" },
                    { 2, "Artificial intelligence and machine learning track", 3, "AI and Data Science" }
                });

            migrationBuilder.InsertData(
                table: "Intake",
                columns: new[] { "Id", "EndDate", "Name", "StartDate", "TrainingProgramId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Intake 44", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Intake 45", new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "Id", "BranchId", "Description", "IntakeId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "C#, EF Core, ASP.NET Core", 1, ".NET Web Developer" },
                    { 2, 2, "Python, Pandas, TensorFlow", 2, "Machine Learning" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "DateOfBirth", "Email", "Fname", "Image", "Lname", "Mname", "NationalId", "Phone", "TrackId" },
                values: new object[,]
                {
                    { 1, new DateTime(2001, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ahmed.ali@test.com", "Ahmed", null, "Mohamed", "Ali", "30101010101011", "01012345678", 1 },
                    { 2, new DateTime(2002, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "sara.m@test.com", "Sara", null, "Hassan", "Mahmoud", "30202020202022", "01112345678", 2 }
                });

            migrationBuilder.InsertData(
                table: "TrackCourses",
                columns: new[] { "CourseId", "TrackId", "Hours" },
                values: new object[,]
                {
                    { 1, 1, 60 },
                    { 2, 1, 45 },
                    { 3, 2, 50 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branches_Name",
                table: "Branches",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Name",
                table: "Courses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Intake_Name_TrainingProgramId",
                table: "Intake",
                columns: new[] { "Name", "TrainingProgramId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Intake_TrainingProgramId",
                table: "Intake",
                column: "TrainingProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_TrackId",
                table: "Students",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackCourses_CourseId",
                table: "TrackCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_BranchId",
                table: "Tracks",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_IntakeId_BranchId_Name",
                table: "Tracks",
                columns: new[] { "IntakeId", "BranchId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPrograms_Name",
                table: "TrainingPrograms",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "TrackCourses");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Intake");

            migrationBuilder.DropTable(
                name: "TrainingPrograms");
        }
    }
}
