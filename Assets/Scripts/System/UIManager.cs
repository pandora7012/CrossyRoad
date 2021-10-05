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


    [Header("Loading")]
    public Image loadingGround;
    public Animator loadingGroundAnim;

    


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
        GameManager.Instance.InitGame();
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

    #endregion
}
