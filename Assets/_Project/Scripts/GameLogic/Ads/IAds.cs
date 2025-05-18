using Cysharp.Threading.Tasks;

namespace GameLogic.Ads
{
    public interface IAds
    {
        
        public void Initialize();

        public void LoadAd();

        public void ShowAd();

        public void LoadRewardedAd();
        public UniTask<bool> ShowRewardedAds();
    }
}