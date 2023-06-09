﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("Core.Domain.Models.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("06949ad9-1f77-46a5-be34-88158be2039f"),
                            Description = "Kończąc ten kurs będziesz w stanie rozumieć Pythona i samodzielnie tworzyć programy rozwiązujące problemy natury informatycznej z wykorzystaniem tego języka. Pozwoli to rozpocząć naukę bardziej zaawansowanych tematów jak np.  przetwarzanie danych, implementowanie algorytmów, budowanie aplikacji webowych czy data science i machine learning.",
                            Name = "Python dla początkujących"
                        },
                        new
                        {
                            Id = new Guid("b3fd1004-f4df-4ef6-848b-2728d6457f1a"),
                            Description = "Ten kurs jest przeznaczony dla osób początkujących, które chcą wejść w świat testowania i pracować jako tester oprogramowania.  Jest wiele ob szarów w których możemy się rozwijać takich jak: automatyzacja, testy wydajnościowe, pentesty i wiele więcej. ",
                            Name = "Praktyczny kurs testowania"
                        });
                });

            modelBuilder.Entity("Core.Domain.Models.LessonSubject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("LessonSubjects");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3bd9773b-317b-4924-96b6-f34b9a93f1e8"),
                            CourseId = new Guid("06949ad9-1f77-46a5-be34-88158be2039f"),
                            Name = "O kursie",
                            Number = 1
                        },
                        new
                        {
                            Id = new Guid("d4d05d6f-dca2-4b0d-b27f-8382263233b1"),
                            CourseId = new Guid("06949ad9-1f77-46a5-be34-88158be2039f"),
                            Name = "Wprowadzenie - pierwsze kroki",
                            Number = 2
                        },
                        new
                        {
                            Id = new Guid("f55bc2e7-e1c2-4057-b4d2-0d704de8a596"),
                            CourseId = new Guid("06949ad9-1f77-46a5-be34-88158be2039f"),
                            Name = "Instalacja Pythona i narzędzi",
                            Number = 3
                        },
                        new
                        {
                            Id = new Guid("b206a42f-9e45-4b8d-9f20-b699c4d8bdcb"),
                            CourseId = new Guid("06949ad9-1f77-46a5-be34-88158be2039f"),
                            Name = "Podstawy wiedzy o języku",
                            Number = 4
                        },
                        new
                        {
                            Id = new Guid("715f0005-f013-46ed-97e0-1695af35f448"),
                            CourseId = new Guid("06949ad9-1f77-46a5-be34-88158be2039f"),
                            Name = "Typy zaawansowane",
                            Number = 5
                        },
                        new
                        {
                            Id = new Guid("38a3fc73-7f1d-402b-81d8-00570d74a93f"),
                            CourseId = new Guid("06949ad9-1f77-46a5-be34-88158be2039f"),
                            Name = "Sterowanie programem",
                            Number = 6
                        },
                        new
                        {
                            Id = new Guid("be49c0c6-6ae8-4d91-bc32-83b887a5fc9b"),
                            CourseId = new Guid("b3fd1004-f4df-4ef6-848b-2728d6457f1a"),
                            Name = "O kursie",
                            Number = 1
                        },
                        new
                        {
                            Id = new Guid("fa3fe1e4-d78c-4776-b333-373dace6645c"),
                            CourseId = new Guid("b3fd1004-f4df-4ef6-848b-2728d6457f1a"),
                            Name = "Część teoretyczna",
                            Number = 2
                        },
                        new
                        {
                            Id = new Guid("ab85d70f-2338-4ca2-8848-1ca168be3e3d"),
                            CourseId = new Guid("b3fd1004-f4df-4ef6-848b-2728d6457f1a"),
                            Name = "Część praktyczna",
                            Number = 3
                        },
                        new
                        {
                            Id = new Guid("6f6c4122-1a2e-4ec8-aca9-13b5e2f2faa0"),
                            CourseId = new Guid("b3fd1004-f4df-4ef6-848b-2728d6457f1a"),
                            Name = "Oprogramowanie JIRA",
                            Number = 4
                        },
                        new
                        {
                            Id = new Guid("176fef16-a6ae-494e-a6bf-47c23f44506c"),
                            CourseId = new Guid("b3fd1004-f4df-4ef6-848b-2728d6457f1a"),
                            Name = "Platforma BrowserStack",
                            Number = 5
                        },
                        new
                        {
                            Id = new Guid("c1d98117-01b1-4e67-afd9-82740a545094"),
                            CourseId = new Guid("b3fd1004-f4df-4ef6-848b-2728d6457f1a"),
                            Name = "Testowanie API",
                            Number = 6
                        },
                        new
                        {
                            Id = new Guid("058a54ad-462c-442c-9f2a-a7799e055c5b"),
                            CourseId = new Guid("b3fd1004-f4df-4ef6-848b-2728d6457f1a"),
                            Name = "Praktyczne testowanie aplikacji webowej",
                            Number = 7
                        });
                });

            modelBuilder.Entity("Core.Domain.Models.LessonSubject", b =>
                {
                    b.HasOne("Core.Domain.Models.Course", "Course")
                        .WithMany("LessonSubjects")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Core.Domain.Models.Course", b =>
                {
                    b.Navigation("LessonSubjects");
                });
#pragma warning restore 612, 618
        }
    }
}
