using Core.Domain.Exceptions;

namespace Core.Domain.Models;

public class Course
{
    private string name;
    private string? description;
    
    private const int NameMaxLength = 30;
    private const int DescriptionMaxLength = 500;
    
    public Guid Id { get; set; }
    
    public string Name
    {
        get => name;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > NameMaxLength)
            {
                throw new BusinessException("Name was not provided or exceeds the maximum length.");
            }

            name = value;
        }
    }

    public string? Description
    {
        get => description;
        set
        {
            if (!string.IsNullOrEmpty(value) && value.Length > DescriptionMaxLength)
            {
                throw new BusinessException("Description exceeds the maximum length.");
            }

            description = value;
        }
    }

    public virtual ICollection<LessonSubject> LessonSubjects { get; set; } = new List<LessonSubject>();
}