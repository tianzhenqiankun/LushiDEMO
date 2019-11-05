using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
{

    private ICard card;
    public Text manaCostText;
    public Text cardNameText;
    public Text describeText;
    public Text attackText;
    public Text hpText;
    public Image cardSType;
    public Image cardImage;
    public Image replaceX;
    public Button button;
    private Transform playerDeckUI;
    private Transform opponentDeckUI;
    
    public ICard Card
    {
        get
        {
            return card;
        }

        set
        {
            card = value;
            if (card.CardPlayerType != PlayerType.Opponent)
            {
                if (card.CardType)
                {
                    attackText.text = card.Attack.ToString();
                    SetHpText(card.CurrentHp);
                    cardSType.gameObject.SetActive(true);
                    cardSType.GetComponentInChildren<Text>().text = card.SType.ToString();
                }
                manaCostText.text = card.ManaCost.ToString();
                cardNameText.text = card.CardName;
                describeText.text = card.SkillDescribe;
                cardImage.sprite = Resources.Load<Sprite>("Textures\\Card\\" + card.imageName);
                if (cardImage.sprite == null)
                {
                    Debug.Log(card.imageName + "不存在");
                }
                
            }
            button.onClick.AddListener(this.OnButtonClick);
        }
    }

    public Transform PlayerDeckUI
    {
        get
        {
            if (playerDeckUI == null)
            {
                playerDeckUI = transform.GetComponentInParent<Transform>().Find("PlayerDeckUI");
            }
            return playerDeckUI;
        }

        set
        {
            playerDeckUI = value;
        }
    }

    public Transform OpponentDeckUI
    {
        get
        {
            if (opponentDeckUI == null)
            {
                opponentDeckUI = transform.GetComponentInParent<Transform>().Find("OpponentDeckUI");
            }
            return opponentDeckUI;
        }

        set
        {
            opponentDeckUI = value;
        }
    }
    private Vector3 nowPostion;
    private float nowRotationZ;
    private int nowIndex;
    private bool ISTransing=false;//是否正在进行位置的改变
    private Vector3 offset;
    private RectTransform rt;
    public void SetHpText(int num)
    {
        hpText.text = num.ToString();
        if (num<card.Hp)
        {
            hpText.color = Color.red;
        }
        else
        {
            hpText.color = Color.white;
        }
    }

    public void SetPostion(PlayerType playerType,int index,List<Vector3> lis, List<float> rolis)
    {
        transform.SetSiblingIndex(index);
        switch (playerType)
        {
            case PlayerType.Desk:
                {
                    transform.localPosition = new Vector3(535, -167, 0);
                    transform.localScale = new Vector3(0,0,0);
                    Vector3 v = Vector3.right* 130 * index;                    
                    transform.DOScale(new Vector3(1, 1, 1),0.5f).OnComplete(()=> { transform.DOLocalMove(v, 0.5f); });                   
                }
                break;
            case PlayerType.Player:
                {
                    nowIndex = index;
                    Vector3 v = lis[index];
                    v = new Vector3(v.x, v.y - 66, v.z);
                    if (transform.localScale==Vector3.zero)
                    {
                        transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(() =>
                        {
                            transform.DOLocalMove(v, 0.5f);
                            transform.DOLocalRotate(new Vector3(0, 0, rolis[index]), 0.5f).OnComplete(() =>
                            {
                                Card.CardPlayerType = PlayerType.Player;
                                nowPostion = transform.localPosition;
                                nowRotationZ = transform.localEulerAngles.z;
                            });
                        });
                    }
                    else
                    {
                        transform.DOLocalMove(v, 0.5f);
                        transform.DOLocalRotate(new Vector3(0, 0, rolis[index]), 0.5f).OnComplete(() =>
                        {
                            Card.CardPlayerType = PlayerType.Player;
                            nowPostion = transform.localPosition;
                            nowRotationZ = transform.localEulerAngles.z;
                        });
                    }
                }
                break;
            case PlayerType.Opponent:
                {

                    if (index==lis.Count-1)
                    {
                        transform.localPosition = new Vector3(535, -126, 0);
                        transform.localScale = new Vector3(0, 0, 0);
                        Vector3 v = lis[index];
                        v = new Vector3(v.x, -v.y + 66, v.z);
                        transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(
                         () =>
                        {
                        transform.DOLocalMove(v, 0.5f);
                        transform.DOLocalRotate(new Vector3(0, 0, 360 - rolis[index]), 0.5f);
                        }
                        );
                    }
                    else
                    {
                        Vector3 v = lis[index];
                        v = new Vector3(v.x, -v.y + 66, v.z);
                        transform.DOLocalMove(v, 0.5f);
                        transform.DOLocalRotate(new Vector3(0, 0, 360 - rolis[index]), 0.5f);
                    }


                }
                break;
            default:
                break;
        }
    }
    public void SetPostion(PlayerType playerType, int index)
    {
        transform.SetSiblingIndex(index);
        switch (playerType)
        {
            case PlayerType.Desk:
                {
                    transform.localPosition = new Vector3(535, -167, 0);
                    transform.localScale = new Vector3(0, 0, 0);
                    Vector3 v = Vector3.right * 130 * index;
                    transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(() => { transform.DOLocalMove(v, 0.5f); });
                }
                break;
            case PlayerType.Player:
                {
                    transform.localPosition = new Vector3(535, -167, 0);
                    transform.localScale = new Vector3(0, 0, 0);
                    Vector3 v = Vector3.right * 80 * index;
                    transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(() => { transform.DOLocalMove(v, 0.5f); });
                }
                break;
            case PlayerType.Opponent:
                {
                    transform.localPosition = new Vector3(535, -126, 0);
                    transform.localScale = new Vector3(0, 0, 0);
                    Vector3 v = Vector3.right * 80 * index;
                    transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(() => { transform.DOLocalMove(v, 0.5f); });
                }
                break;
            default:
                break;
        }
    }

    public void ReturnToDeck()
    {
        Vector3 v = new Vector3(535, -167, 0);
        transform.DOLocalMove(v, 0.5f).OnComplete(() => { transform.DOScale(new Vector3(0, 0, 0), 0.5f).OnComplete(
           ()=>Destroy(this.gameObject) );});        
    }
    public void OnButtonClick()
    {
        if (Card.CardPlayerType==PlayerType.Desk)
        {
            //处理初始发牌后的起始手牌的选择
            if (replaceX.gameObject.activeInHierarchy)
            {
                replaceX.gameObject.SetActive(false);
            }
            else
            {
                replaceX.gameObject.SetActive(true);
            }
            
        }
        if (Card.CardPlayerType==PlayerType.None)
        {
            //处理一些卡牌的发现一张卡牌的选择
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!ISTransing)
        {
            if (Card.CardPlayerType == PlayerType.Player)
            {


                transform.SetSiblingIndex(11);
                ISTransing = true;
                transform.DOLocalMove(new Vector3(nowPostion.x, nowPostion.y + 80, nowPostion.z), 0.5f);
                transform.DOLocalRotate(new Vector3(0, 0, 0), 0.5f).OnComplete(()=>ISTransing=false);
            }
        }

    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {

        if (Card.CardPlayerType == PlayerType.Player)
        {
            transform.SetSiblingIndex(nowIndex);
            ISTransing = true;
            transform.DOLocalMove(new Vector3(nowPostion.x, nowPostion.y, nowPostion.z), 0.5f);
            transform.DOLocalRotate(new Vector3(0, 0, nowRotationZ), 0.5f).OnComplete(() => ISTransing = false);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 goMousePos;
        rt = GetComponent<RectTransform>();
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, null, out goMousePos))
        {
            offset = rt.position - new Vector3(goMousePos.x, goMousePos.y, 0);
        }
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Card.CardPlayerType==PlayerType.Player)
        {
            Vector3 goMousePos;
            rt = GetComponent<RectTransform>();
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, null, out goMousePos))
            {
                rt.position = offset + new Vector3(goMousePos.x, goMousePos.y, 0);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        PlayerControl playerControl = GameObject.Find("PlayHead").GetComponent<PlayerControl>();

        if (transform.localPosition.y>170)
        {
            if (Card.ManaCost <= playerControl.CurrentMana)
            {
                GameObject gb = GameObject.Find("SuiCongView");
                RoundMediator roundMediator = GameObject.Find("BG").GetComponent<RoundMediator>();
                Card.Skill(gb);
                transform.SetParent(gb.transform);
                playerControl.HandCards.Remove(this.card);
                playerControl.HandCardCount--;
                playerControl.SortCardUI();
                playerControl.CurrentMana -= Card.ManaCost;
                roundMediator.CostMana(Card.ManaCost);
                Destroy(this.gameObject);
            }
            else
            {
                GamePanelMediator gamePanelMediator = GameObject.Find("GamePanel").GetComponent<GamePanelMediator>();
                gamePanelMediator.ShowTips("法力值不足");
            }
        }
        if (Card.CardPlayerType == PlayerType.Player)
        {
            transform.SetSiblingIndex(nowIndex);
            ISTransing = true;
            transform.DOLocalMove(new Vector3(nowPostion.x, nowPostion.y, nowPostion.z), 0.5f);
            transform.DOLocalRotate(new Vector3(0, 0, nowRotationZ), 0.5f).OnComplete(() => ISTransing = false);
        }
    }
}
