using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class POISelector : MonoBehaviour
{
    [SerializeField] private Sprite poiIcon;
    [SerializeField] private Image poiIconImage;
    [SerializeField] private int poiID;
    
    public Action<POISelector> OnSelect;
    
    private POIProvider _provider;
    
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => OnSelect?.Invoke(this));
    }

    public void AssignProvider(POIProvider[] providers)
    {
        _provider = providers.First(provider => provider.POIID == poiID);
    }
    
    public void Select()
    {
        poiIconImage.sprite = poiIcon;
        _provider.gameObject.SetActive(true);
    }

    public void UnSelect()
    {
        _provider.gameObject.SetActive(false);
    }
}