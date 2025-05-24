using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewEnemySpawnData", menuName = "EnemySpawnManager/NewEnemySpawnData")]
    public class EnemySpawnManagerSO : ScriptableObject
    {
        [field: SerializeField] public float RespawnRange { get; private set; }
    }
}