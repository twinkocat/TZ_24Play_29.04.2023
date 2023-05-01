using UnityEngine;

public class StickmanAnimatorScript : MonoBehaviour
{
    public static StickmanAnimatorScript s_StickmanAnimatorScript;
    private Animator _animator;

    private void Awake()
    {
        if (s_StickmanAnimatorScript != null)
        {
            print("StickmanAnimatorScript: Instance already set. It should be Singleton");
            return;
        }
        s_StickmanAnimatorScript = this;
        _animator = GetComponent<Animator>();
    }

    public void AnimatorTriggerJump()
    {
        // have bugs 
        //_animator.SetTrigger("Jump");
    }
}
