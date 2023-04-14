using Core.Application.Interfaces;
using Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

internal sealed class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<Course>? Courses { get; set; }
    public DbSet<LessonSubject>? LessonSubjects { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Oczywiście nie jest to
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var pythonCourseId = Guid.NewGuid();
        var testingCourseId = Guid.NewGuid();
        
        var pythonCourseSubjects = new List<LessonSubject>
        {
            LessonSubject.Create(pythonCourseId, 1, "O kursie"),
            LessonSubject.Create(pythonCourseId,2, "Wprowadzenie - pierwsze kroki"),
            LessonSubject.Create(pythonCourseId,3, "Instalacja Pythona i narzędzi"),
            LessonSubject.Create(pythonCourseId,4, "Podstawy wiedzy o języku"),
            LessonSubject.Create(pythonCourseId,5, "Typy zaawansowane"),
            LessonSubject.Create(pythonCourseId,6, "Sterowanie programem"),
        };

        var testingCourseSubjects = new List<LessonSubject>
        {
            LessonSubject.Create(testingCourseId,1, "O kursie"),
            LessonSubject.Create(testingCourseId,2, "Część teoretyczna"),
            LessonSubject.Create(testingCourseId,3, "Część praktyczna"),
            LessonSubject.Create(testingCourseId,4, "Oprogramowanie JIRA"),
            LessonSubject.Create(testingCourseId,5, "Platforma BrowserStack"),
            LessonSubject.Create(testingCourseId,6, "Testowanie API"),
            LessonSubject.Create(testingCourseId,7, "Praktyczne testowanie aplikacji webowej"),
        };

        var seedData = new List<Course>
        {
            new ()
            {
            Id = pythonCourseId,
            Name = "Python dla początkujących",
            Description =
                "Kończąc ten kurs będziesz w stanie rozumieć Pythona i samodzielnie tworzyć programy rozwiązujące" +
                " problemy natury informatycznej z wykorzystaniem tego języka. Pozwoli to rozpocząć naukę" +
                " bardziej zaawansowanych tematów jak np.  przetwarzanie danych, implementowanie algorytmów," +
                " budowanie aplikacji webowych czy data science i machine learning.",
            },
            new()
            {
                Id = testingCourseId,
                Name = "Praktyczny kurs testowania",
                Description =
                    "Ten kurs jest przeznaczony dla osób początkujących, które chcą wejść w świat testowania i pracować " +
                    "jako tester oprogramowania.  Jest wiele ob szarów w których możemy się rozwijać takich jak: automatyzacja," +
                    " testy wydajnościowe, pentesty i wiele więcej. ",
            }
        };

        // zakomentowane żeby nie psuć testów
        // modelBuilder.Entity<Course>().HasData(seedData);
        // modelBuilder.Entity<LessonSubject>().HasData(pythonCourseSubjects.Union(testingCourseSubjects));
    }
}