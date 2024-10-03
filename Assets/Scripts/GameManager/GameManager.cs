using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<int> diceValeus;
    [SerializeField] private List<GameObject> dices;
    [SerializeField] private int totalAcount;
    [SerializeField] private GameObject spawnDices;
    [SerializeField] private bool canRepeat;

    #region getersYseters

    public bool CanRepeat
    {
        get
        {
            return canRepeat;
        }
    }
    #endregion

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
        Reset();
    }
    private void Update() 
    {
        if(diceValeus.Count == 3)
        {
            canRepeat = true;
            totalAcount = diceValeus.Sum();
            UIManager.instance.LastWin(totalAcount);
            Reset();
        }
    }
    public void DicePushBack(int diceValeu)
    {
        diceValeus.Add(diceValeu);
    }
    public void Reset()
    {
        diceValeus.Clear();
        totalAcount = 0;
        dices[0].transform.position = spawnDices.transform.position;
        dices[1].transform.position = spawnDices.transform.position;
        dices[2].transform.position = spawnDices.transform.position;
        canRepeat = false;
    }

    

}
