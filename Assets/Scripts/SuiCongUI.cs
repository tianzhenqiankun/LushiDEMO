using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SuiCongUI : MonoBehaviour, IDragHandler
{

    public Image uIimage;
    public Text hpText;
    public Text attackText;
    private int hp;
    private int attack;
    public Image riggerImage;
    private ICard card;

    public ICard Card
    {
        get
        {
            return card;
        }

        set
        {
            card = value;
            uIimage.sprite = Resources.Load<Sprite>("Textures\\Card\\" + card.imageName);
            Hp = card.Hp;
            Attack = card.Attack;
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
            hpText.text = hp.ToString();
            if (hp>card.Hp)
            {
                hpText.color = Color.green;
            }else if (hp < card.Hp)
            {
                hpText.color = Color.red;
            }
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
            attackText.text = attack.ToString();
            if (attack > card.Attack)
            {
                attackText.color = Color.green;
            }
            else if (attack < card.Attack)
            {
                attackText.color = Color.red;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("qqqq");
    }
}
