using Newtonsoft.Json;
using Client.Models;
using System.Collections.Generic;

namespace Client.Utilities
{
    internal static class ItemsConverter
    {
        internal static bool ValidateJson(string json)
        {
            try
            {
                List<DictionaryItem> items = JsonConvert.DeserializeObject<List<DictionaryItem>>(json);
            }
            catch
            {
                return false;
            }
            return true;
        }

        internal static List<DictionaryItem> ConvertFromJson(string json)
            => JsonConvert.DeserializeObject<List<DictionaryItem>>(json);

        internal static string ConvertItemsToJson(List<DictionaryItem> items)
            => JsonConvert.SerializeObject(items);

        private record JsonRequest
        {
            public string Text { get; set; }
        }

        internal static string ConvertStringToJsonRequest(string text)
            => JsonConvert.SerializeObject(new JsonRequest() { Text = text });
    }
}