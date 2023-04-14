using Core.Application.Interfaces;
using Core.Domain.Exceptions;
using Core.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.AddLessonSubjectToCourse;

/// <summary>
/// Komenda dodaje do podanego kursu nowy temat lekcji.
/// </summary>
public sealed record AddLessonSubjectToCourseCommand(Guid CourseId, int Number, string Name) : IRequest;

internal sealed class AddLessonSubjectToCourseCommandHandler : IRequestHandler<AddLessonSubjectToCourseCommand>
{
    private readonly IAppDbContext context;

    public AddLessonSubjectToCourseCommandHandler(IAppDbContext context)
    {
        this.context = context;
    }

    public async Task Handle(AddLessonSubjectToCourseCommand request, CancellationToken ct)
    {
        var course = await context.Set<Course>().SingleOrDefaultAsync(x => x.Id == request.CourseId, ct).ConfigureAwait(false);
        if (course is null)
        {
            throw new BusinessException("Course does not exist.");
        }

        var lessonSubject = new LessonSubject
        {
            Number = request.Number,
            Name = request.Name
        };
        
        course.LessonSubjects.Add(lessonSubject);
        await context.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
