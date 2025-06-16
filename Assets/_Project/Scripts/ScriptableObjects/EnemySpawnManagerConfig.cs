using UnityEngine;

namespace _Project.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewEnemySpawnData", menuName = "EnemySpawnManager/NewEnemySpawnData")]
    public class EnemySpawnManagerConfig : ScriptableObject
    {
        [field: SerializeField] public float RespawnRange { get; private set; }
    }
}