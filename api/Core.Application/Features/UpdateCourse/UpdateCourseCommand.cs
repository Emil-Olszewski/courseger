using Core.Application.Interfaces;
using Core.Domain.Exceptions;
using Core.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.UpdateCourse;

/// <summary>
/// Komenda pozwala na edycję danych w istniejącym już kursie.
/// </summary>
public sealed record UpdateCourseCommand(Guid Id, string Name, string Description) : IRequest;

internal sealed class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand>
{
    private readonly IAppDbContext context;

    public UpdateCourseCommandHandler(IAppDbContext context)
    {
        this.context = context;
    }

    public async Task Handle(UpdateCourseCommand request, CancellationToken ct)
    {
        var course = await context.Set<Course>().SingleOrDefaultAsync(x => x.Id == request.Id, ct).ConfigureAwait(false);
        if (course is null)
        {
            throw new BusinessException("Course does not exist.");
        }

        course.Name = request.Name;
        course.Description = request.Description;

        await context.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}