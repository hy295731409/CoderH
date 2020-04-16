using Framework.DB.IdGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DB.Utility.Helper
{
    public class IdHelper
    {
        public static IdHelper Instance => (Singleton<IdHelper>.Instance ?? (Singleton<IdHelper>.Instance = new IdHelper()));

        private static readonly IdWorker IdWorker = new IdWorker(1, 1);

        public long LongId => IdWorker.NextId();

        public Guid Guid => System.Guid.NewGuid();

        public string Guid32 => System.Guid.NewGuid().ToString("N");

        public string GuidStr => System.Guid.NewGuid().ToString();
    }
}
