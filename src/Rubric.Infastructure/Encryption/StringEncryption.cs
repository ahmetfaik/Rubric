using System.Security.Cryptography;
using System.Text;

namespace Rubric.Infastructure.Encryption;

public static class StringEncryption
{
    public static string Encrypt(this string encryptString, string? encryptionKey = default)
    {
        var clearBytes = Encoding.Unicode.GetBytes(encryptString);
        using var encryptor = Aes.Create();
        var pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[]
        {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
        encryptor.Key = pdb.GetBytes(32);
        encryptor.IV = pdb.GetBytes(16);
        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(clearBytes, 0, clearBytes.Length);
        cs.Close();
        encryptString = Convert.ToBase64String(ms.ToArray());
        return encryptString;
    }

    public static string Decrypt(this string cipherText, string encryptionKey)
    {
        cipherText = cipherText.Replace(" ", "+");
        var cipherBytes = Convert.FromBase64String(cipherText);
        using var encryptor = Aes.Create();
        var pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[]
        {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
        encryptor.Key = pdb.GetBytes(32);
        encryptor.IV = pdb.GetBytes(16);
        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write);
        cs.Write(cipherBytes, 0, cipherBytes.Length);
        cs.Close();
        cipherText = Encoding.Unicode.GetString(ms.ToArray());
        return cipherText;
    }
}