namespace AOC2025 ;

public static class ListExtensions
{
    public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
    {
        (list[indexA], list[indexB]) = (list[indexB], list[indexA]);
        return list;
    } 

    public static void Swap<T>(this LinkedListNode<T> a, LinkedListNode<T> b)
    {
        if (a == null)
        {
            throw new ArgumentNullException();
        }

        if (b == null)
        {
            throw new ArgumentNullException();
        }
    
        (a.Value, b.Value) = (b.Value, a.Value);
    }

    public static IEnumerable<T> Splice<T>(this IEnumerable<T> list, int offset, int count)
    {
        return list.Skip(offset).Take(count);
    }
}
