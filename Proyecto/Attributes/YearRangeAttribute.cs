using System.ComponentModel.DataAnnotations;

namespace Proyecto.Attributes;

public class YearRangeAttribute : ValidationAttribute
{
    private readonly int _minYear;

    public YearRangeAttribute(int minYear = 1980)
    {
        _minYear = minYear;
    }

    public override bool IsValid(object? value)
    {
        if (value == null) return true; // Let Required attribute handle null validation

        if (!int.TryParse(value.ToString(), out int year))
            return false;

        var maxYear = DateTime.Now.Year + 1;
        return year >= _minYear && year <= maxYear;
    }

    public override string FormatErrorMessage(string name)
    {
        var maxYear = DateTime.Now.Year + 1;
        return $"El año del modelo debe estar entre {_minYear} y {maxYear}";
    }
}