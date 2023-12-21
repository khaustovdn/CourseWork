using System;
using System.Linq;
using ReactiveUI;

namespace CourseWork.Models;

public class ValidInput : ReactiveObject
{
    private int _number;
    private string? _text;

    public ValidInput(string text = "")
    {
        Text = text;
    }

    public string? Text
    {
        get => _text;
        set => this.RaiseAndSetIfChanged(ref _text, value);
    }

    public int Number
    {
        get => _number;
        set => this.RaiseAndSetIfChanged(ref _number, value);
    }

    public bool IsValid<T>()
    {
        if (typeof(T) == typeof(string))
            return !string.IsNullOrWhiteSpace(Text) && Text.Length >= 4;
        if (typeof(T) == typeof(int))
            return !string.IsNullOrWhiteSpace(Text) && int.TryParse(Text, out _);
        if (typeof(T) == typeof(DateTime))
            try
            {
                ToDate();
                return true;
            }
            catch
            {
                return false;
            }

        return false;
    }

    public override string? ToString()
    {
        return Text;
    }

    public int ToInt()
    {
        return int.TryParse(_text, out _) ? int.Parse(_text ?? string.Empty) : 0;
    }

    public DateTime? ToDate()
    {
        if (!IsValid<string>()) return null;
        var data = Text?.Split(':').ToList();
        if (data == null) return null;
        var dateTime = new DateTime(int.Parse(data[2]), int.Parse(data[1]), int.Parse(data[0]));
        return dateTime;
    }
}