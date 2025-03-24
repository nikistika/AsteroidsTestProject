using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class RestartPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        public void ActivateRestartPanel(int score)
        {
            Time.timeScale = 0;
            _scoreText.text = $"Score: {score}";
        }

        public void RestartGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}