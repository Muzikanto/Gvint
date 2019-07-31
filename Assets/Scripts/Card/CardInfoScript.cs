using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardInfoScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField]
    Text score;
    [SerializeField]
    Image type, subtype;
    [SerializeField]
    CardController cardController;

    public void OnPointerEnter(PointerEventData eventData)
    {
        MainScript.ShowPreviewCard(cardController.card, cardController.player);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!cardController.Movement.isDragged)
        {
            MainScript.HidePreviewCard();
        }
    }

    public void ShowCardInfo()
    {
        Card card = cardController.card;

        GetComponent<Image>().sprite = card.sprite;

        if (card.type == CardType.SPEC)
        {
            transform.Find("score").gameObject.SetActive(false);
            type.gameObject.SetActive(false);
            subtype.gameObject.SetActive(false);

            return;
        }

        if (score != null)
        {
            score.text = card.score.ToString();
        }

        switch (card.type)
        {
            case CardType.HELPER:
                type.sprite = Resources.Load<Sprite>("Sprite/icon/helper");
                break;
            case CardType.ARCHER:
                type.sprite = Resources.Load<Sprite>("Sprite/icon/archer");
                break;
            case CardType.KNIGHT:
                type.sprite = Resources.Load<Sprite>("Sprite/icon/knight");
                break;
        }


        switch (cardController.card.subtype)
        {
            case CardSubType.DOCTOR:
                subtype.sprite = Resources.Load<Sprite>("Sprite/icon/doctor");
                break;
            case CardSubType.SPOOK:
                subtype.sprite = Resources.Load<Sprite>("Sprite/icon/spook");
                break;
            case CardSubType.SQUAD:
                subtype.sprite = Resources.Load<Sprite>("Sprite/icon/squad");
                break;
            case CardSubType.NONE:
                subtype.gameObject.SetActive(false);
                break;
        }

        if (cardController.card.god)
        {
            transform.Find("score").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/icon/score_god");
            score.color = new Color(255, 255, 255);
        }    
    }

    public void ActivateAll()
    {
        transform.Find("score").gameObject.SetActive(true);
        type.gameObject.SetActive(true);
        subtype.gameObject.SetActive(true);
    }
}


