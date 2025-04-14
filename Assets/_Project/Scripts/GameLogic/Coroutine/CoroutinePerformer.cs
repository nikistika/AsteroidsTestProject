using _Project.Scripts.GameLogic.Coroutine;
using UnityEngine;

namespace Coroutine
{
    public class CoroutinePerformer : MonoBehaviour, ICoroutinePerformer
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

    }
}

