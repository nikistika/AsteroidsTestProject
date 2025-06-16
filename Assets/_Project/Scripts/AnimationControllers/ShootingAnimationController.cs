using System.Collections;
using UnityEngine;

namespace _Project.Scripts.AnimationControllers
{
    public class ShootingAnimationController : MonoBehaviour
    {
        private const string ShootingAnimation = "Shooting";

        [SerializeField] private Animator animator;

        public void ActivateShooting()
        {
            StartCoroutine(PlayShootAnimationOnce());
        }
        
        private IEnumerator PlayShootAnimationOnce()
        {
            animator.SetBool(ShootingAnimation, true);

            yield return new WaitUntil(() =>
                animator.GetCurrentAnimatorStateInfo(0).IsName(ShootingAnimation));
            
            yield return new WaitUntil(() =>
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

            animator.SetBool(ShootingAnimation, false);
            Debug.Log("Shootnig.ShootingAnimationController: Shooting animation completed.");
        }
    }
}