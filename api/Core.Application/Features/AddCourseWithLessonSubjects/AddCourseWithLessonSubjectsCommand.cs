using Core.Application.Interfaces;
using Core.Domain.Models;
using MediatR;

namespace Core.Application.Features.AddCourseWithLessonSubjects;

public sealed record LessonSubjectAddModel(int Number, string Name);

/// <summary>
/// Komenda tworzy nowu kurs wraz z podanymy tematami zajęć.
/// </summary>
public sealed record AddCourseWithLessonSubjectsCommand(string Name, string Description, List<LessonSubjectAddModel> LessonSubjects) : IRequest;

internal sealed class AddCourseWithLessonSubjectsCommandHandler : IRequestHandler<AddCourseWithLessonSubjectsCommand>
{
    private readonly IAppDbContext context;

    public AddCourseWithLessonSubjectsCommandHandler(IAppDbContext context)
    {
        this.context = context;
    }

    public async Task Handle(AddCourseWithLessonSubjectsCommand request, CancellationToken ct)
    {
        var newCourse = new Course
        {
            Name = request.Name,
            Description = request.Description,
            LessonSubjects = request.LessonSubjects.Select(x => new LessonSubject
            {
                Number = x.Number,
                Name = x.Name
            }).ToList()
        };

        context.Add(newCourse);
        await context.SaveChangesAsync(ct);
    }
}