using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public StageObjectives stageObjectives;

    public static GameHandler instance;

    public TextMeshProUGUI currentGoal, reachGoal;

    public int playerHealth = 3;

    public Sprite fullHeart;
    public Sprite emptyHeart;
    public List<Image> hearts;

    private void Awake()
    {
        instance = this;
        stageObjectives.currentGoals = 0; // Reset the value so that each level will start the goals at 0
    }

    private void Update()
    {
        ObjectiveVisuals();
        UpdateHealthVisual();
    }


    void ObjectiveVisuals()
    {
        currentGoal.text = stageObjectives.currentGoals.ToString("n0");
        reachGoal.text = stageObjectives.levelGoalObjective.ToString("n0");
    }

    void UpdateHealthVisual()
    {
        switch (playerHealth)
        {
            case 0:
                hearts[2].sprite = emptyHeart;
                hearts[1].sprite = emptyHeart;
                hearts[0].sprite = emptyHeart;
                break;
            case 1:
                hearts[2].sprite = emptyHeart;
                hearts[1].sprite = emptyHeart;
                hearts[0].sprite = fullHeart;
                break;
            case 2:
                hearts[2].sprite = emptyHeart;
                hearts[1].sprite = fullHeart;
                hearts[0].sprite = fullHeart;
                break;
            case 3:
                hearts[2].sprite = fullHeart;
                hearts[1].sprite = fullHeart;
                hearts[0].sprite = fullHeart;
                break;
        }
    }

    public void UpdateObjectives(int amount)
    {
        stageObjectives.currentGoals += amount;

        if(stageObjectives.currentGoals >= stageObjectives.levelGoalObjective)
        {
            SetGameWin();
        }
    }

    public void UpdateHealth(int amount)
    {
        playerHealth += amount;

        if(playerHealth <= 0)
        {
            SetGameLose();
        }
    }

    public void SetGameWin()
    {

    }

    public void SetGameLose()
    {

    }
}
