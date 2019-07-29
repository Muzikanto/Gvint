using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfoScript : MonoBehaviour {
    public Text score;
    public Image type, subtype;
    public CardController cardController;

    public void ShowCardInfo()
    {
        Card card = cardController.card;

        GetComponent<Image>().sprite = card.sprite;

        if (card.type == CardType.SPEC)
        {
            Destroy(transform.Find("score").gameObject);
            Destroy(type.gameObject);
            Destroy(subtype.gameObject);

            return;
        }

        score.text = card.score.ToString();

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
}


