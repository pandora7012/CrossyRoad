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
    public NewItem newItem;
    private int oldScore;


    [Header("Loading")]
    public Image loadingGround;
    public Image Logo; 

    [Header("Store")]
    public RectTransform store;

    [Header("Main")]
    public RectTransform main;
    public RectTransform storeBt;
    public RectTransform startGame;


    [Header("Data")]
    public StoreData data;
    public Camera cam;

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
        PlayerPrefs.SetInt("Top", Mathf.Max(PlayerPrefs.GetInt("Top"), score));
        bool temp = false;
        bool checknew = false;

        for (int i = 0; i < data.assets.Count; i++)
        {
            if (oldScore < data.assets[i].scoreNeed && PlayerPrefs.GetInt("Top") >= data.assets[i].scoreNeed)
            {
                checknew = true;
            }
            if ( data.assets[i].scoreNeed > PlayerPrefs.GetInt("Top") )
            {
                popupText.text = "Need " + (data.assets[i].scoreNeed - PlayerPrefs.GetInt("Top") ).ToString() + " more to unlock " + data.assets[i].nameTag;
                temp = true;
                break;
            }
        }
        if (checknew)
        {
            newItem.point = oldScore;
            newItem.gameObject.SetActive(true);
        }


        if (!temp)
        {
            popupText.text = "Good job"; 
        }
        scoreText.rectTransform.DOScale(new Vector3(2, 2, 1), 0.5f);
        Top.gameObject.SetActive(true);
        
        
        Top.rectTransform.DOLocalMoveX(5, 1f).From(-1960).OnComplete(() =>
        {
            popup.gameObject.SetActive(true);
            popup.DOLocalMoveX(0, 0.5f).From(-1960).OnComplete(() =>
            {
                OkButton.gameObject.SetActive(true);
                OkButton.DOLocalMoveX(0, 0.5f).From(-500);
            });
        });
        UpdateUI();
    }

    public void OKButton()
    {
        ResetGame();
        LoadingView();
        GameManager.Instance.state = GameManager.State.MainMenu;
        SoundManager.Instance.Play("ButtonClick");
        
    }
    #endregion

    #region Handle 

    public void ResetGame()
    {
        newItem.gameObject.SetActive(false);
        oldScore = PlayerPrefs.GetInt("Top");
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
        Sequence sq = DOTween.Sequence();
        loadingGround.gameObject.SetActive(true);
        sq.Append(loadingGround.DOColor(new Color(1, 1, 1, 1), 0.5f).OnComplete(()=> {
            GameManager.Instance.InitGame();
        }));
        sq.Append( Logo.transform.DOLocalMove(new Vector3(0, 100, 0), 1f).From(new Vector3(-1400,400)));
        sq.Append(loadingGround.DOColor(new Color(1, 1, 1, 0), 1f).OnComplete(() =>
        {
            ChangeToMain();
            loadingGround.gameObject.SetActive(false);
        })); 
    }


    #endregion


    #region Main 
    public void StoreButton()
    {
        SoundManager.Instance.Play("ButtonClick");
        storeBt.transform.DOLocalMoveX(-1100, 0.5f).OnComplete(() =>
        {
            main.gameObject.SetActive(false);
            store.gameObject.SetActive(true);
        });
        
    }

    public void StartBT()
    {
        SoundManager.Instance.Play("ButtonClick");
        GameManager.Instance.state = GameManager.State.OnPlay;
        oldScore = PlayerPrefs.GetInt("Top");
        startGame.gameObject.SetActive(false);
        storeBt.transform.DOLocalMoveX(-1100, 0.5f);
        Logo.transform.DOLocalMove(new Vector3(1400, -200, 0), 1f);
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
