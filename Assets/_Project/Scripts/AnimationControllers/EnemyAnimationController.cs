using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.AnimationControllers
{
    public class EnemyAnimationController : MonoBehaviour
    {
        private const string ExplosionAnimation = "Explosion";

        [SerializeField] private Animator animator;

        public async UniTask ActivateExplosion()
        {
            if (!animator) return;

            animator.SetBool(ExplosionAnimation, true);

            try
            {
                while (animator != null &&
                       (!animator.GetCurrentAnimatorStateInfo(0).IsName(ExplosionAnimation) ||
                        animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f))
                {
                    await UniTask.Yield();
                }
            }
            catch (MissingReferenceException)
            {
                Debug.Log("Characters.EnemyAnimationController: Animator was destroyed during explosion animation");
            }
        }
    }
}