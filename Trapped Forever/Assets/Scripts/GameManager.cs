using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> discardPile =new List<Card>();
    public Transform[] cardSlots;
    public bool[] AvailableCardSlots;
    public TMP_Text deckSizeText;
    public TMP_Text discardSizeText;
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
        if(discardPile.Count >= 1)
        {
            foreach (Card card in discardPile)
            {
                deck.Add(card);
            }
            discardPile.Clear();
        }
    }
    void Start()
    {}
    void Update()
    {
        deckSizeText.text = deck.Count.ToString();
        discardSizeText.text = discardPile.Count.ToString();
    }
}
