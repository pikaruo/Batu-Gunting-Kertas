using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Cards : MonoBehaviour
{

    public Attack attackValue;
    public CardPlayer player;
    public Vector2 OriginalPositition;
    Vector2 originalScale;
    Color originalColor;
    bool isClickable = true;

    private void Start()
    {
        OriginalPositition = this.transform.position;
        originalScale = this.transform.localScale;
        originalColor = GetComponent<Image>().color;
    }

    public void OnClick()
    {
        if (isClickable)
            player.SetChosenCard(this);
    }

    internal void Reset()
    {
        transform.position = OriginalPositition;
        transform.localScale = originalScale;
        GetComponent<Image>().color = originalColor;
    }

    public void setClickable(bool value)
    {
        isClickable = value;
    }

}
