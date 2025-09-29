using UnityEngine;

public class DisableOnEnable : MonoBehaviour
{
    private MeshRenderer _renderer;
    
    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _renderer.enabled = false;
    }
}
