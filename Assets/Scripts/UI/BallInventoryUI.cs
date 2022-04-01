using Assets.Entities;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallInventoryUI : MonoBehaviour
{

    [SerializeField] private BallLauncher ballLauncher;

    [SerializeField] private BallSelectButton[] ballButtons;
    [SerializeField] private Color defaultBtnColor;
    [SerializeField] private Color selectedBtnColor;
    private void Start()
    {
        ballLauncher.OnBallLaunched += BallLauncher_OnBallLaunched;
        BallInventory.instance.OnInventoryUpdate += BallInventory_OnInventoryUpdate;

        for (int i = 0; i < ballButtons.Length; i++)
        {
            BallType ballType = (BallType)i;
            ballButtons[i].button.onClick.AddListener(delegate { SelectBall(ballType); });
            ballButtons[i].backgroundImage.color = defaultBtnColor;
        }
        ballButtons[(int)ballLauncher.currentBallType].backgroundImage.color = selectedBtnColor;
        UpdateBtnText();
    }


    private void SelectBall(BallType ballType)
    {
        Debug.Log("btnBasicBallOnClick: " + ballType.ToString());
        BallType oldBallType = ballLauncher.currentBallType;
        if (ballLauncher.SelectBallType(ballType)) {
            ballButtons[(int)oldBallType].backgroundImage.color = defaultBtnColor;
            ballButtons[(int)ballType].backgroundImage.color = selectedBtnColor;
            UpdateBtnText();
        }
        
    }
    private void UpdateBtnText()
    {
        for(int i = 1; i < ballButtons.Length; i++) {
            BallType ballType = (BallType)i;
            ballButtons[i].text.text= BallInventory.instance.GetBallCount(ballType).ToString();
        }
    }

    private void BallInventory_OnInventoryUpdate(object sender, System.EventArgs e)
    {
        UpdateBtnText();
    }

    private void BallLauncher_OnBallLaunched(object sender, System.EventArgs e)
    {
        UpdateBtnText();
    }
}
