using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleSheetsToUnity;

public enum CardType
{
    ARCHER,
    KNIGHT,
    HELPER,
    SPEC
}

public enum CardSubType
{
    DOCTOR,
    SPOOK,
    SQUAD,
    NONE,
    DESTROY,
    DUMMY
}

public struct Card {
    public Sprite sprite;
    public int score, basescore, id;
    public CardType type;
    public CardSubType subtype;
    public bool god;

    public Card(int _id, int _score, CardType _type, CardSubType _subtype, bool _god)
    {
        id = _id;
        score = _score;
        basescore = score;
        type = _type;
        subtype = _subtype;
        god = _god;

        switch (subtype)
        {
            case CardSubType.DESTROY:
                sprite = Resources.Load<Sprite>("Sprite/Card/destroy");
                break;
            case CardSubType.DUMMY:
                sprite = Resources.Load<Sprite>("Sprite/Card/dummy");
                break;
            default:
                sprite = Resources.LoadAll<Sprite>("Sprite/Card/map")[_id];
                break;
        }
    }
}

public static class CardManager
{
    public static List<Card> cards;
    public static GameObject cardPref;

    public static void Init()
    {
        cards = new List<Card>();
        cards.Add(new Card(0, 0, CardType.HELPER, CardSubType.SPOOK, false));
        cards.Add(new Card(1, 1, CardType.HELPER, CardSubType.DOCTOR, false));
        cards.Add(new Card(2, 2, CardType.KNIGHT, CardSubType.SPOOK, false));
        cards.Add(new Card(3, 3, CardType.KNIGHT, CardSubType.NONE, true));
        cards.Add(new Card(4, 4, CardType.ARCHER, CardSubType.SQUAD, false));
        cards.Add(new Card(4, 4, CardType.ARCHER, CardSubType.SQUAD, false));
        cards.Add(new Card(5, 0, CardType.SPEC, CardSubType.DUMMY, false));
        cards.Add(new Card(6, 0, CardType.SPEC, CardSubType.DESTROY, false));

        cards.Add(new Card(7, 7, CardType.ARCHER, CardSubType.NONE, false));
        cards.Add(new Card(8, 8, CardType.ARCHER, CardSubType.NONE, false));
        cards.Add(new Card(9, 9, CardType.ARCHER, CardSubType.NONE, false));
        cards.Add(new Card(10, 10, CardType.HELPER, CardSubType.NONE, false));
        cards.Add(new Card(11, 11, CardType.HELPER, CardSubType.NONE, false));
        cards.Add(new Card(12, 12, CardType.HELPER, CardSubType.NONE, false));
        cards.Add(new Card(13, 13, CardType.HELPER, CardSubType.NONE, false));
    }
}
