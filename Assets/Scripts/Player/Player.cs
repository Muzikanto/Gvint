using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int score = 0, handCardsCount = 0;
    public bool isPlayerOne;
    public List<Card> deck = new List<Card>();
    public LineCards helpers;
    public LineCards archers;
    public LineCards knights;
    public PlayerUIIController ui;

    public void setDeck(List<Card> _deck)
    {
        deck = _deck;
        ui.updateCount();
        ui.updateScore();
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

    public void onPlaceCard(Card card)
    {
        switch (card.type)
        {
            case CardType.HELPER:
                helpers.Add(card);
                break;
            case CardType.ARCHER:
                archers.Add(card);
                break;
            case CardType.KNIGHT:
                knights.Add(card);
                break;
        }

        ui.updateScore();

        handCardsCount--;
    }

    public void updateScore()
    {
        score = helpers.score + archers.score + knights.score;
        ui.updateScore();
    }

    public void updateCount()
    {
        ui.updateCount();
    }
}
