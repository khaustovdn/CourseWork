namespace CourseWork.Models;

public interface ICollection
{
    void Remove(int id);
    void ElementAt(int id);
    void Add(object? o);
}