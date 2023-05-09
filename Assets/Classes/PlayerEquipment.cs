using System.Collections.Generic;

public class PlayerEquipment : EntityEquipment
{
    List<Item> m_Items;
    public PlayerEquipment()
    {
        m_Items = new List<Item>();
    }
    public void EquipItem(Item item)
    {
        m_Items.Add(item);
    }

    public int GetAttackSpeed()
    {
        int sum = 0;
        foreach (Item item in m_Items)
        {
            sum += item.attackSpeed;
        }
        return sum;
    }
}
