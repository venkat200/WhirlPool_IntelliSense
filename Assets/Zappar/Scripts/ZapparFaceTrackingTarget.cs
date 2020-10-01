using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

using Zappar;

public class ZapparFaceTrackingTarget : ZapparTrackingTarget, ZapparCamera.ICameraListener
{

    public IntPtr m_faceTracker;
    private bool m_hasInitialised = false;

    private bool m_isMirrored;

    void Start()
    {
        ZapparCamera.Instance.RegisterCameraListener(this);
    }

    public void OnZapparInitialised(IntPtr pipeline)
    {
        m_faceTracker = Z.FaceTrackerCreate(pipeline);
#if UNITY_EDITOR
        byte[] faceTrackerData = Z.LoadRawBytes(Z.FaceTrackingModelPath());
        Z.FaceTrackerModelLoadFromMemory(m_faceTracker, faceTrackerData);
#else
        Z.FaceTrackerModelLoadDefault(m_faceTracker);
#endif
        m_hasInitialised = true;
    }

    public void OnMirroringUpdate(bool mirrored)
    {
        m_isMirrored = mirrored;
    }

    void UpdateTargetPose()
    {
        Matrix4x4 cameraPose = ZapparCamera.Instance.GetPose();
        Matrix4x4 facePose = Z.FaceTrackerAnchorPose(m_faceTracker, 0, cameraPose, m_isMirrored);
        Matrix4x4 targetPose = Z.ConvertToUnityPose(facePose);

        transform.localPosition = Z.GetPosition(targetPose);
        transform.localRotation = Z.GetRotation(targetPose);
        transform.localScale = Z.GetScale(targetPose);
    }

    void Update()
    {
        if (!m_hasInitialised)
        {
            return;
        }
        if (Z.FaceTrackerAnchorCount(m_faceTracker) > 0)
        {

            UpdateTargetPose();
        }
    }

    void OnDestroy()
    {
        if (m_hasInitialised)
        {
            Z.FaceTrackerDestroy(m_faceTracker);
        }
    }

    public override Matrix4x4 AnchorPoseCameraRelative()
    {
        if (Z.FaceTrackerAnchorCount(m_faceTracker) > 0)
        {
            return Z.FaceTrackerAnchorPoseCameraRelative(m_faceTracker, 0, m_isMirrored);
        }
        return Matrix4x4.identity;
    }
}
