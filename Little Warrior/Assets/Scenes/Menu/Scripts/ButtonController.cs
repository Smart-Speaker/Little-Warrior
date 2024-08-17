using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TMP_Text text;
    public Button button;

    public GameObject audioOrigin;
    private AudioSource[] Audio;

    // Start is called before the first frame update
    void Start()
    {
        Audio = audioOrigin.GetComponents<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudioSource(int index)
    {
        if (index >= 0 && index < Audio.Length)
        {
            Audio[index].Play();
        }
        else
        {
            Debug.LogError("Invalid AudioSource index!");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = Color.gray;
        PlayAudioSource(0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        text.color = Color.white;
        PlayAudioSource(1);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
