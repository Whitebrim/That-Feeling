using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using Utils.Extensions;

namespace Core.Services.AssetManagement
{
    /// <summary>
    /// Хранит кэш для <see cref="AddressablesLoader"/>
    /// </summary>
    public class AddressablesCache
    {
        private static readonly Lazy<AddressablesCache> LazyLoader = new Lazy<AddressablesCache>(() => new AddressablesCache());
        public static AddressablesCache Instance => LazyLoader.Value;
        private readonly Dictionary<string, HashSet<AssetReference>> _cache = new();

        public void AddAsset(AssetReference assetReference, string releaseKey)
        {
            _cache.TryAdd(releaseKey, new HashSet<AssetReference>());
            _cache[releaseKey].Add(assetReference);
        }
        
        public void ReleaseAssets(string releaseKey)
        {
            if (!_cache.TryGetValue(releaseKey, out var cacheScope)) return;
            
            foreach (var asset in cacheScope)
            {
                asset.ReleaseAsset();
            }

            _cache.Remove(releaseKey);
        }
    }
}