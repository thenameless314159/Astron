namespace Astron.IoC.Cache
{
    internal static class MappedInstanceCache<T>
    {
        private static int _typeId;
        private static bool _isAlreadyRegistered;

        public static int Value
        {
            get => _typeId;
            set
            {
                if (_isAlreadyRegistered)
                    return;

                _typeId = value;
                _isAlreadyRegistered = true;
            }
        }

        public static bool IsEmpty { get; set; }
    }
}
