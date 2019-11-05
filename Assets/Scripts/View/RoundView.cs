using strange.extensions.mediation.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class RoundView:View
{
    public PlayerControl playerControl;
    public OpponentControl opponentControl;
    public Text playerCurrentMana;
    public Text playerTotalMana;
    public Text opponentCurrentMana;
    public Text opponentTotalMana;
    public GameObject[] manaObjects;
    public Button endButton;
}

