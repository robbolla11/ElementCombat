using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] playerCards; 
    public GameObject[] enemyCards; 
    public GameObject[] counterpartCards; 
    public GameObject playerCardDisplay;
    public GameObject enemyCardDisplay; 

    public GameObject roundResult;
    public Sprite[] results; 


    public Sprite[] puntajesUser; 
    public Sprite[] puntajesRival; 

    public Sprite[] relojes;

    public GameObject reloj;

    public Sprite[] estrellasScore; 
    public GameObject estrella1;
    public GameObject estrella2;
    public GameObject estrella3;

    public GameObject scoreBoardUser;
    public GameObject scoreBoardRival;

    private int currentTurn = 1;
    private bool isPlayerTurn = true;
    private GameObject selectedPlayerCard;
    private GameObject selectedEnemyCard;

    private int playerScore = 0;
    private int enemyScore = 0;

    private int empate = 0;

    public static int currentRound;

    public static int ganadasPlayer;
    public static int ganadasEnemy;

    public static int empatesRondas;

    public static Color estrella1Color = Color.white;
    public static Color estrella2Color = Color.white;
    public static Color estrella3Color = Color.white;

    public TransicionEscena transicionEscena;

    AudioManager audioManager;


    public bool IsPlayerTurn
    {
        get { return isPlayerTurn; }
    }


    public void SelectPlayerCard(GameObject card)
    {
        if (isPlayerTurn && card.activeSelf)
        {
            selectedPlayerCard = card;
            Debug.Log("El jugador elige la carta: " + selectedPlayerCard.name);
            isPlayerTurn = false;

            selectedPlayerCard.SetActive(false);
        }
    }

    void Start()
    {   
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        currentRound++;
        Debug.Log("ronda: " + currentRound);
        StartCoroutine(PlayGame());
        isPlayerTurn = false;
        reloj.SetActive(false);
        playerCardDisplay.SetActive(false);
        enemyCardDisplay.SetActive(false);
        roundResult.SetActive(false);
        StartScore();
        
    }

    IEnumerator PlayGame()
    {
        Image resultComponent = roundResult.GetComponent<Image>();
        for (int i = 0; i < 9; i++) // 9 turns
        {
            Debug.Log("Turno " + currentTurn);
            yield return new WaitForSeconds(1f);

            Image relojComponent = reloj.GetComponent<Image>();
            reloj.SetActive(false);

            
            selectedPlayerCard = null;
            selectedEnemyCard = null;

            // Select random enemy card
            int enemyCardIndex = -1;
            do
            {
                enemyCardIndex = Random.Range(0, enemyCards.Length);
                selectedEnemyCard = enemyCards[enemyCardIndex];
            } while (!selectedEnemyCard.activeSelf);

            Debug.Log("El rival elige la carta: " + selectedEnemyCard.name);
            isPlayerTurn = true;
            // Wait for player to select a card, with a timeout
            float timeout = 12f;
            float timer = 0f;
            reloj.SetActive(true);
            relojComponent.sprite = relojes[0];
            
            float interval = 1f;
            float nextTick = interval;

            audioManager.playSFX(audioManager.tick, 0.1f);

            while (isPlayerTurn && timer < timeout)
            {
                int spriteIndex = Mathf.Clamp((int)timer + 1, 0, relojes.Length - 1);
                relojComponent.sprite = relojes[spriteIndex];

                if (timer >= nextTick)
                {
                    audioManager.playSFX(audioManager.tick, 0.1f);
                    nextTick += interval;
                }

                // Increment the timer
                timer += Time.deltaTime;

                // Play the ticking sound at fixed intervals
                

                // Wait until the next frame
                yield return null;
            }

            isPlayerTurn = false;

            // If the player didn't select a card within the timeout, choose one randomly
            if (selectedPlayerCard == null)
            {
                int playerCardIndex;
                do
                {
                    playerCardIndex = Random.Range(0, playerCards.Length);
                    selectedPlayerCard = playerCards[playerCardIndex];
                } while (!selectedPlayerCard.activeSelf);

                selectedPlayerCard.SetActive(false);
                Debug.Log("El jugador no seleccionó una carta a tiempo. Se elige aleatoriamente: " + selectedPlayerCard.name);
            }

            // Deactivate enemy card and its counterpart
            selectedEnemyCard.SetActive(false);
            GameObject counterpartCard = counterpartCards[enemyCardIndex];
            counterpartCard.SetActive(false);

            // Show selected cards in the middle
            relojComponent.sprite = relojes[13];
            playerCardDisplay.SetActive(true);
            playerCardDisplay.GetComponent<Image>().sprite = selectedPlayerCard.GetComponent<Image>().sprite;
            enemyCardDisplay.SetActive(true);
            enemyCardDisplay.GetComponent<Image>().sprite = selectedEnemyCard.GetComponent<Image>().sprite;
            yield return new WaitForSeconds(5f);

            // Get card values
            int playerCardValue = selectedPlayerCard.GetComponent<Carta>().valor;
            int enemyCardValue = selectedEnemyCard.GetComponent<Carta>().valor;

            // Determine the winner of the round
            DetermineRoundWinner(playerCardValue, enemyCardValue);

            Image userImageComponent = scoreBoardUser.GetComponent<Image>();

            if(playerScore >= 0)
            {
                userImageComponent.sprite = puntajesUser[playerScore];
            }

            Image rivalImageComponent = scoreBoardRival.GetComponent<Image>();

            if(enemyScore >= 0)
            {
                rivalImageComponent.sprite = puntajesRival[enemyScore];
            }

            // Hide cards after displaying them
            playerCardDisplay.SetActive(false);
            enemyCardDisplay.SetActive(false);
            
            reloj.SetActive(false);

            currentTurn++;
            
            if(playerScore==5)
            {
                ganadasPlayer++;
                audioManager.playSFX(audioManager.gong, 0.9f);
                Debug.Log("Juego terminado. Puntaje final - Jugador: " + playerScore + ", Rival: " + enemyScore);
                UpdateScoreBoard();
                roundResult.SetActive(true);
                resultComponent.sprite = results[0];
                yield return new WaitForSeconds(5f);
                roundResult.SetActive(false);
                DetermineOverallWinner();
                break;
                
            }

            if(enemyScore==5)
            {
                ganadasEnemy++;
                audioManager.playSFX(audioManager.gong, 0.9f);
                Debug.Log("Juego terminado. Puntaje final - Jugador: " + playerScore + ", Rival: " + enemyScore);
                UpdateScoreBoard();
                roundResult.SetActive(true);
                resultComponent.sprite = results[1];
                yield return new WaitForSeconds(5f);
                roundResult.SetActive(false);
                DetermineOverallWinner();
                break;
            }
        }

        
        if(playerScore > enemyScore && playerScore < 5)
        {
            ganadasPlayer++;
            audioManager.playSFX(audioManager.gong, 0.9f);
            UpdateScoreBoard();
            roundResult.SetActive(true);
            resultComponent.sprite = results[0];
            yield return new WaitForSeconds(5f);
            roundResult.SetActive(false);
            DetermineOverallWinner();
        }

        else if(playerScore < enemyScore && enemyScore < 5)
        {
            ganadasEnemy++;
            audioManager.playSFX(audioManager.gong, 0.9f);
            UpdateScoreBoard();
            roundResult.SetActive(true);
            resultComponent.sprite = results[1];
            yield return new WaitForSeconds(5f);
            roundResult.SetActive(false);
            DetermineOverallWinner();
        }

        else if(playerScore == enemyScore)
        {
            empatesRondas++;
            audioManager.playSFX(audioManager.gong, 0.9f);
            UpdateScoreBoard();
            roundResult.SetActive(true);
            resultComponent.sprite = results[2];
            yield return new WaitForSeconds(5f);
            roundResult.SetActive(false);
            DetermineOverallWinner();
        }
        
    }

    private void DetermineRoundWinner(int playerCardValue, int enemyCardValue)
    {
        // Player card is fire
        if (playerCardValue < 10)
        {
            if (enemyCardValue > 9 && enemyCardValue < 19) // Enemy card is water
            {
                enemyScore++;
                Debug.Log("El rival gana la ronda " + currentTurn);
            }
            else if (enemyCardValue > 18 && enemyCardValue < 28) // Enemy card is earth
            {
                playerScore++;
                Debug.Log("El jugador gana la ronda " + currentTurn);
            }
            else if (enemyCardValue < 10) // Enemy card is fire
            {
                CompareCardValues(playerCardValue, enemyCardValue);
            }
        }
        // Player card is water
        else if (playerCardValue > 9 && playerCardValue < 19)
        {
            if (enemyCardValue > 9 && enemyCardValue < 19) // Enemy card is water
            {
                CompareCardValues(playerCardValue, enemyCardValue);
            }
            else if (enemyCardValue > 18 && enemyCardValue < 28) // Enemy card is earth
            {
                enemyScore++;
                Debug.Log("El rival gana la ronda " + currentTurn);
            }
            else if (enemyCardValue < 10) // Enemy card is fire
            {
                playerScore++;
                Debug.Log("El jugador gana la ronda " + currentTurn);
            }
        }
        // Player card is earth
        else if (playerCardValue > 18 && playerCardValue < 28)
        {
            if (enemyCardValue > 9 && enemyCardValue < 19) // Enemy card is water
            {
                playerScore++;
                Debug.Log("El jugador gana la ronda " + currentTurn);
            }
            else if (enemyCardValue > 18 && enemyCardValue < 28) // Enemy card is earth
            {
                CompareCardValues(playerCardValue, enemyCardValue);
            }
            else if (enemyCardValue < 10) // Enemy card is fire
            {
                enemyScore++;
                Debug.Log("El rival gana la ronda " + currentTurn);
            }
        }
    }

    private void CompareCardValues(int playerCardValue, int enemyCardValue)
    {
        if (playerCardValue > enemyCardValue)
        {
            playerScore++;
            Debug.Log("El jugador gana la ronda " + currentTurn);
        }
        else if (playerCardValue < enemyCardValue)
        {
            enemyScore++;
            Debug.Log("El rival gana la ronda " + currentTurn);
        }
        else
        {
            empate++;
            Debug.Log("Empate en la ronda " + currentTurn);
        }
    }

    private void UpdateScoreBoard()
    {
        Image estrella1Component = estrella1.GetComponent<Image>();
        Image estrella2Component = estrella2.GetComponent<Image>();
        Image estrella3Component = estrella3.GetComponent<Image>();

        if(currentRound==1)
        {
            if(playerScore>enemyScore)
            {
                estrella1Color = Color.green;
            }
            else if(playerScore<enemyScore)
            {
                estrella1Color = Color.red;
            }
            else
            {
                estrella1Color = new Color(156 / 255f, 156 / 255f, 156 / 255f);
            }
        }

        else if(currentRound==2)
        {
            if(playerScore>enemyScore)
            {
                estrella2Color = Color.green;
            }
            else if(playerScore<enemyScore)
            {
                estrella2Color = Color.red;
            }
            else
            {
                estrella2Color = new Color(156 / 255f, 156 / 255f, 156 / 255f);
            }
        }

        else if(currentRound==3)
        {
            if(playerScore>enemyScore)
            {
                estrella3Color = Color.green;
            }
            else if(playerScore<enemyScore)
            {
                estrella3Color = Color.red;
            }
            else
            {
                estrella3Color = new Color(156 / 255f, 156 / 255f, 156 / 255f);
            }
        }


        estrella1Component.color = estrella1Color;
        estrella2Component.color = estrella2Color;
        estrella3Component.color = estrella3Color;
    }


    private void StartScore()
    {
        Image estrella1Component = estrella1.GetComponent<Image>();
        Image estrella2Component = estrella2.GetComponent<Image>();
        Image estrella3Component = estrella3.GetComponent<Image>();

        estrella1Component.color = estrella1Color;
        estrella2Component.color = estrella2Color;
        estrella3Component.color = estrella3Color;
    }
    private void DetermineOverallWinner()
    {
        if (currentRound == 1)
        {
            // Siempre después de la primera ronda, ve a la escena 2
            Debug.Log("Ronda 1 completada");
            transicionEscena.EscenaRonda2();
        }
        else if (currentRound == 2)
        {
            // En la segunda ronda, verificamos si alguien ha ganado 2 veces
            if (ganadasPlayer == 2)
            {
                Debug.Log("Gano jugador");
                reiniciar();
                transicionEscena.Victoria();
            }
            else if (ganadasEnemy == 2)
            {
                Debug.Log("Gano enemigo");
                reiniciar();
                transicionEscena.Derrota();
            }
            else
            {
                Debug.Log("Ronda 2 completada");
                transicionEscena.EscenaRonda3();
            }
        }
        else if (currentRound == 3)
        {
            // En la tercera ronda, determinamos el resultado final del juego
            if (ganadasPlayer == 2)
            {
                Debug.Log("Gano jugador");
                reiniciar();
                transicionEscena.Victoria();
            }
            else if (ganadasEnemy == 2)
            {
                Debug.Log("Gano enemigo");
                reiniciar();
                transicionEscena.Derrota();
            }
            else if (ganadasPlayer == 1 && empatesRondas == 2)
            {
                Debug.Log("gana user");
                reiniciar();
                transicionEscena.Victoria();
            }
            else if (ganadasEnemy == 1 && empatesRondas == 2)
            {
                Debug.Log("gana enemy");
                reiniciar();
                transicionEscena.Derrota();
            }
            else if (ganadasPlayer == 1 && ganadasEnemy == 1 && empatesRondas == 1)
            {
                Debug.Log("Empate con una victoria cada uno");
                reiniciar();
                transicionEscena.Empate();
            }
            else
            {
                Debug.Log("Juego empatado");
                reiniciar();
                transicionEscena.Empate();
            }
        }
    }

    void reiniciar()
    {
        currentRound = 0;

        ganadasPlayer = 0;
        ganadasEnemy = 0;

        empatesRondas = 0;
        
        estrella1Color = Color.white;
        estrella2Color = Color.white;
        estrella3Color = Color.white;
    }

}
