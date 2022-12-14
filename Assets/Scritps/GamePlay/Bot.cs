using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{

    public Player player;
    public GameManager gameManager;
    public float choosingInterval;
    private float timer = 0;
    int lastSelection = 0;
    Cards[] cards;

    private void Start()
    {
        cards = GetComponentsInChildren<Cards>();
    }

    void Update()
    {
        /* 
        *mengecek apakah 
        */
        if (gameManager.State != GameManager.GameState.ChooseAttack)
        {
            timer = 0;
            return;
        }

        if (timer < choosingInterval)
        {
            timer += Time.deltaTime;
            return;
        }

        timer = 0;
        ChooseAttack();
    }

    /* 
    *berfungsi untuk memilih jenis serangan secara acak
    *berdasarkan panjang array serangan
    *kemudian mengambil jenis serangan terakhir
    */
    public void ChooseAttack()
    {
        var random = Random.Range(1, cards.Length);
        var selection = (lastSelection + random) % cards.Length;
        lastSelection = selection;
        player.SetChosenCard(cards[selection]);
    }
}
