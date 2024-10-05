using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // global variables
    [SerializeField] private List<int> diceValeus;
    [SerializeField] private List<GameObject> dices;
    [SerializeField] private List<GameObject> spawnDices;
    [SerializeField] private int totalAcount;
    [SerializeField] private int delayCouldown;
    [SerializeField] private GameObject spawntop;

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
    private void Start() {
        UIManager.instance.ColdownTime();
    }
    private void Update() 
    {
        if(diceValeus.Count == 3)
        {
            canRepeat = true;
            totalAcount = diceValeus.Sum();
            UIManager.instance.LastWin(totalAcount);
            spawntop.SetActive(true);
            UIManager.instance.ColdownTime();
            diceValeus.Clear();
            StartCoroutine(DiceReset());
        }
    }
    public void DicePushBack(int diceValeu)
    {
        diceValeus.Add(diceValeu);
    }
    public void StartGame()
    {
        totalAcount = 0;
        canRepeat = false;
        spawntop.SetActive(false);
    }
    private IEnumerator DiceReset()
    {
        yield return new WaitForSeconds(3f);
        dices[0].transform.position = spawnDices[0].transform.position;
        dices[1].transform.position = spawnDices[1].transform.position;
        dices[2].transform.position = spawnDices[2].transform.position;

    }

    #region Validacion
    [SerializeField] private List<int> bets;
    [SerializeField] private List<int> betsAcount;
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
        }
        Debug.Log("actualsBets " + bets);
        Debug.Log("actualsBestsACount " + betsAcount);
    }

    #endregion

}
