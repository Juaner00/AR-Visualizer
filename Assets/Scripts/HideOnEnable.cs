using UnityEngine;

public class HideOnEnable : MonoBehaviour
{
    [SerializeField] private ObjectRevealer revealer;
    [SerializeField] private float tweenTime = 3;

    private void Start()
    {
        LeanTween.value(1, 0, tweenTime).setOnUpdate(x => revealer.Reveal = x);
    }
}