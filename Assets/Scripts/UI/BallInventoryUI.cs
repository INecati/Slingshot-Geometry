using Assets.Entities;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class BallInventoryUI : MonoBehaviour
{

    [SerializeField] private BallLauncher ballLauncher;
    //[SerializeField] private GameObject basicBall;
    //[SerializeField] private GameObject bigBall;
    //[SerializeField] private GameObject explosiveBall;

    [SerializeField] private Button btnBasicBall;
    [SerializeField] private TMP_Text txtBasicBall;
    [SerializeField] private Button btnBigBall;
    [SerializeField] private TMP_Text txtBigBall;
    [SerializeField] private Button btnExplosiveBall;
    [SerializeField] private TMP_Text txtExplosiveBall;

    //[SerializeField] private BallType currentBallType=BallType.BasicBall;
    private void Start()
    {
        btnBasicBall.onClick.AddListener(delegate { SelectBall(BallType.BasicBall); });
        btnBigBall.onClick.AddListener(delegate { SelectBall(BallType.BigBall); });
        btnExplosiveBall.onClick.AddListener(delegate { SelectBall(BallType.ExplosiveBall); });
        ballLauncher.OnBallLaunched += BallLauncher_OnBallLaunched;
        UpdateBtnText();
    }

    private void BallLauncher_OnBallLaunched(object sender, System.EventArgs e)
    {
        UpdateBtnText();
    }

    private void SelectBall(BallType ballType)
    {
        Debug.Log("btnBasicBallOnClick: " + ballType.ToString());
        if (ballLauncher.SelectBallType(ballType)) { 
            //currentBallType = ballType;
            UpdateBtnText();
        }

    }
    private void UpdateBtnText()
    {
        txtBasicBall.text = "Basic Ball: " + ballLauncher.ballCounts[(int)BallType.BasicBall];
        txtBigBall.text = "Big Ball: " + ballLauncher.ballCounts[(int)BallType.BigBall];
        txtExplosiveBall.text = "Explosive Ball: " + ballLauncher.ballCounts[(int)BallType.ExplosiveBall];
        //txtBasicBall.text = "Basic Ball: " + ballLauncher.ballCounts[(int)BallType.ExplosiveBall];
    }
    //public void SelectBasicBall()
    //{
    //    if (BallInventory.instance.basicBallCount>0) {
    //        ballLauncher.selectedBallType = basicBall;
    //    }
    //}
    //public void SelectBigBall()
    //{
    //    if (BallInventory.instance.bigBallCount > 0)
    //    {
    //        ballLauncher.selectedBallType = bigBall;
    //    }
    //}
    //public void SelectExplosiveBall()
    //{

    //}
}
