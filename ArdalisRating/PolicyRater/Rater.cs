using System;
using System.Collections.Generic;
using System.Text;

namespace ArdalisRating.PolicyRater
{
    public abstract class Rater
    {
        protected readonly RatingEngine _engine;
        protected readonly Logging _logger;

        public Rater(RatingEngine engine, Logging logger)
        {
            _engine = engine;
            _logger = logger;

        }

        public abstract void Rate(Policy policy);

    }
}
