using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class RestartPanel : MonoBehaviour
    {
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}