using UnityEngine;

public class ScreenSpaceWidget : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new(0, 0, 0);
    [SerializeField] private float lerpMultiplier = 10;
    [SerializeField] private float snapLimit = 350;

    private Camera _mainCamera;
    private RectTransform _rectRoot;

    private Vector3 _screenPos;


    private void Awake()
    {
        _rectRoot = transform.GetChild(0).GetComponent<RectTransform>();

        SetCamera();
    }

    private void SetCamera()
    {
        _mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (!_mainCamera)
            SetCamera();

        if (_mainCamera)
            _screenPos = _mainCamera.WorldToScreenPoint(transform.parent.position + offset);

        NormalizePosition(ref _screenPos);

        _rectRoot.anchoredPosition = _screenPos;
    }

    private void NormalizePosition(ref Vector3 pos)
    {
        pos.x = pos.x / Screen.width * ((RectTransform)transform).sizeDelta.x;
        pos.y = pos.y / Screen.height * ((RectTransform)transform).sizeDelta.y;
    }
}