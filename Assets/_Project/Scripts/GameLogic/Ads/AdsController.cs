using Cysharp.Threading.Tasks;
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

        public void LoadRewardedAd()
        {
            _ads.LoadRewardedAd();
        }

        //TODO: На кнопке "Продолжить" походу висит не тот метод (ShowAd()). Переделать.
        public async UniTask<bool> ShowAdGetReward()
        {
            return await _ads.ShowRewardedAds();
        }
    }
}