using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public bool isPlayerOne;
    public LineCards helpers;
    public LineCards archers;
    public LineCards knights;
    public PlayerUIIController ui;

    [HideInInspector]
    public int score = 0, handCardsCount = 0;
    [HideInInspector]
    public List<Card> deck = new List<Card>();

    public void setDeck(List<Card> _deck)
    {
        deck = _deck;
        ui.updateCount();
        ui.updateScore();
        ui.updateDroppingCount();
    }

    public void takeCard()
    {
        if (deck.Count == 0)
        {
            return;
        }

        Card card = deck[0];
        deck.RemoveAt(0);

        GameObject cardGO = Instantiate(CardManager.cardPref, ui.hand);
        cardGO.GetComponent<CardController>().Init(card, this);

        handCardsCount++;
        ui.updateCount();
        ui.updateDeckCount();
    }

    public void onPlaceCard(CardController cardController)
    {
        switch (cardController.card.type)
        {
            case CardType.HELPER:
                helpers.Add(cardController);
                break;
            case CardType.ARCHER:
                archers.Add(cardController);
                break;
            case CardType.KNIGHT:
                knights.Add(cardController);
                break;
        }

        ui.updateScore();

        handCardsCount--;
    }

    public void updateScore()
    {
        helpers.calcScore();
        archers.calcScore();
        knights.calcScore();
        score = helpers.score + archers.score + knights.score;
        ui.updateScore();
    }

    public void updateCount()
    {
        ui.updateCount();
    }
}
