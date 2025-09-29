using System.Collections;
using UnityEngine;

public class HideOnEnable : MonoBehaviour
{
    [SerializeField] private ObjectRevealer revealer;
    [SerializeField] private float tweenTime = 5;

    private void OnEnable()
    {
        StartCoroutine(Reveal());
    }

    private IEnumerator Reveal()
    {
        revealer.Reveal = 1;

        float time = Time.time;

        while (Time.time - time < tweenTime)
        {
            revealer.Reveal = 1 - (Time.time - time / tweenTime);
            
            yield return null;
        }

        revealer.Reveal = 0;
    }
}