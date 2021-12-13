using System;
using System.Collections.Generic;
using System.Text;

namespace ArdalisRating.PolicyRater
{
    public class PolicyRaterFactory
    {
        public Rater Create(Policy policy, RatingEngine engine)
        {
            //switch (policy.Type)
            //{
            //    case PolicyType.Auto:
            //        return new AutoPolicyRater(engine, engine.Logger);
            //    case PolicyType.Land:
            //        return new LandPolicyRater(engine, engine.Logger);
            //    case PolicyType.Life:
            //        return new LifePolicyRater(engine, engine.Logger);
            //}

            //return null;

            try
            {
                return (Rater)Activator.CreateInstance(Type.GetType($"ArdalisRating.{policy.Type}PolicyRater"),
                    new object[] { engine, engine.Logger});
            } catch
            {
                return new UnknownPolicyRater(engine, engine.Logger);
            }
        }
    }
}
