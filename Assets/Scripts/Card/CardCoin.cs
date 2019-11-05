using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class CardCoin:ICard
{
    public CardCoin()
    {
        ManaCost = 0;
        CurrentCost = ManaCost;
        CardType = false;
        CardName = "幸运币";
        SkillDescribe = "     获得一个仅在本回合可用的法力水晶";
        imageName = this.ToString();
    }
    public override void Skill(GameObject gb)
    {
        Debug.Log("this is a card");
    }
}

