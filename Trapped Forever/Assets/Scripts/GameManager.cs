using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public Transform[] cardSlots;
    public bool[] availableSlots;

    public void DrawCard()
    {
        if(deck.Count >=1)
        {
            Card randCard = deck[Random.Range(0,deck.Count)];
            for (int i = 0; i < availableSlots.Length; i++)
            {
                if(availableSlots[i] == true)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.transform.position = cardSlots[i].position;
                    availableSlots[i] = false;
                    deck.Remove(randCard);
                    return;
                }
            }
        }
    }
    void Start()
    {}
    void Update()
    {}
}
