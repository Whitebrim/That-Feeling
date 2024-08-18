using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using Utils.Extensions;

namespace Core.Services.AssetManagement
{
    /// <summary>
    /// Provides a cache mechanism for managing assets loaded via <see cref="AddressablesLoader"/>.
    /// </summary>
    public class AddressablesCache
    {
        /// <summary>
        /// Singleton instance of the <see cref="AddressablesCache"/> class.
        /// </summary>
        public static AddressablesCache Instance => LazyLoader.Value;
        private static readonly Lazy<AddressablesCache> LazyLoader = new Lazy<AddressablesCache>(() => new AddressablesCache());
        private readonly Dictionary<string, HashSet<AssetReference>> _cache = new();

        /// <summary>
        /// Adds an asset reference to the cache under the specified release key.
        /// </summary>
        /// <param name="assetReference">The asset reference to be cached.</param>
        /// <param name="releaseKey">The key under which the asset reference is cached, used for future resource release.</param>
        public void AddAsset(AssetReference assetReference, string releaseKey)
        {
            _cache.TryAdd(releaseKey, new HashSet<AssetReference>());
            _cache[releaseKey].Add(assetReference);
        }
        
        /// <summary>
        /// Releases all assets associated with the specified release key and removes them from the cache.
        /// </summary>
        /// <param name="releaseKey">The key associated with the assets to be released.</param>
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