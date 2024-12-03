using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;


namespace CEIT.Utils
{
	public class Jsonificator
	{
		public static string ToJson<T>(T[] values) where T : struct
		{
			var finalDict = new Dictionary<string, T[]>();
			finalDict["Elements"] = values;
			var jsonSerial = JsonConvert.SerializeObject(finalDict);
			return jsonSerial;
		}

		public static string ToJson<T>(IEnumerable<T> values) where T : struct
			=> ToJson(values.ToArray());

		public static IEnumerable<T> FromJson<T>(string jsonLines) where T : struct
		{
			JObject jsonData = JObject.Parse(jsonLines);
			List<JToken> data = jsonData["Elements"].Children().ToList();
			var result = data.Select(token => token.ToObject<T>());
			return result;
		}
	}
}