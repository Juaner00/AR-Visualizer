using UnityEngine;

public class POI : MonoBehaviour
{
    [SerializeField] private GameObject visualizer;
    [SerializeField] private Transform root;

    private void Awake()
    {
        visualizer.SetActive(false);
    }

    public void SetView()
    {
        Vector3.RotateTowards()
    }
}
