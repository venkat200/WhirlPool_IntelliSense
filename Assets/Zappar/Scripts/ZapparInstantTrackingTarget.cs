using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

using Zappar;
using UnityEngine.EventSystems;

public class ZapparInstantTrackingTarget : ZapparTrackingTarget, ZapparCamera.ICameraListener
{
    private IntPtr m_instantTracker;
    private bool m_userHasPlaced = false;
    private bool m_hasInitialised = false;
    private bool m_isMirrored = false;

    public GameObject BackGroundCanvas;
    public GameObject LoadingBar;
    public GameObject SceneObject;
 
    bool pinClick = false;

    // Touch Input Check
    private Touch theTouch;
    private float timeTouchBegan;
    private float timeTouchEnded;
    private Vector2 startPosition, endPosition;
    private float displayTime = 0.5f;
    private Touch touch;
    bool pinTry = false;
    // Touch Input Check

    void Start()
    {
        ZapparCamera.Instance.RegisterCameraListener( this );
    }

    void OnEnable()
    {
        BackGroundCanvas.SetActive(false);
        LoadingBar.SetActive(true);
        SceneObject.transform.localScale = new Vector3(3f, 3f, 3f);
    }

    void OnDisable()
    {
        BackGroundCanvas.SetActive(true);
        LoadingBar.SetActive(false);
        SceneObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void OnZapparInitialised(IntPtr pipeline) 
    {
        m_instantTracker = Z.InstantWorldTrackerCreate( pipeline );
        m_hasInitialised = true;
    }

    public void OnMirroringUpdate(bool mirrored)
    {
        m_isMirrored = mirrored;
    }

    void UpdateTargetPose()
    {
        Matrix4x4 cameraPose = ZapparCamera.Instance.GetPose();
        Matrix4x4 instantTrackerPose = Z.InstantWorldTrackerAnchorPose(m_instantTracker, cameraPose, m_isMirrored);
        Matrix4x4 targetPose = Z.ConvertToUnityPose(instantTrackerPose);

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

        if (!m_userHasPlaced)
        {
            Z.InstantWorldTrackerAnchorPoseSetFromCameraOffset(m_instantTracker, 0, 0, -5, Z.InstantTrackerTransformOrientation.MINUS_Z_AWAY_FROM_USER);
        }

        if (Input.touchCount > 0)
        {
            m_userHasPlaced = !m_userHasPlaced;
            UpdateTargetPose();
        }

        // UpdateTargetPose();

        if (Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);
            // Check if finger is over a UI element
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                if (theTouch.phase == TouchPhase.Began)
                {
                    startPosition = theTouch.position;
                    timeTouchBegan = Time.time;
                }
                if (theTouch.phase == TouchPhase.Ended)
                {
                    pinTry = true;
                    endPosition = theTouch.position;
                    timeTouchEnded = Time.time;
                }
            }
        }
        // else if ((Time.time - timeTouchEnded > displayTime) && pinTry==true && (timeTouchEnded - timeTouchBegan < 1f))
        else if (pinTry == true && (timeTouchEnded - timeTouchBegan < 1f) && (Vector2.Distance(startPosition, endPosition) < 4f))
        {
            pinClick = !pinClick;
            m_userHasPlaced = !m_userHasPlaced;
            LoadingBar.SetActive(!pinClick);
            pinTry = false;
        }

    }

    void OnDestroy()
    {
        if (m_hasInitialised) 
        {
            Z.InstantWorldTrackerDestroy(m_instantTracker);
        }
    }

    public override Matrix4x4 AnchorPoseCameraRelative()
    {
        return Z.InstantWorldTrackerAnchorPoseCameraRelative(m_instantTracker, m_isMirrored);
    }
}
