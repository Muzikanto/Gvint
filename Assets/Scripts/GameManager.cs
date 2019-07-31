using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    public MainScript instance;

    public bool isPlayerTurn = true;
    public Player player1;
    public Player player2;

    public GameManager(MainScript _instance, Player _player1, Player _player2)
    {
        instance = _instance;
        player1 = _player1;
        player2 = _player2;
    }

    public void onPlaceCard(CardController cardController)
    {
        if (isPlayerTurn)
        {
            player1.onPlaceCard(cardController);
            player1.updateScore();
            player1.updateCount();

            changeTurn();
        } else
        {
            player2.onPlaceCard(cardController);
            player2.updateScore();
            player2.updateCount();

            changeTurn();
        }
    }

    public void changeTurn()
    {
        if(player2.handCardsCount == 0 && player1.handCardsCount == 0)
        {
            instance.ShowVictory();
        } else
        {
            isPlayerTurn = !isPlayerTurn;
        }

        instance.onChangeTurn();
    }

    public void onPlaceSpook(CardController cardController, FieldType Type)
    {
        if (isPlayerTurn)
        {
            if(Type == FieldType.ENEMY_FIELD)
            {
                player1.takeCard();
                player1.takeCard();

                player2.onPlaceCard(cardController);

                player1.updateCount();
                player2.updateScore();

                changeTurn();
            } else {
                onPlaceCard(cardController);
            }
        } else {
            if(Type == FieldType.SELF_FIELD)
            {
                player2.takeCard();
                player2.takeCard();

                player1.onPlaceCard(cardController);

                player2.updateCount();
                player1.updateScore();

                changeTurn();
            } else {
                onPlaceCard(cardController);
            }
        }
    }

    public void onPlaceDestoy(CardController destroyCardController)
    {
        int maxScore = 0;
        List<CardController> allCards = new List<CardController>();

        allCards.AddRange(player1.helpers.cards);
        allCards.AddRange(player1.archers.cards);
        allCards.AddRange(player1.knights.cards);
        allCards.AddRange(player2.knights.cards);
        allCards.AddRange(player2.archers.cards);
        allCards.AddRange(player2.helpers.cards);

        foreach (CardController cardController in allCards)
        {
            if (cardController.card.god == false) {
                if (cardController.card.score > maxScore)
                {
                    maxScore = cardController.card.score;
                }
            }
        }

        MoveToDropping(player1.helpers, maxScore);
        MoveToDropping(player1.archers, maxScore);
        MoveToDropping(player1.knights, maxScore);
        MoveToDropping(player2.knights, maxScore);
        MoveToDropping(player2.archers, maxScore);
        MoveToDropping(player2.helpers, maxScore);

        player1.updateScore();
        player2.updateScore();
        player1.ui.updateDroppingCount();
        player2.ui.updateDroppingCount();
 
        changeTurn();

        instance.StartCoroutine(onPlaceDestroy(destroyCardController));
    }

    public static void MoveToDropping(LineCards line, int value)
    {
        for (int i = 0; i < line.cards.Count; i++)
        {
            CardController cardController = line.cards[i];

            if (cardController.card.score == value)
            {
                cardController.Movement.MoveToParent(cardController.player.ui.dropping.transform);
                line.cards.RemoveAt(i);
            }
        }
    }

    IEnumerator onPlaceDestroy(CardController cardController)
    {
        yield return new WaitForSeconds(2);

        GameObject.Destroy(cardController.gameObject);
    }
}
