using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetUp : NetworkBehaviour
{
    [SerializeField]
    private List<Behaviour> componentsToDisable;

    [SerializeField] private string remoteLayerName = "RemotePlayer";

    private Camera sceneCam;
    
    void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssighnRemoteLayer();
        }
        else
        {
            sceneCam = Camera.main;
            if (sceneCam != null)
            {
                sceneCam.gameObject.SetActive(false);
            }
        }
    }

    void DisableComponents()
    {
        foreach (var componentToDisable in componentsToDisable)
        {
            componentToDisable.enabled = false;
        }
    }

    void AssighnRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }
}
