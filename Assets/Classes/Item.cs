using UnityEngine;

[System.Serializable]
public class Item : DatabaseItem
{
    [SerializeField] private int m_AttackSpeed;
    //[SerializeField] private 

    public Item(string name)
    {
        m_Name = name;
    }

    public int GetAttackSpeed()
    {
        return m_AttackSpeed;
    }
}
