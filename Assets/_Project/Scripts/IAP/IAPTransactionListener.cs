using System;
using _Project.Scripts.Enums;
using UnityEngine;
using UnityEngine.Purchasing;

namespace _Project.Scripts.IAP
{
    public class IAPTransactionListener : IStoreListener
    {
        public event Action<string> OnPurchaseCompleted;

        public IStoreController StoreController { get; private set; }
        public IExtensionProvider StoreExtensionProvider { get; private set; }

        public void Initialize()
        {
            var module = StandardPurchasingModule.Instance();

#if UNITY_EDITOR
            module.useFakeStoreUIMode = FakeStoreUIMode.StandardUser;
#endif

            var builder = ConfigurationBuilder.Instance(module);
            builder.AddProduct(IAPID.RemoveAds.ToString(), ProductType.NonConsumable);

            UnityPurchasing.Initialize(this, builder);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            StoreController = controller;
            StoreExtensionProvider = extensions;
            Debug.Log("IAP Initialized");
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.LogError("IAP Init Failed: " + error);
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.LogError($"IAP Init Failed: {error}, Message: {message}");
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            Debug.Log("Purchase Successful: " + args.purchasedProduct.definition.id);
            OnPurchaseCompleted?.Invoke(args.purchasedProduct.definition.id);
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.LogWarning("Purchase Failed: " + failureReason);
        }
    }
}