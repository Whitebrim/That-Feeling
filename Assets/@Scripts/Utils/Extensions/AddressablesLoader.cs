using System.Threading.Tasks;
using Core.Services.AssetManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Utils.Extensions
{
    public static class AddressablesLoader
    {
        /// <summary>
        /// Возвращает закэшированный ассет или грузит его и кэширует
        /// </summary>
        public static T LoadAndCache<T>(this AssetReference asset, string releaseKey) where T : Object
        {
            if (asset.IsValid())
                return asset.Asset as T;

            AddressablesCache.Instance.AddAsset(asset, releaseKey);
            return asset.LoadAssetAsync<T>().WaitForCompletion();
        }

        /// <summary>
        /// Возвращает закэшированный ассет или грузит его и кэширует
        /// </summary>
        public static T LoadAndCache<T>(this AssetReferenceT<T> asset, string releaseKey) where T : Object =>
            LoadAndCache<T>((AssetReference)asset, releaseKey);

        /// <summary>
        /// Возвращает закэшированный ассет или ассинхронно грузит его и кэширует
        /// </summary>
        public static async UniTask<T> LoadAndCacheAsync<T>(this AssetReference asset, string releaseKey) where T : Object
        {
            if (asset.IsValid())
                return asset.Asset as T;

            AddressablesCache.Instance.AddAsset(asset, releaseKey);
            return await asset.LoadAssetAsync<T>();
        }

        /// <summary>
        /// Возвращает закэшированный ассет или ассинхронно грузит его и кэширует
        /// </summary>
        public static async UniTask<T> LoadAndCacheAsync<T>(this AssetReferenceT<T> asset, string releaseKey) where T : Object => 
            await LoadAndCacheAsync<T>((AssetReference)asset, releaseKey);
    }
}