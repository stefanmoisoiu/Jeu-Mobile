using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private AnimInfo[] animations;
    private AnimInfo currentAnimation;
    private List<AnimInfo> nextAnimations = new();
    private void Start()
    {
        anim = GetComponent<Animator>();
        currentAnimation = animations[0];
    }
    private void Update()
    {
        CheckAnimationToPlay();
    }
    private void CheckAnimationToPlay()
    {
        if (currentAnimation.mustPlayFullAnimation && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) return;
        if (nextAnimations.Count == 0) return;

        anim.Play(nextAnimations[0].name);
        currentAnimation = nextAnimations[0];
        nextAnimations.RemoveAt(0);
    }
    public void PlayAnimation(int index)
    {
        if (animations[index] == currentAnimation) return;
        nextAnimations.Add(animations[index]);
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
