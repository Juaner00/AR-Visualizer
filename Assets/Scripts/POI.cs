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

        // --- Define Start and End States ---

        // Initial state of the root
        Vector3 startPos = root.position;
        Quaternion startRot = root.rotation;

        // The pivot for the rotation is the visualizer's position
        Vector3 pivotPos = visualizer.transform.position;

        // The offset from the pivot to the root
        Vector3 offset = startPos - pivotPos;

        // --- Calculate Final State ---

        // The final desired position for the root
        Vector3 endPos = _mainCamera.transform.position + offset;

        // The final desired rotation for the root
        float angle = _mainCamera.transform.eulerAngles.y - visualizer.transform.eulerAngles.y;
        Quaternion endRot = Quaternion.AngleAxis(angle, Vector3.up) * startRot;

        // Based on the root's movement, we can find the pivot's final position
        Vector3 endPivotPos = pivotPos + (endPos - startPos);

        // --- Create a Single Tween to Animate Everything ---

        LeanTween.value(gameObject, 0f, 1f, tweenTime)
            .setEase(tweenType)
            .setOnUpdate(t =>
            {
                // Interpolate rotation and the pivot's position over time 't'
                Quaternion currentRot = Quaternion.Slerp(startRot, endRot, t);
                Vector3 currentPivotPos = Vector3.Lerp(pivotPos, endPivotPos, t);

                // Calculate the rotated offset vector for the current time 't'
                Quaternion rotationChange = currentRot * Quaternion.Inverse(startRot);
                Vector3 currentOffset = rotationChange * offset;

                // The new root position is the moving pivot's position plus the rotated offset
                Vector3 newPos = currentPivotPos + currentOffset;

                // Apply the calculated position and rotation to the root
                root.SetPositionAndRotation(newPos, currentRot);
            });
    }
}