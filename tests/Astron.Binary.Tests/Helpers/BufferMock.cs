using System;

namespace Astron.Binary.Tests.Helpers
{
    public static class BufferMock
    {
        private static byte[] _buff;

        public static byte[] Buffer
        {
            get
            {
                if (_buff != null) return _buff;

                _buff = new byte[1024];
                var rdm = new Random();
                for (var i = 0; i < _buff.Length; i++)
                {
                    _buff[i] = (byte)rdm.Next(0, 255);
                }
                return _buff;
            }
        }
    }
}
