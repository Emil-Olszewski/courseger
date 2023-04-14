namespace Core.Application.Features.GetCourseById;

internal sealed class CourseResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<LessonSubjectResponse> LessonSubjects { get; set; }
}

internal sealed class LessonSubjectResponse
{
    public string Id { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }

    public LessonSubjectResponse(Guid id, int number, string name)
    {
        Id = id.ToString();
        Number = number;
        Name = name;
    }
}