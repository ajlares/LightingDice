using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<int> diceValeus;
    [SerializeField] private List<GameObject> dices;
    [SerializeField] private List<GameObject> spawnDices;
    [SerializeField] private int totalAcount;
    [SerializeField] private int delayCouldown;
    [SerializeField] private GameObject spawntop;
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
        UIManager.instance.ColdownTime();
    }
    private void Update() 
    {
        if(diceValeus.Count == 3)
        {
            Debug.Log("validation");
            canRepeat = true;
            totalAcount = diceValeus.Sum();
            UIManager.instance.LastWin(totalAcount);
            spawntop.SetActive(true);
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
        dices[0].transform.position = spawnDices[0].transform.position;
        dices[1].transform.position = spawnDices[1].transform.position;
        dices[2].transform.position = spawnDices[2].transform.position;
        canRepeat = false;
        spawntop.SetActive(false);
    }

}
