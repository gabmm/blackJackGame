using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardDealer : MonoBehaviour
{
    public GameObject[] deck = new GameObject[Constants.numberCards];
    private GameObject dealerCards;
    private GameObject playerCards;

    public GameObject discardCard;
    public GameObject playButtons;

    public TextMeshProUGUI tableCards;
    public TextMeshProUGUI discartedStackCards;
    public TextMeshProUGUI deckCards;


    private static int discardCardNumber;

    private Stack<GameObject> discard;

    private static Vector3 dealerCardPos = Constants.dealerInitialPos;
    private static Vector3 playerCardPos = Constants.playerInitialPos;
    private List<int> playedCards;
    private int index;
    public static int dealerScore = 0;
    private static int playerScore = 0;
    private static bool isDealer = false;
    private static bool isStand = false;
    private static bool isBusted = false;
    private static bool hasAce = false;
    private static bool dealerHasAce = false;

    private GameManager gameManager;

    public static Vector3 DealerCardPos { get => dealerCardPos; set => dealerCardPos = value; }
    public static bool IsDealer { get => isDealer; set => isDealer = value; }
    public static Vector3 PlayerCardPos { get => playerCardPos; set => playerCardPos = value; }
    public static int PlayerScore { get => playerScore; set => playerScore = value; }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Constants.deckInitialPos;

        playedCards = new List<int>();
        discard = new Stack<GameObject>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        StartRoutine();
    }

    public static void StartRoutine()
    {
        dealerCardPos = Constants.dealerInitialPos;
        playerCardPos = Constants.playerInitialPos;

        dealerScore = 0;
        playerScore = 0;

        discardCardNumber = 0;

        isDealer = false;
        isStand = false;
        isBusted = false;
        hasAce = false;
        dealerHasAce = false;
    }


    // Update is called once per frame
    void Update()
    {
        //discartedStackCards.text = "Descartadas: " + discard.Count;
        //deckCards.text = "Cartas no Baralho: " + (Constants.numberCards - playedCards.Count);
        //tableCards.text = "Cartas em Jogo: " + discardCardNumber;
    }

    public void ReShuffle()
    {
        if (playedCards.Count >= Constants.numberCards)
        {
            transform.position = Constants.deckInitialPos;

            playedCards.Clear();

            while(discard.Count > 0)
            {
                Destroy(discard.Pop());
            }

        }
    }

    public void DeckLessCard()
    {
        transform.position -= Constants.deckYStep;
        discardCardNumber++;
    }

    public void PlayerDraw()
    {
        int index = GetIndex();

        GameObject card = Instantiate(deck[index], Constants.deckInitialPos, deck[0].transform.rotation);
        card.tag = "clone";
        card.transform.localScale *= Constants.cardScale;
        DeckLessCard();
        card.GetComponent<CardMovement>().Dealer = false;
        card.GetComponent<CardMovement>().Target = PlayerCardPos;
        PlayerCardPos += Constants.cardSpacement;

        if (card.name.Contains("01"))
        {
            hasAce = true;
        }

        playerScore += addScore(card);

        if (playerScore > Constants.maxScore && hasAce)
        {
            playerScore -= 10;
            hasAce = false;
        }

        gameManager.UpdatePlayerScore(playerScore);

    }

    public void Hit()
    {
        gameManager.DeactivateDDButton();
        gameManager.DeactivatePlayButtons();
        ReShuffle();

        if (playerScore < Constants.maxScore && !isStand)
        {
            PlayerDraw();
        }

        if (playerScore <= Constants.maxScore)
        {
            StartCoroutine(activateButtons());
        }

        if (playerScore > 21)
        {
            isBusted = true;
            gameManager.displayLose();
        }
    }


    public void DiscardCards()
    {

        GameObject card;

        for (int i = 0; i < discardCardNumber; i++)
        {

            card = Instantiate(discardCard, Constants.getDiscardPos() + Constants.discardYStepPos * discard.Count, discardCard.transform.rotation);
            card.transform.localScale *= Constants.cardScale;


            Constants.performDiscardRotation(card);

            discard.Push(card);
        }
    }

    public int GetIndex()
    {
        int index;

        do
        {
            index = Random.Range(0, Constants.numberCards);
        } while (playedCards.Contains(index));

        playedCards.Add(index);

        return index;
    }



    public void BeginGame()
    {
        DealerCardPos += Constants.cardSpacement;
        PlayerCardPos += Constants.cardSpacement;
        StartCoroutine(GiveCards());
    }

    IEnumerator activateButtons()
    {
        yield return new WaitForSeconds(1);
        gameManager.ActivatePlayButtons();
    }

    IEnumerator GiveCards()
    {
        int aux = -1;

        for (int i = 0; i < 4; i++)
        {
            ReShuffle();

            int index = GetIndex();

            GameObject card = Instantiate(deck[index], Constants.deckInitialPos, deck[0].transform.rotation);

            if (i == 3)
            {
                card.tag = "backCard";
            }
            else
            {
                card.tag = "clone";
            }

            card.transform.localScale *= Constants.cardScale;
            DeckLessCard();

            if (aux > 0)
            {
                card.GetComponent<CardMovement>().Dealer = true;
                card.GetComponent<CardMovement>().Target = DealerCardPos;
                DealerCardPos += Constants.cardSpacement;

                if (card.name.Contains("01"))
                {
                    dealerHasAce = true;
                }

                dealerScore += addScore(card);
                gameManager.UpdateDealerScore(dealerScore);
            }
            else
            {
                card.GetComponent<CardMovement>().Dealer = false;
                card.GetComponent<CardMovement>().Target = PlayerCardPos;
                PlayerCardPos += Constants.cardSpacement;

                if (card.name.Contains("01"))
                {
                    hasAce = true;
                }

                playerScore += addScore(card);
                gameManager.UpdatePlayerScore(playerScore);
            }

            yield return new WaitForSeconds(1);

            aux *= -1;
        }

        if (playerScore > 8 && playerScore < 12)
        {
            gameManager.ActivateDDButton();
        }

        gameManager.ActivatePlayButtons();
    }

    public void Stand()
    {
        gameManager.DeactivateDDButton();
        gameManager.DeactivatePlayButtons();
        isStand = true;
        if (!isBusted)
        {
            GameObject card = GameObject.FindGameObjectWithTag("backCard");
            card.transform.Rotate(Vector3.forward, 180);
            StartCoroutine(DrawCards());
        }
    }

    public void DealerDraw()
    {
        int index = GetIndex();

        GameObject card = Instantiate(deck[index], Constants.deckInitialPos, deck[0].transform.rotation);
        card.tag = "clone";

        card.transform.localScale *= Constants.cardScale;
        DeckLessCard();

        card.GetComponent<CardMovement>().Dealer = true;
        card.GetComponent<CardMovement>().Target = DealerCardPos;
        DealerCardPos += Constants.cardSpacement;

        if (card.name.Contains("01"))
        {
            dealerHasAce = true;
        }

        dealerScore += addScore(card);
        gameManager.UpdateDealerScore(dealerScore);
    

        if (dealerScore > Constants.maxScore && dealerHasAce)
        {
            dealerScore -= 10;
            dealerHasAce = false;
        }

        gameManager.UpdateDealerScore(dealerScore);
    }

    IEnumerator DrawCards()
    {
        while (dealerScore < Constants.dealerMaxScore)
        {
            ReShuffle();
            yield return new WaitForSeconds(1);
            DealerDraw();
        }

        if (playerScore > dealerScore || dealerScore > Constants.maxScore)
        {
            gameManager.displayWin();
        }
        else if (playerScore == dealerScore)
        {
            gameManager.displayTie();
        }
        else
        {
            gameManager.displayLose();
        }
    }

    private int addScore(GameObject drawnCard)
    {
        int score = 0;

        if (drawnCard.name.Contains("01"))
        {
            score += 11;
        }
        else if (drawnCard.name.Contains("02"))
        {
            score += 2;
        }
        else if (drawnCard.name.Contains("03"))
        {
            score += 3;
        }
        else if (drawnCard.name.Contains("04"))
        {
            score += 4;
        }
        else if (drawnCard.name.Contains("05"))
        {
            score += 5;
        }
        else if (drawnCard.name.Contains("06"))
        {
            score += 6;
        }
        else if (drawnCard.name.Contains("07"))
        {
            score += 7;
        }
        else if (drawnCard.name.Contains("08"))
        {
            score += 8;
        }
        else if (drawnCard.name.Contains("09"))
        {
            score += 9;
        }
        else
        {
            score += 10;
        }

        return score;
    }

}
