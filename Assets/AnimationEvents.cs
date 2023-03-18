using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AnimationEvents : MonoBehaviour
{
    [SerializeField] private AnimationEventInfo[] animationEvents;
    public void Invoke(int eventIndex) => animationEvents[eventIndex].unityEvent.Invoke();

    [System.Serializable]
    public struct AnimationEventInfo
    {
        public string name;
        public UnityEvent unityEvent;
    }
}
