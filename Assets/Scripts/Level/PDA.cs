using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PDA : InteractionObject
{
    public UnityEvent OnPDAPickup;
    [SerializeField] private int m_nextProgression;
    [SerializeField] private string m_progressionText;
    [SerializeField] private Animation m_animator;
    [SerializeField] private TMP_Text m_text;

    private void Start()
    {
        m_text.text = m_progressionText;
    }

    public override void Interact()
    {
        GameObject rendererGO = gameObject.GetComponentInChildren<MeshRenderer>().gameObject;
        if (PlayerPrefs.GetInt("PlayerProLevel") == m_nextProgression)
        {
            rendererGO.SetActive(false);
            return;
        }

        PlayerPrefs.SetInt("PlayerProLevel", m_nextProgression);
        m_animator.Play();
        rendererGO.SetActive(false);
        OnPDAPickup?.Invoke();
    }
}
