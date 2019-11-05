using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoundMediator : EventMediator
{
    [Inject]
    public RoundView roundView { get; set; }
    [Inject]
    public RoundModel roundModel { get; set; }
    public static Action BeginRoundHandler;
    public override void OnRegister()
    {
        RoundModel.PlayerHandler += BeginPlayerRound;
        RoundModel.opponentHandler += BeginOpponentRound;
        roundView.opponentControl.roundModel = this.roundModel;
        roundView.endButton.onClick.AddListener(RoundTurn);
    }
    public override void OnRemove()
    {
        RoundModel.PlayerHandler -= BeginPlayerRound;
        RoundModel.opponentHandler -= BeginOpponentRound;
        roundView.endButton.onClick.RemoveListener(RoundTurn);
    }
    
    private void BeginPlayerRound()//回合开始阶段
    {
        roundModel.IsPlayerRound = true;
        ShowTipsArgs arg = new ShowTipsArgs { tip = "你的回合！" };
        dispatcher.Dispatch(ViewEvent.ShowTips,arg);
        if (BeginRoundHandler!=null)
        {
            BeginRoundHandler();
        }
        DealCard(true);
        roundView.endButton.interactable = true;
    }
    private void BeginOpponentRound()//回合开始阶段
    {
        roundModel.IsPlayerRound = false;
        if (BeginRoundHandler != null)
        {
            BeginRoundHandler();
        }
        DealCard(false);
        roundView.endButton.interactable = false;
        roundView.opponentControl.EmptyPass();
    }
    private void DealCard(bool isPlayer)
    {
        //增加水晶数量
        CreatMana(isPlayer);
        if (isPlayer)
        {
            roundView.playerControl.DealCard(PlayerType.Player);
            roundView.playerControl.SortCardUI();
            
        }
        else
        {
            roundView.opponentControl.DealCard(PlayerType.Opponent);
            roundView.opponentControl.SortCardUI();
        }
    }

    private void CreatMana(bool isPlayer)
    {
        if (isPlayer)
        {
            roundView.playerControl.TotalMana++;
            roundView.playerControl.CurrentMana = roundView.playerControl.TotalMana;
            roundView.playerCurrentMana.text = roundView.playerControl.TotalMana.ToString();
            roundView.playerTotalMana.text = roundView.playerControl.TotalMana.ToString();
            for (int i = 0; i < roundView.playerControl.TotalMana; i++)
            {
                if (!roundView.manaObjects[i].activeInHierarchy)
                {
                    
                    roundView.manaObjects[i].SetActive(true);
                }
                else if (!roundView.manaObjects[i].transform.Find("Full").gameObject.activeInHierarchy)
                {
                    roundView.manaObjects[i].transform.Find("Full").gameObject.SetActive(true);
                }
            }
        }
        else
        {
            roundView.opponentControl.TotalMana++;
            roundView.opponentControl.CurrentMana = roundView.opponentControl.TotalMana;
            roundView.opponentCurrentMana.text = roundView.opponentControl.TotalMana.ToString();
            roundView.opponentTotalMana.text = roundView.opponentControl.TotalMana.ToString();
        }
    }
    public void CostMana(int n)
    {
        roundView.playerCurrentMana.text = roundView.playerControl.CurrentMana.ToString();
        for (int i = roundView.playerControl.TotalMana-1; i >= roundView.playerControl.TotalMana-n; i--)
        {
            Debug.Log(roundView.manaObjects[i].transform.Find("Full").gameObject.name);
            roundView.manaObjects[i].transform.Find("Full").gameObject.SetActive(false);
        }
    }

    public void RoundTurn()
    {
        roundModel.BeginWith(!roundModel.IsPlayerRound);
    }
}
