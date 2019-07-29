using UnityEngine;
using UnityEngine.EventSystems;

public enum FieldType
{
    SELF_FIELD,
    ENEMY_FIELD,
    SPECS_FIELD
}

public class DropPlaceScript : MonoBehaviour, IDropHandler {
    public FieldType Type;
    public CardType TypeCard;

    public void OnDrop(PointerEventData eventData)
    {
        CardController cardController = eventData.pointerDrag.GetComponent<CardController>();
        Card card = cardController.card;

        bool myTurn = Type == FieldType.SELF_FIELD && MainScript.game.isPlayerTurn;
        bool enemyTurn = Type == FieldType.ENEMY_FIELD && !MainScript.game.isPlayerTurn;

        if ((myTurn || enemyTurn) == false)
        {
            bool isSpook = card.subtype == CardSubType.SPOOK;
            bool isSpec = card.type == CardType.SPEC && Type == FieldType.SPECS_FIELD;
        
            if ((isSpook || isSpec || myTurn || enemyTurn) == false)
            {
                return;
            }
        }

        if (TypeCard == card.type && cardController.Movement.isDraggable)
        {
            switch (card.subtype)
            {
                case CardSubType.SPOOK:
                    MainScript.game.onPlaceSpook(cardController, Type);
                    break;
                case CardSubType.DESTROY:
                    MainScript.game.onPlaceDestoy(cardController);
                    break;
                default:
                    MainScript.game.onPlaceCard(cardController);
                    break;
            }

            cardController.Movement.DefaultParent = transform;            
        }
    }
}
