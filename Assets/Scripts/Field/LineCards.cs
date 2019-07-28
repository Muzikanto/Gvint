using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineCards: MonoBehaviour {
    List<Card> cards = new List<Card>();
    public bool baff = false;
    public int score = 0;

    Text scoreText;

    private void Start()
    {
        scoreText = transform.Find("Score").GetComponent<Text>();
        calcScore();
    }

    public void Add(Card card)
    {
        cards.Add(card);
        calcScore();
    }

    public void calcScore()
    {
        int secondScore = 0;

        foreach (Card card in cards)
        {
            secondScore += card.score;
        }

        score = secondScore;
        scoreText.text = score.ToString();
    }
}
