using System.Net;
using Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Tests.Integration.Tests;

/// <summary>
/// Ten test sprawdza czy wszystkie tematy lekcji usuwają się razem z kursem, do którego są przypisane.
/// </summary>
[TestFixture]
internal sealed class CourseDeleteTest : TestBase
{
    private Course course;
    
    [OneTimeSetUp]
    public async Task Prepare()
    {
        course = new Course
        {
            Name = "Name",
            LessonSubjects = new List<LessonSubject>
            {
                new()
                {
                    Number = 1,
                    Name = "Name"
                },
                new()
                {
                    Number = 2,
                    Name = "Name"
                }
            }
        };

        context.Add(course);
        await context.SaveChangesAsync();
    }

    [Test]
    public async Task DeleteCourse_HasLessonSubjects_EverythingRemoved()
    {
        var response = await testClient.DeleteAsync($"api/Course/DeleteCourse?Id={course.Id}");
        var courses = await context.Courses.AsNoTracking().ToListAsync();
        var lessonSubjects = await context.LessonSubjects.AsNoTracking().ToListAsync();
        
        Assert.That(response.StatusCode == HttpStatusCode.OK);
        Assert.That(!courses.Any());
        Assert.That(!lessonSubjects.Any());
    }
}