using Assets.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInventory : MonoBehaviour
{
    public static BallInventory instance;
    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There is more than one " + nameof(BallInventory));
        }
    }
    #endregion
    [SerializeField] private int[] ballCounts;
    public event EventHandler OnInventoryUpdate;
    
    public int GetBallCount(BallType ballType)
    {
        return ballCounts[(int)ballType];
    }
    public void AddBall(BallType ballType,int amount) {
        if (ballType == BallType.BasicBall)
            return;
        ballCounts[(int)ballType] += amount;
        OnInventoryUpdate?.Invoke(this, EventArgs.Empty);
    }
    
}
