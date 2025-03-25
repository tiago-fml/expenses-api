namespace expenses_api.Utils;

public class Helpers
{
    public static bool IsPasswordValid(string password)
    {
        return password.Length >= 8 && password.Any(char.IsDigit) && 
               password.Any(char.IsLetter) && password.Any(char.IsUpper);
    }
}