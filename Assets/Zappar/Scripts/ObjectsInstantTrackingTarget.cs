using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using Zappar;

public class ObjectsInstantTrackingTarget : ZapparTrackingTarget, ZapparCamera.ICameraListener
{
    enum TOUCHSTATE
    {
        ONTOUCH,
        UNTOUCH,
        SINGLETAP,
        DOUBLETAP,
        TOUCHDRAG,
    };

    private TOUCHSTATE  _tState;
    private Vector2     _touch_start, _touch_dragPos;
    private int         _touchCount;
    private float       _touch_1_time, _touch_2_time;
    private float       _diff_Start, _diff_move;
    private bool        _single_Tap, _double_Tap;
    private GameObject  _object_to_rotate;

    public GameObject Objects, Sofa, Wardrobe, Coffeetable, Recamier, MessageBox, LoadingBar;
    public Text Message;
    public Vector2 ScaleRange;
    public GameObject icon_anim_door_empty, icon_anim_door_items, icon_anim_coffeetable;

    private IntPtr m_instantTracker;
    private GameObject _selectedOvject;
    private bool m_userHasPlaced = false;
    private bool m_hasInitialised = false;
    private bool m_isMirrored = false;

    //variables for wardrobe animations
    private Animator _wardrobe_animator, _coffeeTable_animator;
    private bool _doorOpen, _itemsShown;
    private bool _shownSits;

    void Start()
    {
        _object_to_rotate = null;
        _tState = TOUCHSTATE.UNTOUCH;
        _touchCount = 0;
        _touch_1_time = 0f;
        _touch_2_time = 0f;
        _double_Tap = false;

        _doorOpen = false;
        _itemsShown = false;
        _shownSits = false;

        _wardrobe_animator = Wardrobe.GetComponent<Animator>();
        _coffeeTable_animator = Coffeetable.GetComponent<Animator>();
        Close_all_anim_icons();

        Message.text = "Double Tap to Place";

        ZapparCamera.Instance.RegisterCameraListener( this );
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

        TouchUpdate();

        if (!m_userHasPlaced)
        {
            Z.InstantWorldTrackerAnchorPoseSetFromCameraOffset(m_instantTracker, 0, 0, -5, Z.InstantTrackerTransformOrientation.MINUS_Z_AWAY_FROM_USER);

            UpdateTargetPose();
        }        

         //UpdateTargetPose();
    }

    void TouchUpdate()
    {
        switch(_tState)
        {
            case TOUCHSTATE.UNTOUCH:

                if (Input.touchCount > 0)
                {
                    _tState = TOUCHSTATE.ONTOUCH;
                }
                else
                {
                    if (_single_Tap)
                    {
                        _single_Tap = false;

                        if (!_double_Tap)
                        {
                            _touch_1_time = Time.time;
                            _double_Tap = true;
                        }
                        //add single tap functions
                    }
                    else
                    {
                        _object_to_rotate = null;
                    }
                }
                break;

            case TOUCHSTATE.ONTOUCH:
                if (Input.touchCount == 1)
                {
                    if (!_single_Tap)
                    {
                        _single_Tap = true;

                        if (Input.touches[0].phase == TouchPhase.Moved)
                        {
                                //add touch and drag functions
                        }
                    }                    
                    else if (_double_Tap)
                    {
                        if (Time.time - _touch_1_time < 2f)
                        {
                            //add double tap functions

                            m_userHasPlaced = !m_userHasPlaced;
                            LoadingBar.SetActive(!m_userHasPlaced);
                            Message.text = (m_userHasPlaced == false) ? "Double Tap to Place" : "Double Tap to Move";

                            //add object to rotate
                            //_object_to_rotate = someobject;
                        }
                       

                        _double_Tap = false;
                    }
                }
                else if(Input.touchCount == 2)
                {            

                    if (Input.touches[1].phase == TouchPhase.Began)
                    {
                        _diff_Start = Vector3.Distance(Input.touches[1].position, Input.touches[0].position);
                    }

                    if (_object_to_rotate != null)
                    {
                        if (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved)
                        {
                            
                                //add objects rotation code
                                //hold this
                           
                        }
                    }
                    else
                    {

                        if (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved)
                        {
                            _diff_move = Vector3.Distance(Input.touches[1].position, Input.touches[0].position);

                            if (_diff_move > _diff_Start) //pinch out
                            {
                                if (Objects.transform.localScale.y < ScaleRange.y)
                                    Objects.transform.localScale += new Vector3(1f, 1f, 1f) * Time.deltaTime * 0.5f;

                                _diff_Start = _diff_move;
                            }
                            else if (_diff_move < _diff_Start) //pinch in
                            {
                                if (Objects.transform.localScale.y > ScaleRange.x)
                                    Objects.transform.localScale -= new Vector3(1f, 1f, 1f) * Time.deltaTime * 0.5f;

                                _diff_Start = _diff_move;
                            }
                            else
                            { }
                        }
                    }
                }
                else
                    _tState = TOUCHSTATE.UNTOUCH;
                break;
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

    //show objects

    public void ToggleSofa()
    {
        Close_all_anim_icons();

        Sofa.SetActive(true);
        Wardrobe.SetActive(false);
        Recamier.SetActive(false);
        Coffeetable.SetActive(false);
    }    

    public void ToggleWardrobe()
    {
        Close_all_anim_icons();
        icon_anim_door_empty.SetActive(true);
        icon_anim_door_items.SetActive(true);

        Sofa.SetActive(false);
        Wardrobe.SetActive(true);
        Recamier.SetActive(false);
        Coffeetable.SetActive(false);
    }

    public void ToggleCoffeeTable()
    {
        Close_all_anim_icons();
        icon_anim_coffeetable.SetActive(true);

        Sofa.SetActive(false);
        Wardrobe.SetActive(false);
        Recamier.SetActive(false);
        Coffeetable.SetActive(true);
    }

    public void ToggleRecramier()
    {
        Close_all_anim_icons();

        Sofa.SetActive(false);
        Wardrobe.SetActive(false);
        Recamier.SetActive(true);
        Coffeetable.SetActive(false);
    }

    //play animations - wardrobe

    public void OnClick_EmptyOpen()
    {
        if(!_doorOpen)
        {
            _wardrobe_animator.Play("Door opening");
            _doorOpen = true;
        }
        else
        {
            if(_itemsShown)
            {
                _wardrobe_animator.Play("close with products");
                _itemsShown = false;
                _doorOpen = false;
            }
            else
            {
                _wardrobe_animator.Play("close without products");
                _itemsShown = false;
                _doorOpen = false;
            }
        }
    }

    public void OnClick_ItemsOpen()
    {
        if (!_doorOpen)
        {
            _wardrobe_animator.Play("open with everything");
            _doorOpen = true;
        }
        else
        {
            if (_itemsShown)
            {
                _wardrobe_animator.Play("products disappearing");
                _itemsShown = false;
                
            }
            else
            {
                _wardrobe_animator.Play("Products appearing");
                _itemsShown = true;
            }
        }
    }

    //play animations - coffeeTable

    public void OnClick_OpenTable()
    {
        if (!_shownSits)
        {
            _coffeeTable_animator.Play("open");
            _shownSits = true;
        }
        else
        {
            _coffeeTable_animator.Play("close");
            _shownSits = false;
        }
    }

    //close all anim icons

    void Close_all_anim_icons()
    {
        icon_anim_coffeetable.SetActive(false);
        icon_anim_door_empty.SetActive(false);
        icon_anim_door_items.SetActive(false);
    }
    
    public void ClickCam()
    {
        StartCoroutine(UploadPNG());
        //Debug.log (encodedText);
    }

    IEnumerator UploadPNG()
    {
        // We should only read the screen after all rendering is complete
        yield return new WaitForEndOfFrame();

        // Create a texture the size of the screen, RGB24 format
        int width = Screen.width;
        int height = Screen.height;
        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        
        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();
        Destroy(tex);

        //string ToBase64String byte[]
        string encodedText = System.Convert.ToBase64String(bytes);

        var image_url = "data:image/png;base64," + encodedText;

        Debug.Log(image_url);

#if !UNITY_EDITOR
        openWindow(image_url);
#endif
    }

    [DllImport("__Internal")]
    private static extern void openWindow(string url);

}
