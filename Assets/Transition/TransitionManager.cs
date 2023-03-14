using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public delegate void OnTransitionComplete();
    [SerializeField] private AnimationClip transitionIn, transitionOut;
    private Animator animator;
    public enum TransitionType { In, Out }

    private void Awake() => animator = GetComponentInChildren<Animator>();
    private void Start() => PlayTransition(TransitionType.Out);
    public void PlayTransition(TransitionType transitionType, string sceneToLoadOnEnd = null, OnTransitionComplete onTransitionComplete = null)
    {
        AnimationClip clip = transitionIn;
        switch (transitionType)
        {
            case TransitionType.In:
                clip = transitionIn;
                break;
            case TransitionType.Out:
                clip = transitionOut;
                break;
        }
        StartCoroutine(PlayTransitionCoroutine(clip, sceneToLoadOnEnd, onTransitionComplete));
    }
    private IEnumerator PlayTransitionCoroutine(AnimationClip clip, string sceneToLoadOnEnd = null, OnTransitionComplete onTransitionComplete = null)
    {
        animator.Play(clip.name);
        yield return new WaitForSeconds(clip.length);
        if (sceneToLoadOnEnd != null) SceneManager.LoadScene(sceneToLoadOnEnd);
        onTransitionComplete?.Invoke();
    }

}
