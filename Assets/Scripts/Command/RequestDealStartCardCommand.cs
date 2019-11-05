using strange.extensions.command.impl;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class RequestDealStartCardCommand:EventCommand
{
    DealCardArgs args = new DealCardArgs();
    DealCardArgs args2 = new DealCardArgs();
    public override void Execute()
    {
        int temp = Random.Range(0, 2);
        if (temp==1)
        {
            args.playerType = PlayerType.Desk;
            args.count = 3;
            args2.playerType = PlayerType.Opponent;
            args2.count = 4;    
        }
        else
        {
            args.playerType = PlayerType.Desk;
            args.count = 4;
            args2.playerType = PlayerType.Opponent;
            args2.count = 3;
        }
        dispatcher.Dispatch(ViewEvent.DealCard, args);
        dispatcher.Dispatch(ViewEvent.DealCard, args2);
    }
    public override void Release()
    {
        
    }
}

