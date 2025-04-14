using UnityEngine;

namespace Coroutine
{
    public class CoroutinePerformer : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

    }
}

