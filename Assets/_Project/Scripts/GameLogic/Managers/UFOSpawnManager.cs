using System.Collections;
using Characters;
using Factories;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class UFOSpawnManager : BaseSpawnManager<UFO>
    {
        private WaitForSeconds _waitRespawnUFORange;

        [SerializeField] private UFOFactory _ufoFactory;
        [SerializeField] private float _minRespawnUFORange = 5;
        [SerializeField] private float _maxRespawnUFORange = 10;

        private void Start()
        {
            _waitRespawnUFORange = new WaitForSeconds(Random.Range(_minRespawnUFORange, _maxRespawnUFORange));
            StartCoroutine(SpawnUFOCoroutine());
        }

        protected override UFO SpawnObject()
        {
            var ufo = _ufoFactory.SpawnObject();
            ufo.OnReturnUFO += ReturnUFO;
            return ufo;
        }

        private void ReturnUFO(UFO ufo)
        {
            ufo.OnReturnUFO -= ReturnUFO;
            _ufoFactory.ReturnObject(ufo);
        }

        private IEnumerator SpawnUFOCoroutine()
        {
            while (!_flagGameOver)
            {
                SpawnObject();
                yield return _waitRespawnUFORange;
            }
        }
    }
}