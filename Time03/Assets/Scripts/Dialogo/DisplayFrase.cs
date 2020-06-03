﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayFrase : MonoBehaviour
{
    public int MyTurn;
    public PersonagemFraseSO Frases;
    public DialogueTrigger Trigger;

    public Image ChatBox;

    private Text Chat;

    private bool FraseEnd;

    private bool ControlAcess = true;

    private bool StartIt;
    private GeneralCounts Counts;

    void Start()
    {
        Counts = SaveSystem.GetInstance().generalCounts;
        Chat = ChatBox.transform.GetChild(0).GetComponent<Text>();
    }

    
    void Update()
    {
        if(Trigger.CanChat()) {
            if((Frases.Frase[Counts.Index].Turn != MyTurn) && FraseEnd) {
                HideFrase();
                return;
            }

            if(Frases.Frase[Counts.Index].Options.Count > 0) {
                HideFrase();
                return;
            }

            if(!ChatBox.gameObject.activeSelf && Frases.Frase[Counts.Index].Turn == MyTurn && StartIt) {
                ShowFrase();
            }
        }

        if(Input.GetAxisRaw("PressButton") > 0 && ControlAcess) {
            if(ChatBox.gameObject.activeSelf) {
                Next();
            }
            StartCoroutine(GrantAcess());
        }
    }

    private void Next() {
        if(!FraseEnd) {
            Chat.text = Frases.Frase[Counts.Index].Texto;
            FraseEnd = true;
        }
        else {
            StartCoroutine(ShowLetters());
        }
    }

    private void ShowFrase() {
        ChatBox.gameObject.SetActive(true);
        StartCoroutine(ShowLetters());
        StartIt = false;
    }

    public void ShowFrase(int NewIndex) {
        ChatBox.gameObject.SetActive(true);
        StartCoroutine(ShowLetters(NewIndex));
        StartIt = false;
    }

    private void HideFrase() {
        ChatBox.gameObject.SetActive(false);
        if(Frases.Frase[Counts.Index].Turn == 0) {
            Trigger.EndConversation();
        }
        StartIt = true;
    }

    private IEnumerator ShowLetters() {
        Chat.text = "";
        FraseEnd = false;
        foreach(char c in Frases.Frase[Counts.Index].Texto) {
            if(FraseEnd) {
                break;
            }
            Chat.text += c;
            yield return new WaitForSeconds(0.1f);
        }
        FraseEnd = true;
        Counts.Index++;
    }

    private IEnumerator ShowLetters(int Index) {
        Chat.text = "";
        FraseEnd = false;
        foreach(char c in Frases.Frase[Counts.Index].Texto) {
            if(FraseEnd) {
                break;
            }
            Chat.text += c;
            yield return new WaitForSeconds(0.1f);
        }
        FraseEnd = true;
        Counts.Index = Index;
    }

    private IEnumerator GrantAcess()
    {
        ControlAcess = false;
        yield return new WaitForSecondsRealtime(0.3f);
        ControlAcess = true;
    }
}
