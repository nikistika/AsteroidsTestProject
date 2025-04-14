using UnityEngine;

namespace SciptableObjects
{
    [CreateAssetMenu(fileName = "NewPoolSizeData", menuName = "PoolSize/NewPoolSizeData")]
    public class PoolSizeSO : ScriptableObject
    {
            [field:SerializeField] public int DefaultPoolSize {get; private set;}
            [field:SerializeField] public int MaxPoolSize {get; private set;}
        }
    }