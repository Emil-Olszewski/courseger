using Core.Application.Interfaces;
using Core.Domain.Models;
using MediatR;

namespace Core.Application.Features.AddCourse;

/// <summary>
/// Komenda tworzy nowy kurs.
/// </summary>
public sealed record AddCourseCommand(string Name, string Description) : IRequest;

internal sealed class AddCourseCommandHandler : IRequestHandler<AddCourseCommand>
{
    private readonly IAppDbContext context;

    public AddCourseCommandHandler(IAppDbContext context)
    {
        this.context = context;
    }
    
    public async Task Handle(AddCourseCommand request, CancellationToken ct)
    {
        var newCourse = new Course
        {
            Name = request.Name,
            Description = request.Description
        };

        context.Add(newCourse);
        await context.SaveChangesAsync(ct);
    }
}