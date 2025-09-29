using UnityEngine;

public class ScreenSpaceWidget : MonoBehaviour
{
    [SerializeField] private FrustumCheck frustumCheck;
    
    private Camera _mainCamera;
    private RectTransform _rectRoot;

    private Vector3 _screenPos;
    private Vector2 _referenceResolution;
    private Canvas _canvas;

    private float _distance;

    private void Awake()
    {
        _rectRoot = transform.GetChild(0).GetComponent<RectTransform>();
        _canvas = GetComponent<Canvas>();

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

        if (!frustumCheck.IsInCamera(_mainCamera))
        {
            _canvas.enabled = false;
            return;
        }
        
        _canvas.enabled = true;
        
        if (_mainCamera)
            _screenPos = _mainCamera.WorldToScreenPoint(transform.parent.parent.position);

        NormalizePosition(ref _screenPos);
        
        _rectRoot.anchoredPosition = _screenPos;
    }

    private void NormalizePosition(ref Vector3 pos)
    {
        pos.x = pos.x / Screen.width * ((RectTransform)transform).sizeDelta.x;
        pos.y = pos.y / Screen.height * ((RectTransform)transform).sizeDelta.y;
    }
}