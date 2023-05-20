using UnityEngine;

[System.Serializable]
public class Item
{
    [SerializeField] private string m_Name;
    [SerializeField] private int m_AttackSpeed;

    public string name { get => m_Name; }
    public int attackSpeed { get => m_AttackSpeed; }

    public Item(string name)
    {
        m_Name = name;
    }
}