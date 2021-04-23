using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Tutorial used: https://www.youtube.com/watch?v=Ll3yujn9GVQ
public class ScaleTween : MonoBehaviour
{
    public LeanTweenType inType;
    public LeanTweenType outType;
    public float duration;
    public float delay;
    public UnityEvent onCompleteCallback;

    private void OnEnable()
    {
        transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), duration).setDelay(delay).setOnComplete(OnComplete).setEase(inType);
    }

    public void OnComplete()
    {
        if (onCompleteCallback != null) { onCompleteCallback.Invoke(); }
    }

    public void OnClose()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), duration).setEase(outType).setOnComplete(DestroyMe);
    }

    void DestroyMe() { Destroy(gameObject); }
}
