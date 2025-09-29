using UnityEngine;
using UnityEngine.UI;

public class POI : MonoBehaviour
{
    [SerializeField] private GameObject visualizer;
    [SerializeField] private Transform root;
    [SerializeField] private float tweenTime = 2;
    [SerializeField] private LeanTweenType tweenType = LeanTweenType.easeInOutCubic;

    private Camera _mainCamera;

    private void Awake()
    {
        visualizer.SetActive(false);
        GetComponentInChildren<Button>().onClick.AddListener(SetView);
    }

    private void SetCamera()
    {
        _mainCamera = Camera.main;
    }

    private void SetView()
    {
        if (!_mainCamera)
            SetCamera();

        Vector3 offset = root.position - visualizer.transform.position;
        LeanTween.move(root.gameObject, _mainCamera.transform.position + offset, tweenTime).setEase(tweenType)
            .setOnComplete(() =>
            {
                float angle = _mainCamera.transform.eulerAngles.y - visualizer.transform.eulerAngles.y;

                // starting rotation
                Quaternion startRot = root.rotation;

                // final rotation: rotate root around pivot by targetAngle
                Quaternion endRot = Quaternion.AngleAxis(angle, Vector3.up) * startRot;

                // store pivot offset
                Vector3 pivotOffset = root.position - visualizer.transform.position;

                // tween with LeanTween
                LeanTween.value(gameObject, 0f, 1f, tweenTime)
                    .setEase(LeanTweenType.easeInOutSine)
                    .setOnUpdate(t =>
                    {
                        // interpolate rotation
                        Quaternion rot = Quaternion.Slerp(startRot, endRot, t);

                        // apply rotation to offset
                        Vector3 newPos = visualizer.transform.position +
                                         rot * Quaternion.Inverse(startRot) * pivotOffset;

                        root.SetPositionAndRotation(newPos, rot);
                    });
            });
    }
}