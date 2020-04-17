using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class RijndaelEncryption
{

    /// <summary>
    /// Encrypts a string using DES encryption (Rijndael) with a given key
    /// </summary>
    /// <param name="original">
    /// the string to be encrypted</param>
    /// <param name="key">string key that will be used for encryption
    /// must be 32 char long </param>
    public static byte[] Encrypt(string original, string key)
    {
        byte[] encrypted = null;
        try
        {
            byte[] iv = Encoding.ASCII.GetBytes("1234567890123456");
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);

            using (Rijndael myRijndael = Rijndael.Create())
            {
                myRijndael.Key = keyBytes;
                myRijndael.IV = iv;

                // Encrypt the string to an array of bytes.
                encrypted = EncryptStringToBytes(original, myRijndael.Key, myRijndael.IV);

                //Display the original data and the decrypted data.
                Debug.LogFormat("Original:   {0}", original);
                Debug.LogFormat("Encrypted:   {0}", encrypted);
            }
        }
        catch (Exception e)
        {
            Debug.LogFormat("Error: {0}", e.Message);
        }

        return encrypted;
    }

    /// <summary>
    /// Decrypts the cipher text that was encrypted with DES encryption (Rijndael) with a given key
    /// </summary>
    /// <param name="encryptedString">
    /// the cipher text that was encrypted</param>
    /// <param name="key">key to decrypt with, needs to be 32 chars long</param>
    /// <returns></returns>
    public static string Decrypt(byte[] encryptedString, string key)
    {
        string decrypted = String.Empty;

        try
        {
            byte[] iv = Encoding.ASCII.GetBytes("1234567890123456");
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);

            using (Rijndael myRijndael = Rijndael.Create())
            {
                myRijndael.Key = keyBytes;
                myRijndael.IV = iv;

                // Encrypt the string to an array of bytes.
                decrypted = DecryptStringFromBytes(encryptedString, myRijndael.Key, myRijndael.IV);

                //Display the original data and the decrypted data.
                Debug.LogFormat("Original:   {0}", encryptedString);
                Debug.LogFormat("Encrypted:   {0}", decrypted);
            }
        }
        catch (Exception e)
        {
            Debug.LogFormat("Error: {0}", e.Message);
        }

        return decrypted;
    }

    private static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("plainText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");
        byte[] encrypted;

        // Create an Rijndael object
        // with the specified key and IV.
        using (Rijndael rijAlg = Rijndael.Create())
        {
            rijAlg.Key = Key;
            rijAlg.IV = IV;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }

    private static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");

        // Declare the string used to hold
        // the decrypted text.
        string plaintext = null;

        // Create an Rijndael object
        // with the specified key and IV.
        using (Rijndael rijAlg = Rijndael.Create())
        {
            rijAlg.Key = Key;
            rijAlg.IV = IV;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        return plaintext;
    }
}