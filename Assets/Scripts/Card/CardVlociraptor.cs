using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class CardVelociraptor : ICard
{

    public CardVelociraptor()
    {
        ManaCost = 2;
        CurrentCost = ManaCost;
        Attack = 3;
        Hp = 2;
        CurrentHp = Hp;
        CardType = true;
        SType = CardSType.野兽;
        CardName = "沼泽迅猛龙";
        SkillDescribe = "     战吼：召唤一条龙。";
        imageName = this.ToString();
    }
    public override void Skill(GameObject gb)
    {
        gb.GetComponent<SuiCongMediator>().CreatSuiCong(this);
        
    }
}

