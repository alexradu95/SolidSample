using ArdalisRating.PolicyRater;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;

namespace ArdalisRating
{
    /// <summary>
    /// The RatingEngine reads the policy application details from a file and produces a numeric 
    /// rating value based on the details.
    /// </summary>
    public class RatingEngine
    {

        private PolicyReader policyReader = new PolicyReader();
        public Logging Logger = new Logging();
        public decimal Rating { get; set; }
        public void Rate()
        {
            Logger.Log("Starting rate.");

            Logger.Log("Loading policy.");

            var policy = policyReader.ReadPolicy();

            var factory = new PolicyRaterFactory();

            var rater = factory.Create(policy, this);

            rater?.Rate(policy);


            Logger.Log("Rating completed.");
        }
    }
}
