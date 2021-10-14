using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ResetCameraPos : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook cinemachineCam;
    [SerializeField] GameObject player;

    private float camHeight;
    private float camRadius;
    private float xAxisValue;

    private void Awake()
    {
        camHeight = cinemachineCam.m_Orbits[1].m_Height;
        camRadius = cinemachineCam.m_Orbits[1].m_Radius;

    }


    public void ResetPos()
    {
        var offset = player.transform.rotation * new Vector3(0, camHeight, camRadius);
        cinemachineCam.ForceCameraPosition(player.transform.position + offset, player.transform.rotation);
        cinemachineCam.m_XAxis.Value = 0f;
    }
}