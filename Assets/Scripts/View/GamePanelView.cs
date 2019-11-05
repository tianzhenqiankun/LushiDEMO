using strange.extensions.mediation.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class GamePanelView:View
{
    public Image playerHead;
    public Image opponentHead;
    public Image VS;
    public Image PlayerHP;
    public Image OpponentHP;
    public Image PlayerDeckUI;
    public Image OpponentDeckUI;
    public Text tisps;
    public Transform playerHeadTrs1;
    public Transform opponentHeadTrs1;
    public Transform playerHeadTrs2;
    public Transform opponentHeadTrs2;
    public Transform DeskCreatPoint;
    public Transform PlayerCreatPoint;
    public Transform OpponentCreatPoint;
    public Button sureButton;
    public PlayerControl playerControl;
    public OpponentControl opponentControl;
    /// <summary>
    /// 设置血量UI显示
    /// </summary>
    /// <param name="character"></param>
    public void SetHpUI(CharacterControlBase character)
    {
        if (!PlayerHP.gameObject.activeInHierarchy || !OpponentHP.gameObject.activeInHierarchy)
        {
            PlayerHP.gameObject.SetActive(true);
            OpponentHP.gameObject.SetActive(true);
        }
        Color color = new Color();
        if (character.CurrentHP == character.HpBase)
        {
            color = Color.white;
            if (character.IsPlayer)
            {
                PlayerHP.GetComponentInChildren<Text>().text = character.CurrentHP.ToString();
                PlayerHP.GetComponentInChildren<Text>().color = color;
            }
            else
            {
                OpponentHP.GetComponentInChildren<Text>().text = character.CurrentHP.ToString();
                OpponentHP.GetComponentInChildren<Text>().color = color;
            }
        }
        if (character.CurrentHP < character.HpBase)
        {
            color = Color.red;
            if (character.IsPlayer)
            {
                PlayerHP.GetComponentInChildren<Text>().text = character.CurrentHP.ToString();
                PlayerHP.GetComponentInChildren<Text>().color = color;
            }
            else
            {
                OpponentHP.GetComponentInChildren<Text>().text = character.CurrentHP.ToString();
                OpponentHP.GetComponentInChildren<Text>().color = color;
            }
        }
        if (character.CurrentHP > character.HpBase)
        {
            color = Color.green;
            if (character.IsPlayer)
            {
                PlayerHP.GetComponentInChildren<Text>().text = character.CurrentHP.ToString();
                PlayerHP.GetComponentInChildren<Text>().color = color;
            }
            else
            {
                OpponentHP.GetComponentInChildren<Text>().text = character.CurrentHP.ToString();
                OpponentHP.GetComponentInChildren<Text>().color = color;
            }
        }
    }

    public void SetRemainCardUI()
    {
        if (!PlayerDeckUI.gameObject.activeInHierarchy || !OpponentDeckUI.gameObject.activeInHierarchy)
        {
            PlayerDeckUI.gameObject.SetActive(true);
            OpponentDeckUI.gameObject.SetActive(true);
        }

        PlayerDeckUI.GetComponentInChildren<Text>().text = playerControl.CardCount.ToString();
        OpponentDeckUI.GetComponentInChildren<Text>().text = opponentControl.CardCount.ToString();

    }
}

