using PayvectorHosted.Payment.Enums;
using System;
using System.Security.Cryptography;
using System.Text;

namespace PayvectorHosted.Payment.Helpers
{
	public static class HashUtil
	{
		public static string ByteArrayToHexString(byte[] source)
		{
			var str = new StringBuilder();
			for (int i = 0; i < source.Length; i++)
			{
				str.Append(source[i].ToString("x2"));
			}
			return str.ToString();
		}

		public static string ComputeHashDigest(string hashString, string preSharedKey, HashMethod hashMethod)
		{
			byte[] numArray;
			var hash = StringToByteArray(hashString);
			var pre = StringToByteArray(preSharedKey);
			switch (hashMethod)
			{
				case HashMethod.Md5:
					{
						numArray = (new MD5CryptoServiceProvider()).ComputeHash(hash);
						break;
					}
				case HashMethod.Sha1:
					{
						numArray = (new SHA1CryptoServiceProvider()).ComputeHash(hash);
						break;
					}
				case HashMethod.Hmacmd5:
					{
						numArray = (new HMACMD5(pre)).ComputeHash(hash);
						break;
					}
				case HashMethod.Hmacsha1:
					{
						numArray = (new HMACSHA1(pre)).ComputeHash(hash);
						break;
					}
				default:
					{
						throw new InvalidOperationException("Invalid hash method");
					}
			}
			return ByteArrayToHexString(numArray);
		}

		public static byte[] StringToByteArray(string source, bool useASCII = false)
		{
			Encoding encoding;
			if (!useASCII)
				encoding = new UTF8Encoding();
			else
				encoding = new ASCIIEncoding();
			return encoding.GetBytes(source);
		}
	}
}
