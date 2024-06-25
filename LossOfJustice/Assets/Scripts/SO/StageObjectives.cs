using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stage Objective", menuName = "Stages/Objectives")]
public class StageObjectives : ScriptableObject
{
    public int levelID;
    public int levelGoalObjective;
    public int currentGoals;
}