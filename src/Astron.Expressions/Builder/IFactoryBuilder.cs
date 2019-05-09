using System;
using System.Collections.Generic;
using System.Text;

namespace Astron.Expressions.Builder
{
    public interface IFactoryBuilder<in TKey, in TVal, out TFact> : IBuilder<TFact>
    {
        IFactoryBuilder<TKey, TVal, TFact> Register(TKey key, TVal val);
    }
}
