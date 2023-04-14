using Core.Application.Interfaces;
using Core.Domain.Exceptions;
using Core.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.UpdateLessonSubject;

/// <summary>
/// Komenda pozwala na edycję danych w istniejącym już temacie lekcji.
/// </summary>
public sealed record UpdateLessonSubjectCommand(Guid Id, int Number, string Name) : IRequest;

internal sealed class UpdateLessonSubjectCommandHandler : IRequestHandler<UpdateLessonSubjectCommand>
{
    private readonly IAppDbContext context;

    public UpdateLessonSubjectCommandHandler(IAppDbContext context)
    {
        this.context = context;
    }

    public async Task Handle(UpdateLessonSubjectCommand request, CancellationToken ct)
    {
        var lessonSubject = await context.Set<LessonSubject>().SingleOrDefaultAsync(x => x.Id == request.Id, ct).ConfigureAwait(false);
        if (lessonSubject is null)
        {
            throw new BusinessException("Lesson subject does not exist.");
        }

        lessonSubject.Number = request.Number;
        lessonSubject.Name = request.Name;

        await context.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}