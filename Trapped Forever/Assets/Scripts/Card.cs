using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
	public bool hasBeenPlayed;
	public int handIndex;

	private GameManager gm;
	void Start()
    {
		gm = FindObjectOfType<GameManager>();
	}
    void Update()
    {}
	private void OnMouseDown()
	{
		if(hasBeenPlayed == false)
		{
			transform.position+=Vector3.up*5;
			hasBeenPlayed = true;
			gm.AvailableCardSlots[handIndex] = true;
			Invoke("MoveToDiscardPile", 2f);
		}
	}
	void MoveToDiscardPile()
	{
		gm.discardPile.Add(this);
		gameObject.SetActive(false);
	}
	
}
