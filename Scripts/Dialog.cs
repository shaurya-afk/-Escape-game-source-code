using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public string[] sentences;
    private int index;
    public float typeSpeed;

    public GameObject continueButton, playButton;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Type());
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogText.text == sentences[index])
        {
            continueButton.SetActive(true);
        }
    }
    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
    public void NxtSentence()
    {
        continueButton.SetActive(false);
        if (index < sentences.Length - 1)
        {
            index++;
            dialogText.text = "";
            StartCoroutine(Type());
        }
        else
        {
            dialogText.text = "";
            continueButton.SetActive(false);
            playButton.SetActive(true);
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }
}
