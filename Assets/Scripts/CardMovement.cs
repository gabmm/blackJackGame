using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMovement : MonoBehaviour
{
    private bool dealer = true;
    public float speed = 1.0f;
    private Vector3 target;
    private GameObject deck;
    private GameObject cardRot;

    public bool Dealer { get => dealer; set => dealer = value; }
    public Vector3 Target { get => target; set => target = value; }


    // Start is called before the first frame update
    void Start()
    {
        transform.position = GameObject.Find("Deck").transform.position + Constants.deckHeigth;
        cardRot = GameObject.Find("cardRot");
        deck = GameObject.Find("Deck");
        transform.rotation = deck.transform.rotation;
        if (tag == "backCard")
        {
            transform.Rotate(Vector3.forward, 180);
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target, speed * Time.deltaTime);

        if (transform.eulerAngles.y > 1.0f)
        {
            if (tag == "backCard")
            {
                transform.Rotate(Vector3.up, speed * Time.deltaTime * 100.0f);
            }
            else
            {
                transform.Rotate(Vector3.up, -speed * Time.deltaTime * 100.0f);
            }
        }


    }
}
