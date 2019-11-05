using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCommand : Command {
    [Inject]
    public CollectionModel collectionModel { get; set; }
    public override void Execute()
    {
        Debug.Log("Game Start!");
        //初始化
        Tools.CreatUIPanel(PanelType.ChoseHero);
        collectionModel.Init();
    }

}
