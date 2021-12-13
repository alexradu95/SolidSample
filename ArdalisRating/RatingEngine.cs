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
        private Logging Logging = new Logging();

        public decimal Rating { get; set; }
        public void Rate()
        {
            Logging.Log("Starting rate.");

            Logging.Log("Loading policy.");

            var policy = policyReader.ReadPolicy();

            switch (policy.Type)
            {
                case PolicyType.Auto:
                    Logging.Log("Rating AUTO policy...");
                    Logging.Log("Validating policy.");
                    if (String.IsNullOrEmpty(policy.Make))
                    {
                        Logging.Log("Auto policy must specify Make");
                        return;
                    }
                    if (policy.Make == "BMW")
                    {
                        if (policy.Deductible < 500)
                        {
                            Rating = 1000m;
                        }
                        Rating = 900m;
                    }
                    break;

                case PolicyType.Land:
                    Logging.Log("Rating LAND policy...");
                    Logging.Log("Validating policy.");
                    if (policy.BondAmount == 0 || policy.Valuation == 0)
                    {
                        Logging.Log("Land policy must specify Bond Amount and Valuation.");
                        return;
                    }
                    if (policy.BondAmount < 0.8m * policy.Valuation)
                    {
                        Logging.Log("Insufficient bond amount.");
                        return;
                    }
                    Rating = policy.BondAmount * 0.05m;
                    break;

                case PolicyType.Life:
                    Logging.Log("Rating LIFE policy...");
                    Logging.Log("Validating policy.");
                    if (policy.DateOfBirth == DateTime.MinValue)
                    {
                        Logging.Log("Life policy must include Date of Birth.");
                        return;
                    }
                    if (policy.DateOfBirth < DateTime.Today.AddYears(-100))
                    {
                        Logging.Log("Centenarians are not eligible for coverage.");
                        return;
                    }
                    if (policy.Amount == 0)
                    {
                        Logging.Log("Life policy must include an Amount.");
                        return;
                    }
                    int age = DateTime.Today.Year - policy.DateOfBirth.Year;
                    if (policy.DateOfBirth.Month == DateTime.Today.Month &&
                        DateTime.Today.Day < policy.DateOfBirth.Day ||
                        DateTime.Today.Month < policy.DateOfBirth.Month)
                    {
                        age--;
                    }
                    decimal baseRate = policy.Amount * age / 200;
                    if (policy.IsSmoker)
                    {
                        Rating = baseRate * 2;
                        break;
                    }
                    Rating = baseRate;
                    break;

                default:
                    Logging.Log("Unknown policy type");
                    break;
            }

            Logging.Log("Rating completed.");
        }
    }
}
