/*
// I was searching for a pattern to simulate template specialization, too.
// There are some approaches which may work in some circumstances. However what about the case

static void Add<T>(T value1, T value2)
{
    //add the 2 numeric values
}

// It would be possible to choose the action using statements e.g. if (typeof(T) == typeof(int)).
// But there is a better way to simulate real template specialization with the overhead of a single virtual function call:

public interface IMath<T>
{
    T Add(T value1, T value2);
}

public class Math<T> : IMath<T>
{
    public static readonly IMath<T> P = Math.P as IMath<T> ?? new Math<T>();

    //default implementation
    T IMath<T>.Add(T value1, T value2)
    {
        throw new NotSupportedException();    
    }
}

class Math : IMath<int>, IMath<double>
{
    public static Math P = new Math();

    //specialized for int
    int IMath<int>.Add(int value1, int value2)
    {
        return value1 + value2;
    }

    //specialized for double
    double IMath<double>.Add(double value1, double value2)
    {
        return value1 + value2;
    }
}

// Now we can write, without having to know the type in advance:

static T Add<T>(T value1, T value2)
{
    return Math<T>.P.Add(value1, value2);
}

private static void Main(string[] args)
{
    var result1 = Add(1, 2);
    var result2 = Add(1.5, 2.5);

    return;
}

// If the specialization should not only be called for the implemented types, but also derived types,
// one could use an In parameter for the interface. However, in this case the return types of the methods
// cannot be of the generic type T any more.
*/