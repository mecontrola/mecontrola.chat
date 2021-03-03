namespace MeControla.Core.Extensions
{
    public static class IntegerExtension
    {
        public static string Pad(this int number, int length)
        {
            return number.ToString($"D{length}");
        }
    }
}