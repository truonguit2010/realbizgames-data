using System.Reflection;
using System.Text;

public class ToStringUtils
{
    public const string ASSIGN = ": ";
    public const string SEP = " ";

    public static string ToStringFor(object obj)
    {
        PropertyInfo[] _PropertyInfos = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
        var sb = new StringBuilder();

        sb.Append("[").Append(obj.GetType().Name).Append(" ");
        foreach (var info in _PropertyInfos)
        {
            var value = info.GetValue(obj, null) ?? "(null)";
            sb.Append(info.Name).Append(ASSIGN).Append(value.ToString()).Append(SEP);
        }
        sb.Append("]");

        return sb.ToString();
    }
}
