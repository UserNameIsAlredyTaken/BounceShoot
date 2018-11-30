using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerHealth : HealthClass {
    
    [SyncVar(hook = "SetHealthUI")]
    public float m_CurrentHealth;
    public float m_StartingHealth = 100;
    public RectTransform m_Healthbar;

    private NetworkStartPosition[] spawnPoints;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.AddComponent<ChildrenComponentHealth>();
        }

        if (isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }

    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
    }

    public override void TakeDamage(float amount)
    {
        if (isServer)
        {
            m_CurrentHealth -= amount;

            if(m_CurrentHealth <= 0)
            {
                OnDeath();
            } 
        }
    }

    private void SetHealthUI(float currentHealth)
    {
        m_Healthbar.sizeDelta = new Vector2(currentHealth, m_Healthbar.sizeDelta.y);
    }

    private void OnDeath()
    {
        m_CurrentHealth = m_StartingHealth;
        RpcRespawn();
    }

    [ClientRpc]
    private void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            Vector3 spawnPoint = Vector3.zero;//default spawn point

            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }

            transform.position = spawnPoint;
        }
    }
}
