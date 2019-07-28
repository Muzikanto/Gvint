using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIIController : MonoBehaviour {
    public Text score;
    public Text count;
    public Text deck;
    public Transform hand;
    public Player player;

    public PlayerUIIController(Player _player)
    {
        player = _player;
    }

    public void updateCount()
    {
        count.text = player.handCardsCount.ToString();
    }


    public void updateDeckCount()
    {
        deck.text = player.deck.Count.ToString();
    }

    public void updateScore()
    {
        score.text = player.score.ToString();
    }
}
