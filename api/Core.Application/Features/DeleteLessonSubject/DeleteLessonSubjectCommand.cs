using Core.Application.Interfaces;
using Core.Domain;
using Core.Domain.Exceptions;
using Core.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.DeleteLessonSubject;

/// <summary>
/// Komenda usuwa podany temat lekcji.
/// </summary>
public sealed record DeleteLessonSubjectCommand(Guid Id) : IRequest;

internal sealed class DeleteLessonSubjectCommandHandler : IRequestHandler<DeleteLessonSubjectCommand>
{
    private readonly IAppDbContext context;

    public DeleteLessonSubjectCommandHandler(IAppDbContext context)
    {
        this.context = context;
    }

    public async Task Handle(DeleteLessonSubjectCommand request, CancellationToken ct)
    {
        var lessonSubject = await context.Set<LessonSubject>().SingleOrDefaultAsync(x => x.Id == request.Id, ct).ConfigureAwait(false);
        if (lessonSubject is null)
        {
            throw new BusinessException("Lesson subject does not exists.");
        }

        context.Remove(lessonSubject);
        await context.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}