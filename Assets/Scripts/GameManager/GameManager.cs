using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // global variables
    [SerializeField] private List<int> diceValeus;
    [SerializeField] private List<GameObject> dices;
    [SerializeField] private List<GameObject> spawnDices;
    [SerializeField] private int totalAcount;
    [SerializeField] private int delayCouldown;
    [SerializeField] private GameObject spawntopLeft;
    [SerializeField] private GameObject spawntopRight;
    [SerializeField] private Transform pointLeftA;
    [SerializeField] private Transform pointLeftb;
    [SerializeField] private Transform pointRighta;
    [SerializeField] private Transform pointRightb;
    [SerializeField] private float speed;
    [SerializeField] private bool doorsCanMove;
    [SerializeField] private bool isDoorsOn_a;

    #region getersYseters
    [SerializeField] private bool canRepeat;
    [SerializeField] private float actualMoney;
    [SerializeField] private int actualBetAcount;
    public bool CanRepeat
    {
        get
        {
            return canRepeat;
        }
    }

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
    public int ActualBetAcount
    {
        get
        {
            return actualBetAcount;
        }
        set
        {
            actualBetAcount = value;
        }
        
    }
    #endregion

    #region Instance
    public static GameManager instance;
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
        UIManager.instance.ColdownTime();
        doorsCanMove = false;
    }
    private void Update() 
    {
        if(diceValeus.Count == 3)
        {
            CloseDoors();
            canRepeat = true;
            totalAcount = diceValeus.Sum();
            UIManager.instance.LastWin(totalAcount);
            UIManager.instance.ColdownTime();
            UIManager.instance.BetPanel(true);
            diceValeus.Clear();
            StartCoroutine(DiceReset());
            winBets();
        }
    }
    public void DicePushBack(int diceValeu)
    {
        diceValeus.Add(diceValeu);
    }
    public void StartGame()
    {
        canRepeat = false;
        OpenDors();
    }
    private IEnumerator DiceReset()
    {
        yield return new WaitForSeconds(3f);
        dices[0].transform.position = spawnDices[0].transform.position;
        dices[1].transform.position = spawnDices[1].transform.position;
        dices[2].transform.position = spawnDices[2].transform.position;
    }

    private void OpenDors()
    {
        spawntopLeft.transform.position = pointLeftb.position;
        spawntopRight.transform.position = pointRightb.position;
    }
    private void CloseDoors()
    {
        spawntopLeft.transform.position = pointLeftA.position;
        spawntopRight.transform.position = pointRighta.position;
    }

    #region Validacion
    [SerializeField] private List<int> bets;
    [SerializeField] private List<int> betsAcount;
    [SerializeField] private List<int> betPayment;
    public void AddBet(int betNumber)
    {
        bool isInArray = false;
        if(bets.Count() != 0)
        {
            for(int i = 0; i < bets.Count(); i++)
            {
                if(bets[i] == betNumber)
                {
                    betsAcount[i] += actualBetAcount;
                    actualMoney -= actualBetAcount;
                    UIManager.instance.UpdateMoney();
                    isInArray = true;
                }
            }
        }
        else
        {
            isInArray = false;
        }

        if(!isInArray)
        { 
            bets.Add(betNumber);
            betsAcount.Add(actualBetAcount);
            actualMoney -= actualBetAcount;
            UIManager.instance.UpdateMoney();
        }
    }

    private void winBets()
    {
        int payAcount = 0;
        if(bets.Count() != 0)
        {
            for(int i = 0; i < bets.Count(); i++)
            {
                // numbers
                if(totalAcount == bets[i])
                {    
                    payAcount += betsAcount[i] * betPayment[bets[i-1]];
                }
                //low
                if(bets[i] == 1)
                {
                    // validation valeu
                    if(totalAcount < 11)
                    {
                        payAcount += betsAcount[i] * 2;
                    }
                }
                //high
                if(bets[i] == 2)
                {
                    // validation valeu
                    if(totalAcount > 10)
                    {
                        payAcount += betsAcount[i] * 2;
                    }
                }
            }
        }
        PayBets(payAcount);
    }
    private void PayBets(int payAmount)
    {
        actualMoney += payAmount;
        UIManager.instance.UpdateMoney();
        bets.Clear();
        betsAcount.Clear();
        RestartBets();
    }

    private void RestartBets()
    {
        bets.Clear();
        betsAcount.Clear();
    }
    #endregion

}
