using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour {
    [SerializeField]
    public CardInfoScript Info;
    [SerializeField]
    public CardMovementScript Movement;

    [HideInInspector]
    public Player player;
    [HideInInspector]
    public Card card;
    [HideInInspector]
    public bool isPlayerOneCard;

    public void Init(Card _card, Player _player)
    {
        player = _player;
        card = _card;
        isPlayerOneCard = player.isPlayerOne;

        Info.ShowCardInfo();
    }
}
