using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour {
    public CardInfoScript Info;
    public CardMovementScript Movement;
    public Card card;
    public bool isPlayerOneCard;
    public Player player;

    public void Init(Card _card, Player _player)
    {
        player = _player;
        card = _card;
        isPlayerOneCard = player.isPlayerOne;

        Info = gameObject.AddComponent<CardInfoScript>();
        Movement = gameObject.AddComponent<CardMovementScript>();

        Info.ShowCardInfo();
    }
}
