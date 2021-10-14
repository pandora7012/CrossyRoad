using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StoreSystem : MonoBehaviour
{
    [SerializeField] private  StoreData data;
    public StoreElement storeElement;
    public RectTransform content;

    public Text nameTag;

    public Image okbt;
    public RectTransform storeBT;
    public RectTransform main; 

    private void Start()
    {
        for (int i = 0; i < data.assets.Count; i++)
        {
            StoreElement se = Instantiate(storeElement, content);
            se.ID = data.assets[i].ID;
            se.needScore = data.assets[i].scoreNeed;
            se.enable = data.assets[i].icon; 
        }
        
        Observer.ElementClick += UIUpdate;
    }

    private void OnEnable()
    {
        this.transform.DOLocalMoveX(0, 0.5f).From(3000);
        UIUpdate();
    }

    private void UIUpdate()
    {
        int p = PlayerPrefs.GetInt("Skin");
        nameTag.text = p == -1 ? "Unknown" : data.assets[p].nameTag;
        okbt.color = p == -1 ? Color.gray : Color.yellow;
    }

    public void OKButton()
    {
        SoundManager.Instance.Play("ButtonClick");
        if (PlayerPrefs.GetInt("Skin") == -1)
            return;
        this.transform.DOLocalMoveX(3000, 0.5f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
            main.gameObject.SetActive(true);
            storeBT.DOLocalMoveX(-800, 0.5f).From(-1000);
        });
        
    }
    private void OnDestroy()
    {
        Observer.ElementClick -= UIUpdate;
    }



}
