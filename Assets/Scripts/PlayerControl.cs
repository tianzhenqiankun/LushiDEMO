using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : CharacterControlBase
{
    public void SortCardUI()
    {
        Button[] buttons = PlayerCreatPoint.GetComponentsInChildren<Button>();
        CalculateCardUI(PlayerCreatPoint, buttons.Length);
        for (int i = 0; i < buttons.Length; i++)
        {
            CardUI uI = buttons[i].GetComponent<CardUI>();
            uI.SetPostion(PlayerType.Player, i, lis, rolis);
        }        
    }
}

