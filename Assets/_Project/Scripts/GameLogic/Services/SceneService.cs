using Service;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.GameLogic.Services
{
    public class SceneService : ISceneService
    {
        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void GoToMenu()
        {
            SceneManager.LoadScene(1);
        }
        
        public void GoToGame()
        {
            SceneManager.LoadScene(2);
        }
    }
}