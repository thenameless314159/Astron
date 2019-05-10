using System;

namespace Astron.IoC.Enums
{
    [Flags]
    public enum RegistrationMode
    {
        New,
        Singleton,
        NewWithDep,
        NewWithValues,
        NewWithValuesAndDep
    }
}
