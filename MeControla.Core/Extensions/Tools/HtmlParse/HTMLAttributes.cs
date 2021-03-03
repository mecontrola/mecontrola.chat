using System;
using System.Collections.Generic;
using System.Linq;

namespace MeControla.Core.Tools.HtmlParse
{
    public class HTMLAttributes : Dictionary<string, string>
    {
        public new bool ContainsValue(string value)
        {
            return !string.IsNullOrWhiteSpace(GetKeyByValue(value));
        }

        public string GetKeyByValue(string value)
        {
            foreach (var key in Keys)
            {
                if (this[key].Split(" ").Contains(value))
                {
                    return key;
                }
            }

            return string.Empty;
        }

        public bool ExistsAllKeys(HTMLAttributes parameters)
        {
            if (parameters == null)
            {
                return false;
            }

            return Keys.Intersect(parameters.Keys).Count() == parameters.Keys.Count;
        }

        public bool ExistsAllValues(HTMLAttributes parameters)
        {
            if (parameters == null)
            {
                return false;
            }

            if (!ExistsAllKeys(parameters))
            {
                return false;
            }

            var finded = true;
            foreach (var keyValuePair in parameters)
            {
                var valueExpected = parameters[keyValuePair.Key].Split(" ");
                finded &= this[keyValuePair.Key].Split(" ").Intersect(valueExpected).Count()
                       == valueExpected.Length;

                if (!finded)
                {
                    break;
                }
            }
            return finded;
        }
    }
}