using DG.Tweening;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GamePanelMediator : EventMediator
{
    [Inject]
    public GamePanelView gamePanelView { get; set; }
    [Inject]
    public RoundModel roundModel { get; set; }
    List<Vector3> lis = new List<Vector3>();
    List<float> rolis = new List<float>();

    public override void OnRegister()
    {
        gamePanelView.sureButton.onClick.AddListener(OnSureButtonClick);
        dispatcher.AddListener(ViewEvent.BattleStart, OnBattleStart);
        dispatcher.AddListener(ViewEvent.DealCard, OnDealCard);
        dispatcher.AddListener(ViewEvent.ShowTips,ShowTip);
    }
    public override void OnRemove()
    {

        StopAllCoroutines();
        gamePanelView.sureButton.onClick.RemoveListener(OnSureButtonClick);
        dispatcher.RemoveListener(ViewEvent.BattleStart, OnBattleStart);
        dispatcher.RemoveListener(ViewEvent.DealCard, OnDealCard);
        dispatcher.RemoveListener(ViewEvent.ShowTips, ShowTip);
    }
    public void Init()
    {
        PlayInitAnim();
        StartCoroutine(PlayInitAnim());

    }
    public IEnumerator PlayInitAnim()
    {
        gamePanelView.playerHead.transform.DOMove(gamePanelView.playerHeadTrs1.position, 1f);
        gamePanelView.opponentHead.transform.DOMove(gamePanelView.opponentHeadTrs1.position, 1f);
        yield return new WaitForSeconds(2f);
        gamePanelView.VS.transform.DOScale(new Vector3(0, 0, 0), 1f).OnComplete(() => { gamePanelView.VS.gameObject.SetActive(false); });
        gamePanelView.playerHead.transform.DOMove(gamePanelView.playerHeadTrs2.position, 1f).OnComplete(
            ()=> { gamePanelView.SetHpUI(gamePanelView.playerControl);gamePanelView.SetRemainCardUI(); });
        gamePanelView.opponentHead.transform.DOMove(gamePanelView.opponentHeadTrs2.position, 1f).OnComplete(
            () => { gamePanelView.SetHpUI(gamePanelView.opponentControl);gamePanelView.SetRemainCardUI();RequestDealStartCard(); });
        StopCoroutine(PlayInitAnim());
    }
    public void RequestDealStartCard()
    {
        dispatcher.Dispatch(CommandEvent.RequestDealStartCard);
        
    }
    public void OnBattleStart(IEvent evt)
    {
        BattleStartArgs args = (BattleStartArgs)evt.data;
        string occupationName = args.playerDeckModel.OccupationType.ToString();
        gamePanelView.playerHead.sprite = Resources.Load<Sprite>("Textures\\" + occupationName + "Head");
        //初始化characterControl
        gamePanelView.playerControl.HpBase = 30;
        gamePanelView.playerControl.CurrentHP = 30;
        gamePanelView.playerControl.IsPlayer = true;
        gamePanelView.playerControl.Init(args.playerDeckModel);        
        occupationName = args.opponentDeckModel.OccupationType.ToString();
        gamePanelView.opponentHead.sprite = Resources.Load<Sprite>("Textures\\" + occupationName + "Head");
        gamePanelView.opponentControl.CurrentHP = 30;
        gamePanelView.opponentControl.HpBase = 30;
        gamePanelView.opponentControl.IsPlayer = false;
        gamePanelView.opponentControl.Init(args.opponentDeckModel);
        Init();
    }
    
    public void OnDealCard(IEvent e)
    {
        StartCoroutine(OnDealCard1(e));
        //OnDealCard1(e);

    }
    public IEnumerator OnDealCard1(IEvent e)
    {
        DealCardArgs args = (DealCardArgs)e.data;
        String s;
        for (int i = 0; i < args.count; i++)
        {
            switch (args.playerType)
            {
                case PlayerType.Desk:
                    {
                        gamePanelView.playerControl.DealCard(PlayerType.Desk);
                    }
                    break;
                case PlayerType.Opponent:
                    {
                        gamePanelView.opponentControl.DealCard(PlayerType.Opponent);
                        gamePanelView.opponentControl.SortCardUI();
                    }
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(1f);
        }
        gamePanelView.sureButton.gameObject.SetActive(true);
        if (args.playerType==PlayerType.Desk)
        {
            Debug.Log(args.playerType);
            if (args.count == 4)
            {
                s = "你是后手";
            }
            else
            {
                s = "你是先手";
            }
            ShowTips(s);

        }
    }
    public void OnDealCard1(int index)
    {       
        gamePanelView.playerControl.DealCard(PlayerType.Desk,index);       
    }
    public void OnSureButtonClick()
    {
        gamePanelView.sureButton.gameObject.SetActive(false);
        StartCoroutine(OnSureButtonClick1());
    }
    public IEnumerator OnSureButtonClick1()
    {
        Queue<int> q = new Queue<int>();
        int[] tempInt = new int[4];
        Button[] buttons = gamePanelView.DeskCreatPoint.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Count(); i++)
        {
            Transform temp = buttons[i].GetComponent<Transform>().Find("ReplaceX");
            if (temp.gameObject.activeInHierarchy)
            {
                CardUI uI = buttons[i].GetComponent<CardUI>();
                gamePanelView.playerControl.AddCards(uI.Card);
                uI.ReturnToDeck();                
                gamePanelView.playerControl.HandCardCount--;
                q.Enqueue(i);
            }            
        }
        int t = q.Count;
        for (int i = 0; i < t; i++)
        {
            OnDealCard1(q.Dequeue());
            yield return new WaitForSeconds(1f);
        }
        buttons = gamePanelView.DeskCreatPoint.GetComponentsInChildren<Button>();
       
        for (int i = 0; i < buttons.Count(); i++)
        {            
            buttons[i].transform.SetParent(gamePanelView.PlayerCreatPoint);
            gamePanelView.playerControl.DeskCardCount--;
        }
        gamePanelView.playerControl.SortCardUI();
        if (buttons.Count()==4)//派发幸运币
        {
            CardCoin coin = new CardCoin();
            gamePanelView.playerControl.HandCards.Add(coin);
            gamePanelView.playerControl.HandCardCount++;
            GameObject go = Resources.Load<GameObject>("Prefab\\CardF");
            GameObject gb = GameObject.Instantiate(go, gamePanelView.DeskCreatPoint);
            gb.name = coin.imageName +gamePanelView.playerControl.HandCardCount;
            CardUI cardUI = gb.GetComponent<CardUI>();
            coin.CardPlayerType = PlayerType.Player;
            cardUI.Card = coin;
            gb.transform.localPosition = Vector3.zero;
            gb.transform.localScale = Vector3.zero;
            gb.transform.DOScale(Vector3.one,1f).OnComplete(
                () =>
                {
                    gb.transform.SetParent(gamePanelView.PlayerCreatPoint);
                    gamePanelView.playerControl.SortCardUI();
                    roundModel.BeginWith(false);
                }
                );
        }
        else 
        {
            CardCoin coin = new CardCoin();
            gamePanelView.opponentControl.HandCards.Add(coin);
            gamePanelView.opponentControl.HandCardCount++;
            GameObject go = Resources.Load<GameObject>("Prefab\\CardBack");
            GameObject gb = GameObject.Instantiate(go, gamePanelView.OpponentCreatPoint);
            gb.name = coin.imageName +gamePanelView.opponentControl.HandCardCount;
            CardUI cardUI = gb.GetComponent<CardUI>();
            coin.CardPlayerType = PlayerType.Opponent;
            cardUI.Card = coin;
            gb.transform.localPosition = new Vector3(535, -126, 0);
            gb.transform.localScale = new Vector3(0, 0, 0);
            gamePanelView.opponentControl.SortCardUI();
            roundModel.BeginWith(true);
        }
    }

    public void ShowTips(string text)
    {
        StartCoroutine(ShowTipsS(text));
    }
    private IEnumerator ShowTipsS(string text)
    {
        gamePanelView.tisps.text = text;
        gamePanelView.tisps.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        gamePanelView.tisps.gameObject.SetActive(false);
    }
    public IEnumerator Delay(float t)
    {
        yield return new WaitForSeconds(t);
    }
    public void ShowTip(IEvent e)
    {
        ShowTipsArgs args = (ShowTipsArgs)e.data;
        ShowTips(args.tip);
    }
}

