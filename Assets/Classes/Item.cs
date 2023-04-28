using UnityEngine;

[System.Serializable]
public class Item
{
    [SerializeField] private string m_Name;
    [SerializeField] private int m_AttackSpeed;
    //[SerializeField] private 

    public Item(string name)
    {
        m_Name = name;
    }

    public int GetAttackSpeed() { return m_AttackSpeed; }
    public string GetName() { return m_Name; }
}
