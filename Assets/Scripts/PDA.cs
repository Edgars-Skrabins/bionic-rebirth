using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PDA : InteractionObject
{
    
    public UnityEvent eventPDA;
    [SerializeField] private int nextProgression;
    [SerializeField] private string progressionText;
    [SerializeField] private Animation anim;
    [SerializeField] private TMP_Text tMP_Text;

    private void Start()
    {
        tMP_Text.text = progressionText.ToString();

    }

    public override void Interact()
    {
        GameObject rendererGO = gameObject.GetComponentInChildren<MeshRenderer>().gameObject;
        if (PlayerPrefs.GetInt("PlayerProLevel") == nextProgression) { rendererGO.SetActive(false); return; }
        PlayerPrefs.SetInt("PlayerProLevel", nextProgression);
        anim.Play();
        rendererGO.SetActive(false);
        if (eventPDA == null) { return; }
        else { eventPDA.Invoke(); }
        
    }

}
