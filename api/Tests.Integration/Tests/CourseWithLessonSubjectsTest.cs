using System.Net;
using Core.Application.Features.AddCourseWithLessonSubjects;
using Core.Application.Features.UpdateCourseWithLessonSubjects;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using CourseResponse = Core.Application.Features.GetAllCourses.CourseResponse;

namespace Tests.Integration.Tests;

/// <summary>
/// Zestaw testów sprawdzający podstawowe operacje, które można przeprowadzić na tematach lekcji
/// razem z kursami. Wiarygodność rezultatów może zostać tylko osiągnięta przez uruchomienie ich
/// w określonej sekwencji. Te testy nie są niezależne od siebie, a całość tego pliku stanowi jeden
/// scenariusz testowy.
/// </summary>
[TestFixture]
internal sealed class CourseWithLessonSubjectsTest : TestBase
{
    private Guid courseId;
    
    [Test, Order(0)]
    public async Task AddCourseWithLessonSubjects_WithoutLessonSubjects_Ok()
    {
        var request = new AddCourseWithLessonSubjectsCommand("Name", "Description", new List<LessonSubjectAddModel>());
        var response = await testClient.PostAsJsonAsync("api/Course/AddCourseWithLessonSubjects", request).ConfigureAwait(false);
        
        Assert.That(response.StatusCode == HttpStatusCode.OK);
    } 
    
    [Test, Order(1)]
    public async Task AddCourseWithLessonSubjects_Ok()
    {
        var lessonSubjects = new List<LessonSubjectAddModel>
        {
            new(1, "Name"),
            new(2, "Name2"),
        };
        
        var request = new AddCourseWithLessonSubjectsCommand("Name2", "Description", lessonSubjects);
        var response = await testClient.PostAsJsonAsync("api/Course/AddCourseWithLessonSubjects", request).ConfigureAwait(false);
        
        Assert.That(response.StatusCode == HttpStatusCode.OK);
    }
    
    [Test, Order(2)]
    public async Task GetAllCourses_TwoExistsWithValidNameAndSubjectNumbers()
    {
        var response = await testClient.GetAsync("api/Course/GetAllCourses");
        var result = await response.Content.ReadAsAsync<List<CourseResponse>>();
        
        Assert.That(result.Count == 2);
        Assert.That(result.Any(x => x.Name == "Name"));
        Assert.That(result.Any(x => x.Name == "Name2"));
        Assert.That(result.Any(x => x.NumberOfLessonSubjects == 0));
        Assert.That(result.Any(x => x.NumberOfLessonSubjects == 2));

        courseId = Guid.Parse(result.First(x => x.NumberOfLessonSubjects == 2).Id);
    }

    [Test, Order(3)]
    public async Task UpdateCourseWithLessonSubjects_Ok()
    {
        var lessonSubjects = await context.LessonSubjects
            .AsNoTracking()
            .Where(x => x.Course.Id == courseId)
            .ToListAsync();

        var request = new UpdateCourseWithLessonSubjectsCommand
        {
            Id = courseId,
            Name = "Edited name",
            Description = "Edited description",
            LessonSubjects = new List<Core.Application.Features.UpdateCourseWithLessonSubjects.LessonSubjectUpdateModel>
            {
                new(null, 4, "New subject"),
                new(lessonSubjects.First().Id, 50, "Edited subject")
            }
        };

        var response = await testClient.PostAsJsonAsync("api/Course/UpdateCourseWithLessonSubjects", request);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test, Order(4)]
    public async Task GetCourseByIdQuery_OneAddedOneUpdatedOneDeleted()
    {
        var response = await testClient.GetAsync($"api/Course/GetCourseById?Id={courseId}");
        var result = await response.Content.ReadAsAsync<Core.Application.Features.GetCourseById.CourseResponse>();
        
        Assert.That(result.Name == "Edited name" && result.Description == "Edited description");
        Assert.That(result.LessonSubjects.Count, Is.EqualTo(2));
        Assert.That(result.LessonSubjects.Any(x => x.Number == 4));
        Assert.That(result.LessonSubjects.Any(x => x.Number == 50));
    }
}