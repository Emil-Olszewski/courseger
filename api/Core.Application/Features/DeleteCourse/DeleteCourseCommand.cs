using Core.Application.Interfaces;
using Core.Domain;
using Core.Domain.Exceptions;
using Core.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.DeleteCourse;

/// <summary>
/// Komenda usuwa z bazy podany kurs.
/// </summary>
public sealed record DeleteCourseCommand(Guid Id) : IRequest;

internal sealed class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand>
{
    private readonly IAppDbContext context;

    public DeleteCourseCommandHandler(IAppDbContext context)
    {
        this.context = context;
    }

    public async Task Handle(DeleteCourseCommand request, CancellationToken ct)
    {
        var course = await context.Set<Course>().SingleOrDefaultAsync(x => x.Id == request.Id, ct).ConfigureAwait(false);
        if (course is null)
        {
            throw new BusinessException("Course does not exist.");
        }

        context.Remove(course);
        await context.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}