using System.Collections;
using UnityEngine;

public class RevealOnEnable : MonoBehaviour
{
    [SerializeField] private ObjectRevealer revealer;
    [SerializeField] private float tweenTime = 5;

    private void OnEnable()
    {
        StartCoroutine(Reveal());
    }

    private IEnumerator Reveal()
    {
        revealer.Reveal = 0;

        float time = Time.time;

        while (Time.time - time < tweenTime)
        {
            revealer.Reveal = Time.time - time / tweenTime;
            
            yield return null;
        }

        revealer.Reveal = 1;
    }
}
