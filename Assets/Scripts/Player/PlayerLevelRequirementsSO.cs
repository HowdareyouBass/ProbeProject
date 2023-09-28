using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerLevelRequirementDB", menuName = "PlayerLevelRequirementDB")]
public class PlayerLevelRequirementsSO : ScriptableObject
{
    public const int PLAYER_MAX_LEVEL = 5;

    [SerializeField] private float[] data;

    public void OnEnable()
    {
        if (data == null)
            data = new float[PLAYER_MAX_LEVEL];
    }

    public float GetLevelRequirement(int level)
    {
        if (level >= PLAYER_MAX_LEVEL || level < 0)
        {
            throw new System.ArgumentOutOfRangeException();
        }
        return data[level - 1];
    }
}