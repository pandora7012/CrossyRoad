using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [Header("OnPlay")]
    public Text scoreText;
    public Text Top;
    public int score;
    public RectTransform popup;
    public RectTransform OkButton;
    public Text popupText;


    [Header("Loading")]
    public Image loadingGround;

    [Header("Store")]
    public RectTransform store;

    [Header("Main")]
    public RectTransform main;
    public RectTransform storeBt;
    public RectTransform startGame;


    [Header("Data")]
    public StoreData data;
    void Start()
    {
        ResetGame();
    }

    #region OnPlay
    public void UpdateUI()
    {
        scoreText.text = score.ToString();
        Top.text = "TOP: " + PlayerPrefs.GetInt("Top").ToString();
    }

    public void GameOver()
    {
        bool temp = false; 
        for (int i = 0; i < data.assets.Count; i++)
        {
            if ( data.assets[i].scoreNeed > PlayerPrefs.GetInt("Top") )
            {
                popupText.text = "Need " + (data.assets[i].scoreNeed - PlayerPrefs.GetInt("Top") ).ToString() + " more to unlock " + data.assets[i].nameTag;
                temp = true; 
            }
        }
        if (!temp)
        {
            popupText.text = "Good job"; 
        }
        scoreText.rectTransform.DOScale(new Vector3(2, 2, 1), 0.5f);
        Top.gameObject.SetActive(true);
        OkButton.gameObject.SetActive(true);
        popup.gameObject.SetActive(true);
        
        Top.rectTransform.DOLocalMoveX(5, 1f).OnComplete(() =>
        {
            popup.DOLocalMoveX(0, 0.5f).OnComplete(() =>
            {
                OkButton.DOLocalMoveX(0, 0.5f);
            });
        });
    }

    public void OKButton()
    {
        ResetGame();
        LoadingView();
        GameManager.Instance.InitGame();
        GameManager.Instance.state = GameManager.State.MainMenu;
        
    }
    #endregion

    #region Handle 

    public void ResetGame()
    {
        scoreText.text = "0";
        Top.text = "TOP : ";
        score = 0;
        scoreText.rectTransform.DOScale(Vector3.one, 0.25f);
        OkButton.gameObject.SetActive(false);
        Top.gameObject.SetActive(false);
        popup.gameObject.SetActive(false);
        UpdateUI();
    }

    public void LoadingView()
    {
        loadingGround.gameObject.SetActive(true);
        loadingGround.DOColor(Color.clear, 3f).OnComplete(() =>
        { 
            loadingGround.gameObject.SetActive(false);
            ChangeToMain();
        });
    }


    #endregion


    #region Main 
    public void StoreButton()
    {

        storeBt.transform.DOLocalMoveX(-1100, 0.5f).OnComplete(() =>
        {
            main.gameObject.SetActive(false);
            store.gameObject.SetActive(true);
        });
        
    }

    public void StartBT()
    {
        GameManager.Instance.state = GameManager.State.OnPlay;
        startGame.gameObject.SetActive(false);
        storeBt.transform.DOLocalMoveX(-1100, 0.5f);
    }


    public void ChangeToMain()
    {
        storeBt.transform.DOLocalMoveX(-800, 0.5f).OnComplete(() =>
        {
            startGame.gameObject.SetActive(true);
        });
    }
    

    #endregion
}
