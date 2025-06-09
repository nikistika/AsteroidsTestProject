using _Project.Scripts.Enums;
using _Project.Scripts.Save;
using GameLogic.SaveLogic.SaveData;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.IAP
{
    public class IAPService : IInitializable, IIAPService
    {
        private readonly ISaveService _saveService;

        private IAPInitializer _iapInitializer;

        public IAPService(
            ISaveService saveService)
        {
            _saveService = saveService;
        }

        public void Initialize()
        {
            _iapInitializer = new IAPInitializer(_saveService);
            _iapInitializer.Initialize();
        }

        public void RemoveAds()
        {
            if (_iapInitializer.storeController != null && _saveService.CurrentSaveData.AdsRemoved == false)
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