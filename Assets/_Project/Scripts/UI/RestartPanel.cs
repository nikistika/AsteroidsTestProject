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
            _scoreText.text = $"Score: {score}";
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}