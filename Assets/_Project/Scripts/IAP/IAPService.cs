using GameLogic.Enums;
using GameLogic.SaveLogic.SaveData;
using SaveLogic;
using UnityEngine;
using Zenject;

namespace IAP
{
    public class IAPService : IInitializable, IIAPService
    {
        private readonly ILocalSaveService _localSaveService;
        private readonly ICloudSaveService _cloudSaveService;

        private IAPInitializer _iapInitializer;

        public IAPService(
            ILocalSaveService localSaveService,
            ICloudSaveService cloudSaveService)
        {
            _localSaveService = localSaveService;
            _cloudSaveService = cloudSaveService;
        }

        public void Initialize()
        {
            _iapInitializer = new IAPInitializer(_localSaveService, _cloudSaveService);
            _iapInitializer.Initialize();
        }

        public void RemoveAds()
        {
            if (_iapInitializer.storeController != null && _localSaveService.GetData().AdsRemoved == false)
            {
                _iapInitializer.storeController.InitiatePurchase(IAPID.RemoveAds.ToString());
            }
            else
            {
                Debug.LogWarning("Store not initialized.");
            }
        }
    }
}