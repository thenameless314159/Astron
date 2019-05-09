using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Astron.Logging.Strategy;

namespace Astron.Logging
{
    public partial class Logger
    {
        public static Builder CreateBuilder() => new Builder();

        public class Builder
        {
            private readonly ImmutableArray<ILoggingStrategy>.Builder _builder;

            public Builder() => _builder = ImmutableArray.CreateBuilder<ILoggingStrategy>();

            public Builder Register(ILoggingStrategy strategy)
            { 
                _builder.Add(strategy);
                return this;
            }
            public Builder Register(params ILoggingStrategy[] strategies)
            { 
                _builder.AddRange(strategies);
                return this;
            }

            public Logger Build()
            {
                var logger = new Logger(_builder.ToImmutable());
                _builder.Clear();
                return logger;
            }
        }
    }
}
