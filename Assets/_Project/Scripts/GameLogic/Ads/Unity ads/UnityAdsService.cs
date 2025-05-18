using Cysharp.Threading.Tasks;

namespace GameLogic.Ads.Unity_ads
{
    public class UnityAdsService : IAds
    {
        
        private InterstitialAdExample _interstitialAdExample;
        private RewardedAdsButton _rewardedAdsButton;
        
        public void Initialize()
        {
            _interstitialAdExample = new InterstitialAdExample();
            _rewardedAdsButton = new RewardedAdsButton();
            
            _interstitialAdExample.Initialize();
            _rewardedAdsButton.Initialize();
        }
        
        public void LoadAd()
        {
            _interstitialAdExample.LoadAd();
        }
        
        public void ShowAd()
        {
            _interstitialAdExample.ShowAd();
        }
        
        //TODO: Этот метод походу не выполняется 
        public void LoadRewardedAd()
        {
            _rewardedAdsButton.LoadAd();
        }
        
        //TODO: На кнопке "Продолжить" походу висит не тот метод (ShowAd()). Переделать.
        public async UniTask<bool> ShowRewardedAds()
        {
            _rewardedAdsButton.ShowAd();
        }
    }
}