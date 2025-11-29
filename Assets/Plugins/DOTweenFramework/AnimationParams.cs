using System;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class ScaleAnimationParams
{
    [field: SerializeField] public float Delay { get; set; }
    [field: SerializeField] public float Duration { get; set; }
            
    [field: SerializeField] public Vector3 OriginScale { get; set; }
    [field: SerializeField] public Vector3 Scale { get; set; }
    [field: SerializeField] public EaseParams EaseFunc { get; set; }

    public ScaleAnimationParams Clone()
    {
        return (ScaleAnimationParams)MemberwiseClone();
    }
    [Serializable]
    public class EaseParams
    {
        [field: SerializeField] public Ease Func { get; private set; }
        [field: SerializeField] public float Amplitude { get; private set; }
        [field: SerializeField] public float Period { get; private set; }
    }
}