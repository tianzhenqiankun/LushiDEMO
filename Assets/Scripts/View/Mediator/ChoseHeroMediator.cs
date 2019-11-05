using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChoseHeroMediator : EventMediator {

    [Inject]
    public ChoseHeroView choseHeroView { get; set; }
    [Inject]
    public CollectionModel collectionModel { get; set; }

    public string currentChose;
    public List<DeckModel> deckModelsList = new List<DeckModel>();
    public List<Button> HeroGroupButtons = new List<Button>();
    public override void OnRegister()
    {
        //加载卡组列表
        LoadDeckList();
        choseHeroView.chose.enabled = false;
        //绑定事件
        choseHeroView.chose.onClick.AddListener(OnChoseButtonClick);
    }
    public override void OnRemove()
    {
        foreach (var button in HeroGroupButtons)
        {
            button.onClick.RemoveAllListeners();
        }
        choseHeroView.chose.onClick.RemoveListener(OnChoseButtonClick);
    }

    public void LoadDeckList()
    {
        DeckModel deckModel;
        foreach (KeyValuePair<string,DeckModel> deck in collectionModel.DeckCollection)//取出所有卡组，存入deckModelList
        {
            deckModelsList.Add(deck.Value);
        }
        int n;
        if (collectionModel.DeckCollection.Count>=9)
        {
            n = 9;
        }
        else
        {
            n = collectionModel.DeckCollection.Count;
        }
        for (int i = 0; i < n; i++)//根据卡组创建出Button
        {
            deckModel = deckModelsList[i];
            if (deckModel==null)
            {
                break;
            }
            GameObject go = Resources.Load<GameObject>("Prefab\\button");
            if (go==null)
            {
                Debug.Log("button不存在");
                break;
            }
            GameObject goButton= GameObject.Instantiate(go,choseHeroView.HeroGroup.transform);
            goButton.GetComponentInChildren<Text>().text = deckModel.Name;
            goButton.GetComponent<Button>().onClick.AddListener(()=> { OnChoseHeroButtonClick(goButton.GetComponentInChildren<Text>()); });
            HeroGroupButtons.Add(goButton.GetComponent<Button>());
        }
    }
    public void OnChoseHeroButtonClick(Text deckName)
    {
        string occupationName = collectionModel.DeckCollection[deckName.text].OccupationType.ToString();
        currentChose = deckName.text;
        choseHeroView.HeroSelected.sprite = Resources.Load<Sprite>("Textures\\" + occupationName + "Head");
        if (choseHeroView.HeroSelected.sprite==null)
        {
            Debug.Log("加载图片失败");
        }
        else
        {
            choseHeroView.chose.enabled = true;
            Color color = new Color(255, 255, 255, 1);
            choseHeroView.HeroSelected.color = color;
        }
    }

    public void OnChoseButtonClick()
    {
        if (currentChose=="")
        {
            Debug.Log("currentChose为空！");
            return;
        }
        Destroy(choseHeroView.gameObject);
        BattleStartArgs battleStartArgs = new BattleStartArgs()
        {
            playerDeckModel = collectionModel.DeckCollection[currentChose],
            opponentDeckModel=collectionModel.DeckCollection[currentChose]
        };
        Tools.CreatUIPanel(PanelType.GamePanel);
        dispatcher.Dispatch(ViewEvent.BattleStart,battleStartArgs); 

    }
}
