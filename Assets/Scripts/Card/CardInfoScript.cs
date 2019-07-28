using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfoScript : MonoBehaviour {
    Text score;
    Image type;
    CardController cardController;

    void Awake()
    {
        score = transform.Find("score").transform.Find("value").GetComponent<Text>();
        type = transform.Find("type").GetComponent<Image>();
        cardController = GetComponent<CardController>();
    }

    public void ShowCardInfo()
    {
        GetComponent<Image>().sprite = cardController.card.sprite;

        score.text = cardController.card.score.ToString();

        switch (cardController.card.type)
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

        if (cardController.card.god)
        {
            transform.Find("score").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/icon/score_god");
            score.color = new Color(255, 255, 255);
        }
       
    }
}


