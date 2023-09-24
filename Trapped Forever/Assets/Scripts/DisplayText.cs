using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class DisplayText : MonoBehaviour
{
    
    private TextMeshProUGUI DialogueText;
    private bool IsDisplaying = false;
    private bool CanPress = true;

    private string[] DialogueArray;

    private AudioSource spade_voice1;
    private AudioSource spade_voice2;
    private AudioSource spade_voice3;
    private AudioSource spade_voice4;
    private AudioSource GuySound;
    private AudioSource DemonSound;

    private string Dialogue01 = " I can not believe we found one of your kind, we have not seen one of you in a long time. ";
    private string Dialogue02 = " Let me make you a DEAL kid. ";
    private string Dialogue03 = " Win five Deckeption games against my best card players and I will let you go. ";
    private string Dialogue04 = " *cards shuffles* ";
    private string Dialogue05 = "May the cards be ever in your favor... ";
    private void Start()
    {
        DialogueText = GameObject.FindGameObjectWithTag("Text").GetComponent<TextMeshProUGUI>();
        DialogueArray = new string[5];
        DialogueArray[0] = Dialogue01;
        DialogueArray[1] = Dialogue02;
        DialogueArray[2] = Dialogue03;
        DialogueArray[3] = Dialogue04;
        DialogueArray[4] = Dialogue05;
    }
    private void Update()
    {
        if(CanPress)
        {
            if(Input.GetMouseButtonDown(0) && IsDisplaying == false)
            {
                IsDisplaying = true;
                CanPress = false;
                StartCoroutine(DialogueDisplay(DialogueArray));
            }
            
        }
    }


    private IEnumerator DialogueDisplay(string[] Dialogue)
    {
        for(int i = 0; i < Dialogue.Length; i++)
        {
            StringSplitter(Dialogue[i]);
            string[] Characters = new string[Dialogue[i].Length];

            yield return new WaitForSeconds((Characters.Length + 45) * 0.035f);

        }
        NextScene();
    }
    
    private void StringSplitter(string Sentence)
    {
        DialogueText.text = "";
        string[] Characters = new string[Sentence.Length];

        for(int i = 0; i < Sentence.Length; i++)
        {
            Characters[i] = System.Convert.ToString(Sentence[i]);
        }
        StartCoroutine(StringDisplayDelay(Characters));
    }

    private IEnumerator StringDisplayDelay(string[] Characters)
    {
        for(int i = 0; i < Characters.Length - 1; i++)
        {
            DialogueText.text += Characters[i];
            if(Characters[Characters.Length - 1] == "A")
            {
                if(i % 6 == 0) DemonSound.Play();
                if(i % 3 == 0)
                {
                    int Randomint = Random.Range(0,2);
                    if(Randomint == 0) spade_voice1.Play();
                    else spade_voice2.Play();
                }
            }
            else if(Characters[Characters.Length - 1] == "O")
            {
                if(i % 6 == 0) GuySound.Play();
                if(i % 3 == 0)
                {
                    int Randomint = Random.Range(0,2);
                    if(Randomint == 0) spade_voice3.Play();
                    else spade_voice4.Play();
                }
            }

            yield return new WaitForSeconds(0.025f);

        }
        IsDisplaying = false;
    }
    private void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
