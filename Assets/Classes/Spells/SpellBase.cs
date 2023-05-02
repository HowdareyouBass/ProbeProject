using UnityEngine;

public abstract class Spell
{
    [SerializeField] private string m_Name;
    [SerializeField] private GameObject m_Effect = null;

    public GameObject effect { get => m_Effect; }
}