namespace Core.Application.Features.GetAllCourses;

internal sealed class CourseResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int NumberOfLessonSubjects { get; set; }

    public CourseResponse(Guid id, string name, int numberOfLessonSubjects)
    {
        Id = id.ToString();
        Name = name;
        NumberOfLessonSubjects = numberOfLessonSubjects;
    }
}