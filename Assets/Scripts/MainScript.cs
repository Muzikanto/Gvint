using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public GameObject cardPref;
    public static GameObject VictoryContainer;
    [SerializeField]
    private GameObject victoryContainer;
    [SerializeField]
    private GameObject ESCcontainer;

    public static GameManager game = null;

    private void Awake()
    {
        CardManager.cardPref = cardPref;
        CardManager.Init();
        VictoryContainer = victoryContainer;
    }

    private void Start()
    {
        StartGame(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            ESCcontainer.SetActive(!ESCcontainer.activeSelf);
        }
    }

    public static void StartGame(MonoBehaviour instance)
    {
        game = new GameManager(instance, GameObject.Find("Player1").GetComponent<Player>(), GameObject.Find("Player2").GetComponent<Player>());

        List<Card> deck1 = new List<Card>();
        List<Card> deck2 = new List<Card>();

        for (int i = 0; i < AppManager.DECK_SIZE; i++)
        {
            deck1.Add(CardManager.cards[Random.Range(0, CardManager.cards.Count)]);
            deck2.Add(CardManager.cards[Random.Range(0, CardManager.cards.Count)]);
        }


        game.player1.setDeck(deck1);
        game.player2.setDeck(deck2);

        for (int i = 0; i < AppManager.CARD_IN_HAND; i++)
        {
            game.player1.takeCard();
            game.player2.takeCard();
        }
    }

    public static void onEndGame()
    {
        VictoryContainer.SetActive(true);

        Text textField = VictoryContainer.transform.Find("Text").GetComponent<Text>();

        if (game.player1.score != game.player2.score)
        {
            bool playerOne = game.player1.score > game.player2.score;
            textField.text = playerOne ? "Вы победили!!!" : "Вы проиграли :(";
        } else
        {
            textField.text = "Ничья.";
        }
    }
}
