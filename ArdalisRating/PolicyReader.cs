using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ArdalisRating
{
    internal class PolicyReader
    {

        public Policy ReadPolicy()
        {
            // load policy - open file policy.json
            string policyJson = File.ReadAllText("policy.json");

            return JsonConvert.DeserializeObject<Policy>(policyJson,
                new StringEnumConverter());
        }

    }
}
