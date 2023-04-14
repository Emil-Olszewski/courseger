using Core.Application.Features.AddCourse;
using Core.Application.Features.AddCourseWithLessonSubjects;
using Core.Application.Features.AddLessonSubjectToCourse;
using Core.Application.Features.DeleteCourse;
using Core.Application.Features.DeleteLessonSubject;
using Core.Application.Features.GetAllCourses;
using Core.Application.Features.GetCourseById;
using Core.Application.Features.UpdateCourse;
using Core.Application.Features.UpdateCourseWithLessonSubjects;
using Core.Application.Features.UpdateLessonSubject;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CourseController : ControllerBase
{
    private readonly IMediator mediator;

    public CourseController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    [HttpGet("GetAllCourses")]
    public async Task<IActionResult> GetAllCourses(CancellationToken ct)
    {
        return Ok(await mediator.Send(new GetAllCoursesQuery(), ct).ConfigureAwait(false));
    }
    
    [HttpGet("GetCourseById")]
    public async Task<IActionResult> GetAllCourses([FromQuery] GetCourseByIdQuery request, CancellationToken ct)
    {
        return Ok(await mediator.Send(request, ct).ConfigureAwait(false));
    }

    [HttpPost("AddCourse")]
    public async Task<IActionResult> AddCourse([FromBody] AddCourseCommand request, CancellationToken ct)
    {
        await mediator.Send(request, ct).ConfigureAwait(false);
        return Ok();
    }
    
    [HttpPost("AddLessonSubjectToCourse")]
    public async Task<IActionResult> AddLessonSubjectToCourse([FromBody] AddLessonSubjectToCourseCommand request, CancellationToken ct)
    {
        await mediator.Send(request, ct).ConfigureAwait(false);
        return Ok();
    }
    
    [HttpPost("AddCourseWithLessonSubjects")]
    public async Task<IActionResult> AddCourseWithLessonSubjects([FromBody] AddCourseWithLessonSubjectsCommand request, CancellationToken ct)
    {
        await mediator.Send(request, ct).ConfigureAwait(false);
        return Ok();
    }


    [HttpPost("UpdateCourse")]
    public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseCommand request, CancellationToken ct)
    {
        await mediator.Send(request, ct).ConfigureAwait(false);
        return Ok();
    } 
    
    [HttpPost("UpdateLessonSubject")]
    public async Task<IActionResult> UpdateCourse([FromBody] UpdateLessonSubjectCommand request, CancellationToken ct)
    {
        await mediator.Send(request, ct).ConfigureAwait(false);
        return Ok();
    }
    
    [HttpPost("UpdateCourseWithLessonSubjects")]
    public async Task<IActionResult> UpdateCourseWithLessonSubjects([FromBody] UpdateCourseWithLessonSubjectsCommand request, CancellationToken ct)
    {
        await mediator.Send(request, ct).ConfigureAwait(false);
        return Ok();
    }

    [HttpDelete("DeleteCourse")]
    public async Task<IActionResult> DeleteCourse([FromQuery] DeleteCourseCommand request, CancellationToken ct)
    {
        await mediator.Send(request, ct).ConfigureAwait(false);
        return Ok();
    }
    
    [HttpDelete("DeleteLessonSubject")]
    public async Task<IActionResult> DeleteLessonSubject([FromQuery] DeleteLessonSubjectCommand request, CancellationToken ct)
    {
        await mediator.Send(request, ct).ConfigureAwait(false);
        return Ok();
    }
}