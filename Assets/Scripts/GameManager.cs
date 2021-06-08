using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI dealerScoreText;
    public TextMeshProUGUI playerScoreText;

    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;
    public TextMeshProUGUI tieText;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI dealerMoneyText;

    public TextMeshProUGUI betText;

    public TextMeshProUGUI playValue;

    public GameObject playButtons;
    public GameObject betButtons;

    public Button doubleDown;

    public Button restartButton;


    private CardDealer deckManager;
    public GameObject deck;

    private bool win;

    private int betValue;
    private int playerMoney;

    public GameObject whiteCoin;
    public GameObject redCoin;
    public GameObject blueCoin;
    public GameObject greenCoin;
    public GameObject blackCoin;

    private Stack<GameObject> dealerBlackStack;
    private Stack<GameObject> dealerBlueStack;
    private Stack<GameObject> dealerRedStack;
    private Stack<GameObject> dealerGreenStack;
    private Stack<GameObject> dealerWhiteStack;

    private Stack<GameObject> playerBlackStack;
    private Stack<GameObject> playerBlueStack;
    private Stack<GameObject> playerRedStack;
    private Stack<GameObject> playerGreenStack;
    private Stack<GameObject> playerWhiteStack;

    // Start is called before the first frame update
    void Start()
    {
        deckManager = deck.gameObject.GetComponent<CardDealer>();

        dealerBlackStack = new Stack<GameObject>();
        dealerGreenStack = new Stack<GameObject>();
        dealerBlueStack = new Stack<GameObject>();
        dealerRedStack = new Stack<GameObject>();
        dealerWhiteStack = new Stack<GameObject>();

        playerBlackStack = new Stack<GameObject>();
        playerGreenStack = new Stack<GameObject>();
        playerBlueStack = new Stack<GameObject>();
        playerRedStack = new Stack<GameObject>();
        playerWhiteStack = new Stack<GameObject>();

        doubleDown.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);
        tieText.gameObject.SetActive(false);
        playButtons.gameObject.SetActive(false);
        betButtons.gameObject.SetActive(true);

        betValue = 0;
        playValue.text = Constants.PotMoneyText + betValue;

        dealerScoreText.text = "Dealer: " + 0;
        playerScoreText.text = "Player: " + 0;
        betText.text = "" + betValue;
        playerMoney = Constants.playerIntialMoney;
        moneyText.text = Constants.PlayerMoneyText + playerMoney;

        win = false;

        manageDealerCoins(Constants.dealerInitialBlacks, Constants.dealerInitialGreens, Constants.dealerInitialBlues,
            Constants.dealerInitialReds, Constants.dealerInitialWhites, false);

        managePlayerCoins(Constants.playerInitialBlacks, Constants.playerInitialGreens, Constants.playerInitialBlues,
            Constants.playerInitialReds, Constants.playerInitialWhites, true);

        UpdateDealerMoney();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateDealerScore(int dealerScore)
    {
        dealerScoreText.text = "Dealer: " + dealerScore;
        if (dealerScore > 21)
        {
            dealerScoreText.color = Color.red;
        }
        else if(dealerScore == 20 || dealerScore == 21)
        {
            dealerScoreText.color = Color.green;
        }
    }

    public void UpdatePlayerScore(int playerScore)
    {
        playerScoreText.text = "Player: " + playerScore;
        if (playerScore > 21)
        {
            playerScoreText.color = Color.red;
        }
        else if (playerScore == 20 || playerScore == 21)
        {
            playerScoreText.color = Color.green;
        }
    }

    public void Restart()
    {

        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);
        tieText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        playButtons.gameObject.SetActive(false);
        betButtons.gameObject.SetActive(true);

        dealerScoreText.text = "Dealer: " + 0;
        dealerScoreText.color = Color.white;

        playerScoreText.text = "Player: " + 0;
        playerScoreText.color = Color.white;

        betValue = 0;
        betText.text = "" + betValue;
        playValue.text = Constants.PotMoneyText + betValue;

        deckManager.DiscardCards();
        CardDealer.StartRoutine();
        

        var clones = GameObject.FindGameObjectsWithTag("clone");

        foreach (var clone in clones)
        {
            Destroy(clone);
        }

        Destroy(GameObject.FindGameObjectWithTag("backCard"));
    }

    public void displayWin()
    {
        StartCoroutine(WaitForWin());
    }

    private void calculateChips(bool win)
    {
        int betValueAux = betValue;

        int blackCoins = betValueAux / Constants.blackValue;
        betValueAux %= Constants.blackValue;

        int greenCoins = betValueAux / Constants.greenValue;
        betValueAux %= Constants.greenValue;

        int blueCoins = betValueAux / Constants.blueValue;
        betValueAux %= Constants.blueValue;

        int redCoins = betValueAux / Constants.redValue;
        betValueAux %= Constants.redValue;

        int whiteCoins = betValueAux / Constants.whiteValue;

        manageDealerCoins(blackCoins, greenCoins, blueCoins, redCoins, whiteCoins, win);
        managePlayerCoins(blackCoins, greenCoins, blueCoins, redCoins, whiteCoins, win);

    }

    private void managePlayerCoins(int blacks, int greens, int blues, int reds, int whites, bool win)
    {
        GameObject chip;

        for (int i = 0; i < blacks; i++)
        {
            if (win)
            {
                chip = Instantiate(blackCoin, Constants.playerCoinBlackPos + playerBlackStack.Count * -Constants.coinYStep,
                    blackCoin.transform.rotation);

                Constants.performPlayerCoinRotation(chip);

                playerBlackStack.Push(chip);
            }
            else
            {
                try
                {
                    Destroy(playerBlackStack.Pop());
                } 
                catch(InvalidOperationException)
                {
                    greens += (Constants.blackValue / Constants.greenValue);
                }
            }
        }

        for (int i = 0; i < greens; i++)
        {
            if (win)
            {
                chip = Instantiate(greenCoin, Constants.playerCoinGreenPos + playerGreenStack.Count * -Constants.coinYStep,
                    greenCoin.transform.rotation);

                Constants.performPlayerCoinRotation(chip);

                playerGreenStack.Push(chip);
            }
            else
            {
                try
                {
                    Destroy(playerGreenStack.Pop());
                }
                catch (InvalidOperationException)
                {
                    blues += (Constants.greenValue / Constants.blueValue);
                    reds++;
                }
            }
        }

        for (int i = 0; i < blues; i++)
        {
            if (win)
            {
                chip = Instantiate(blueCoin, Constants.playerCoinBluePos + playerBlueStack.Count * -Constants.coinYStep,
                    blueCoin.transform.rotation);

                Constants.performPlayerCoinRotation(chip);

                playerBlueStack.Push(chip);
            }
            else
            {
                try
                {
                    Destroy(playerBlueStack.Pop());
                }
                catch (InvalidOperationException)
                {
                    reds += (Constants.blueValue / Constants.redValue);
                }
            }
        }

        for (int i = 0; i < reds; i++)
        {
            if (win)
            {
                chip = Instantiate(redCoin, Constants.playerCoinRedPos + playerRedStack.Count * -Constants.coinYStep,
                    redCoin.transform.rotation);

                Constants.performPlayerCoinRotation(chip);

                playerRedStack.Push(chip);
            }
            else
            {
                try
                {
                    Destroy(playerRedStack.Pop());
                }
                catch (InvalidOperationException)
                {
                    whites += (Constants.redValue / Constants.whiteValue);
                }
            }
        }

        for (int i = 0; i < whites; i++)
        {
            if (win)
            {
                if (playerWhiteStack.Count < 40)
                {
                    chip = Instantiate(whiteCoin, Constants.playerCoinWhite1Pos + playerWhiteStack.Count * -Constants.coinYStep,
                        whiteCoin.transform.rotation);
                }
                else if (playerWhiteStack.Count >= 40 && playerWhiteStack.Count < 80)
                {
                    chip = Instantiate(whiteCoin, Constants.playerCoinWhite2Pos + (playerWhiteStack.Count - 40) * -Constants.coinYStep,
                        whiteCoin.transform.rotation);
                }
                else
                {
                    chip = Instantiate(whiteCoin, Constants.playerCoinWhite3Pos + (playerWhiteStack.Count - 80) * -Constants.coinYStep,
                        whiteCoin.transform.rotation);
                }

                Constants.performPlayerCoinRotation(chip);

                playerWhiteStack.Push(chip);
            }
            else
            {
                try
                {
                    Destroy(playerWhiteStack.Pop());
                }
                catch (InvalidOperationException)
                {
                    //Condicao de Game Over
                }
            }
        }

    }

    private void manageDealerCoins(int blacks, int greens, int blues, int reds, int whites, bool win)
    {

        GameObject chip;

        for (int i = 0; i < blacks; i++)
        {
            if (!win)
            {
                chip = Instantiate(blackCoin, Constants.dealerCoinBlackPlace + dealerBlackStack.Count * Constants.coinZStep,
                    blackCoin.transform.rotation);

                Constants.performDealerCoinRotation(chip);

                dealerBlackStack.Push(chip);
            }
            else
            {
                try
                {
                    Destroy(dealerBlackStack.Pop());
                }
                catch (InvalidOperationException)
                {
                    greens += (Constants.blackValue / Constants.greenValue);
                }
            }
        }

        for (int i = 0; i < greens; i++)
        {
            if (!win)
            {
                chip = Instantiate(greenCoin, Constants.dealerCoinGreenPlace + dealerGreenStack.Count * Constants.coinZStep,
                    greenCoin.transform.rotation);

                Constants.performDealerCoinRotation(chip);

                dealerGreenStack.Push(chip);
            }
            else
            {
                try
                {
                    Destroy(dealerGreenStack.Pop());
                }
                catch (InvalidOperationException)
                {
                    blues += (Constants.greenValue / Constants.blueValue);
                    reds++;
                }
            }
        }

        for (int i = 0; i < blues; i++)
        {
            if (!win)
            {
                chip = Instantiate(blueCoin, Constants.dealerCoinBluePlace + dealerBlueStack.Count * Constants.coinZStep,
                    blueCoin.transform.rotation);

                Constants.performDealerCoinRotation(chip);

                dealerBlueStack.Push(chip);
            }
            else
            {
                try
                {
                    Destroy(dealerBlueStack.Pop());
                }
                catch (InvalidOperationException)
                {
                    reds += (Constants.blueValue / Constants.redValue);
                }
            }
        }

        for (int i = 0; i < reds; i++)
        {
            if (!win)
            {
                chip = Instantiate(redCoin, Constants.dealerCoinRedPlace + dealerRedStack.Count * Constants.coinZStep,
                    redCoin.transform.rotation);

                Constants.performDealerCoinRotation(chip);

                dealerRedStack.Push(chip);
            }
            else
            {
                try
                {
                    Destroy(dealerRedStack.Pop());
                }
                catch (InvalidOperationException)
                {
                    whites += (Constants.redValue / Constants.whiteValue);
                }
            }
        }

        for (int i = 0; i < whites; i++)
        {
            if (!win)
            {
                if (dealerWhiteStack.Count < 40)
                {
                    chip = Instantiate(whiteCoin, Constants.dealerCoinWhite1Place + dealerWhiteStack.Count * -Constants.coinYStep,
                        whiteCoin.transform.rotation);
                }
                else if(dealerWhiteStack.Count >= 40 && dealerWhiteStack.Count < 80)
                {
                    chip = Instantiate(whiteCoin, Constants.dealerCoinWhite2Place + (dealerWhiteStack.Count - 40) * -Constants.coinYStep,
                        whiteCoin.transform.rotation);
                }
                else
                {
                    chip = Instantiate(whiteCoin, Constants.dealerCoinWhite3Place + (dealerWhiteStack.Count - 80) * -Constants.coinYStep,
                        whiteCoin.transform.rotation);
                }

                Constants.performPlayerCoinRotation(chip);

                dealerWhiteStack.Push(chip);
            }
            else
            {
                try
                {
                    Destroy(dealerWhiteStack.Pop());
                }
                catch (InvalidOperationException)
                {
                    //condicao de vitoria
                }
            }
        }

    }

    public void UpdateDealerMoney()
    {
        int dealerChipValue = dealerBlackStack.Count * Constants.blackValue + dealerBlueStack.Count * Constants.blueValue +
            dealerGreenStack.Count * Constants.greenValue + dealerRedStack.Count * Constants.redValue +
            dealerWhiteStack.Count * Constants.whiteValue;

        dealerMoneyText.text = Constants.DealerMoneyText + dealerChipValue;
    }

    public void displayLose()
    {
        StartCoroutine(WaitForLose());
    }

    public void displayTie()
    {
        StartCoroutine(WaitForTie());
    }

    public void PlaceBet()
    {
        betButtons.gameObject.SetActive(false);
        playerMoney -= betValue;
        moneyText.text = Constants.PlayerMoneyText + playerMoney;
        playValue.text = Constants.PotMoneyText + (2 * betValue);
        deckManager.BeginGame();
    }

    public void Bet1More()
    {

        if ((betValue + Constants.betStep) <= playerMoney)
        {
            betValue += Constants.betStep;
            betText.text = "" + betValue;
        }
    }

    public void Bet5More()
    {

        if ((betValue + 5 * Constants.betStep) <= playerMoney)
        {
            betValue += 5 * Constants.betStep;
            betText.text = "" + betValue;
        }
    }

    public void Bet50More()
    {

        if ((betValue + 50 * Constants.betStep) <= playerMoney)
        {
            betValue += 50 * Constants.betStep;
            betText.text = "" + betValue;
        }
    }

    public void Bet1Less()
    {
        if ((betValue - Constants.betStep) >= 0)
        {
            betValue -= Constants.betStep;
            betText.text = "" + betValue;
        }
    }

    public void Bet5Less()
    {
        if ((betValue - 5 * Constants.betStep) >= 0)
        {
            betValue -= 5 * Constants.betStep;
            betText.text = "" + betValue;
        }
    }

    public void Bet50Less()
    {
        if ((betValue - 50 * Constants.betStep) >= 0)
        {
            betValue -= 50 * Constants.betStep;
            betText.text = "" + betValue;
        }
    }

    public void DoublingDown()
    {
        playerMoney -= betValue;
        betValue *= 2;
        moneyText.text = Constants.PlayerMoneyText + playerMoney;
        playValue.text = Constants.PotMoneyText + (2 * betValue);
        DeactivateDDButton();
    }

    public void ActivatePlayButtons()
    {
        playButtons.gameObject.SetActive(true);
    }

    public void DeactivatePlayButtons()
    {
        playButtons.gameObject.SetActive(false);
    }

    public void ActivateDDButton()
    {
        if (playerMoney >= betValue)
        {
            doubleDown.gameObject.SetActive(true);
        }
    }

    public void DeactivateDDButton()
    {
        doubleDown.gameObject.SetActive(false);
    }

    IEnumerator WaitForLose()
    {
        yield return new WaitForSeconds(1);

        loseText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        playButtons.gameObject.SetActive(false);

        win = false;

        calculateChips(win);

        moneyText.text = Constants.PlayerMoneyText + playerMoney;

        UpdateDealerMoney();
    }

    IEnumerator WaitForWin()
    {
        yield return new WaitForSeconds(1);

        winText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        playButtons.gameObject.SetActive(false);

        win = true;

        calculateChips(win);

        playerMoney += 2 * betValue;
        moneyText.text = Constants.PlayerMoneyText + playerMoney;

        UpdateDealerMoney();
    }

    IEnumerator WaitForTie()
    {
        yield return new WaitForSeconds(1);

        tieText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        playButtons.gameObject.SetActive(false);

        playerMoney += betValue;
        moneyText.text = Constants.PlayerMoneyText + playerMoney;
    }

}
