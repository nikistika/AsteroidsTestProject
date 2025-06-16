using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.MenuScene
{
    public class MenuUIView : MonoBehaviour
    {
        public event Action OnStartGame;
        public event Action OnRemoveAds;
        public event Action OnQuitGame;
        
        [SerializeField] private Image RemoveAdsPanel;
        [SerializeField] private Button RemoveAdsButton;
        [SerializeField] private Button BuyButton;
        [SerializeField] private Button ExitButton;

        public void StartGame()
        {
            OnStartGame?.Invoke();
        }

        public void OpenRemoveAdsPanel()
        {
            RemoveAdsPanel.gameObject.SetActive(true);
        }

        public void CloseRemoveAdsPanel()
        {
            RemoveAdsPanel.gameObject.SetActive(false);
        }

        public void RemoveAds()
        {
            OnRemoveAds?.Invoke();
        }

        public void HideRemoveAdsButton()
        {
            RemoveAdsButton.interactable = false;
            BuyButton.interactable = false;
        }
        
        public void QuitGame()
        {
            OnQuitGame?.Invoke();
        }
    }
}