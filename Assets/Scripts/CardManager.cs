using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleSheetsToUnity;

public enum CardType
{
    ARCHER,
    KNIGHT,
    HELPER
}

public struct Card {
    public Sprite sprite;
    public int score, id;
    public CardType type;
    public bool god;

    public Card(int _id, int _score, CardType _type, bool _god = false)
    {
        id = _id;
        score = _score;
        sprite = Resources.LoadAll<Sprite>("Sprite/Card/map")[_id];
        type = _type;
        god = _god;
    }
}

public static class CardManager
{
    public static List<Card> cards;
    public static GameObject cardPref;

    public static void Init()
    {
        cards = new List<Card>();
        cards.Add(new Card(0, 0, CardType.ARCHER));
        cards.Add(new Card(1, 1, CardType.ARCHER));
        cards.Add(new Card(2, 2, CardType.ARCHER));
        cards.Add(new Card(3, 3, CardType.KNIGHT));
        cards.Add(new Card(4, 4, CardType.KNIGHT));
        cards.Add(new Card(5, 5, CardType.KNIGHT));
        cards.Add(new Card(6, 6, CardType.HELPER));
        cards.Add(new Card(7, 7, CardType.HELPER));
        cards.Add(new Card(8, 8, CardType.HELPER));
        cards.Add(new Card(9, 9, CardType.ARCHER, true));
        cards.Add(new Card(10, 10, CardType.HELPER, true));
    }
}
