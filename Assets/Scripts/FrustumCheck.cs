using UnityEngine;

public class FrustumCheck : MonoBehaviour
{
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    public bool IsInCamera(Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);

        return GeometryUtility.TestPlanesAABB(planes, _renderer.bounds);
    }
}