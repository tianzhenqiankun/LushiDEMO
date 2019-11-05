using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class CardKillCommand : ICard
{

    public CardKillCommand()
    {
        ManaCost = 2;
        CurrentCost = ManaCost;
        CardType = false;
        CardName = "杀戮命令";
        SkillDescribe = "     造成2点伤害，如果你控制一个野兽，改为造成3点伤害。";
        imageName = this.ToString();
    }
    public override void Skill(GameObject gb)
    {
        Debug.Log("this is a card");
    }
}

