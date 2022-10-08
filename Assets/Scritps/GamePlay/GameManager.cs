using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Player P1;
    public Player P2;
    public GameState State = GameState.ChooseAttack;
    public GameObject gameOverPanel;
    public TMP_Text winnerText;
    private Player damagedPlayer;
    private Player winner;

    // Enum State
    public enum GameState
    {
        ChooseAttack,
        Attack,
        Damages,
        Draw,
        GameOver
    }


    private void Start()
    {
        gameOverPanel.SetActive(false);
        //* Mengecek posisi state
        //     Debug.Log(state == State.ChooseAttack);
        //     state = State.Damages;
        //     Debug.Log(state == State.ChooseAttack);
        //     Debug.Log(state);


    }

    private void Update()
    {
        // Logic gameplay
        switch (State)
        {
            case GameState.ChooseAttack:
                if (P1.AttackValue != null && P2.AttackValue != null)
                {
                    P1.AnimateAttack();
                    P2.AnimateAttack();
                    P1.SetClickable(false);
                    P2.SetClickable(false);
                    State = GameState.Attack;
                }
                break;

            case GameState.Attack:
                if (P1.IsAnimating() == false && P2.IsAnimating() == false)
                {
                    damagedPlayer = GetDamagePlayer();
                    if (damagedPlayer != null)
                    {
                        damagedPlayer.AnimateDamage();
                        State = GameState.Damages;
                    }
                    else
                    {
                        P1.AnimateDraw();
                        P2.AnimateDraw();
                        State = GameState.Draw;
                    }
                }

                break;
            case GameState.Damages:
                if (P1.IsAnimating() == false && P2.IsAnimating() == false)
                {
                    //* Calculate Health
                    if (damagedPlayer == P1)
                    {
                        P1.ChangeHealth(-10);
                        P2.ChangeHealth(5);
                    }
                    else
                    {
                        P2.ChangeHealth(-10);
                        P1.ChangeHealth(5);
                    }

                    var winner = GetWinner();

                    if (winner == null)
                    {
                        ResetPlayers();
                        P1.SetClickable(true);
                        P2.SetClickable(true);
                        State = GameState.ChooseAttack;
                    }
                    else
                    {
                        gameOverPanel.SetActive(true);
                        winnerText.text = winner == P1 ? "Player 1 Wins" : "Player 2 Wins";
                        ResetPlayers();
                        State = GameState.GameOver;
                    }

                }
                break;
            case GameState.Draw:
                if (P1.IsAnimating() == false && P2.IsAnimating() == false)
                {
                    ResetPlayers();
                    P1.SetClickable(true);
                    P2.SetClickable(true);
                    State = GameState.ChooseAttack;
                }
                break;
        }
    }

    private void ResetPlayers()
    {
        damagedPlayer = null;
        P1.Reset();
        P2.Reset();
    }

    private Player GetDamagePlayer()
    {
        Attack? PlayerAttack1 = P1.AttackValue;
        Attack? PlayerAttack2 = P2.AttackValue;

        if (PlayerAttack1 == Attack.Rock && PlayerAttack2 == Attack.Paper)
        {
            return P1;
        }
        else if (PlayerAttack1 == Attack.Rock && PlayerAttack2 == Attack.Scissor)
        {
            return P2;
        }
        else if (PlayerAttack1 == Attack.Paper && PlayerAttack2 == Attack.Rock)
        {
            return P2;
        }
        else if (PlayerAttack1 == Attack.Paper && PlayerAttack2 == Attack.Scissor)
        {
            return P1;
        }
        else if (PlayerAttack1 == Attack.Scissor && PlayerAttack2 == Attack.Rock)
        {
            return P1;
        }
        else if (PlayerAttack1 == Attack.Scissor && PlayerAttack2 == Attack.Paper)
        {
            return P2;
        }

        return null;
    }

    private Player GetWinner()
    {
        if (P1.Health == 0)
        {
            return P2;
        }
        else if (P2.Health == 0)
        {
            return P1;
        }
        return null;
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
