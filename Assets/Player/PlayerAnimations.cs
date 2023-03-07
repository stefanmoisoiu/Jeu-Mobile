using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private AnimInfo[] animations;
    private AnimInfo currentAnimation, nextAnimation;
    private void Start()
    {
        anim = GetComponent<Animator>();
        currentAnimation = animations[0];
        nextAnimation = animations[0];
    }
    private void Update()
    {
        CheckAnimationToPlay();
    }
    private void CheckAnimationToPlay()
    {
        if (currentAnimation.mustPlayFullAnimation && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) return;
        if (nextAnimation == currentAnimation) return;

        anim.Play(nextAnimation.name);
        currentAnimation = nextAnimation;
    }
    public void PlayAnimation(int index)
    {
        if (animations[index] == currentAnimation) return;
        nextAnimation = animations[index];
    }
    [System.Serializable]
    public class AnimInfo
    {
        public string name;
        public bool mustPlayFullAnimation;
        public AnimInfo(string name, bool mustPlayFullAnimation = false)
        {
            this.name = name;
            this.mustPlayFullAnimation = mustPlayFullAnimation;
        }
    }
}
