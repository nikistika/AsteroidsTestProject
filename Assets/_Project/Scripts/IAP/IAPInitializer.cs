using _Project.Scripts.Enums;
using _Project.Scripts.Save;
using UnityEngine;
using UnityEngine.Purchasing;

namespace _Project.Scripts.IAP
{
    public class IAPInitializer : IStoreListener
    {
        private readonly ISaveService _saveService;

        public IStoreController storeController { get; private set; }
        public IExtensionProvider storeExtensionProvider { get; private set; }

        public IAPInitializer(
            ISaveService saveService)
        {
            _saveService = saveService;
        }

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
            storeController = controller;
            storeExtensionProvider = extensions;
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

            if (args.purchasedProduct.definition.id == IAPID.RemoveAds.ToString())
            {
                if (_saveService.CurrentSaveData.AdsRemoved == false)
                {
                    _saveService.CurrentSaveData.AdsRemoved = true;
                    _saveService.SaveData(_saveService.CurrentSaveData);
                }
            }

            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.LogWarning("Purchase Failed: " + failureReason);
        }
    }
}