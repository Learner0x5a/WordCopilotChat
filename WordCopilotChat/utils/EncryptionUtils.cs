using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WordCopilotChat.utils
{
    /// <summary>
    /// 加密解密工具类
    /// 使用AES-256-CBC算法配合HMAC验证，支持密码验证，每次加密结果都不同
    /// </summary>
    public static class EncryptionUtils
    {
        private const int KEY_SIZE = 32; // 256 bits
        private const int IV_SIZE = 16;  // 128 bits
        private const int SALT_SIZE = 16; // 128 bits
        private const int HMAC_SIZE = 32; // 256 bits
        private const int ITERATIONS = 100000; // PBKDF2迭代次数

        /// <summary>
        /// 加密文本内容
        /// </summary>
        /// <param name="plainText">要加密的明文</param>
        /// <param name="password">密码</param>
        /// <returns>加密后的Base64字符串</returns>
        public static string Encrypt(string plainText, string password)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentException("明文不能为空", nameof(plainText));
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("密码不能为空", nameof(password));

            try
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                
                // 生成随机盐和IV
                byte[] salt = new byte[SALT_SIZE];
                byte[] iv = new byte[IV_SIZE];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                    rng.GetBytes(iv);
                }

                // 从密码派生密钥
                byte[] keys = DeriveKeys(password, salt);
                byte[] aesKey = new byte[KEY_SIZE];
                byte[] hmacKey = new byte[KEY_SIZE];
                Buffer.BlockCopy(keys, 0, aesKey, 0, KEY_SIZE);
                Buffer.BlockCopy(keys, KEY_SIZE, hmacKey, 0, KEY_SIZE);

                // 使用AES-CBC加密
                byte[] cipherText;
                using (var aes = Aes.Create())
                {
                    aes.KeySize = 256;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = aesKey;
                    aes.IV = iv;

                    using (var encryptor = aes.CreateEncryptor())
                    {
                        cipherText = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                    }
                }

                // 计算HMAC
                byte[] hmac;
                using (var hmacSha256 = new HMACSHA256(hmacKey))
                {
                    // HMAC计算：盐 + IV + 密文
                    byte[] dataToAuthenticate = new byte[SALT_SIZE + IV_SIZE + cipherText.Length];
                    Buffer.BlockCopy(salt, 0, dataToAuthenticate, 0, SALT_SIZE);
                    Buffer.BlockCopy(iv, 0, dataToAuthenticate, SALT_SIZE, IV_SIZE);
                    Buffer.BlockCopy(cipherText, 0, dataToAuthenticate, SALT_SIZE + IV_SIZE, cipherText.Length);
                    
                    hmac = hmacSha256.ComputeHash(dataToAuthenticate);
                }

                // 组合所有数据：盐 + IV + HMAC + 密文
                byte[] result = new byte[SALT_SIZE + IV_SIZE + HMAC_SIZE + cipherText.Length];
                Buffer.BlockCopy(salt, 0, result, 0, SALT_SIZE);
                Buffer.BlockCopy(iv, 0, result, SALT_SIZE, IV_SIZE);
                Buffer.BlockCopy(hmac, 0, result, SALT_SIZE + IV_SIZE, HMAC_SIZE);
                Buffer.BlockCopy(cipherText, 0, result, SALT_SIZE + IV_SIZE + HMAC_SIZE, cipherText.Length);

                // 清理敏感数据
                Array.Clear(keys, 0, keys.Length);
                Array.Clear(aesKey, 0, aesKey.Length);
                Array.Clear(hmacKey, 0, hmacKey.Length);
                Array.Clear(plainBytes, 0, plainBytes.Length);

                return Convert.ToBase64String(result);
            }
            catch (Exception ex)
            {
                throw new CryptographicException($"加密失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 解密文本内容
        /// </summary>
        /// <param name="encryptedText">加密的Base64字符串</param>
        /// <param name="password">密码</param>
        /// <returns>解密后的明文</returns>
        public static string Decrypt(string encryptedText, string password)
        {
            if (string.IsNullOrEmpty(encryptedText))
                throw new ArgumentException("加密文本不能为空", nameof(encryptedText));
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("密码不能为空", nameof(password));

            try
            {
                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                
                if (encryptedBytes.Length < SALT_SIZE + IV_SIZE + HMAC_SIZE + 1)
                    throw new CryptographicException("加密数据格式无效");

                // 提取各部分数据
                byte[] salt = new byte[SALT_SIZE];
                byte[] iv = new byte[IV_SIZE];
                byte[] expectedHmac = new byte[HMAC_SIZE];
                byte[] cipherText = new byte[encryptedBytes.Length - SALT_SIZE - IV_SIZE - HMAC_SIZE];

                Buffer.BlockCopy(encryptedBytes, 0, salt, 0, SALT_SIZE);
                Buffer.BlockCopy(encryptedBytes, SALT_SIZE, iv, 0, IV_SIZE);
                Buffer.BlockCopy(encryptedBytes, SALT_SIZE + IV_SIZE, expectedHmac, 0, HMAC_SIZE);
                Buffer.BlockCopy(encryptedBytes, SALT_SIZE + IV_SIZE + HMAC_SIZE, cipherText, 0, cipherText.Length);

                // 从密码派生密钥
                byte[] keys = DeriveKeys(password, salt);
                byte[] aesKey = new byte[KEY_SIZE];
                byte[] hmacKey = new byte[KEY_SIZE];
                Buffer.BlockCopy(keys, 0, aesKey, 0, KEY_SIZE);
                Buffer.BlockCopy(keys, KEY_SIZE, hmacKey, 0, KEY_SIZE);

                // 验证HMAC
                byte[] computedHmac;
                using (var hmacSha256 = new HMACSHA256(hmacKey))
                {
                    // HMAC计算：盐 + IV + 密文
                    byte[] dataToAuthenticate = new byte[SALT_SIZE + IV_SIZE + cipherText.Length];
                    Buffer.BlockCopy(salt, 0, dataToAuthenticate, 0, SALT_SIZE);
                    Buffer.BlockCopy(iv, 0, dataToAuthenticate, SALT_SIZE, IV_SIZE);
                    Buffer.BlockCopy(cipherText, 0, dataToAuthenticate, SALT_SIZE + IV_SIZE, cipherText.Length);
                    
                    computedHmac = hmacSha256.ComputeHash(dataToAuthenticate);
                }

                // 比较HMAC
                if (!ConstantTimeEquals(expectedHmac, computedHmac))
                {
                    throw new CryptographicException("数据完整性验证失败");
                }

                // 使用AES-CBC解密
                byte[] plainBytes;
                using (var aes = Aes.Create())
                {
                    aes.KeySize = 256;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = aesKey;
                    aes.IV = iv;

                    using (var decryptor = aes.CreateDecryptor())
                    {
                        plainBytes = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
                    }
                }

                // 清理敏感数据
                Array.Clear(keys, 0, keys.Length);
                Array.Clear(aesKey, 0, aesKey.Length);
                Array.Clear(hmacKey, 0, hmacKey.Length);

                return Encoding.UTF8.GetString(plainBytes);
            }
            catch (CryptographicException)
            {
                throw new CryptographicException("解密失败，请检查密码是否正确");
            }
            catch (Exception ex)
            {
                throw new CryptographicException($"解密失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 从密码派生密钥（AES密钥 + HMAC密钥）
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="salt">盐值</param>
        /// <returns>派生的密钥（64字节：32字节AES + 32字节HMAC）</returns>
        private static byte[] DeriveKeys(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, ITERATIONS, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(KEY_SIZE * 2); // AES密钥 + HMAC密钥
            }
        }

        /// <summary>
        /// 恒定时间比较两个字节数组（防止时序攻击）
        /// </summary>
        /// <param name="a">数组A</param>
        /// <param name="b">数组B</param>
        /// <returns>是否相等</returns>
        private static bool ConstantTimeEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            int result = 0;
            for (int i = 0; i < a.Length; i++)
            {
                result |= a[i] ^ b[i];
            }
            return result == 0;
        }

        /// <summary>
        /// 验证密码是否正确（不进行完整解密）
        /// </summary>
        /// <param name="encryptedText">加密的文本</param>
        /// <param name="password">要验证的密码</param>
        /// <returns>密码是否正确</returns>
        public static bool ValidatePassword(string encryptedText, string password)
        {
            try
            {
                // 尝试解密前几个字节来验证密码
                Decrypt(encryptedText, password);
                return true;
            }
            catch (CryptographicException)
            {
                return false;
            }
        }

        /// <summary>
        /// 生成安全的随机密码
        /// </summary>
        /// <param name="length">密码长度</param>
        /// <returns>随机密码</returns>
        public static string GenerateRandomPassword(int length = 12)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
            var result = new StringBuilder(length);
            
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] randomBytes = new byte[length];
                rng.GetBytes(randomBytes);
                
                for (int i = 0; i < length; i++)
                {
                    result.Append(chars[randomBytes[i] % chars.Length]);
                }
            }
            
            return result.ToString();
        }
    }
} 