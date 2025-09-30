using UnityEngine;

public class POIManager : MonoBehaviour
{
    [SerializeField] private POISelector[] poiSelectors;
    [SerializeField] private GameObject poiImage;
    [SerializeField] private GameObject poiButtonsContainer;
    
    private POISelector _selectedPoi;

    private void Awake()
    {
        foreach (var poiSelector in poiSelectors)
        {
            poiSelector.OnSelect += selector =>
            {
                poiImage.SetActive(true);
                _selectedPoi?.UnSelect();
                _selectedPoi = selector;
                _selectedPoi.Select();
            };
        }
        
        poiImage.SetActive(false);
        poiButtonsContainer.SetActive(false);
    }

    public void OnElementSpawned(GameObject element)
    {
        POIProvider[] providers = element.GetComponentsInChildren<POIProvider>(true);

        foreach (var selector in poiSelectors)
        {
            selector.AssignProvider(providers);
        }
    }
    
    public void TogglePoi()
    {
        _selectedPoi?.UnSelect();
        _selectedPoi = null;
        poiImage.SetActive(false);
        poiButtonsContainer.SetActive(!poiButtonsContainer.activeSelf);
    }
}