using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    [SerializeField]
    public Text turnField;
    [SerializeField]
    public GameObject cardPref;
    [SerializeField]
    private GameObject victoryContainer;
    [SerializeField]
    private GameObject ESCcontainer;
    [SerializeField]
    private CardController _previewCard;

    public static CardController previewCard;
    public static GameManager game = null;

    private void Awake()
    {
        CardManager.cardPref = cardPref;
        previewCard = _previewCard;
        CardManager.Init();
    }

    private void Start()
    {
        HidePreviewCard();
        StartGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (victoryContainer.activeSelf)
            {
                victoryContainer.SetActive(false);
            }
            ESCcontainer.SetActive(!ESCcontainer.activeSelf);
        }
    }

    public void StartGame()
    {
        game = new GameManager(this, GameObject.Find("Player1").GetComponent<Player>(), GameObject.Find("Player2").GetComponent<Player>());
        game.player2.ui.hand.gameObject.SetActive(true);

        List<Card> deck1 = new List<Card>();
        List<Card> deck2 = new List<Card>();

        for (int i = 0; i < AppManager.DECK_SIZE; i++)
        {
            deck1.Add(CardManager.cards[i]);
            deck2.Add(CardManager.cards[i]);
        }


        game.player1.setDeck(deck1);
        game.player2.setDeck(deck2);

        for (int i = 0; i < AppManager.CARD_IN_HAND; i++)
        {
            game.player1.takeCard();
            game.player2.takeCard();
        }
        game.player2.ui.hand.gameObject.SetActive(false);
    }

    public void ShowVictory()
    {
        victoryContainer.SetActive(true);

        Text textField = victoryContainer.transform.Find("Text").GetComponent<Text>();

        if (game.player1.score != game.player2.score)
        {
            bool playerOne = game.player1.score > game.player2.score;
            textField.text = playerOne ? "Вы победили!!!" : "Вы проиграли :(";
        } else
        {
            textField.text = "Ничья.";
        }
    }

    public void onChangeTurn()
    {
        turnField.text = MainScript.game.isPlayerTurn ? "Your Turn!" : "Enemy Turn.";
        game.player1.ui.hand.gameObject.SetActive(game.isPlayerTurn);
        game.player2.ui.hand.gameObject.SetActive(!game.isPlayerTurn);

        HidePreviewCard();
    }

    public static void ShowPreviewCard(Card card, Player player)
    {
        previewCard.Init(card, player);
        previewCard.Info.ActivateAll();
        previewCard.Info.ShowCardInfo();
        previewCard.gameObject.SetActive(true);
    }

    public static void HidePreviewCard()
    {
        previewCard.gameObject.SetActive(false);
    }
}
