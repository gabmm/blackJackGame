using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDealer : MonoBehaviour
{
    public GameObject[] deck = new GameObject[Constants.numberCards];
    private GameObject dealerCards;
    private GameObject playerCards;
    private Vector3 dealerCardPos = Constants.dealerInitialPos;
    private Vector3 playerCardPos = Constants.playerInitialPos;
    private List<int> playedCards = new List<int>();
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            do
            {
                index = Random.Range(0, Constants.numberCards);
            } while (playedCards.Contains(index));

            playedCards.Add(index);
            dealerCards = Instantiate(deck[index], dealerCardPos, deck[0].transform.rotation);
            dealerCards.transform.localScale *= Constants.cardScale;
            dealerCardPos += Constants.cardSpacement;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            do
            {
                index = Random.Range(0, Constants.numberCards);
            } while (playedCards.Contains(index));

            playedCards.Add(index);
            playerCards = Instantiate(deck[index], playerCardPos, deck[0].transform.rotation);
            playerCards.transform.localScale *= Constants.cardScale;
            playerCardPos += Constants.cardSpacement;
        }

    }
}
