using System;
using System.Security.Cryptography;
using System.Text;

namespace ECommerce.Helpers;

public abstract class HashHandler
{
    private static readonly int KeySize = 64;
    private static readonly int Iterations = 350000;
    private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA256;

    public static string HashUserPassword(string password, out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(KeySize);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                Iterations,
                HashAlgorithm,
                KeySize
            );

        return Convert.ToHexString(hash);
    }

    public static bool VerifyPassword(string password, string hash, byte[] salt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithm,
                KeySize
            );

        return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
    }
}