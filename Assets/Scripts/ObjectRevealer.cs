using UnityEngine;

[ExecuteAlways]
public class ObjectRevealer : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float reveal = 0f;
    [SerializeField] private Vector3 direction = Vector3.up;

    public float Reveal
    {
        get => reveal;
        set => reveal = value;
    }

    void Update()
    {
        var renderer = GetComponent<Renderer>();
        if (!renderer) return;

        var bounds = renderer.localBounds;

        foreach (var mat in renderer.sharedMaterials)
        {
            mat.SetVector("_BoundsMin", bounds.min);
            mat.SetVector("_BoundsMax", bounds.max);
            mat.SetFloat("_Reveal", reveal);
            mat.SetVector("_Direction", direction);
        }
    }
}