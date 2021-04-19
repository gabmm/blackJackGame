using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public const float cardYPos = 0.85f;
    public const float cardXPos = -0.38f;
    public const float cardXspace = 0.20f;
    public const float dealerCardZPos = 0.13f;
    public const float playerCardZPos = -0.16f;
    public const float cardScale = 3.0f;
    public const int numberCards = 54;

    public static Vector3 dealerInitialPos = new Vector3(cardXPos, cardYPos, dealerCardZPos);
    public static Vector3 playerInitialPos = new Vector3(cardXPos, cardYPos, playerCardZPos);
    public static Vector3 cardSpacement = new Vector3(cardXspace, 0, 0);
}
