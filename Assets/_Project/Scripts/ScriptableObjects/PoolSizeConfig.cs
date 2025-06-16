using UnityEngine;

namespace _Project.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewPoolSizeData", menuName = "PoolSize/NewPoolSizeData")]
    public class PoolSizeConfig : ScriptableObject
    {
        [field: SerializeField] public int DefaultPoolSize { get; private set; }
        [field: SerializeField] public int MaxPoolSize { get; private set; }
    }
}