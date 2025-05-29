using GameLogic.Enums;
using GameLogic.SaveLogic.SaveData;
using UnityEngine;
using Zenject;

namespace IAP
{
    public class IAPService : IInitializable
    {
        private readonly SaveController _saveController;

        private IAPInitializer _iapInitializer;

        public IAPService(
            SaveController saveController)
        {
            _saveController = saveController;
        }

        public void Initialize()
        {
            _iapInitializer = new IAPInitializer(_saveController);
            _iapInitializer.Initialize();
        }

        public void RemoveAds()
        {
            if (_iapInitializer.storeController != null && _saveController.GetData().AdsRemoved == false)
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