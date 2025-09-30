using UnityEngine;

public class Element : MonoBehaviour
{
    private void Awake()
    {
        Manager.Instance.OnElementSpawned(gameObject);
    }
}