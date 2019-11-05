using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;

[Serializable]
public class ICard:ICloneable  {


    int manaCost;
    int currentCost;
    int attack;
    int hp;
    int currentHp;
    bool cardType;
    string cardName;
    string skillDescribe;
    public string imageName;

    CardSType sType;
 
    PlayerType cardPlayerType;
    public int ManaCost
    {
        get
        {
            return manaCost;
        }

        set
        {
            manaCost = value;
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

    public int Hp
    {
        get
        {
            return hp;
        }

        set
        {
            hp = value;
        }
    }
    /// <summary>
    /// 是否为随从
    /// </summary>
    public bool CardType
    {
        get
        {
            return cardType;
        }

        set
        {
            cardType = value;
        }
    }

    public string CardName
    {
        get
        {
            return cardName;
        }

        set
        {
            cardName = value;
        }
    }

    public string SkillDescribe
    {
        get
        {
            return skillDescribe;
        }

        set
        {
            skillDescribe = value;
        }
    }

    public int CurrentHp
    {
        get
        {
            return currentHp;
        }

        set
        {
            currentHp = value;
        }
    }

    public int CurrentCost
    {
        get
        {
            return currentCost;
        }

        set
        {
            currentCost = value;
        }
    }
    /// <summary>
    /// 随从类型（野兽，鱼人等）
    /// </summary>
    public CardSType SType
    {
        get
        {
            return sType;
        }

        set
        {
            sType = value;
        }
    }
    /// <summary>
    /// 卡牌当前所有者（位置）
    /// </summary>
    public PlayerType CardPlayerType
    {
        get
        {
            return cardPlayerType;
        }

        set
        {
            cardPlayerType = value;
        }
    }

    public virtual void Skill(GameObject gb) { }
    /// <summary>
    /// 浅拷贝
    /// </summary>
    /// <returns></returns>
    public object Clone()
    {
        return MemberwiseClone();
    }
    /// <summary>
    /// 利用序列化进行深拷贝
    /// </summary>
    /// <returns></returns>
    public ICard DeepClone()
    {
        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream ms = new MemoryStream();
        bf.Serialize(ms, this); //复制到流中
        ms.Position = 0;
        return ((ICard)bf.Deserialize(ms));
    }
}
