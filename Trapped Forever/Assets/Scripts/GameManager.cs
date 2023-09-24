using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        PlayerTurn,
        EnemyTurn,
        GameOver
    }
    private GameState currentState;
    public List<Card> deck = new List<Card>();
    public List<Card> Enemydeck = new List<Card>();
    public List<Card> discardPile =new List<Card>();
    public List<Card> Enemydiscard =new List<Card>();
    public Transform[] cardSlots;
    public Transform[] EnemycardSlots;
    public bool[] AvailableCardSlots;
    public bool[] EnemyAvailableCardSlots;
    public int Turn = 0;
    public int playerMaxHealth = 100;
    public int enemyMaxHealth = 100;

    public int currentPlayerHealth;
    public int currentEnemyHealth;

    private bool playerTurnEnded = false;
    public bool end_turn;
    public TMP_Text deckSizeText;
    public TMP_Text discardSizeText;
    public TMP_Text turnCounter;
    public TMP_Text PlayerHealthText;
    public TMP_Text EnemyHealthText;
    public void DrawCard()
    {
        if(deck.Count>=1)
        {
            Card randCard = deck[Random.Range(0,deck.Count)];

            for (int i = 0; i < AvailableCardSlots.Length; i++)
            {
                if(AvailableCardSlots[i] == true)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;
                    randCard.transform.position = cardSlots[i].position;
                    randCard.hasBeenPlayed = false;
                    
                    AvailableCardSlots[i] = false;
                    deck.Remove(randCard);
                    return;
                }
            }
        }
    }
    public void Shuffle()
    {
        if(deck.Count == 0)
        {
            foreach (Card card in discardPile)
            {
                deck.Add(card);
            }
            discardPile.Clear();
        }
    }
    void SwitchTurns()
    {
        currentState = (currentState == GameState.PlayerTurn) ? GameState.EnemyTurn : GameState.PlayerTurn;
    }
    public void InflictPlayerDamage(int damage)
    {
        currentPlayerHealth -= damage;
        // Update UI to display current player health

        if (currentPlayerHealth <= 0)
        {
            // Player has lost the game
            HandleGameOver(false);
        }
    }

    public void InflictEnemyDamage(int damage)
    {
        currentEnemyHealth -= damage;
        // Update UI to display current enemy health

        if (currentEnemyHealth <= 0)
        {
            // Player has won the game
            HandleGameOver(true);
        }
    }
    void HandleGameOver(bool playerWon)
    {
        currentState = GameState.GameOver;

        if (playerWon)
        {
            // Display a victory message
            Debug.Log("Player wins!");
        }
        else
        {
            // Display a defeat message
            Debug.Log("Player loses!");
        }
    }
    public void PlayCard(Card card, bool isEnemyCard)
    {
        if (currentState == GameState.EnemyTurn && isEnemyCard)
        {
            // Implement card-specific logic for the enemy here
            // For example, you can check the card's type and apply its effect
            
            // For demonstration, let's say the enemy card deals 10 damage to the player
            InflictPlayerDamage(10);
        }
        else
        {
            // Invalid move, as it's not the player's turn when they try to play a card
            // Or it's not the enemy's turn when they try to play a card
            Debug.Log("Invalid move. It's not your turn.");
        }

        // After processing the card's effect, you can move it to the appropriate discard pile
        card.hasBeenPlayed = true;
        card.gameObject.SetActive(false);

        if (!isEnemyCard)
        {
            discardPile.Add(card);
        }
        else
        {
            Enemydiscard.Add(card);
        }
    }
    void HandleEnemyTurn()
    {
        // Simulate enemy AI
        // For example, make the enemy draw cards and play cards randomly

        // Randomly draw a card from the deck
        if (Enemydeck.Count > 0)
        {
            Card randCard = Enemydeck[Random.Range(0, Enemydeck.Count)];
            randCard.SetIsEnemyCard(true);
            PlayCard(randCard, true); // Simulate playing the card
        }

        // End the enemy's turn
        EndTurn();
    }
    public void EndTurn()
    {
        if (currentState == GameState.PlayerTurn)
        {
            currentState = GameState.EnemyTurn;
            playerTurnEnded = true; // Player's turn is over, set the flag
        }
        else if (currentState == GameState.EnemyTurn)
        {
            // Enemy's turn is over
            // You can add enemy AI logic here if needed
            HandleEnemyTurn();
            currentState = GameState.PlayerTurn;
            playerTurnEnded = false; // Reset the flag for the player's turn
            Turn += 1;
        }
    }
    void Start()
    {
        currentState = GameState.PlayerTurn;
        turnCounter.text = "Turn: "+Turn.ToString();
        currentPlayerHealth = playerMaxHealth;
        currentEnemyHealth = enemyMaxHealth;
    }
    void Update()
    {
        currentPlayerHealth = playerMaxHealth;
        currentEnemyHealth = enemyMaxHealth;
        turnCounter.text = "Turn: "+Turn.ToString();
        deckSizeText.text = deck.Count.ToString();
        discardSizeText.text = discardPile.Count.ToString();
        PlayerHealthText.text = currentPlayerHealth.ToString();
        EnemyHealthText.text = currentEnemyHealth.ToString();
        switch (currentState)
        {
            case GameState.PlayerTurn:
                
                break;
            case GameState.EnemyTurn:
                if (playerTurnEnded)
                {
                    HandleEnemyTurn();
                }
                else 
                {
                    SwitchTurns();
                }
                break;
            default:
            
                break;
        }
    }
}
