using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "StoreData", menuName = "Data/StoreData", order = 1)]
public class StoreData : ScriptableObject
{
    public List<ShopElement> assets;
}

[System.Serializable]
public class ShopElement
{
    public int ID;
    public string nameTag;
    public string Describe;
    public Sprite texture;
    public int scoreNeed; 
    public bool hasComplete; 
}
