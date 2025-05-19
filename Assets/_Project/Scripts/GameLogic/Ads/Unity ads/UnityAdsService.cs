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
        
        public void LoadRewardedAd()
        {
            _rewardedAdsButton.LoadAd();
        }
        
        public async UniTask<bool> ShowRewardedAds()
        {
            return await _rewardedAdsButton.ShowAd();
        }
    }
}