using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuiCongMediator : EventMediator {
    [Inject]
    public SuiCongView suiCongView { get; set; }
    public override void OnRegister()
    {
    }
    public override void OnRemove()
    {
    }
    public void CreatSuiCong(ICard card)
    {
        GameObject g = Resources.Load<GameObject>("Prefab\\SuiCong");
        GameObject gb=null;
        if (card.CardPlayerType==PlayerType.Player)
        {
             gb = GameObject.Instantiate(g, suiCongView.playerSuiCongPos);
        }else if (card.CardPlayerType == PlayerType.Opponent)
        {
             gb = GameObject.Instantiate(g, suiCongView.opponentSuiCongPos);
        }
        gb.transform.localPosition = Vector3.zero;
        SuiCongUI suiCongUI = gb.GetComponent<SuiCongUI>();
        suiCongUI.Card = card;        
    }
}
