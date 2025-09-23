namespace RevrenLove.SimplishAuth;

public static class Extensions
{
    public static string ShrinkFirstLetter(this string s)
    {
        if (s.Length > 0)
        {
            s = $"{s[0].ToString().ToLower()}{s[1..]}";
        }

        return s;
    }
}