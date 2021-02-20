using Newtonsoft.Json;
using Client.Models;
using System.Collections.Generic;

namespace Client.Utilities
{
    public static class ItemsConverter
    {
        public static bool ValidateJson(string json)
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

        public static List<DictionaryItem> ConvertFromJson(string json)
            => JsonConvert.DeserializeObject<List<DictionaryItem>>(json);

        public static string ConvertToJson(List<DictionaryItem> items) 
            => JsonConvert.SerializeObject(items);
    }
}