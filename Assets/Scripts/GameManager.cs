using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    public MonoBehaviour instance;

    public bool isPlayerTurn = true;
    public Player player1;
    public Player player2;

    public GameManager(MonoBehaviour _instance, Player _player1, Player _player2)
    {
        instance = _instance;
        player1 = _player1;
        player2 = _player2;
    }

    public void onPlayerPlaceCard(Card card)
    {
        if (isPlayerTurn)
        {
            player1.onPlaceCard(card);
            player1.updateScore();
            player1.updateCount();

            if (player2.handCardsCount != 0)
            {
                isPlayerTurn = false;
            } else
            {
                if(player1.handCardsCount == 0)
                {
                    MainScript.onEndGame();
                }
            }
        } else
        {
            player2.onPlaceCard(card);
            player2.updateScore();
            player2.updateCount();

            if (player1.handCardsCount != 0)
            {
                isPlayerTurn = true;
            } else
            {
                if (player1.handCardsCount == 0)
                {
                    MainScript.onEndGame();
                }
            }
        }
    }
}
