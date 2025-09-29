using UnityEngine;

public class RevealOnEnable : MonoBehaviour
{
    [SerializeField] private ObjectRevealer revealer;
    [SerializeField] private float tweenTime = 3;

    private void Start()
    {
        LeanTween.value(0, 1, tweenTime).setOnUpdate(x => revealer.Reveal = x);
    }
}
