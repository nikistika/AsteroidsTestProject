using GameLogic.Ads.Unity_ads;
using Zenject;

namespace GameLogic.Ads
{
    public class AdsController : IInitializable
    {
        
        private IAds _ads;
        
        public void Initialize()
        {
            _ads = new UnityAdsService();
            _ads.Initialize();
        }
        
        public void LoadAd()
        {
            _ads.LoadAd();
        }
        
        public void ShowAd()
        {
            _ads.ShowAd();
        }
        
        //TODO: На кнопке "Продолжить" походу висит не тот метод (ShowAd()). Переделать.
        public void ShowAdAdGetReward()
        {
            _ads.ShowRewardedAds();
        }
        
        
    }
}