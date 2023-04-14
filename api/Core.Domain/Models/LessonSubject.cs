using Core.Domain.Exceptions;

namespace Core.Domain.Models;

public class LessonSubject
{
    private int number;
    private string name;
    
    private const int NameMaxLength = 40;
    private const int NumberMaxValue = 999;
    
    public Guid Id { get; set; }

    public static LessonSubject Create(Guid courseId, int number, string name)
    {
        return new LessonSubject
        {
            CourseId = courseId,
            Id = Guid.NewGuid(),
            Number = number,
            Name = name
        };
    }

    public int Number
    {
        get => number;
        set
        {
            if (value is <= 0 or >= NumberMaxValue)
            {
                throw new BusinessException("Number does not match the valid range.");
            }

            number = value;
        }
    }

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

    public virtual Guid CourseId { get; set; }
    public virtual Course Course { get; set; }
}