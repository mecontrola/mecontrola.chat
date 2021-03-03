using MeControla.Core.Extensions;
using System.Collections.Generic;

namespace MeControla.Core.Tools.Rest
{
    public class Parameters
    {
        private readonly IDictionary<string, string> parameters;

        public Parameters()
        {
            parameters = new Dictionary<string, string>();
        }

        public string this[string key]
        {
            get { return parameters[key]; }
            set { parameters[key] = value; }
        }

        public void Clear()
        {
            parameters.Clear();
        }

        public bool HasAny()
        {
            return parameters.HasAny();
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return parameters.GetEnumerator();
        }
    }
}