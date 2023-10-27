using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LevelTextUpdater : MonoBehaviour
{
    [SerializeField] private PlayerScript m_Player;
    private Text m_LevelText;

    private void Start()
    {
        m_LevelText = GetComponent<Text>();
    }
    private void Update()
    {
        m_LevelText.text = m_Player.GetEntity().Stats.CurrentLevel.ToString();
    }
}
