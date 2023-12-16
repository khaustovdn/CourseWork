using ReactiveUI;

namespace CourseWork.Models;

public class ConnectedObject : ReactiveObject
{
    private string? _text;

    public ConnectedObject(string text = "")
    {
        Text = text;
    }

    public string? Text
    {
        get => _text;
        set => this.RaiseAndSetIfChanged(ref _text, value);
    }

    public bool IsValid<T>()
    {
        if (typeof(T) == typeof(string))
            return !string.IsNullOrWhiteSpace(Text);
        if (typeof(T) == typeof(int))
            return IsValid<string>() && int.TryParse(Text, out _);
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
}