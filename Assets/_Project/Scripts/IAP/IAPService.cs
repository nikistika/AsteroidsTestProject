using _Project.Scripts.Enums;
using _Project.Scripts.Save;
using Zenject;

namespace _Project.Scripts.IAP
{
    public class IAPService : IInitializable, IIAPService
    {
        private readonly ISaveService _saveService;

        private IAPTransactionListener _iapTransactionListener;

        public IAPService(
            ISaveService saveService)
        {
            _saveService = saveService;
        }

        public void Initialize()
        {
            _iapTransactionListener = new IAPTransactionListener();
            _iapTransactionListener.Initialize();
            _iapTransactionListener.OnPurchaseCompleted += HandlePurchaseCompleted;
        }

        public void MakePurchase(string productId)
        {
            if (_iapTransactionListener.StoreController != null)
            {
                if (productId == IAPID.RemoveAds.ToString())
                {
                    if (_saveService.CurrentSaveData.AdsRemoved == false)
                    {
                        _iapTransactionListener.StoreController.InitiatePurchase(productId);
                    }
                }
                else
                {
                    _iapTransactionListener.StoreController.InitiatePurchase(productId);
                }
            }
        }

        private void HandlePurchaseCompleted(string productId)
        {
            if (productId == IAPID.RemoveAds.ToString())
            {
                RemoveAds();
            }
        }

        private void RemoveAds()
        {
            if (_saveService.CurrentSaveData.AdsRemoved == false)
            {
                _saveService.CurrentSaveData.AdsRemoved = true;
                _saveService.SaveData(_saveService.CurrentSaveData);
            }
        }
    }
}