using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

using DatReaderWriter;
using DatReaderWriter.Options;
using DatReaderWriter.Lib.IO;
using DatReaderWriter.Lib.IO.BlockAllocators;

namespace ACE.DatLoader
{
    public class CellDatabaseCache : CellDatabase
    {
        private readonly ConcurrentDictionary<uint, IDBObj> _cache = new ConcurrentDictionary<uint, IDBObj>();
        
        public CellDatabaseCache(Action<DatDatabaseOptions> options, IDatBlockAllocator? blockAllocator = null) : base(options, blockAllocator) { }

        public CellDatabaseCache(string datFilePath, DatAccessType accessType = DatAccessType.Read) : base(datFilePath, accessType) { }

        public bool TryReadFileCache<T>(uint fileId, [MaybeNullWhen(false)] out T value) where T : IDBObj
        {
            var success = true;
            if (!_cache.TryGetValue(fileId, out var dbObj))
            {
                if (success = TryReadFile(fileId, out T _dbObj))
                {
                    dbObj = _dbObj;
                    _cache.TryAdd(fileId, dbObj);
                }
            }
            value = (T)dbObj;
            return success;
        }
    }
}
