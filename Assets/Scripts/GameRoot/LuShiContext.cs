using strange.extensions.context.api;
using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuShiContext : MVCSContext {

    public LuShiContext(MonoBehaviour view) : base(view) { }

    protected override void mapBindings()
    {
        //Start
        commandBinder.Bind(ContextEvent.START).To<StartCommand>();
        //Command
        commandBinder.Bind(CommandEvent.RequestDealCard).To<RequestDealCardCommand>();
        commandBinder.Bind(CommandEvent.RequestDealStartCard).To<RequestDealStartCardCommand>();
        //Model
        injectionBinder.Bind<CollectionModel>().To<CollectionModel>().ToSingleton();
        injectionBinder.Bind<RoundModel>().To<RoundModel>().ToSingleton();
        //View
        mediationBinder.Bind<ChoseHeroView>().To<ChoseHeroMediator>();
        mediationBinder.Bind<GamePanelView>().To<GamePanelMediator>();
        mediationBinder.Bind<RoundView>().To<RoundMediator>();
        mediationBinder.Bind<SuiCongView>().To<SuiCongMediator>();
    }
}
