using System;

using static War3Api.Common;

namespace War3Lib.Mmd
{
    /// <summary>
    /// Stores previously sent messages for tamper detection purposes.
    /// </summary>
    internal class Node : IDisposable
    {
        private readonly string _key;
        private readonly string _message;
        private readonly float _timeout;
        private readonly int _checksum;

        public Node(int id, string message, float timeout, int checksum)
        {
            _key = $"{id}";
            _message = message;
            _timeout = timeout;
            _checksum = checksum;
        }

        public string Key => _key;

        public string Message => _message;

        /// <summary>
        /// Wait until game time passed this timeout before checking for tampering.
        /// </summary>
        public float Timeout => _timeout;

        public int Checksum => _checksum;

        public bool Send()
        {
            var val = Mmd.KeyVal + _key;
            var chk = Mmd.KeyChk + _key;
            StoreInteger(Mmd.Gamecache, val, _message, _checksum);
            StoreInteger(Mmd.Gamecache, chk, _key, _checksum);
            SyncStoredInteger(Mmd.Gamecache, val, _message);
            SyncStoredInteger(Mmd.Gamecache, chk, _key);

            return true;
        }

        public void Dispose()
        {
            FlushStoredInteger(Mmd.Gamecache, Mmd.KeyVal + _key, _message);
            FlushStoredInteger(Mmd.Gamecache, Mmd.KeyChk + _key, _key);
        }
    }
}