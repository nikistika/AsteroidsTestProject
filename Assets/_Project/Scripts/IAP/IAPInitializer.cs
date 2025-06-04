using Cysharp.Threading.Tasks;
using GameLogic.Enums;
using GameLogic.SaveLogic.SaveData;
using SaveLogic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace IAP
{
    public class IAPInitializer : IStoreListener
    {
        private readonly ILocalSaveService _localSaveService;
        private readonly ICloudSaveService _cloudSaveService;

        public IStoreController storeController { get; private set; }
        public IExtensionProvider storeExtensionProvider { get; private set; }

        public IAPInitializer(
            ILocalSaveService localSaveService,
            ICloudSaveService cloudSaveService)
        {
            _localSaveService = localSaveService;
            _cloudSaveService = cloudSaveService;
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
                SaveConfig data = _localSaveService.GetData();
                if (data.AdsRemoved == false)
                {
                    data.AdsRemoved = true;
                    _localSaveService.SetData(data);
                    _cloudSaveService.SaveData(data).Forget();
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