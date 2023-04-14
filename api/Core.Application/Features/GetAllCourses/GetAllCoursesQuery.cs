using Core.Application.Interfaces;
using Core.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.GetAllCourses;

/// <summary>
/// Zapytanie zwraca wszystkie kursy wraz ilością tematów do nich przypisanych.
/// </summary>
public sealed record GetAllCoursesQuery : IRequest<List<CourseResponse>>;

internal sealed class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, List<CourseResponse>>
{
    private readonly IAppDbContext context;

    public GetAllCoursesQueryHandler(IAppDbContext context)
    {
        this.context = context;
    }

    public async Task<List<CourseResponse>> Handle(GetAllCoursesQuery request, CancellationToken ct)
    {
        return await context.Set<Course>()
            .AsNoTracking()
            .Select(x => new CourseResponse(x.Id, x.Name, x.LessonSubjects.Count))
            .ToListAsync(ct)
            .ConfigureAwait(false);
    }
}