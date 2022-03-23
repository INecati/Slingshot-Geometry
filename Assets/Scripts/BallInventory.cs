using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInventory : MonoBehaviour
{
    public static BallInventory instance;
    public float basicBallCount;
    public float bigBallCount;
    public float explosiveBallCount;
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

}
