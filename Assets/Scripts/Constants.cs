using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public const string PlayerMoneyText = "You:" + "\n$";
    public const string DealerMoneyText = "House:" + "\n$";
    public const string PotMoneyText = "Pot:" + "\n$";

    public const float cardYPos = 0.85f;
    public const float cardXPos = -0.38f;
    public const float cardXspace = 0.20f;
    public const float dealerCardZPos = 0.13f;
    public const float playerCardZPos = -0.16f;
    public const float cardScale = 3.0f;
    public const int numberCards = 52;
    public const int maxScore = 21;
    public const int dealerMaxScore = 17;
    public const int playerIntialMoney = 2000;
    public const int betStep = 1;

    public const float dealerCoinYPos = 0.8382f;
    public const float dealerCoinZPos = 0.4523f;
    public const float dealerCoinBluePos = 0.0262f;

    public const float coinWidth = -0.0027f;
    public const float dealerCoinXStep = 2 * dealerCoinBluePos;

    public const int maxCoinNumber = 60;

    public const int whiteValue = 1;
    public const int redValue = 5;
    public const int blueValue = 10;
    public const int greenValue = 25;
    public const int blackValue = 100;

    public const int dealerInitialBlacks = 15;
    public const int dealerInitialGreens = 15;
    public const int dealerInitialBlues = 20;
    public const int dealerInitialReds = 15;
    public const int dealerInitialWhites = 40;

    public const int playerInitialBlacks = 5;
    public const int playerInitialGreens = 20;
    public const int playerInitialBlues = 58;
    public const int playerInitialReds = 60;
    public const int playerInitialWhites = 120;

    public const float discardXMax = 0.755f;
    public const float discardXMin = 0.502f;

    public const float discardZMax = 0.299f;
    public const float discardZMin = 0.059f;

    public const float discardYInital = 0.8439579f;
    public const float discardYStep = 0.0001f;

    public const float dicardXInitialRotation = 180f;

    public static Vector3 discardYStepPos = new Vector3(0f, discardYStep, 0f);

    public static Vector3 dealerInitialPos = new Vector3(cardXPos - cardXspace, cardYPos, dealerCardZPos);
    public static Vector3 playerInitialPos = new Vector3(cardXPos - cardXspace, cardYPos, playerCardZPos);
    public static Vector3 cardSpacement = new Vector3(cardXspace, 0, 0);

    public static Vector3 dealerCoinBluePlace = new Vector3(dealerCoinBluePos, dealerCoinYPos, dealerCoinZPos);
    public static Vector3 dealerCoinRedPlace = new Vector3(dealerCoinBluePos + dealerCoinXStep, dealerCoinYPos, dealerCoinZPos);
    public static Vector3 dealerCoinGreenPlace = new Vector3(dealerCoinBluePos - dealerCoinXStep, dealerCoinYPos, dealerCoinZPos);
    public static Vector3 dealerCoinBlackPlace = new Vector3(dealerCoinBluePos - 2 * dealerCoinXStep, dealerCoinYPos, dealerCoinZPos);
    public static Vector3 dealerCoinWhite1Place = new Vector3(0.245714f, 0.8453789f, 0.4532516f);
    public static Vector3 dealerCoinWhite2Place = new Vector3(0.2169338f, 0.8453789f, 0.4222575f);
    public static Vector3 dealerCoinWhite3Place = new Vector3(0.2523556f, 0.8453789f, 0.4111882f);

    public static Vector3 dealerCoinPlaceXStep = new Vector3(dealerCoinXStep, 0, 0);
    public static Vector3 coinZStep = new Vector3(0, 0, coinWidth);

    public static Vector3 playerCoinBlackPos = new Vector3(-0.0487184f, 0.8018222f, -0.3769449f);
    public static Vector3 playerCoinGreenPos = new Vector3(0.044264f, 0.8018222f, -0.359234f);
    public static Vector3 playerCoinRedPos = new Vector3(0.013f, 0.8018222f, -0.333f);
    public static Vector3 playerCoinBluePos = new Vector3(-0.027f, 0.8018222f, -0.339f);
    public static Vector3 playerCoinWhite1Pos = new Vector3(0.1173216f, 0.8453789f, -0.4123668f);
    public static Vector3 playerCoinWhite2Pos = new Vector3(0.143888f, 0.8453789f, -0.3791588f);
    public static Vector3 playerCoinWhite3Pos = new Vector3(0.1571712f, 0.8453789f, -0.4167945f);

    public static Vector3 coinYStep = new Vector3(0, coinWidth, 0);

    public static Vector3 deckInitialPos = new Vector3(-0.613f, 0.8440f, 0.252f);
    public static Vector3 deckFinalPos = new Vector3(-0.613f, 0.8268f, 0.252f);
    public static Vector3 deckYStep = new Vector3(0f, 0.000325f, 0f);

    public static Vector3 deckHeigth = new Vector3(0f, 0.0164675f, 0f);


    public static void performDealerCoinRotation(GameObject chip)
    {
        chip.transform.Rotate(Random.Range(0f, 360f), -90f, -90f);
    }

    public static void performPlayerCoinRotation(GameObject chip)
    {
        chip.transform.Rotate(0f, Random.Range(0f, 360f), 0f);
    }

    public static void performDiscardRotation(GameObject card)
    {
        card.transform.Rotate(dicardXInitialRotation, Random.Range(0f, 360f), 0f);
    }

    public static Vector3 getDiscardPos()
    {
        return new Vector3(Random.Range(discardXMin, discardXMax), discardYInital, Random.Range(discardZMin, discardZMax));
    }

}
