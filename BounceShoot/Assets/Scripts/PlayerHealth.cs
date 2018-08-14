using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : HealthClass {

    public float m_StartingHealth = 100;
    public Slider m_Slider;
    public Image m_FillImage;
    public Color m_FullHealthColor = Color.green;
    public Color m_ZeroHealthColor = Color.red;

    private float m_CurrentHealth;
    private bool m_Dead;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.AddComponent<ChildrenComponentHealth>();
        }
    }

    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;
        SetHealthUI();
    }

    public override void TakeDamage(float amount)
    {
        m_CurrentHealth -= amount;
        SetHealthUI();

        if(m_CurrentHealth <= 0 && !m_Dead)
        {
            OnDeath();
        }
    }

    private void SetHealthUI()
    {
        m_Slider.value = m_CurrentHealth;
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
    }

    private void OnDeath()
    {
        m_Dead = true;
        gameObject.SetActive(false);
    }
}
