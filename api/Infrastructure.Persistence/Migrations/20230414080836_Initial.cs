using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LessonSubjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CourseId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonSubjects_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("06949ad9-1f77-46a5-be34-88158be2039f"), "Kończąc ten kurs będziesz w stanie rozumieć Pythona i samodzielnie tworzyć programy rozwiązujące problemy natury informatycznej z wykorzystaniem tego języka. Pozwoli to rozpocząć naukę bardziej zaawansowanych tematów jak np.  przetwarzanie danych, implementowanie algorytmów, budowanie aplikacji webowych czy data science i machine learning.", "Python dla początkujących" },
                    { new Guid("b3fd1004-f4df-4ef6-848b-2728d6457f1a"), "Ten kurs jest przeznaczony dla osób początkujących, które chcą wejść w świat testowania i pracować jako tester oprogramowania.  Jest wiele ob szarów w których możemy się rozwijać takich jak: automatyzacja, testy wydajnościowe, pentesty i wiele więcej. ", "Praktyczny kurs testowania" }
                });

            migrationBuilder.InsertData(
                table: "LessonSubjects",
                columns: new[] { "Id", "CourseId", "Name", "Number" },
                values: new object[,]
                {
                    { new Guid("058a54ad-462c-442c-9f2a-a7799e055c5b"), new Guid("b3fd1004-f4df-4ef6-848b-2728d6457f1a"), "Praktyczne testowanie aplikacji webowej", 7 },
                    { new Guid("176fef16-a6ae-494e-a6bf-47c23f44506c"), new Guid("b3fd1004-f4df-4ef6-848b-2728d6457f1a"), "Platforma BrowserStack", 5 },
                    { new Guid("38a3fc73-7f1d-402b-81d8-00570d74a93f"), new Guid("06949ad9-1f77-46a5-be34-88158be2039f"), "Sterowanie programem", 6 },
                    { new Guid("3bd9773b-317b-4924-96b6-f34b9a93f1e8"), new Guid("06949ad9-1f77-46a5-be34-88158be2039f"), "O kursie", 1 },
                    { new Guid("6f6c4122-1a2e-4ec8-aca9-13b5e2f2faa0"), new Guid("b3fd1004-f4df-4ef6-848b-2728d6457f1a"), "Oprogramowanie JIRA", 4 },
                    { new Guid("715f0005-f013-46ed-97e0-1695af35f448"), new Guid("06949ad9-1f77-46a5-be34-88158be2039f"), "Typy zaawansowane", 5 },
                    { new Guid("ab85d70f-2338-4ca2-8848-1ca168be3e3d"), new Guid("b3fd1004-f4df-4ef6-848b-2728d6457f1a"), "Część praktyczna", 3 },
                    { new Guid("b206a42f-9e45-4b8d-9f20-b699c4d8bdcb"), new Guid("06949ad9-1f77-46a5-be34-88158be2039f"), "Podstawy wiedzy o języku", 4 },
                    { new Guid("be49c0c6-6ae8-4d91-bc32-83b887a5fc9b"), new Guid("b3fd1004-f4df-4ef6-848b-2728d6457f1a"), "O kursie", 1 },
                    { new Guid("c1d98117-01b1-4e67-afd9-82740a545094"), new Guid("b3fd1004-f4df-4ef6-848b-2728d6457f1a"), "Testowanie API", 6 },
                    { new Guid("d4d05d6f-dca2-4b0d-b27f-8382263233b1"), new Guid("06949ad9-1f77-46a5-be34-88158be2039f"), "Wprowadzenie - pierwsze kroki", 2 },
                    { new Guid("f55bc2e7-e1c2-4057-b4d2-0d704de8a596"), new Guid("06949ad9-1f77-46a5-be34-88158be2039f"), "Instalacja Pythona i narzędzi", 3 },
                    { new Guid("fa3fe1e4-d78c-4776-b333-373dace6645c"), new Guid("b3fd1004-f4df-4ef6-848b-2728d6457f1a"), "Część teoretyczna", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LessonSubjects_CourseId",
                table: "LessonSubjects",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonSubjects");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
