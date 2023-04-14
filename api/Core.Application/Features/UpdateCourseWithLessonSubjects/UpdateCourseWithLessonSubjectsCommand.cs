using Core.Application.Interfaces;
using Core.Domain.Exceptions;
using Core.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.UpdateCourseWithLessonSubjects;

public sealed record LessonSubjectUpdateModel(Guid? Id, int Number, string Name);

/// <summary>
/// Komenda pozwala na edycję danych i tematów lekcji w już istniejącym kursie.
/// </summary>
public sealed record UpdateCourseWithLessonSubjectsCommand : IRequest
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public List<LessonSubjectUpdateModel> LessonSubjects { get; init; }
}

internal sealed class UpdateCourseWithLessonSubjectsCommandHandler : IRequestHandler<UpdateCourseWithLessonSubjectsCommand>
{
    private readonly IAppDbContext context;

    public UpdateCourseWithLessonSubjectsCommandHandler(IAppDbContext context)
    {
        this.context = context;
    }

    public async Task Handle(UpdateCourseWithLessonSubjectsCommand request, CancellationToken ct)
    {
        var course = await context.Set<Course>()
            .Include(x => x.LessonSubjects)
            .SingleOrDefaultAsync(x => x.Id == request.Id, ct).ConfigureAwait(false);
        
        if (course is null)
        {
            throw new BusinessException("Course does not exist.");
        }

        course.Name = request.Name;
        course.Description = request.Description;

        var subjectsToAdd = request.LessonSubjects.Where(x => x.Id is null).ToList();
        
        var subjectsToDelete = course.LessonSubjects
            .Where(x => request.LessonSubjects.All(y => y.Id != x.Id))
            .ToList();
        
        var subjectsToUpdate = request.LessonSubjects
            .Where(x => course.LessonSubjects.Any(y => y.Id == x.Id))
            .ToList();
        
        course.LessonSubjects = course.LessonSubjects.Union(subjectsToAdd.Select(x => new LessonSubject
        {
            Number = x.Number,
            Name = x.Name
        })).ToList();
        
        foreach (var subject in subjectsToDelete)
        {
            context.Remove(subject);
        }

        foreach (var model in subjectsToUpdate)
        {
            var lessonSubject = course.LessonSubjects.First(x => x.Id == model.Id);
            lessonSubject.Number = model.Number;
            lessonSubject.Name = model.Name;
        }
        
        await context.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}