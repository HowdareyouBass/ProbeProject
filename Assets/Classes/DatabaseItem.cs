using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseItem
{
    [SerializeField] protected string m_Name;

    public string GetName()
    {
        return m_Name;
    }
}
