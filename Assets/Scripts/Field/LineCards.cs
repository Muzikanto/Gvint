using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineCards: MonoBehaviour {
   // [HideInInspector]
   // public List<CardController> cards = new List<CardController>();
    [HideInInspector]
    public bool baff = false;
    [HideInInspector]
    public int score = 0;

    public Text scoreText;

    private void Start()
    {
        updateScore();
    }

    public void updateScore()
    {
        List<CardController> cards = getCards();
        int secondScore = 0;

        foreach (CardController cardController in cards)
        {
            if (cardController.card.subtype != CardSubType.SQUAD)
            {
                secondScore += cardController.card.score;
            }
        }

        foreach(List<CardController> squad in findSquads(cards))
        {
            foreach(CardController cardController in squad)
            {
                cardController.Info.UpdateBaffCard(squad.Count);
            }

            secondScore += squad.Count * squad[0].card.score;
        }


        score = secondScore;
        scoreText.text = score.ToString();
    }

    public List<List<CardController>> findSquads(List<CardController> cards)
    {
        List<List<CardController>> listSquads = new List<List<CardController>>();

        foreach (CardController cardController in getCards())
        {
            if (cardController.card.subtype == CardSubType.SQUAD)
            {
                List<CardController> squad = null;

                foreach (List<CardController> findSquad in listSquads)
                {
                    if (findSquad[0].card.id == cardController.card.id)
                    {
                        squad = findSquad;
                        break;
                    }
                }

                if (squad == null)
                {
                    squad = new List<CardController>();
                    squad.Add(cardController);
                    listSquads.Add(squad);
                } else {
                    squad.Add(cardController);
                }
            }
        }

        return listSquads;
    }

    public List<CardController> getCards()
    {
        List<CardController> list = new List<CardController>();

        foreach(Transform child in transform.Find("Container"))
        {
            CardController cardController = child.GetComponent<CardController>();

            if (cardController)
            {
                list.Add(cardController);
            }
        }

        return list;
    }
}
