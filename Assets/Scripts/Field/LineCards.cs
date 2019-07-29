using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineCards: MonoBehaviour {
    public List<CardController> cards = new List<CardController>();
    public bool baff = false;
    public int score = 0;

    public Text scoreText;

    private void Start()
    {
        calcScore();
    }

    public void Add(CardController cardController)
    {
        cards.Add(cardController);
        calcScore();
    }

    public void calcScore()
    {
        int secondScore = 0;

        foreach (CardController cardController in cards)
        {
            secondScore += cardController.card.score;
        }

        score = secondScore;
        scoreText.text = score.ToString();
    }
}
