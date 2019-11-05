using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;

public class CharacterControlBase : MonoBehaviour {

    int hpBase;
    int currentHP;
    int attack;//攻击力
    int totalMana;
    int currentMana;
    int cardCount;
    int handCardCount;
    int deskCardCount;
    bool isPlayer;
    Occupation occupation;
    BuffType buffType;
    List<ICard> cards = new List<ICard>();
    List<ICard> handCards = new List<ICard>();

    protected List<Vector3> lis = new List<Vector3>();
    protected List<float> rolis = new List<float>();

    public Transform PlayerCreatPoint;
    public Transform OpponentCreatPoint;
    public Transform deskCreatPoint;
    GamePanelView panelView;
    public int HpBase
    {
        get
        {
            return hpBase;
        }

        set
        {
            hpBase = value;
        }
    }

    public int Attack
    {
        get
        {
            return attack;
        }

        set
        {
            attack = value;
        }
    }

    public int CardCount
    {
        get
        {
            return cardCount;
        }

        set
        {
            cardCount = value;
            panelView.SetRemainCardUI();
        }
    }

    public Occupation Occupation
    {
        get
        {
            return occupation;
        }

        set
        {
            occupation = value;
        }
    }

    public BuffType BuffType
    {
        get
        {
            return buffType;
        }

        set
        {
            buffType = value;
        }
    }

    public List<ICard> Cards
    {
        get
        {
            return cards;
        }

        set
        {
            cards = value;
        }
    }

    public int CurrentHP
    {
        get
        {
            return currentHP;
        }

        set
        {
            currentHP = value;
        }
    }

    public bool IsPlayer
    {
        get
        {
            return isPlayer;
        }

        set
        {
            isPlayer = value;
        }
    }

    public List<ICard> HandCards
    {
        get
        {
            return handCards;
        }

        set
        {
            handCards = value;
        }
    }

    public int HandCardCount
    {
        get
        {
            return handCardCount;
        }

        set
        {
            handCardCount = value;
        }
    }

    public Transform DeskCreatPoint
    {
        get
        {
            if (deskCreatPoint == null)
            {
                deskCreatPoint = GameObject.Find("GamePanel").transform.Find("DeskCreatCardPoint");
            }
            if (deskCreatPoint==null)
            {
                Debug.Log("未找到creatPoint");
            }
            return deskCreatPoint;
        }

        set
        {
            deskCreatPoint = value;
        }
    }

    public int DeskCardCount
    {
        get
        {
            return deskCardCount;
        }

        set
        {
            deskCardCount = value;
        }
    }

    public int TotalMana
    {
        get
        {
            return totalMana;
        }

        set
        {
            totalMana = value;
        }
    }

    public int CurrentMana
    {
        get
        {
            return currentMana;
        }

        set
        {
            currentMana = value;
        }
    }

    public void Init(DeckModel deckModel)
    {
        panelView = GetComponentInParent<GamePanelView>();
        occupation = deckModel.OccupationType;
        List<ICard> tempList = new List<ICard>();//创建临时链表复制deckModel中的卡组
        foreach (ICard item in deckModel.CardList)
        {
            tempList.Add(item.DeepClone());
        }
        //将临时链表中的卡以随机顺序存入Cards
        while (tempList.Count!=0)
        {
            int n = Random.Range(0, tempList.Count);
            Cards.Add(tempList[n]);
            tempList.Remove(tempList[n]);
            CardCount++;
        }
    }
    /// <summary>
    /// 随机位置存入卡牌
    /// </summary>
    /// <param name="card"></param>
    public void AddCards(ICard card)
    {
        int temp = Random.Range(0, CardCount + 1);
        if (temp==CardCount)
        {
            Cards.Add(card);
        }
        else
        {
            Cards.Insert(temp, card);
        }
        CardCount++;
    }
    public void AddCards(ICard[] cards)
    {
        foreach (ICard card in cards)
        {
            AddCards(card);
        }
    }

    public void DealCard(PlayerType playerType)
    {
        HandCards.Add(Cards[0]);
        HandCardCount++;
        GameObject go;
        if (playerType != PlayerType.Opponent)
        {
            if (Cards[0].CardType)
            {
                go = Resources.Load<GameObject>("Prefab\\CardS");
            }
            else
            {
                go = Resources.Load<GameObject>("Prefab\\CardF");
            }
        }
        else
        {
            go = Resources.Load<GameObject>("Prefab\\CardBack");
        }
        if (go == null)
        {
            Debug.Log("找不到cardPrefab");
            return;
        }
        if (playerType == PlayerType.Desk)
        {
            GameObject gb = GameObject.Instantiate(go, DeskCreatPoint);
            if (gb == null)
            {
                Debug.Log("生成失败");
            }
            DeskCardCount++;
            gb.name = Cards[0].imageName + DeskCardCount.ToString();
            CardUI cardUI = gb.GetComponent<CardUI>();
            Cards[0].CardPlayerType = PlayerType.Desk;
            cardUI.Card = Cards[0];
            cardUI.SetPostion(playerType, DeskCardCount-1);            
        }
        else if (playerType == PlayerType.Player)
        {
            GameObject gb = GameObject.Instantiate(go, PlayerCreatPoint);
            gb.name = Cards[0].imageName + HandCardCount;
            CardUI cardUI = gb.GetComponent<CardUI>();
            Cards[0].CardPlayerType = PlayerType.Player;
            cardUI.Card = Cards[0];
            gb.transform.localPosition = new Vector3(514,69, 0);
            gb.transform.localScale = new Vector3(0, 0, 0);
        }
        else if (playerType == PlayerType.Opponent)
        {
            GameObject gb = GameObject.Instantiate(go, OpponentCreatPoint);
            gb.name = Cards[0].imageName + HandCardCount;
            CardUI cardUI = gb.GetComponent<CardUI>();
            Cards[0].CardPlayerType = PlayerType.Opponent;
            cardUI.Card = Cards[0];
        }
        Cards.Remove(Cards[0]);
        CardCount--;
    }
    /// <summary>
    /// 用来往桌面上发牌（初始牌的替换）
    /// </summary>
    /// <param name="playerType"></param>
    /// <param name="index"></param>
    public void DealCard(PlayerType playerType,int index)
    {
        HandCards[index] = Cards[0];
        HandCardCount++;
        GameObject go;
        if (playerType != PlayerType.Opponent)
        {
            if (Cards[0].CardType)
            {
                go = Resources.Load<GameObject>("Prefab\\CardS");
            }
            else
            {
                go = Resources.Load<GameObject>("Prefab\\CardF");
            }
        }
        else
        {
            go = Resources.Load<GameObject>("Prefab\\CardBack");
        }
        if (go == null)
        {
            Debug.Log("找不到cardPrefab");
            return;
        }
        if (playerType == PlayerType.Desk)
        {
            GameObject gb = GameObject.Instantiate(go, DeskCreatPoint);
            if (gb == null)
            {
                Debug.Log("生成失败");
            }
            gb.name = Cards[0].imageName + index;
            CardUI cardUI = gb.GetComponent<CardUI>();
            Cards[0].CardPlayerType = PlayerType.Desk;
            cardUI.Card = Cards[0];
            cardUI.SetPostion(playerType, index);
            DeskCardCount++;
        }
        Cards.Remove(Cards[0]);
        CardCount--;
    }
    public void Skill(Occupation skillType)
    {

    }
    /// <summary>
    /// 计算进行扇形排列后的每一张牌的位置信息，存入lis，rolis中
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="handCount"></param>
    public void CalculateCardUI(Transform parent, int handCount)
    {
        lis.Clear();
        rolis.Clear();
        GameObject gb = Resources.Load<GameObject>("Prefab\\getpoint");
        GameObject go = GameObject.Instantiate<GameObject>(gb, parent);
        go.transform.localPosition = Vector3.zero;
        go.transform.localEulerAngles = Vector3.zero;
        Vector3 vector3;
        float roz;
        go.transform.Rotate(new Vector3(0, 0, 1), 30);
        vector3 = go.transform.localPosition;
        lis.Add(vector3);
        go.transform.localPosition = Vector3.zero;
        roz = go.transform.localEulerAngles.z;
        rolis.Add(roz);
        go.transform.localEulerAngles = Vector3.zero;
        for (int i = 1; i < handCount; i++)
        {
            if (i <= handCount / 2)
            {
                go.transform.localPosition = (new Vector3((300 / handCount) * i, 80 / handCount * i, 0));
                vector3 = go.transform.localPosition;
                lis.Add(vector3);
                go.transform.localPosition = Vector3.zero;
            }
            else
            {
                go.transform.localPosition = (new Vector3((300 / handCount) * i, 80 / handCount * (handCount - i), 0));
                vector3 = go.transform.localPosition;
                lis.Add(vector3);
                go.transform.localPosition = Vector3.zero;
            }
            go.transform.localEulerAngles = new Vector3(0, 0, 30 - ((60 / handCount) * i));
            roz = go.transform.localEulerAngles.z;
            rolis.Add(roz);
            go.transform.localEulerAngles = Vector3.zero;
        }
        Destroy(go);
    }
}
