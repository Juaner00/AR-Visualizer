using UnityEngine;

public class POIProvider : MonoBehaviour
{
    [SerializeField] private int poiID;
    
    public int POIID => poiID;
}