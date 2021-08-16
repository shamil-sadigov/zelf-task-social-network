namespace Domain.Extensions
{
    public static class CharExtensions
    {
        public static bool IsLetter(this char symbol)
            => char.IsLetter(symbol);

        public static bool IsWhiteSpace(this char symbol)
            => char.IsWhiteSpace(symbol);
    }
}