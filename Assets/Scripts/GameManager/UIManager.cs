using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour

{
    [SerializeField] private TextMeshProUGUI acountText;
    [SerializeField] private Image TimeImage;
    [SerializeField] private float indexTime;
    [SerializeField] private float betTime;
    [SerializeField] private bool updateImage;
    [SerializeField] private TextMeshProUGUI actualMoney;
    [SerializeField] private GameObject betPanel;
    
    #region  instance
      public static UIManager instance;
    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy( this);
        }  
    }
    #endregion

    private void Start() 
    {
        UpdateMoney();    
    }
    private void Update()
    {
        if(updateImage)
        {
            if(indexTime > betTime)
            {
                updateImage = false;
                TimeImage.fillAmount = 0;
                indexTime = 0;
                BetPanel(false);
                GameManager.instance.StartGame();
                acountText.text = " ";
                UpdateMoney();
            }
            else
            {
                indexTime += Time.deltaTime;
                imageUpdate();
            }
        }
    }
    public void LastWin(int valeu)
    {
        acountText.text = valeu.ToString();
    }

    public void ColdownTime()
    {
        TimeImage.fillAmount = 1;
        updateImage = true;
    }
    private void imageUpdate()
    {
        TimeImage.fillAmount =  indexTime / betTime;
    }

    public void UpdateMoney()
    {
        actualMoney.text = "$ " + GameManager.instance.ActualMoney.ToString();
    }
    public void Bet(int betNumber)
    {
        GameManager.instance.AddBet(betNumber);

    }
    public void BetPanel(bool valeu)
    {
        betPanel.SetActive(valeu);
    }
}
