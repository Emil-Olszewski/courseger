using Core.Application.Interfaces;
using Core.Domain.Exceptions;
using Core.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.GetCourseById;

/// <summary>
/// Zapytanie zwraca szczegółowe informacje o podanym kursie i przypisanych do niego tematach lekcji.
/// </summary>
public sealed record GetCourseByIdQuery(Guid Id) : IRequest<CourseResponse>;

internal sealed class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, CourseResponse>
{
    private readonly IAppDbContext context;

    public GetCourseByIdQueryHandler(IAppDbContext context)
    {
        this.context = context;
    }

    public async Task<CourseResponse> Handle(GetCourseByIdQuery request, CancellationToken ct)
    {
        var course = await context.Set<Course>()
            .AsNoTracking()
            .Include(x => x.LessonSubjects.OrderBy(y => y.Number))
            .SingleOrDefaultAsync(x => x.Id == request.Id, ct)
            .ConfigureAwait(false);

        if (course is null)
        {
            throw new BusinessException("Course does not exists");
        }
        
        return new CourseResponse
        {
            Id = course.Id.ToString(),
            Name = course.Name,
            Description = course.Description,
            LessonSubjects = course.LessonSubjects.Select(x => new LessonSubjectResponse(x.Id, x.Number, x.Name)).ToList()
        };
    }
}