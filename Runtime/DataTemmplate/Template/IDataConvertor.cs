using System.Collections.Generic;

public interface IDataConvertor<F, T>
{
    T From(F f);

    List<T> From(List<F> fs);
}
