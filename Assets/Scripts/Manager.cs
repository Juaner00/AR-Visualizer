using UnityEditor;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private POIManager poiManager;
    
    private GameObject _element;

    private Vector3 _initialScale;

    public static Manager Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void Home()
    {
        if (!_element)
            return;
        
        _element.SetActive(false);
        _element.SetActive(true);
        _element.transform.localScale = _initialScale;
    }

    public void ToggleShower()
    {
        if (!_element)
            return;
        
        foreach (var particle in _element.GetComponentsInChildren<ParticleSystem>())
        {
            if (particle.isPlaying)
                particle.Stop();
            else
                particle.Play();
        }
    }

    public void OnElementSpawned(GameObject element)
    {
        if (_element)
            return;
        
        _element = element;
        _initialScale = _element.transform.localScale;
        
        poiManager.OnElementSpawned(element);
    }
}
