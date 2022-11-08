using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardPlayer : MonoBehaviour
{

    public Transform attackPosRef;
    public Cards chosenCard;
    public HealthBar healthBar;
    private Tweener animationTweener;

    public float Health;
    public float MaxHealth;

    public TMP_Text nameText;
    public TMP_Text healthText;

    public TMP_Text NickName { get => nameText; }

    private void Start()
    {
        Health = MaxHealth;
    }

    public Attack? AttackValue
    {
        get => chosenCard == null ? null : chosenCard.attackValue;
    }


    public void SetChosenCard(Cards newCard)
    {
        if (chosenCard != null)
        {
            chosenCard.Reset();
        }

        chosenCard = newCard;
        chosenCard.transform.DOScale(chosenCard.transform.localScale * 1.1f, 0.2f);
    }

    public void ChangeHealth(float amount)
    {
        Health += amount;
        Health = Math.Clamp(Health, 0, 100);
        // Health Bar
        healthBar.UpdateBar(Health / MaxHealth);
        // Text
        healthText.text = Health + "/" + MaxHealth;
    }

    public void AnimateAttack()
    {
        animationTweener = chosenCard.transform.DOMove(attackPosRef.position, 1);
    }

    public void AnimateDamage()
    {
        var image = chosenCard.GetComponent<Image>();
        animationTweener = image
        .DOColor(Color.red, 0.1f)
        .SetLoops(3, LoopType.Yoyo)
        .SetDelay(0.2f);
    }
    internal void AnimateDraw()
    {
        var image = chosenCard.GetComponent<Image>();
        animationTweener = image
        .DOColor(Color.blue, 0.1f)
        .SetLoops(2, LoopType.Yoyo)
        .SetDelay(0.2f);
        animationTweener = chosenCard.transform
        .DOMove(chosenCard.OriginalPositition, 1)
        .SetEase(Ease.InBounce)
        .SetDelay(0.8f);
    }

    public bool IsAnimating()
    {
        return animationTweener.IsActive();
    }

    internal void Reset()
    {
        if (chosenCard != null)
        {
            chosenCard.Reset();
        }
        chosenCard = null;
    }
    public void SetClickable(bool value)
    {
        Cards[] cards = GetComponentsInChildren<Cards>();
        foreach (var card in cards)
        {
            card.setClickable(value);
        }
    }
}
