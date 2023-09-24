using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
	public bool hasBeenPlayed;
	public int handIndex;
	private bool isEnemyCard;

	private GameManager gm;
	void Start()
    {
		gm = FindObjectOfType<GameManager>();
	}
    void Update()
    {
		
	}
	public void SetIsEnemyCard(bool isEnemy)
	{
		isEnemyCard = isEnemy;
	}
	private void OnMouseDown()
	{
		if(hasBeenPlayed == false)
		{
			transform.position+=Vector3.up*5;
			hasBeenPlayed = true;
			gm.AvailableCardSlots[handIndex] = true;
			if (isEnemyCard)
            {
                gm.currentPlayerHealth-=10; // Damage to player
            }
            else
            {
                gm.currentEnemyHealth-=15; // Damage to enemy
            }
			Invoke("MoveToDiscardPile", 0.2f);
		}
	}
	void MoveToDiscardPile()
	{
		if (isEnemyCard)
        {
            gm.Enemydiscard.Add(this);
        }
        else
        {
            gm.discardPile.Add(this);
        }
        gameObject.SetActive(false);
	}
	
}
