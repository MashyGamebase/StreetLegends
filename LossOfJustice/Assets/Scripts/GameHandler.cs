using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public StageObjectives stageObjectives;
    public GameObject cutsceneHolder;
    public GameObject character;

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
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < playerHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void UpdateObjectives(int amount)
    {
        stageObjectives.currentGoals += amount;

        if(stageObjectives.currentGoals >= stageObjectives.levelGoalObjective)
        {
            cutsceneHolder.SetActive(true);
            character.SetActive(false);
        }
    }

    public void ManualShowDialogSequence()
    {
        cutsceneHolder.SetActive(true);
        character.SetActive(false);
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
        FindObjectOfType<LeveLoadTrigger>().ManualLoadLevel();
    }

    public void SetGameLose()
    {
        FindObjectOfType<LeveLoadTrigger>().RestartLevel();
    }
}
