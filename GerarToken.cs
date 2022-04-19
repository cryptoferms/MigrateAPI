using Jose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MigrateAPI
{
    public class GerarToken
    {
		public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

		public static void Chamar()
		{
			var now = DateTime.UtcNow;

			var secretClear = "Als_Jwt_Secret_NonProd_dlkYjkjGH24";
			var encodedSecret = GetSecretKeyAsBytes(secretClear);

			var secretKey = new byte[] { 164, 60, 194, 0, 161, 189, 41, 38, 130, 89, 141, 164, 45, 170, 159, 209, 69, 137, 243, 216, 191, 131, 47, 250, 32, 107, 231, 117, 37, 158, 225, 234 };

			string token = Jose.JWT.Encode(GetPayload(now), encodedSecret, JwsAlgorithm.HS256);

			Console.WriteLine("nbf = " + GetNbf(now));
			Console.WriteLine("exp = " + GetExp(now));

			Console.WriteLine("token = " + token);
		}

		private static Dictionary<string, object> GetPayload(DateTime utcNow)
		{
			//var user = "corp\\lee.western";

			var payload = new Dictionary<string, object>()
		{
			{ "env", "Development" },
			{ "id", "1342" },
			{ "sub", "corp\\lee.western" },  /* netwotk user */
			{ "lab", "36@PR" },
			{ "iss", "ALS.LIMS.Angel"},
			{ "aud", "ALS.LIMS.Angel.ClientApp"},
			{ "nbf", GetNbf(utcNow) }, /* please see the fiddle to see how to generate these value - https://dotnetfiddle.net/z4jTFn*/
			{ "exp", GetExp(utcNow)} /*same as above*/
		};

			return payload;
		}

		private static byte[] GetSecretKeyAsBytes(string secret)
		{
			return Encoding.UTF8.GetBytes(secret);
		}

		public static string SecretAsBase64Encoded(string secret)
		{
			return Convert.ToBase64String(GetSecretKeyAsBytes(secret));
		}



		private static long GetNbf(DateTime utcNow) { return GetEpochDateTimeAsInt(utcNow); /* nbf */ }

		private static long GetExp(DateTime utcNow)
		{
			var tokenValidFor = TimeSpan.FromSeconds(120);
			var expiry = utcNow.Add(tokenValidFor);
			return GetEpochDateTimeAsInt(expiry); // exp	
		}

		public static long GetEpochDateTimeAsInt(DateTime datetime)
		{
			DateTime dateTime = datetime;
			if (datetime.Kind != DateTimeKind.Utc)
				dateTime = datetime.ToUniversalTime();
			if (dateTime.ToUniversalTime() <= UnixEpoch)
				return 0;
			return (long)(dateTime - UnixEpoch).TotalSeconds;
		}
	}
}
