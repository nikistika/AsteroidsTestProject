using ModestTree;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace GameLogic
{
    public class BootstrapEntryPoint : IInitializable
    {
        public void Initialize()
        {
            SceneManager.LoadScene(1);
        }
    }
}