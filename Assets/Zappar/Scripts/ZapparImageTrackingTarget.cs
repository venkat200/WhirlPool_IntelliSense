
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

using Zappar;

public class ZapparImageTrackingTarget : ZapparTrackingTarget, ZapparCamera.ICameraListener
{
    public string imageTargetZPT;

    private bool m_isMirrored;

    private IntPtr m_imageTracker;
    private bool m_hasInitialised = false;

    void Start()
    {
        ZapparCamera.Instance.RegisterCameraListener(this);
    }

    public void OnZapparInitialised(IntPtr pipeline)
    {
        m_hasInitialised = true;
        m_imageTracker = Z.ImageTrackerCreate(pipeline);

        string filename = imageTargetZPT;
        StartCoroutine(Z.LoadZPTTarget(filename, TargetDataAvailableCallback));
    }

    public void OnMirroringUpdate(bool mirrored)
    {
        m_isMirrored = mirrored;
    }

    void UpdateTargetPose()
    {
        Matrix4x4 cameraPose = ZapparCamera.Instance.GetPose();
        Matrix4x4 imagePose = Z.ImageTrackerAnchorPose(m_imageTracker, 0, cameraPose, m_isMirrored);
        Matrix4x4 targetPose = Z.ConvertToUnityPose(imagePose);

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

        if (Z.ImageTrackerAnchorCount(m_imageTracker) > 0)
        {
            UpdateTargetPose();
        }
    }

    private void TargetDataAvailableCallback(byte[] data)
    {
        Z.ImageTrackerTargetLoadFromMemory(m_imageTracker, data);
    }

    void OnDestroy()
    {
        if (m_hasInitialised)
        {
            Z.ImageTrackerDestroy(m_imageTracker);
        }
    }

    public override Matrix4x4 AnchorPoseCameraRelative()
    {
        if (Z.ImageTrackerAnchorCount(m_imageTracker) > 0)
        {
            return Z.ImageTrackerAnchorPoseCameraRelative(m_imageTracker, 0, m_isMirrored);
        }
        return Matrix4x4.identity;
    }

}
