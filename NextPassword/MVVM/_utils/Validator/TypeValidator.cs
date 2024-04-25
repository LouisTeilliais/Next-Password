using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace NextPassword.MVVM._utils.Validator
{
    public class TypeValidator
    {
        // Check if string can be parse
        public static bool TryParseJSON(string json)
        {
            if (string.IsNullOrEmpty(json)) return false;
            try
            {
                var JsonObj = JsonConvert.DeserializeObject<object>(json);

                return true;
            }
            catch { return false; }
        }
    }
}
