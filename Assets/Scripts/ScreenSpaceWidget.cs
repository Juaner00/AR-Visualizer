using UnityEngine;
using UnityEngine.UI;

public class ScreenSpaceWidget : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new(0, 0, 0);
    [SerializeField] private float lerpMultiplier = 10;
    [SerializeField] private float snapLimit = 350;

    private Camera _mainCamera;
    private RectTransform _rectRoot;

    private Vector3 _screenPos;
    private Vector2 _referenceResolution;

    private float _distance;

    public Vector3 ScreenPos => _screenPos;

    private void Awake()
    {
        _rectRoot = transform.GetChild(0).GetComponent<RectTransform>();
        _referenceResolution = GetComponent<CanvasScaler>().referenceResolution;

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

        if (_mainCamera != null)
            _screenPos = _mainCamera.WorldToScreenPoint(transform.parent.position + offset);

        NormalizePosition(ref _screenPos);

        _distance = Vector2.Distance(_rectRoot.anchoredPosition, ScreenPos);

        if (_distance > snapLimit)
            _rectRoot.anchoredPosition = _screenPos;
        else
            _rectRoot.anchoredPosition = Vector2.Lerp(_rectRoot.anchoredPosition, _screenPos,
                Time.fixedDeltaTime * lerpMultiplier);
    }

    private void NormalizePosition(ref Vector3 pos)
    {
        pos.x = pos.x / Screen.width * _referenceResolution.x;
        pos.y = pos.y / Screen.height * _referenceResolution.y;
    }
}