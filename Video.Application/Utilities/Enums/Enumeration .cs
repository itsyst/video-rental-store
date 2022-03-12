using System.Reflection;

namespace Video.Application.Utilities.Enums;

#nullable disable
public class Enumeration : IComparable
{
    public int Value { get; private set; }
    public string Name { get; private set; }

    protected Enumeration(ushort id, string name)
    {
        (Value, Name) = (id, name);
    }

    public override string ToString()
    {
        return Name;
    }
 
    public int CompareTo(object other)
    {
        return Value.CompareTo(((Enumeration)other).Value);
    }

    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
        return typeof(T).GetFields(
            BindingFlags.Public |
            BindingFlags.Static |
            BindingFlags.DeclaredOnly)
            .Select(f => f.GetValue(null))
            .Cast<T>();
    }
}
