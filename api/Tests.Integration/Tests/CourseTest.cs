using System.Net;
using Core.Application.Features.AddCourse;
using Core.Application.Features.GetAllCourses;
using Core.Application.Features.UpdateCourse;
using NUnit.Framework;

namespace Tests.Integration.Tests;

/// <summary>
/// Zestaw testów sprawdzający podstawowe operacje, które można przeprowadzić na kursach. Wiarygodność
/// rezultatów może zostać tylko osiągnięta przez uruchomienie ich w określonej sekwencji. Te testy nie są
/// niezależne od siebie, a całość tego pliku stanowi jeden scenariusz testowy.
/// </summary>
[TestFixture]
internal sealed class CourseTest : TestBase
{
    private Guid courseId;
    
    [Test, Order(0)]
    public async Task AddCourse_NameIsNotProvided_BadRequest()
    {
        var request = new AddCourseCommand(string.Empty, string.Empty);
        var response = await testClient.PostAsJsonAsync("api/Course/AddCourse", request).ConfigureAwait(false);
        
        Assert.That(response.StatusCode == HttpStatusCode.BadRequest);
    }
    
    [Test, Order(1)]
    public async Task AddCourse_NameIsTooLong_BadRequest()
    {
        var request = new AddCourseCommand("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", string.Empty);
        var response = await testClient.PostAsJsonAsync("api/Course/AddCourse", request).ConfigureAwait(false);
        
        Assert.That(response.StatusCode == HttpStatusCode.BadRequest);
    }
    
    [Test, Order(2)]
    public async Task AddCourse_Ok()
    {
        var request = new AddCourseCommand("Name", "Description");
        var response = await testClient.PostAsJsonAsync("api/Course/AddCourse", request).ConfigureAwait(false);
        
        Assert.That(response.StatusCode == HttpStatusCode.OK);
    }

    [Test, Order(3)]
    public async Task GetAllCourses_OneExistsWithValidName()
    {
        var response = await testClient.GetAsync("api/Course/GetAllCourses");
        var result = await response.Content.ReadAsAsync<List<CourseResponse>>();
        
        Assert.That(result.Count == 1);
        Assert.That(result.First().Name == "Name");

        courseId = Guid.Parse(result.First().Id);
    }

    [Test, Order(4)]
    public async Task UpdateCourse_WrongId_BadRequest()
    {
        var request = new UpdateCourseCommand(Guid.NewGuid(), "Name", "Description");
        var response = await testClient.PostAsJsonAsync("api/Course/UpdateCourse", request);
        
        Assert.That(response.StatusCode == HttpStatusCode.BadRequest);
    }
    
    [Test, Order(5)]
    public async Task UpdateCourse_Ok()
    {
        var request = new UpdateCourseCommand(courseId, "Renamed", "Description2");
        var response = await testClient.PostAsJsonAsync("api/Course/UpdateCourse", request);
        
        Assert.That(response.StatusCode == HttpStatusCode.OK);
    }

    [Test, Order(6)]
    public async Task GetCourseById_WrongId_BadRequest()
    {
        var response = await testClient.GetAsync($"api/Course/GetCourseById?Id={Guid.NewGuid()}");
        
        Assert.That(response.StatusCode == HttpStatusCode.BadRequest);
    }
    
    [Test, Order(7)]
    public async Task GetCourseById_SuccessfullyRenamed()
    {
        var response = await testClient.GetAsync($"api/Course/GetCourseById?Id={courseId}");
        var result = await response.Content.ReadAsAsync<Core.Application.Features.GetCourseById.CourseResponse>();
        
        Assert.That(result.Name == "Renamed" && result.Description == "Description2");
    }

    [Test, Order(8)]
    public async Task DeleteCourse_WrongId_BadRequest()
    {
        var response = await testClient.DeleteAsync($"api/Course/DeleteCourse?Id={Guid.NewGuid()}");
        
        Assert.That(response.StatusCode == HttpStatusCode.BadRequest);
    }
    
    [Test, Order(9)]
    public async Task DeleteCourse_Ok()
    {
        var response = await testClient.DeleteAsync($"api/Course/DeleteCourse?Id={courseId}");
        
        Assert.That(response.StatusCode == HttpStatusCode.OK);
    }
    
    [Test, Order(10)]
    public async Task GetCourseById_Removed_BadRequest()
    {
        var response = await testClient.GetAsync($"api/Course/GetCourseById?Id={courseId}");
        Assert.That(response.StatusCode == HttpStatusCode.BadRequest);
    }
}