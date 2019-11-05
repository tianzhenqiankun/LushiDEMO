using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class RequestDealCardCommand:EventCommand
{
    public override void Execute()
    {
        //通过arg传入参数包括发给谁，发几张，发到什么位置
        //deal
        
    }

    public override void Release()
    {
        
    }

    public void DealCard(int n,bool isPlayer)
    {
        //通过dispacher调用gamePanelMediater中的发牌方法，再调用charactercontrol中的方法，
    }
}

