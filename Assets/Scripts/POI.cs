using UnityEngine;
using UnityEngine.UI;

public class POI : MonoBehaviour
{
    [SerializeField] private GameObject visualizer;
    [SerializeField] private Transform root;
    [SerializeField] private float tweenTime = 2;
    [SerializeField] private LeanTweenType tweenType = LeanTweenType.easeInOutCubic;

    private void Awake()
    {
        visualizer.SetActive(false);
        GetComponentInChildren<Button>().onClick.AddListener(SetView);
    }

    private void SetView()
    {
        float angle = visualizer.transform.eulerAngles.y + Camera.main.transform.eulerAngles.y;
        // LeanTween.rotateY(root.gameObject, angle, tweenTime).setEase(tweenType);
        Vector3 offset = root.position - visualizer.transform.position;
        LeanTween.move(root.gameObject, Camera.main.transform.position + offset, tweenTime).setEase(tweenType);
    }
}