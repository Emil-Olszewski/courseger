using System.Net;
using Core.Application.Features.AddLessonSubjectToCourse;
using Core.Application.Features.GetAllCourses;
using Core.Application.Features.UpdateLessonSubject;
using Core.Domain.Models;
using NUnit.Framework;

namespace Tests.Integration.Tests;

/// <summary>
/// Zestaw testów sprawdzający podstawowe operacje, które można przeprowadzić na tematach lekcji. Wiarygodność
/// rezultatów może zostać tylko osiągnięta przez uruchomienie ich w określonej sekwencji. Te testy nie są
/// niezależne od siebie, a całość tego pliku stanowi jeden scenariusz testowy.
/// </summary>
[TestFixture]
internal sealed class LessonSubjectTest : TestBase
{
    private Course course;
    private Guid lessonSubjectId;
    
    [OneTimeSetUp]
    public async Task Prepare()
    {
        course = new Course
        {
            Name = "Name",
            Description = "Description"
        };

        context.Add(course);
        await context.SaveChangesAsync();
    }
    
    [Test, Order(0)]
    public async Task AddLessonSubject_WrongCourseId_BadRequest()
    {
        var request = new AddLessonSubjectToCourseCommand(Guid.NewGuid(), 1, "Name");
        var response = await testClient.PostAsJsonAsync("api/Course/AddLessonSubjectToCourse", request);
        
        Assert.That(response.StatusCode == HttpStatusCode.BadRequest);
    }
    
    [Test, Order(1)]
    public async Task AddLessonSubject_NameNotProvided_BadRequest()
    {
        var request = new AddLessonSubjectToCourseCommand(course.Id, 1, string.Empty);
        var response = await testClient.PostAsJsonAsync("api/Course/AddLessonSubjectToCourse", request);
        
        Assert.That(response.StatusCode == HttpStatusCode.BadRequest);
    }
    
    [Test, Order(2)]
    public async Task AddLessonSubject_NumberNotInRange_BadRequest()
    {
        var request = new AddLessonSubjectToCourseCommand(course.Id, 1_000_000, "Name");
        var response = await testClient.PostAsJsonAsync("api/Course/AddLessonSubjectToCourse", request);
        
        Assert.That(response.StatusCode == HttpStatusCode.BadRequest);
    }
    
    [Test, Order(3)]
    public async Task AddLessonSubject_Ok()
    {
        var request = new AddLessonSubjectToCourseCommand(course.Id, 1, "Name");
        var response = await testClient.PostAsJsonAsync("api/Course/AddLessonSubjectToCourse", request);
        
        Assert.That(response.StatusCode == HttpStatusCode.OK);
    }
    
    [Test, Order(4)]
    public async Task GetCourseById_HasOneLessonSubject()
    {
        var response = await testClient.GetAsync($"api/Course/GetCourseById?Id={course.Id}");
        var result = await response.Content.ReadAsAsync<Core.Application.Features.GetCourseById.CourseResponse>();
        
        Assert.That(result.LessonSubjects.Count == 1);

        lessonSubjectId = Guid.Parse(result.LessonSubjects.First().Id);
    }
    
    [Test, Order(5)]
    public async Task UpdateLessonSubject_WrongId_BadRequest()
    {
        var request = new UpdateLessonSubjectCommand(Guid.NewGuid(), 1, "Renamed");
        var response = await testClient.PostAsJsonAsync("api/Course/UpdateLessonSubject", request);
        
        Assert.That(response.StatusCode == HttpStatusCode.BadRequest);
    }
    
    [Test, Order(6)]
    public async Task UpdateLessonSubject_Ok()
    {
        var request = new UpdateLessonSubjectCommand(lessonSubjectId, 1, "Renamed");
        var response = await testClient.PostAsJsonAsync("api/Course/UpdateLessonSubject", request);
        
        Assert.That(response.StatusCode == HttpStatusCode.OK);
    }
    
    [Test, Order(7)]
    public async Task DeleteLessonSubject_WrongId_BadRequest()
    {
        var response = await testClient.DeleteAsync($"api/Course/DeleteLessonSubject?Id={Guid.NewGuid()}");
        
        Assert.That(response.StatusCode == HttpStatusCode.BadRequest);
    }
    
    [Test, Order(8)]
    public async Task DeleteLessonSubject_Ok()
    {
        var response = await testClient.DeleteAsync($"api/Course/DeleteLessonSubject?Id={lessonSubjectId}");
        
        Assert.That(response.StatusCode == HttpStatusCode.OK);
    }
    
    [Test, Order(9)]
    public async Task GetAllCourses_NoLessonSubjects()
    {
        var response = await testClient.GetAsync("api/Course/GetAllCourses");
        var result = await response.Content.ReadAsAsync<List<CourseResponse>>();
        
        Assert.That(result.All(x => x.NumberOfLessonSubjects == 0));
    }
}