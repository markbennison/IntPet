using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Utilities;

public class MoveTo : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The camera that objects will face when spawned. If not set, defaults to the main camera.")]
    Camera m_CameraToFace;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// The camera that objects will face when spawned. If not set, defaults to the <see cref="Camera.main"/> camera.
    /// </summary>
    public Camera cameraToFace
    {
        get
        {
            EnsureFacingCamera();
            return m_CameraToFace;
        }
        set => m_CameraToFace = value;
    }

    void EnsureFacingCamera()
    {
        if (m_CameraToFace == null)
            m_CameraToFace = Camera.main;
    }


    public bool TryMoveTo(Vector3 spawnPoint, Vector3 spawnNormal)
    {
        transform.position = spawnPoint;
        var facePosition = m_CameraToFace.transform.position;
        var forward = facePosition - spawnPoint;
        BurstMathUtility.ProjectOnPlane(forward, spawnNormal, out var projectedForward);
        transform.rotation = Quaternion.LookRotation(projectedForward, spawnNormal);
        return true;
    }
}
