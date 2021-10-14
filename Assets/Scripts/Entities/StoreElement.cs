using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreElement : MonoBehaviour
{
    public int ID;
    public Image background;
    public Image ava;
    public Sprite sprite;
    public Sprite enable;
    public int needScore;
    
    private void Start()
    {
        Observer.ElementClick += uiUpdate;
        ava.sprite = needScore > PlayerPrefs.GetInt("Top") ? sprite : enable; 
        uiUpdate();
    }

    public void OnClick()
    {
        SoundManager.Instance.Play("ButtonClick");
        
        PlayerPrefs.SetInt("Skin", needScore <= PlayerPrefs.GetInt("Top") ? ID : -1);
        Observer.ElementClick?.Invoke();
        uiUpdate();
    }

    public void uiUpdate()
    {
        background.color = this.ID == PlayerPrefs.GetInt("Skin") ? Color.yellow : Color.white;
    }

    private void OnDestroy()
    {
        Observer.ElementClick -= uiUpdate;

    }
}
