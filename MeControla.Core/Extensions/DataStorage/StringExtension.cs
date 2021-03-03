namespace MeControla.Core.Extensions.DataStorage
{
    public static class StringExtension
    {
        public static string GetColumnName(this string str, string prefix)
        {
            return $"{prefix}_{str.ToSnakeCase()}";
        }
    }
}