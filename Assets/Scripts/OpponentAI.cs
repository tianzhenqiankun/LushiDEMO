using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class OpponentAI
{
    public RoundModel roundModel;
    public void SmartAI()
    {

    }
    public void Pass()
    {
        roundModel.BeginWith(true);
    }
}

