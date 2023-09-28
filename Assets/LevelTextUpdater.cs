using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LevelTextUpdater : MonoBehaviour
{
    [SerializeField] private PlayerScript m_Player;
    [SerializeField] private int m_StartLevel = 1;

    private Text m_LevelText;
    private int m_CurrentLevel;

    private void Start()
    {
        m_LevelText = GetComponent<Text>();
        m_CurrentLevel = m_StartLevel;
        m_LevelText.text = m_CurrentLevel.ToString();
        m_Player.events.OnLevelUp.Subscribe(UpdateText);
    }

    private void UpdateText()
    {
        m_CurrentLevel++;
        m_LevelText.text = m_CurrentLevel.ToString();
    }
}
