using UnityEngine;
using UnityEngine.EventSystems;

public enum FieldType
{
    SELF_HAND,
    SELF_FIELD,
    ENEMY_FIELD,
    ENEMY_HAND,
}

public class DropPlaceScript : MonoBehaviour, IDropHandler {
    public FieldType Type;
    public CardType TypeCard;

    public void OnDrop(PointerEventData eventData)
    {
        // if (Type != FieldType.SELF_FIELD)
        if (Type != FieldType.SELF_FIELD && MainScript.game.isPlayerTurn || Type == FieldType.SELF_FIELD && !MainScript.game.isPlayerTurn)
        {
            return;
        }

        CardController cardController = eventData.pointerDrag.GetComponent<CardController>();
        Card card = cardController.card;

        if (cardController && TypeCard == card.type && cardController.Movement.isDraggable)
        {
            cardController.Movement.DefaultParent = transform;
            MainScript.game.onPlayerPlaceCard(card);
        }
    }
}
