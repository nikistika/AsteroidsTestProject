using UnityEngine;

namespace SciptableObjects
{
    [CreateAssetMenu(fileName = "NewEnemySpawnData", menuName = "EnemySpawnManager/NewEnemySpawnData")]
    public class EnemySpawnManagerSO : ScriptableObject
    {
        [field: SerializeField] public float RespawnRange { get; private set; }
    }
}