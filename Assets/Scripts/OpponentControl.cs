using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class OpponentControl : CharacterControlBase
{
    public OpponentAI opponentAI=new OpponentAI();
    public RoundModel roundModel;
    public void SortCardUI()
    {
        Button[] buttons = OpponentCreatPoint.GetComponentsInChildren<Button>();
        CalculateCardUI(OpponentCreatPoint,HandCardCount);
        for (int i = 0; i < buttons.Count(); i++)
        {
            CardUI uI = buttons[i].GetComponent<CardUI>();
            uI.SetPostion(uI.Card.CardPlayerType, i, lis, rolis);
        }
    }

    public void EmptyPass()
    {
        StartCoroutine(DelayPass(1f));
    }
    IEnumerator DelayPass(float t)
    {
        yield return new WaitForSeconds(t);
        opponentAI.roundModel = this.roundModel;
        opponentAI.Pass();
    }
}

