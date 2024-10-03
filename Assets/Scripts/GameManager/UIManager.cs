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
    [SerializeField] private float actualMoney;
    [SerializeField] private bool updateImage;
      public static UIManager instance;
      #region getter y setter

    public float ActualMoney
        {
            get
            {
                return actualMoney;
            }
            set
            {
                actualMoney += value;
            }
        }

      #endregion
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
    private void Update()
    {
        if(updateImage)
        {
            if(indexTime > betTime)
            {
                updateImage = false;
                TimeImage.fillAmount = 0;
                indexTime = 0;
                GameManager.instance.StartGame();
                acountText.text = " ";
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



}
