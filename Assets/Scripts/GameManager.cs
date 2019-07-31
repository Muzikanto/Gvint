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

    public void changeTurn()
    {
        if (player2.ui.hand.childCount == 0 && player1.ui.hand.childCount == 0)
        {
            instance.ShowVictory();
        }
        else
        {
            isPlayerTurn = !isPlayerTurn;
        }

        instance.onChangeTurn();
    }

    public void onPlaceCard(CardController cardController, FieldType Type)
    {
        Player turnPlayer = isPlayerTurn ? player1 : player2;
        Player turnNextPlayer = isPlayerTurn ? player2 : player1;
    
        switch (cardController.card.subtype)
        {
            case CardSubType.SPOOK:
                onPlaceSpook(cardController, Type, turnPlayer, turnNextPlayer);
                break;
            case CardSubType.DESTROY:
                onPlaceDestroy(cardController);
                break;
            case CardSubType.DOCTOR:
                onPlaceDoctor(cardController, turnPlayer);
                break;
            case CardSubType.SQUAD:
                onPlaceSquad(cardController, turnPlayer);
                break;
            default:
                onPlaceStandart(cardController, turnPlayer);
                break;
        }
    }

    public void onPlaceStandart(CardController cardController, Player player)
    {
        player.onPlaceCard(cardController);
        player.updateScore();
        player.updateCount();

        changeTurn();
    }

    public void onPlaceSpook(CardController cardController, FieldType Type, Player turnPlayer, Player turnNextPlayer)
    {
        if (Type == FieldType.ENEMY_FIELD && turnPlayer.isPlayerOne || Type == FieldType.SELF_FIELD && !turnPlayer.isPlayerOne)
        {
            turnPlayer.takeCard();
            turnPlayer.takeCard();

            turnNextPlayer.onPlaceCard(cardController);

            turnPlayer.updateCount();
            turnNextPlayer.updateScore();

            changeTurn();
        }
        else
        {
            onPlaceStandart(cardController, turnPlayer);
        }
    }

    public void onPlaceDestroy(CardController destroyCardController)
    {
        int maxScore = 0;
        List<CardController> allCards = new List<CardController>();

        allCards.AddRange(player1.helpers.getCards());
        allCards.AddRange(player1.archers.getCards());
        allCards.AddRange(player1.knights.getCards());
        allCards.AddRange(player2.knights.getCards());
        allCards.AddRange(player2.archers.getCards());
        allCards.AddRange(player2.helpers.getCards());

        foreach (CardController cardController in allCards)
        {
            if (cardController.card.god == false) {
                if (cardController.card.score > maxScore)
                {
                    maxScore = cardController.card.score;
                }
            }
        }

        MoveToDropping(player1.helpers.getCards(), maxScore);
        MoveToDropping(player1.archers.getCards(), maxScore);
        MoveToDropping(player1.knights.getCards(), maxScore);
        MoveToDropping(player2.knights.getCards(), maxScore);
        MoveToDropping(player2.archers.getCards(), maxScore);
        MoveToDropping(player2.helpers.getCards(), maxScore);

        player1.updateScore();
        player2.updateScore();
        player1.ui.updateDroppingCount();
        player2.ui.updateDroppingCount();
 
        changeTurn();

        instance.StartCoroutine(onPlaceDestroyRoutine(destroyCardController));
    }

    public void onPlaceDoctor(CardController cardController, Player player)
    {
        Transform dropping = player.ui.dropping.transform;

        if (dropping.childCount > 0)
        {
            Transform card = dropping.GetChild(dropping.childCount - 1);
            card.SetParent(player.ui.hand);

            player.ui.updateDroppingCount();
        }

        onPlaceStandart(cardController, player);
    }

    public void onPlaceSquad(CardController cardController, Player player)
    {
        MainScript.game.onPlaceStandart(cardController, player);

        int sibIndex = 0;

        for (int i = 0; i < cardController.transform.parent.childCount; i++)
        {
            CardController fieldCardController = cardController.transform.parent.GetChild(i).GetComponent<CardController>();

            if (fieldCardController && fieldCardController.card.id == cardController.card.id)
            {
                sibIndex = fieldCardController.transform.GetSiblingIndex();
                break;
            }
        }

        cardController.transform.SetSiblingIndex(sibIndex);
    }

    public static void MoveToDropping(List<CardController> line, int value)
    {
        foreach (CardController cardController in line)
        {
            if (cardController.card.score == value)
            {
                cardController.Movement.MoveToParent(cardController.player.ui.dropping.transform);
                if(cardController.card.subtype == CardSubType.SQUAD)
                {
                    cardController.card.score = cardController.card.basescore;
                    cardController.Info.UpdateBaffCard();
                }
            }
        }
    }

    IEnumerator onPlaceDestroyRoutine(CardController cardController)
    {
        yield return new WaitForSeconds(2);

        GameObject.Destroy(cardController.gameObject);
    }
}
