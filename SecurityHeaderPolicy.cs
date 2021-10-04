using System.Collections.Generic;

namespace AspNetCore.SecurityHeader
{
    public class SecurityHeaderPolicy
    {
        public IDictionary<string, IList<string>> AddedHeaders { get; private set; } = new Dictionary<string, IList<string>>();
        public IList<string> RemovedHeaders { get; private set; } = new List<string>();

        public void AddHeader(string key, string value)
        {
            if (AddedHeaders.ContainsKey(key) && !AddedHeaders[key].Contains(value))
                AddedHeaders[key].Add(value);
            else
                AddedHeaders.Add(key, new List<string> { value });
        }

        public void AddHeader(string key, string value, bool allowMultiple)
        {
            if (allowMultiple)
            {
                AddHeader(key, value);
                return;
            }
            if (AddedHeaders.ContainsKey(key))
                AddedHeaders[key] = new List<string> { value };
            else
                AddedHeaders.Add(key, new List<string> { value });
        }

        public void RemoveHeader(string key)
        {
            if (!RemovedHeaders.Contains(key))
                RemovedHeaders.Add(key);
        }
    }
}
