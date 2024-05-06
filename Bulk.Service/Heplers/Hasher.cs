namespace Bulk.Service.Heplers;

public static class Hasher
{
    public static string Hash(string content)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
        return BCrypt.Net.BCrypt.HashPassword(content, salt);
    }

    public static bool Verify(string content, string hash)
    {
        try
        {
            return BCrypt.Net.BCrypt.Verify(content, hash);
        }
        catch(Exception ex) 
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
