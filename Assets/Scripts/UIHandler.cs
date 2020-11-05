using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{

    [SerializeField]
    GameObject _Zappar_Camera, _InstantTracker, _Virtual_Camera;
    Vector3 initialPosition;
    bool ResetPosition = false;

    [SerializeField]
    GameObject _SceneObject, _ModelSphere;

    [SerializeField]
    GameObject _AR_Button_Icon_LandScape, _Virtual_Button_Icon_LandScape, _AR_Button_Icon_Portrait, _Virtual_Button_Icon_Portrait;
    public bool ARView = false, VirtualView = true;

    [SerializeField]
    GameObject _FadeInOut;
    public Image fadeImage;

    [SerializeField]
    GameObject _Panel_LandScape, _Panel_Portrait;
    [SerializeField]
    GameObject _InfoPanel_LandScape, _InfoButton_LandScape, _InfoImage_LandScape;
    public bool InfoClicked = false;
    [SerializeField]
    GameObject _BottomPanel_LandScape;
    [SerializeField]
    GameObject _Plus_White_LandScape, _Plus_Gold_LandScape, _CM_White_LandScape, _CM_Gold_LandScape, _Door_White_LandScape, _Door_Gold_LandScape, _Door_Bottom_White_LandScape, _Door_Bottom_Gold_LandScape;
    [SerializeField]
    GameObject _BackPanel_LandScape;

    [SerializeField]
    GameObject _InfoPanel_Portrait, _InfoButton_Portrait, _InfoImage_Portrait;
    [SerializeField]
    GameObject _BottomPanel_Portrait;
    [SerializeField]
    GameObject _Plus_White_Portrait, _Plus_Gold_Portrait, _CM_White_Portrait, _CM_Gold_Portrait, _Door_White_Portrait, _Door_Gold_Portrait, _Door_Bottom_White_Portrait, _Door_Bottom_Gold_Portrait;
    [SerializeField]
    GameObject _BackPanel_Portrait;


    [SerializeField]
    GameObject _FridgeModel, _FridgeDoor;
    Animator animatorStartFridge, animatorFridgeDoor;

    [SerializeField]
    GameObject _ProductName, _ProductName_Portrait;

    [SerializeField]
    GameObject InsideObject_1, InsideObject_2, InsideObject_3, InsideObject_4, InsideObject_5, InsideObject_6, InsideObject_7, InsideObject_8,
               InsideObject_9, InsideObject_10, InsideObject_11, InsideObject_12, InsideObject_13, InsideObject_14, InsideObject_15, InsideObject_16,
               InsideObject_17, InsideObject_18, InsideObject_19, InsideObject_20, InsideObject_21, InsideObject_22, InsideObject_23;
    [SerializeField]
    GameObject InsideDoorObject_Top, InsideDoorObject_Bottom;

    [SerializeField]
    GameObject ObjectRotateGameObject;
    ObjectRotate ObjectRotateScript;

    [SerializeField]
    GameObject SceneZoomGameObject;
    SceneZoom SceneZoomScript;

    Vector3 defaultScale;

    RaycastHit hit;
    bool toggle = true;
    bool toggleSwitchEnabled = false;

    public bool PlayingFeature;
    [SerializeField]
    GameObject _CameraContainer;


    // Start is called before the first frame update
    void Start()
    {
        animatorStartFridge = _FridgeModel.GetComponent<Animator>();
        animatorFridgeDoor = _FridgeDoor.GetComponent<Animator>();
        // animator3DFlow = _3DFlow_Sprite.GetComponent<Animator>();
        animatorMicroBlock = MicroBlock_Sprite.GetComponent<Animator>();
        animatorVariableTemperature = VariableTemperatureModel.GetComponent<Animator>();


        StartCoroutine(StartTransition());

        if (Screen.width > Screen.height)
        {
            _Panel_LandScape.SetActive(true);
            _Panel_Portrait.SetActive(false);
        }
        else
        {
            _Panel_LandScape.SetActive(false);
            _Panel_Portrait.SetActive(true);
        }

        ObjectRotateScript = ObjectRotateGameObject.GetComponent<ObjectRotate>();
        SceneZoomScript = SceneZoomGameObject.GetComponent<SceneZoom>();

        if (GoogleAnalyticsAdapter.Instance != null)
        {
            GoogleAnalyticsAdapter.Instance.googleAnalyticsLogEvent("Pageview", "Viewed");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.width > Screen.height)
        {
            _Panel_LandScape.SetActive(true);
            _Panel_Portrait.SetActive(false);

            _Virtual_Camera.GetComponent<Camera>().fieldOfView = 60f;
          
            initialPosition = new Vector3(0, 0, -2.2f);

            _CameraContainer.transform.localPosition = new Vector3(0f, -0.05f, -0.95f);
        }
        else
        {
            _Panel_LandScape.SetActive(false);
            _Panel_Portrait.SetActive(true);

            _Virtual_Camera.GetComponent<Camera>().fieldOfView = 27f;

            initialPosition = new Vector3(0.25f, 0, -2.2f);

            _CameraContainer.transform.localPosition = new Vector3(0f, 0.22f, -0.55f);

        }

        if (ResetPosition)
        {
            _Virtual_Camera.transform.localPosition = Vector3.MoveTowards(_Virtual_Camera.transform.localPosition, initialPosition, 5 * Time.deltaTime);

            if (_Virtual_Camera.transform.localPosition == initialPosition)
            {
                ResetPosition = false;
            }
        }

        if (ARView)
        {
            defaultScale = new Vector3(4f, 4f, 4f);
        }
        else
        {
            defaultScale = new Vector3(1f, 1f, 1f);
        }

        if(toggleSwitchEnabled)
        {
            Ray ray = _Virtual_Camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                if (hit.transform.gameObject.name == "ToggleSwitch" && Input.GetMouseButtonDown(0))
                {
                    toggle = !toggle;

                    if(toggle == false)
                    {
                        VariableTemperature_ModelButton.transform.localPosition = new Vector3(VariableTemperature_ModelButton.transform.localPosition.x - 0.106f, VariableTemperature_ModelButton.transform.localPosition.y, VariableTemperature_ModelButton.transform.localPosition.z);
                        VariableTemperature_DairyProduct.SetActive(true);
                        VariableTemperature_Fruits.SetActive(false);

                        VariableTemperature_Sprite_1.SetActive(true);
                        VariableTemperature_Sprite_2.SetActive(false);
                    }
                    else
                    {
                        VariableTemperature_ModelButton.transform.localPosition = new Vector3(VariableTemperature_ModelButton.transform.localPosition.x + 0.106f, VariableTemperature_ModelButton.transform.localPosition.y, VariableTemperature_ModelButton.transform.localPosition.z);
                        VariableTemperature_Fruits.SetActive(true);
                        VariableTemperature_DairyProduct.SetActive(false);

                        VariableTemperature_Sprite_2.SetActive(true);
                        VariableTemperature_Sprite_1.SetActive(false);

                    }
                }
            }
        }

        PlayingFeature = onAITechnologyClickedBool || onVariableTemperatureClickedBool || on3DFlowClickedBool || onPortableIceClickedBool || onMicroBlockClickedBool;
    }

    IEnumerator StartTransition()
    {
        if(Screen.width > Screen.height || ARView)
        {
            yield return new WaitForSeconds(0.5f);

            animatorStartFridge.Play("StartFridge_Animation");

            yield return new WaitForSeconds(4f);

            /*
            _ProductName.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _ProductName.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            yield return new WaitForSeconds(1f);
            */

            _BottomPanel_LandScape.SetActive(true);
            InfoClicked = !InfoClicked;
            _InfoPanel_LandScape.SetActive(true);
            _InfoImage_LandScape.SetActive(true);
            _InfoImage_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _InfoImage_LandScape.GetComponent<Image>().color = newColor;
                yield return null;
            }

            yield return new WaitForSeconds(1f);

            // _InfoImage_LandScape.SetActive(false);
        }
        else
        {
            animatorStartFridge.Play("StartFridge_Animation");

            yield return new WaitForSeconds(4f);


            _BottomPanel_Portrait.SetActive(true);
            InfoClicked = !InfoClicked;
            _InfoPanel_Portrait.SetActive(true);
            _InfoImage_Portrait.SetActive(true);
            _InfoImage_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _InfoImage_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }

            yield return new WaitForSeconds(1f);

            // _InfoImage_Portrait.SetActive(false);
        }


    }


    public void ResetActions(bool DoorFunction = true)
    {
        // animatorStartFridge.Play("Still");
        // animatorFridgeDoor.Play("Still");
        _SceneObject.transform.localScale = defaultScale;

        /*
        if (DoorFunction == true)
        {
            doorOpen = false;

            doorOpen_Top = false;
            doorOpen_Bottom = false;
        }
        */
            
        _BackPanel_LandScape.SetActive(false);
        if (InfoClicked)
        {
            OnInfoButtonClicked();
        }
        _InfoImage_LandScape.SetActive(false);
        _MenuPanel_LandScape.SetActive(false);
        _BackPanel_Portrait.SetActive(false);
        _InfoImage_Portrait.SetActive(false);
        _MenuPanel_Portrait.SetActive(false);
        _ProductName_Portrait.SetActive(false);

        _AITechnology_White_Button_LandScape.SetActive(false);
        _AITechnology_Gold_Button_LandScape.SetActive(false);
        _variableTemperature_White_Button_LandScape.SetActive(false);
        _variableTemperature_Gold_Button_LandScape.SetActive(false);
        _3DAir_White_Button_LandScape.SetActive(false);
        _3DAir_Gold_Button_LandScape.SetActive(false);
        _PortableIce_White_Button_LandScape.SetActive(false);
        _PortableIce_Gold_Button_LandScape.SetActive(false);
        _MicroBlock_White_Button_LandScape.SetActive(false);
        _MicroBlock_Gold_Button_LandScape.SetActive(false);
        _AITechnology_White_Button_Portrait.SetActive(false);
        _AITechnology_Gold_Button_Portrait.SetActive(false);
        _variableTemperature_White_Button_Portrait.SetActive(false);
        _variableTemperature_Gold_Button_Portrait.SetActive(false);
        _3DAir_White_Button_Portrait.SetActive(false);
        _3DAir_Gold_Button_Portrait.SetActive(false);
        _PortableIce_White_Button_Portrait.SetActive(false);
        _PortableIce_Gold_Button_Portrait.SetActive(false);
        _MicroBlock_White_Button_Portrait.SetActive(false);
        _MicroBlock_Gold_Button_Portrait.SetActive(false);


        // _AICallout_1.SetActive(false);
        // _AICallout_2.SetActive(false);
        // _AICallout_3.SetActive(false);

        // _3DFlow_Sprite.SetActive(false);
        // _3DFlowCallout_1.SetActive(false);
        // _3DFlowCallout_2.SetActive(false);
        // _3DFlowCallout_3.SetActive(false);
        // _3DFlowCallout_4.SetActive(false);

        // PortableIce_1.SetActive(false);
        // PortableIce_2.SetActive(false);
        // PortableIce_3.SetActive(false);
        // PortableIce_Model.SetActive(false);

        InsideObject_1.SetActive(true);
        InsideObject_2.SetActive(true);
        InsideObject_3.SetActive(true);
        InsideObject_4.SetActive(true);
        InsideObject_5.SetActive(true);
        InsideObject_6.SetActive(true);
        InsideObject_7.SetActive(true);
        InsideObject_8.SetActive(true);
        InsideObject_9.SetActive(true);
        InsideObject_10.SetActive(true);
        InsideObject_11.SetActive(true);
        InsideObject_12.SetActive(true);
        InsideObject_13.SetActive(true);
        InsideObject_14.SetActive(true);
        InsideObject_15.SetActive(true);
        InsideObject_16.SetActive(true);
        InsideObject_17.SetActive(true);
        InsideObject_18.SetActive(true);
        InsideObject_19.SetActive(true);
        InsideObject_20.SetActive(true);
        InsideObject_21.SetActive(true);
        InsideObject_22.SetActive(true);
        InsideObject_23.SetActive(true);
        InsideDoorObject_Top.SetActive(true);
        InsideDoorObject_Bottom.SetActive(true);

    }

    IEnumerator BackTransition()
    {
        if (plusClicked)
        {
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                _Virtual_Camera.transform.localPosition = Vector3.MoveTowards(_Virtual_Camera.transform.localPosition, initialPosition, t);
                yield return null;

                if (_Virtual_Camera.transform.localPosition == initialPosition)
                    break;
            }
        }
        else
        {
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                _Virtual_Camera.transform.localPosition = Vector3.MoveTowards(_Virtual_Camera.transform.localPosition, new Vector3(0f, 0f, -2.2f), t);
                yield return null;

                if (_Virtual_Camera.transform.localPosition == new Vector3(0f, 0f, -2.2f))
                    break;
            }
        }
        
    }



    public IEnumerator FadeInOutTransition()
    {
        fadeImage.canvasRenderer.SetAlpha(1.0f);
        fadeImage.CrossFadeAlpha(0, 1.5f, false);
        yield return new WaitForSeconds(1.5f);

        _FadeInOut.SetActive(false);
    }

    [SerializeField]
    GameObject ARInstruction_Panel, ARInstructions_1, ARInstructions_2;

    IEnumerator ARInstructionTransition()
    {
        yield return new WaitForSeconds(1f);
        ARInstruction_Panel.SetActive(true);
        ARInstructions_1.SetActive(true);
        ARInstructions_1.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
            ARInstructions_1.GetComponent<Image>().color = newColor;
            yield return null;
        }

        yield return new WaitForSeconds(3f);
        ARInstructions_1.SetActive(false);
        ARInstructions_2.SetActive(true);
        ARInstructions_2.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
            ARInstructions_2.GetComponent<Image>().color = newColor;
            yield return null;
        }

        yield return new WaitForSeconds(3f);
        ARInstructions_2.SetActive(false);
        ARInstruction_Panel.SetActive(false);
    }


    public void OnARButtonClicked()
    {
        // ResetActions();
        ObjectRotateScript.resetRotation = true;

        ARView = !ARView;
        VirtualView = !VirtualView;

        _Virtual_Camera.SetActive(true);
        _Zappar_Camera.SetActive(true);

        _FadeInOut.SetActive(true);
        StartCoroutine(FadeInOutTransition());

        // Camera Switch
        if (VirtualView == true)
        {
            _Virtual_Camera.SetActive(VirtualView);
            _Zappar_Camera.SetActive(ARView);
            _InstantTracker.SetActive(ARView);
            BackButtonClicked();
            _SceneObject.transform.localScale = new Vector3(1f, 1f, 1f);

            VariableTemperatureModel.GetComponent<BoxCollider>().size = new Vector3(0.14f, 0.19f, 0.08f);

            ARInstruction_Panel.SetActive(false);
            ARInstructions_1.SetActive(false);
            ARInstructions_2.SetActive(false);
            StopCoroutine(ARInstructionTransition());

            if (GoogleAnalyticsAdapter.Instance != null)
            {
                GoogleAnalyticsAdapter.Instance.googleAnalyticsLogEvent("ARClose_Button", "Clicked");
            }
        }
        else
        {
            _Zappar_Camera.SetActive(ARView);
            _InstantTracker.SetActive(ARView);
            _Virtual_Camera.SetActive(VirtualView);
            _SceneObject.transform.localScale = new Vector3(4f, 4f, 4f);

            ARInstruction_Panel.SetActive(false);
            ARInstructions_1.SetActive(false);
            ARInstructions_2.SetActive(false);

            VariableTemperatureModel.GetComponent<BoxCollider>().size = new Vector3(0.56f, 0.76f, 0.4f);

            StartCoroutine(ARInstructionTransition());

            if (GoogleAnalyticsAdapter.Instance != null)
            {
                GoogleAnalyticsAdapter.Instance.googleAnalyticsLogEvent("AR_Button", "Clicked");
            }
        }

        _AR_Button_Icon_LandScape.SetActive(VirtualView);
        _Virtual_Button_Icon_LandScape.SetActive(ARView);
        _AR_Button_Icon_Portrait.SetActive(VirtualView);
        _Virtual_Button_Icon_Portrait.SetActive(ARView);


        //remove later
        _BottomPanel_LandScape.SetActive(true);
        _BottomPanel_Portrait.SetActive(true);
        //remove later

    }


    IEnumerator InfoButtonTransition()
    {
        if(Screen.width > Screen.height || ARView)
        {
            if (InfoClicked)
            {
                _InfoImage_LandScape.SetActive(true);
                _InfoImage_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
                {
                    Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                    _InfoImage_LandScape.GetComponent<Image>().color = newColor;
                    yield return null;
                }
            }
            else
            {
                _InfoImage_LandScape.SetActive(false);
            }
            yield return null;
        }
        else
        {
            if (InfoClicked)
            {
                _InfoImage_Portrait.SetActive(true);
                _InfoImage_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
                {
                    Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                    _InfoImage_Portrait.GetComponent<Image>().color = newColor;
                    yield return null;
                }
            }
            else
            {
                _InfoImage_Portrait.SetActive(false);
            }
            yield return null;
        }
        
    }

    public void OnInfoButtonClicked()
    {
        InfoClicked = !InfoClicked;
        StartCoroutine(InfoButtonTransition());

        if (GoogleAnalyticsAdapter.Instance != null)
        {
            GoogleAnalyticsAdapter.Instance.googleAnalyticsLogEvent("Info_Button", "Clicked");
        }
    }




    [SerializeField]
    GameObject _MenuPanel_LandScape, _AITechnology_White_Button_LandScape, _AITechnology_Gold_Button_LandScape, _variableTemperature_White_Button_LandScape, _variableTemperature_Gold_Button_LandScape,
                                     _3DAir_White_Button_LandScape, _3DAir_Gold_Button_LandScape, _PortableIce_White_Button_LandScape, _PortableIce_Gold_Button_LandScape,
                                     _MicroBlock_White_Button_LandScape, _MicroBlock_Gold_Button_LandScape;
    [SerializeField]
    GameObject _MenuPanel_Portrait, _AITechnology_White_Button_Portrait, _AITechnology_Gold_Button_Portrait, _variableTemperature_White_Button_Portrait, _variableTemperature_Gold_Button_Portrait,
                                     _3DAir_White_Button_Portrait, _3DAir_Gold_Button_Portrait, _PortableIce_White_Button_Portrait, _PortableIce_Gold_Button_Portrait,
                                     _MicroBlock_White_Button_Portrait, _MicroBlock_Gold_Button_Portrait;

    public bool plusClicked = false;

    IEnumerator PlusButtonTransition(bool doorClickResetBool)
    {
        if(delayStart_PlusButton)
        {
            yield return new WaitForSeconds(1f);
            delayStart_PlusButton = false;
        }

        if (plusClicked)
        {
            if (Screen.width < Screen.height )
            {
                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
                {
                    _Virtual_Camera.transform.localPosition = Vector3.MoveTowards(_Virtual_Camera.transform.localPosition, new Vector3(0.25f, 0f, -2.2f), t);
                    yield return null;

                    if (_Virtual_Camera.transform.localPosition == new Vector3(0.25f, 0f, -2.2f))
                        break;
                }

                _MenuPanel_Portrait.SetActive(true);

                /*
                _ProductName_Portrait.SetActive(true);
                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
                {
                    Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                    _ProductName_Portrait.GetComponent<Image>().color = newColor;
                    yield return null;
                }
                */

                _AITechnology_Gold_Button_Portrait.SetActive(true);
                // yield return new WaitForSeconds(0.3f);
                _variableTemperature_Gold_Button_Portrait.SetActive(true);
                // yield return new WaitForSeconds(0.3f);
                _3DAir_Gold_Button_Portrait.SetActive(true);
                // yield return new WaitForSeconds(0.3f);
                _PortableIce_Gold_Button_Portrait.SetActive(true);
                // yield return new WaitForSeconds(0.3f);
                _MicroBlock_Gold_Button_Portrait.SetActive(true);
            }
            else
            {
                _MenuPanel_LandScape.SetActive(true);

                _AITechnology_Gold_Button_LandScape.SetActive(true);
                // yield return new WaitForSeconds(0.3f);
                _variableTemperature_Gold_Button_LandScape.SetActive(true);
                // yield return new WaitForSeconds(0.3f);
                _3DAir_Gold_Button_LandScape.SetActive(true);
                // yield return new WaitForSeconds(0.3f);
                _PortableIce_Gold_Button_LandScape.SetActive(true);
                // yield return new WaitForSeconds(0.3f);
                _MicroBlock_Gold_Button_LandScape.SetActive(true);
            }
       

        }
        else
        {
            _MenuPanel_LandScape.SetActive(false);

            _AITechnology_White_Button_LandScape.SetActive(false);
            _AITechnology_Gold_Button_LandScape.SetActive(false);
            _variableTemperature_White_Button_LandScape.SetActive(false);
            _variableTemperature_Gold_Button_LandScape.SetActive(false);
            _3DAir_White_Button_LandScape.SetActive(false);
            _3DAir_Gold_Button_LandScape.SetActive(false);
            _PortableIce_White_Button_LandScape.SetActive(false);
            _PortableIce_Gold_Button_LandScape.SetActive(false);
            _MicroBlock_White_Button_LandScape.SetActive(false);
            _MicroBlock_Gold_Button_LandScape.SetActive(false);

            _MenuPanel_Portrait.SetActive(false);

            _AITechnology_White_Button_Portrait.SetActive(false);
            _AITechnology_Gold_Button_Portrait.SetActive(false);
            _variableTemperature_White_Button_Portrait.SetActive(false);
            _variableTemperature_Gold_Button_Portrait.SetActive(false);
            _3DAir_White_Button_Portrait.SetActive(false);
            _3DAir_Gold_Button_Portrait.SetActive(false);
            _PortableIce_White_Button_Portrait.SetActive(false);
            _PortableIce_Gold_Button_Portrait.SetActive(false);
            _MicroBlock_White_Button_Portrait.SetActive(false);
            _MicroBlock_Gold_Button_Portrait.SetActive(false);

            if (!doorClickResetBool)
            {
                if (Screen.width < Screen.height)
                {
                    for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
                    {
                        _Virtual_Camera.transform.localPosition = Vector3.MoveTowards(_Virtual_Camera.transform.localPosition, new Vector3(0f, 0f, -2.2f), t);
                        yield return null;

                        if (_Virtual_Camera.transform.localPosition == new Vector3(0f, 0f, -2.2f))
                            break;
                    }
                }
            }
            
        }
    }

    public bool delayStart_PlusButton = false;

    public void OnPlusButtonClicked(bool doorClickReset = false)
    {
        ResetActions();
        plusClicked = !plusClicked;
        ObjectRotateScript.resetRotation = true;

        delayStart_PlusButton = false;
        if (dimensionClick && plusClicked)
        {
            CMButtonClicked();
            ObjectRotateScript.OnDimensionClicked();
        }
        if (doorOpen && plusClicked)
        {
            OnDoorOpenCloseClicked();
            delayStart_PlusButton = true;
        }
        if (doorOpen_Top && plusClicked)
        {
            OnDoorOpenCloseClicked_Top();
            delayStart_PlusButton = true;
        }
        if (doorOpen_Bottom && plusClicked)
        {
            OnDoorOpenCloseClicked_Bottom();
            delayStart_PlusButton = true;
        }

        if (plusClicked)
        {
            _Plus_Gold_LandScape.SetActive(true);
            _Plus_White_LandScape.SetActive(false);
            _CM_Gold_LandScape.SetActive(false);
            _CM_White_LandScape.SetActive(true);
            _Door_Gold_LandScape.SetActive(false);
            _Door_White_LandScape.SetActive(true);
            _Door_Bottom_Gold_LandScape.SetActive(false);
            _Door_Bottom_White_LandScape.SetActive(true);

            _Plus_Gold_Portrait.SetActive(true);
            _Plus_White_Portrait.SetActive(false);
            _CM_Gold_Portrait.SetActive(false);
            _CM_White_Portrait.SetActive(true);
            _Door_Gold_Portrait.SetActive(false);
            _Door_White_Portrait.SetActive(true);
            _Door_Bottom_Gold_Portrait.SetActive(false);
            _Door_Bottom_White_Portrait.SetActive(true);
        }
        else
        {
            _Plus_Gold_LandScape.SetActive(false);
            _Plus_White_LandScape.SetActive(true);
            _CM_Gold_LandScape.SetActive(false);
            _CM_White_LandScape.SetActive(true);
            _Door_Gold_LandScape.SetActive(false);
            _Door_White_LandScape.SetActive(true);
            _Door_Bottom_Gold_LandScape.SetActive(false);
            _Door_Bottom_White_LandScape.SetActive(true);

            _Plus_Gold_Portrait.SetActive(false);
            _Plus_White_Portrait.SetActive(true);
            _CM_Gold_Portrait.SetActive(false);
            _CM_White_Portrait.SetActive(true);
            _Door_Gold_Portrait.SetActive(false);
            _Door_White_Portrait.SetActive(true);
            _Door_Bottom_Gold_Portrait.SetActive(false);
            _Door_Bottom_White_Portrait.SetActive(true);
        }
        StartCoroutine(PlusButtonTransition(doorClickReset));

        if (InfoClicked)
        {
            OnInfoButtonClicked();
        }


        if (GoogleAnalyticsAdapter.Instance != null)
        {
            GoogleAnalyticsAdapter.Instance.googleAnalyticsLogEvent("FeatureShowCase_Button", "Clicked");
        }
    }


    [SerializeField]
    GameObject Arrow_L, Arrow_B, Arrow_H;

    public bool delayStart_CMButton = false;

    bool dimensionClick = false;

    public void CMButtonClicked()
    {
        ResetActions();
        dimensionClick = !dimensionClick;
        ObjectRotateScript.resetRotation = true;

        delayStart_CMButton = false;
        if (plusClicked && dimensionClick)
        {
            OnPlusButtonClicked();
        }
        if (doorOpen && dimensionClick)
        {
            OnDoorOpenCloseClicked();
            delayStart_CMButton = true;
        }
        if (doorOpen_Top && dimensionClick)
        {
            OnDoorOpenCloseClicked_Top();
            delayStart_CMButton = true;
        }
        if (doorOpen_Bottom && dimensionClick)
        {
            OnDoorOpenCloseClicked_Bottom();
            delayStart_CMButton = true;
        }

        if (dimensionClick)
        {
            _Virtual_Camera.transform.localPosition = new Vector3(0f, 0f, -2.2f);

            _Plus_Gold_LandScape.SetActive(false);
            _Plus_White_LandScape.SetActive(true);
            _CM_Gold_LandScape.SetActive(true);
            _CM_White_LandScape.SetActive(false);
            _Door_Gold_LandScape.SetActive(false);
            _Door_White_LandScape.SetActive(true);
            _Door_Bottom_Gold_LandScape.SetActive(false);
            _Door_Bottom_White_LandScape.SetActive(true);

            _Plus_Gold_Portrait.SetActive(false);
            _Plus_White_Portrait.SetActive(true);
            _CM_Gold_Portrait.SetActive(true);
            _CM_White_Portrait.SetActive(false);
            _Door_Gold_Portrait.SetActive(false);
            _Door_White_Portrait.SetActive(true);
            _Door_Bottom_Gold_Portrait.SetActive(false);
            _Door_Bottom_White_Portrait.SetActive(true);
        }
        else
        {
            _Plus_Gold_LandScape.SetActive(false);
            _Plus_White_LandScape.SetActive(true);
            _CM_Gold_LandScape.SetActive(false);
            _CM_White_LandScape.SetActive(true);
            _Door_Gold_LandScape.SetActive(false);
            _Door_White_LandScape.SetActive(true);
            _Door_Bottom_Gold_LandScape.SetActive(false);
            _Door_Bottom_White_LandScape.SetActive(true);

            _Plus_Gold_Portrait.SetActive(false);
            _Plus_White_Portrait.SetActive(true);
            _CM_Gold_Portrait.SetActive(false);
            _CM_White_Portrait.SetActive(true);
            _Door_Gold_Portrait.SetActive(false);
            _Door_White_Portrait.SetActive(true);
            _Door_Bottom_Gold_Portrait.SetActive(false);
            _Door_Bottom_White_Portrait.SetActive(true);
        }

        

        if (dimensionClick == true)
        {
            ResetActions();

            // Arrow_B.SetActive(true);
            // Arrow_H.SetActive(true);
            // Arrow_L.SetActive(true);
        }
        else
        {
            // Arrow_B.SetActive(false);
            // Arrow_H.SetActive(false);
            // Arrow_L.SetActive(false);
        }

        if(GoogleAnalyticsAdapter.Instance != null)
        {
            GoogleAnalyticsAdapter.Instance.googleAnalyticsLogEvent("Dimension_Button", "Clicked");
        }
    }



    IEnumerator OpenCloseDoorButtonClickedTransition(int open)
    {
        if(open == 1)
        {
            if (Screen.width < Screen.height)
            {
                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 10)
                {
                    _Virtual_Camera.transform.localPosition = Vector3.MoveTowards(_Virtual_Camera.transform.localPosition, new Vector3(0.28f, 0.0f, -2.2f), t);
                    yield return null;

                    if (_Virtual_Camera.transform.localPosition == new Vector3(0.28f, 0.0f, -2.2f))
                        break;
                }

                // _Virtual_Camera.GetComponent<Camera>().fieldOfView = 85f;
            }
            else
            {
                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime/100)
                {
                    _Virtual_Camera.transform.localPosition = Vector3.MoveTowards(_Virtual_Camera.transform.localPosition, new Vector3(0.28f, 0.0f, -2.2f), t);
                    yield return null;

                    if (_Virtual_Camera.transform.localPosition == new Vector3(0.28f, 0.0f, -2.2f))
                        break;
                }
            }
        }
        else if(open == 0)
        {
            if (Screen.width < Screen.height)
            {
                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
                {
                    _Virtual_Camera.transform.localPosition = Vector3.MoveTowards(_Virtual_Camera.transform.localPosition, new Vector3(0.0f, 0.0f, -2.2f), t);
                    yield return null;

                    if (_Virtual_Camera.transform.localPosition == new Vector3(0.0f, 0.0f, -2.2f))
                        break;
                }

                // _Virtual_Camera.GetComponent<Camera>().fieldOfView = 79f;
            }
            else
            {
                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime/100)
                {
                    _Virtual_Camera.transform.localPosition = Vector3.MoveTowards(_Virtual_Camera.transform.localPosition, new Vector3(0.0f, 0.0f, -2.2f), t);
                    yield return null;

                    if (_Virtual_Camera.transform.localPosition == new Vector3(0.0f, 0.0f, -2.2f))
                        break;
                }
            }
        }
    }

    public bool doorOpen = false;

    public void OnDoorOpenCloseClicked(bool check = false)
    {
        // ResetActions(false);

        doorOpen = !doorOpen;
        ObjectRotateScript.resetRotation = true;


        if (plusClicked && doorOpen && !check)
        {
            OnPlusButtonClicked(true);
            doorOpen = true;
        }
        if (dimensionClick && doorOpen)
        {
            CMButtonClicked();
            ObjectRotateScript.OnDimensionClicked();
            doorOpen = true;
        }

        if (doorOpen)
        {
            animatorStartFridge.Play("DoorOpen_Animation");
            animatorFridgeDoor.Play("DoorOpen_Lower_Animation");

            // Only change the bottom icon color when other bottom icon is clicked 
            if (!check)
            {
                _Plus_Gold_LandScape.SetActive(false);
                _Plus_White_LandScape.SetActive(true);
                _Door_Gold_LandScape.SetActive(true);
                _Door_White_LandScape.SetActive(false);
                _Door_Bottom_Gold_LandScape.SetActive(true);
                _Door_Bottom_White_LandScape.SetActive(false);
                _CM_Gold_LandScape.SetActive(false);
                _CM_White_LandScape.SetActive(true);

                _Plus_Gold_Portrait.SetActive(false);
                _Plus_White_Portrait.SetActive(true);
                _Door_Gold_Portrait.SetActive(true);
                _Door_White_Portrait.SetActive(false);
                _Door_Bottom_Gold_Portrait.SetActive(true);
                _Door_Bottom_White_Portrait.SetActive(false);
                _CM_Gold_Portrait.SetActive(false);
                _CM_White_Portrait.SetActive(true);

                StartCoroutine(OpenCloseDoorButtonClickedTransition(1));
                           
            }
        }
        else
        {
            animatorStartFridge.Play("DoorClose_Animation");
            animatorFridgeDoor.Play("DoorClose_Lower_Animation");

            if (!check)
            {
                _Plus_Gold_LandScape.SetActive(false);
                _Plus_White_LandScape.SetActive(true);
                _Door_Gold_LandScape.SetActive(false);
                _Door_White_LandScape.SetActive(true);
                _Door_Bottom_Gold_LandScape.SetActive(false);
                _Door_Bottom_White_LandScape.SetActive(true);
                _CM_Gold_LandScape.SetActive(false);
                _CM_White_LandScape.SetActive(true);

                _Plus_Gold_Portrait.SetActive(false);
                _Plus_White_Portrait.SetActive(true);
                _Door_Gold_Portrait.SetActive(false);
                _Door_White_Portrait.SetActive(true);
                _Door_Bottom_Gold_Portrait.SetActive(false);
                _Door_Bottom_White_Portrait.SetActive(true);
                _CM_Gold_Portrait.SetActive(false);
                _CM_White_Portrait.SetActive(true);

                StartCoroutine(OpenCloseDoorButtonClickedTransition(0));

            }
        }

        if (InfoClicked)
        {
            OnInfoButtonClicked();
        }
    }







    public bool doorOpen_Top = false;

    public void OnDoorOpenCloseClicked_Top(bool check = false)
    {
        // ResetActions(false);

        doorOpen_Top = !doorOpen_Top;
        ObjectRotateScript.resetRotation = true;


        if (plusClicked && doorOpen_Top && !check)
        {
            OnPlusButtonClicked(true);
            doorOpen_Top = true;
        }
        if (dimensionClick && doorOpen_Top)
        {
            CMButtonClicked();
            ObjectRotateScript.OnDimensionClicked();
            doorOpen_Top = true;
        }

        if (doorOpen_Top)
        {
            animatorStartFridge.Play("DoorOpen_Animation");
            // animatorFridgeDoor.Play("DoorOpen_Lower_Animation");

            // Only change the bottom icon color when other bottom icon is clicked 
            if (!check)
            {
                _Plus_Gold_LandScape.SetActive(false);
                _Plus_White_LandScape.SetActive(true);
                _Door_Gold_LandScape.SetActive(true);
                _Door_White_LandScape.SetActive(false);
                // _Door_Bottom_Gold_LandScape.SetActive(false);
                // _Door_Bottom_White_LandScape.SetActive(true);
                _CM_Gold_LandScape.SetActive(false);
                _CM_White_LandScape.SetActive(true);

                _Plus_Gold_Portrait.SetActive(false);
                _Plus_White_Portrait.SetActive(true);
                _Door_Gold_Portrait.SetActive(true);
                _Door_White_Portrait.SetActive(false);
                // _Door_Bottom_Gold_Portrait.SetActive(false);
                // _Door_Bottom_White_Portrait.SetActive(true);
                _CM_Gold_Portrait.SetActive(false);
                _CM_White_Portrait.SetActive(true);

                StartCoroutine(OpenCloseDoorButtonClickedTransition(1));

            }
        }
        else
        {
            animatorStartFridge.Play("DoorClose_Animation");
            // animatorFridgeDoor.Play("DoorClose_Lower_Animation");

            if (!check)
            {
                _Plus_Gold_LandScape.SetActive(false);
                _Plus_White_LandScape.SetActive(true);
                _Door_Gold_LandScape.SetActive(false);
                _Door_White_LandScape.SetActive(true);
                // _Door_Bottom_Gold_LandScape.SetActive(false);
                // _Door_Bottom_White_LandScape.SetActive(true);
                _CM_Gold_LandScape.SetActive(false);
                _CM_White_LandScape.SetActive(true);

                _Plus_Gold_Portrait.SetActive(false);
                _Plus_White_Portrait.SetActive(true);
                _Door_Gold_Portrait.SetActive(false);
                _Door_White_Portrait.SetActive(true);
                // _Door_Bottom_Gold_Portrait.SetActive(false);
                // _Door_Bottom_White_Portrait.SetActive(true);
                _CM_Gold_Portrait.SetActive(false);
                _CM_White_Portrait.SetActive(true);

                
                if (!doorOpen_Bottom)
                {
                    StartCoroutine(OpenCloseDoorButtonClickedTransition(0));
                }

            }

        }
        if (InfoClicked)
        {
            OnInfoButtonClicked();
        }


        if (GoogleAnalyticsAdapter.Instance != null)
        {
            GoogleAnalyticsAdapter.Instance.googleAnalyticsLogEvent("TopDoor_Button", "Clicked");
        }
    }





    public bool doorOpen_Bottom = false;

    public void OnDoorOpenCloseClicked_Bottom(bool check = false)
    {
        // ResetActions(false);

        doorOpen_Bottom = !doorOpen_Bottom;
        ObjectRotateScript.resetRotation = true;


        if (plusClicked && doorOpen_Bottom && !check)
        {
            OnPlusButtonClicked(true);
            doorOpen_Bottom = true;
        }
        if (dimensionClick && doorOpen_Bottom)
        {
            CMButtonClicked();
            ObjectRotateScript.OnDimensionClicked();
            doorOpen_Bottom = true;
        }

        if (doorOpen_Bottom)
        {
            // animatorStartFridge.Play("DoorOpen_Animation");
            animatorFridgeDoor.Play("DoorOpen_Lower_Animation");

            // Only change the bottom icon color when other bottom icon is clicked 
            if (!check)
            {
                _Plus_Gold_LandScape.SetActive(false);
                _Plus_White_LandScape.SetActive(true);
                // _Door_Gold_LandScape.SetActive(false);
                // _Door_White_LandScape.SetActive(true);
                _Door_Bottom_Gold_LandScape.SetActive(true);
                _Door_Bottom_White_LandScape.SetActive(false);
                _CM_Gold_LandScape.SetActive(false);
                _CM_White_LandScape.SetActive(true);

                _Plus_Gold_Portrait.SetActive(false);
                _Plus_White_Portrait.SetActive(true);
                // _Door_Gold_Portrait.SetActive(false);
                // _Door_White_Portrait.SetActive(true);
                _Door_Bottom_Gold_Portrait.SetActive(true);
                _Door_Bottom_White_Portrait.SetActive(false);
                _CM_Gold_Portrait.SetActive(false);
                _CM_White_Portrait.SetActive(true);

                StartCoroutine(OpenCloseDoorButtonClickedTransition(1));

            }
        }
        else
        {
            // animatorStartFridge.Play("DoorClose_Animation");
            animatorFridgeDoor.Play("DoorClose_Lower_Animation");

            if (!check)
            {
                _Plus_Gold_LandScape.SetActive(false);
                _Plus_White_LandScape.SetActive(true);
                // _Door_Gold_LandScape.SetActive(false);
                // _Door_White_LandScape.SetActive(true);
                _Door_Bottom_Gold_LandScape.SetActive(false);
                _Door_Bottom_White_LandScape.SetActive(true);
                _CM_Gold_LandScape.SetActive(false);
                _CM_White_LandScape.SetActive(true);

                _Plus_Gold_Portrait.SetActive(false);
                _Plus_White_Portrait.SetActive(true);
                // _Door_Gold_Portrait.SetActive(false);
                // _Door_White_Portrait.SetActive(true);
                _Door_Bottom_Gold_Portrait.SetActive(false);
                _Door_Bottom_White_Portrait.SetActive(true);
                _CM_Gold_Portrait.SetActive(false);
                _CM_White_Portrait.SetActive(true);

                
                if (!doorOpen_Top)
                {
                    StartCoroutine(OpenCloseDoorButtonClickedTransition(0));
                }

            }

        }

        if (InfoClicked)
        {
            OnInfoButtonClicked();
        }

        if (GoogleAnalyticsAdapter.Instance != null)
        {
            GoogleAnalyticsAdapter.Instance.googleAnalyticsLogEvent("BottomDoor_Button", "Clicked");
        }
    }







































    public void BackButtonClicked()
    {
        if (ARView)
        {

        }
        else
        {
            _SceneObject.transform.localScale = defaultScale;
        }

        if (Screen.width > Screen.height)
        {
            _BottomPanel_LandScape.SetActive(true);
            _MenuPanel_LandScape.SetActive(true);
            // _ProductName.SetActive(true);
            _InfoPanel_LandScape.SetActive(true);

            _BackPanel_LandScape.SetActive(false);
        }
        else
        {
            _BottomPanel_Portrait.SetActive(true);
            _MenuPanel_Portrait.SetActive(true);
            _InfoPanel_Portrait.SetActive(true);

            _BackPanel_Portrait.SetActive(false);
        }
        onAITechnologyClickedBool = false;
        onVariableTemperatureClickedBool = false;
        on3DFlowClickedBool = false;
        onPortableIceClickedBool = false;
        onMicroBlockClickedBool = false;


        StopAllCoroutines();
        // ResetPosition = true;
        StartCoroutine(BackTransition());

        _AICallout_1.SetActive(false);
        _AICallout_2.SetActive(false);
        _AICallout_3.SetActive(false);

        
        AIFeaturePanel.SetActive(false);
        AIUsageSensor_Silver_Button.SetActive(false);
        AIUsageSensor_Gold_Button.SetActive(false);
        AIWeatherSensor_Silver_Button.SetActive(false);
        AIWeatherSensor_Gold_Button.SetActive(false);
        AILoadSensor_Silver_Button.SetActive(false);
        AILoadSensor_Gold_Button.SetActive(false);
        AIFeaturePanel_Portrait.SetActive(false);
        AIUsageSensor_Silver_Button_Portrait.SetActive(false);
        AIUsageSensor_Gold_Button_Portrait.SetActive(false);
        AIWeatherSensor_Silver_Button_Portrait.SetActive(false);
        AIWeatherSensor_Gold_Button_Portrait.SetActive(false);
        AILoadSensor_Silver_Button_Portrait.SetActive(false);
        AILoadSensor_Gold_Button_Portrait.SetActive(false);

        AIUsageSensor_Callout_1.SetActive(false);
        AIWeatherSensor_Callout_1.SetActive(false);
        AILoadSensor_Callout_1.SetActive(false);
        AIWeatherSensor_Icon_1.SetActive(false);
        AIWeatherSensor_Icon_2.SetActive(false);



        toggleSwitchEnabled = false;
        VariableTemperatureCallout_1.SetActive(false);
        VariableTemperatureCallout_2.SetActive(false);
        VariableTemperatureCallout_3.SetActive(false);
        VariableTemperatureCallout_4.SetActive(false);
        VariableTemperatureModel.SetActive(false);
        // VariableTemperature_ModelButton.SetActive(false);
        VariableTemperature_DairyProduct.SetActive(false);
        VariableTemperature_Fruits.SetActive(false);
        VariableTemperature_Sprite_1.SetActive(false);
        VariableTemperature_Sprite_2.SetActive(false);
        VariableTemperature_Tray.transform.localPosition = new Vector3(0f, 0f, 0f);
        toggle = true;
        VariableTemperature_ModelButton.transform.localPosition = new Vector3(0.05f, 1.076237f, -0.343f);



        _3DFlow_Sprite.SetActive(false);
        _3DFlowCallout_1.SetActive(false);
        _3DFlowCallout_2.SetActive(false);
        _3DFlowCallout_3.SetActive(false);
        _3DFlowCallout_4.SetActive(false);
        _3DFlow_BowlModel.SetActive(false);

        if (doorOpen)
        {
            OnDoorOpenCloseClicked(true);
        }
        if (doorOpen_Top)
        {
            OnDoorOpenCloseClicked_Top(true);
        }
        if (doorOpen_Bottom)
        {
            OnDoorOpenCloseClicked_Bottom(true);
        }

        PortableIce_1.SetActive(false);
        PortableIce_2.SetActive(false);
        PortableIce_3.SetActive(false);
        PortableIce_4.SetActive(false);

        // PortableIce_Model.SetActive(false);
        PortableIce_Model.transform.localPosition = new Vector3(0, 0, 0);
        InsideObject_4.transform.localPosition = new Vector3(-0.1516571f, -0.5090525f, 0.09480139f);
        InsideObject_5.transform.localPosition = new Vector3(-0.04314014f, -0.506731f, -0.01076187f);

        MicroBlock_1.SetActive(false);
        MicroBlock_2.SetActive(false);
        MicroBlock_3.SetActive(false);
        MicroBlock_4.SetActive(false);
        MicroBlock_Disclaimer.SetActive(false);
        MicroBlock_Sprite.SetActive(false);
        // MicroBlock_Model.SetActive(false);
        MicroBlock_Model.transform.localPosition = new Vector3(0, 0.7986788f, -0.144f);
        _TrayObject_1.SetActive(false);
        _TrayObject_2.SetActive(false);
        _TrayObject_3.SetActive(false);
        _TrayObject_4.SetActive(false);
        _TrayObject_5.SetActive(false);
        _TrayObject_6.SetActive(false);
        _TrayObject_7.SetActive(false);
        _TrayObject_8.SetActive(false);

        ObjectRotateScript.resetRotation = true;

        ObjectRotateScript.AllowRotation = 1;
        SceneZoomScript.AllowScaling = 1;


        InsideObject_1.SetActive(true);
        InsideObject_2.SetActive(true);
        InsideObject_3.SetActive(true);
        InsideObject_4.SetActive(true);
        InsideObject_5.SetActive(true);
        InsideObject_6.SetActive(true);
        InsideObject_7.SetActive(true);
        InsideObject_8.SetActive(true);
        InsideObject_9.SetActive(true);
        InsideObject_10.SetActive(true);
        InsideObject_11.SetActive(true);
        InsideObject_12.SetActive(true);
        InsideObject_13.SetActive(true);
        InsideObject_14.SetActive(true);
        InsideObject_15.SetActive(true);
        InsideObject_16.SetActive(true);
        InsideObject_17.SetActive(true);
        InsideObject_18.SetActive(true);
        InsideObject_19.SetActive(true);
        InsideObject_20.SetActive(true);
        InsideObject_21.SetActive(true);
        InsideObject_22.SetActive(true);
        InsideObject_23.SetActive(true);
        InsideDoorObject_Top.SetActive(true);
        InsideDoorObject_Bottom.SetActive(true);



        if (GoogleAnalyticsAdapter.Instance != null)
        {
            GoogleAnalyticsAdapter.Instance.googleAnalyticsLogEvent("Back_Button", "Clicked");
        }
    }

    [SerializeField]
    GameObject _AICallout_1, _AICallout_2, _AICallout_3;
    [SerializeField]
    GameObject AIFeaturePanel, AIUsageSensor_Silver_Button, AIUsageSensor_Gold_Button, AIWeatherSensor_Silver_Button, AIWeatherSensor_Gold_Button, AILoadSensor_Silver_Button, AILoadSensor_Gold_Button;
    [SerializeField]
    GameObject AIFeaturePanel_Portrait, AIUsageSensor_Silver_Button_Portrait, AIUsageSensor_Gold_Button_Portrait, AIWeatherSensor_Silver_Button_Portrait, AIWeatherSensor_Gold_Button_Portrait, AILoadSensor_Silver_Button_Portrait, AILoadSensor_Gold_Button_Portrait;
    [SerializeField]
    GameObject AIUsageSensor_Callout_1;
    [SerializeField]
    GameObject AIWeatherSensor_Callout_1, AIWeatherSensor_Icon_1, AIWeatherSensor_Icon_2;
    [SerializeField]
    GameObject AILoadSensor_Callout_1;

    Animator animatorAITechSequence;

    IEnumerator AITechnologyTransition()
    {
        ObjectRotateScript.AllowRotation = 0;
        SceneZoomScript.AllowScaling = 0;

        if (Screen.width > Screen.height)
        {            
            _AICallout_1.transform.localPosition = new Vector3(-0.981f, 0.729f, 1.370314f);
            _AICallout_1.transform.localScale = new Vector3(0.1937504f, 0.1937504f, 0.1937504f);
            _AICallout_2.transform.localPosition = new Vector3(-0.97f, 0.299f, 1.370314f);
            _AICallout_2.transform.localScale = new Vector3(0.1679856f, 0.1679856f, 0.1679856f);
            _AICallout_3.transform.localPosition = new Vector3(-0.9700001f, -0.242f, 1.370314f);
            _AICallout_3.transform.localScale = new Vector3(0.1890155f, 0.1890155f, 0.1890155f);

            AIUsageSensor_Callout_1.transform.localPosition = new Vector3(-0.9710001f, -0.234f, 1.370314f);
            AIUsageSensor_Callout_1.transform.localScale = new Vector3(0.2403796f, 0.2403796f, 0.2403796f);
            AIWeatherSensor_Callout_1.transform.localPosition = new Vector3(-0.9710001f, -0.251f, 1.370314f);
            AIWeatherSensor_Callout_1.transform.localScale = new Vector3(0.2403796f, 0.2403796f, 0.2403796f);
            AILoadSensor_Callout_1.transform.localPosition = new Vector3(-0.9710001f, -0.245f, 1.370314f);
            AILoadSensor_Callout_1.transform.localScale = new Vector3(0.2403796f, 0.2403796f, 0.2403796f);

            AIWeatherSensor_Icon_1.transform.localPosition = new Vector3(1.06f, 0.04f, 1.370314f);
            AIWeatherSensor_Icon_1.transform.localScale = new Vector3(0.173635f, 0.173635f, 0.173635f);
            AIWeatherSensor_Icon_2.transform.localPosition = new Vector3(1.06f, 0.04f, 1.370314f);
            AIWeatherSensor_Icon_2.transform.localScale = new Vector3(0.173635f, 0.173635f, 0.173635f);

            _AICallout_1.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _AICallout_1.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            _AICallout_2.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _AICallout_2.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            _AICallout_3.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _AICallout_3.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            AIFeaturePanel.SetActive(true);
            AIUsageSensor_Silver_Button.SetActive(false);
            AIUsageSensor_Gold_Button.SetActive(true);
            AIWeatherSensor_Silver_Button.SetActive(false);
            AIWeatherSensor_Gold_Button.SetActive(true);
            AILoadSensor_Silver_Button.SetActive(false);
            AILoadSensor_Gold_Button.SetActive(true);
        }
        else
        {
            _AICallout_1.transform.localPosition = new Vector3(-0.84f, 0.783f, 1.370314f);
            _AICallout_1.transform.localScale = new Vector3(0.2041859f, 0.2041859f, 0.2041859f);
            _AICallout_2.transform.localPosition = new Vector3(-0.84f, 0.366f, 1.370314f);
            _AICallout_2.transform.localScale = new Vector3(0.1672822f, 0.1672822f, 0.1672822f);
            _AICallout_3.transform.localPosition = new Vector3(-0.84f, -0.16f, 1.370314f);
            _AICallout_3.transform.localScale = new Vector3(0.2242467f, 0.2242467f, 0.2242467f);

            AIUsageSensor_Callout_1.transform.localPosition = new Vector3(-0.84f, -0.152f, 1.370314f);
            AIUsageSensor_Callout_1.transform.localScale = new Vector3(0.2999876f, 0.2999876f, 0.2999876f);
            AIWeatherSensor_Callout_1.transform.localPosition = new Vector3(-0.84f, -0.169f, 1.370314f);
            AIWeatherSensor_Callout_1.transform.localScale = new Vector3(0.2999876f, 0.2999876f, 0.2999876f);
            AILoadSensor_Callout_1.transform.localPosition = new Vector3(-0.84f, -0.163f, 1.370314f);
            AILoadSensor_Callout_1.transform.localScale = new Vector3(0.2999876f, 0.2999876f, 0.2999876f);

            AIWeatherSensor_Icon_1.transform.localPosition = new Vector3(-0.04f, 1.24f, 1.370314f);
            AIWeatherSensor_Icon_1.transform.localScale = new Vector3(0.09509016f, 0.09509016f, 0.09509016f);
            AIWeatherSensor_Icon_2.transform.localPosition = new Vector3(-0.04f, 1.24f, 1.370314f);
            AIWeatherSensor_Icon_2.transform.localScale = new Vector3(0.09509016f, 0.09509016f, 0.09509016f);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime/10)
            {
                _Virtual_Camera.transform.localPosition = Vector3.MoveTowards(_Virtual_Camera.transform.localPosition, new Vector3(-0.34f, 0f, -2.2f), t);
                yield return null;

                if (_Virtual_Camera.transform.localPosition == new Vector3(-0.34f, 0f, -2.2f))
                    break;
            }

            _AICallout_1.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _AICallout_1.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            _AICallout_2.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _AICallout_2.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            _AICallout_3.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _AICallout_3.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            AIFeaturePanel_Portrait.SetActive(true);
            AIUsageSensor_Silver_Button_Portrait.SetActive(false);
            AIUsageSensor_Gold_Button_Portrait.SetActive(true);
            AIWeatherSensor_Silver_Button_Portrait.SetActive(false);
            AIWeatherSensor_Gold_Button_Portrait.SetActive(true);
            AILoadSensor_Silver_Button_Portrait.SetActive(false);
            AILoadSensor_Gold_Button_Portrait.SetActive(true);

        }
        

        _BackPanel_LandScape.GetComponent<RectTransform>().localPosition = new Vector3(-80f, -170f, 0f);
        _BackPanel_LandScape.SetActive(true);
        // _BackPanel_Portrait.GetComponent<RectTransform>().localPosition = new Vector3(5f, -115f, 0);
        _BackPanel_Portrait.SetActive(true);

        yield return new WaitForSeconds(0f);

    }

    public bool onAITechnologyClickedBool = false;

    public void OnAITechnologyClicked()
    {
        if (ARView)
        {

        }
        else
        {
            _SceneObject.transform.localScale = defaultScale;
        }

        onAITechnologyClickedBool = !onAITechnologyClickedBool;
        // ResetActions();
        ObjectRotateScript.resetRotation = true;

        if (Screen.width > Screen.height)
        {
            _AITechnology_White_Button_LandScape.SetActive(true);
            _AITechnology_Gold_Button_LandScape.SetActive(false);
            _variableTemperature_White_Button_LandScape.SetActive(false);
            _variableTemperature_Gold_Button_LandScape.SetActive(true);
            _3DAir_White_Button_LandScape.SetActive(false);
            _3DAir_Gold_Button_LandScape.SetActive(true);
            _PortableIce_White_Button_LandScape.SetActive(false);
            _PortableIce_Gold_Button_LandScape.SetActive(true);
            _MicroBlock_White_Button_LandScape.SetActive(false);
            _MicroBlock_Gold_Button_LandScape.SetActive(true);

            _BottomPanel_LandScape.SetActive(false);
            _MenuPanel_LandScape.SetActive(false);
            _InfoPanel_LandScape.SetActive(false);
            _ProductName.SetActive(false);
        }
        else
        {
            _AITechnology_White_Button_Portrait.SetActive(true);
            _AITechnology_Gold_Button_Portrait.SetActive(false);
            _variableTemperature_White_Button_Portrait.SetActive(false);
            _variableTemperature_Gold_Button_Portrait.SetActive(true);
            _3DAir_White_Button_Portrait.SetActive(false);
            _3DAir_Gold_Button_Portrait.SetActive(true);
            _PortableIce_White_Button_Portrait.SetActive(false);
            _PortableIce_Gold_Button_Portrait.SetActive(true);
            _MicroBlock_White_Button_Portrait.SetActive(false);
            _MicroBlock_Gold_Button_Portrait.SetActive(true);

            _BottomPanel_Portrait.SetActive(false);
            _MenuPanel_Portrait.SetActive(false);
            _InfoPanel_Portrait.SetActive(false);
        }
        

        StartCoroutine(AITechnologyTransition());

        if (GoogleAnalyticsAdapter.Instance != null)
        {
            GoogleAnalyticsAdapter.Instance.googleAnalyticsLogEvent("AITechnology_Button", "Clicked");
        }
    }








    public void ResetAIAction()
    {
        _AICallout_3.SetActive(true);
        AIUsageSensor_Callout_1.SetActive(false);
        AIWeatherSensor_Callout_1.SetActive(false);
        AILoadSensor_Callout_1.SetActive(false);

        AIWeatherSensor_Icon_1.SetActive(false);
        AIWeatherSensor_Icon_2.SetActive(false);

        // animatorFridgeDoor.Play("Still");
        // animatorStartFridge.Play("Still");

        InsideObject_1.SetActive(true);
        InsideObject_2.SetActive(true);
        InsideObject_3.SetActive(true);
        InsideObject_4.SetActive(true);
        InsideObject_5.SetActive(true);
        InsideObject_6.SetActive(true);
        InsideObject_7.SetActive(true);
        InsideObject_8.SetActive(true);
        InsideObject_9.SetActive(true);
        InsideObject_10.SetActive(true);
        InsideObject_11.SetActive(true);
        InsideObject_12.SetActive(true);
        InsideObject_13.SetActive(true);
        InsideObject_14.SetActive(true);
        InsideObject_15.SetActive(true);
        InsideObject_16.SetActive(true);
        InsideObject_17.SetActive(true);
        InsideObject_18.SetActive(true);
        InsideObject_19.SetActive(true);
        InsideObject_20.SetActive(true);
        InsideObject_21.SetActive(true);
        InsideObject_22.SetActive(true);
        InsideObject_23.SetActive(true);
        InsideDoorObject_Top.SetActive(true);
        InsideDoorObject_Bottom.SetActive(true);
    }



    public bool usageSensorClicked = false;
    Coroutine UsageSensorCoroutine;

    IEnumerator UsageSensorButtonTransition()
    {
        InsideObject_1.SetActive(true);
        InsideObject_2.SetActive(true);
        InsideObject_3.SetActive(true);
        InsideObject_4.SetActive(true);
        InsideObject_5.SetActive(true);
        InsideObject_6.SetActive(true);
        InsideObject_7.SetActive(true);
        InsideObject_8.SetActive(true);
        InsideObject_9.SetActive(true);
        InsideObject_10.SetActive(true);
        InsideObject_11.SetActive(true);
        InsideObject_12.SetActive(true);
        InsideObject_13.SetActive(true);
        InsideObject_14.SetActive(true);
        InsideObject_15.SetActive(true);
        InsideObject_16.SetActive(true);
        InsideObject_17.SetActive(true);
        InsideObject_18.SetActive(true);
        InsideObject_19.SetActive(true);
        InsideObject_20.SetActive(true);
        InsideObject_21.SetActive(true);
        InsideObject_22.SetActive(true);
        InsideObject_23.SetActive(true);
        InsideDoorObject_Top.SetActive(true);
        InsideDoorObject_Bottom.SetActive(true);

        if (Screen.width > Screen.height)
        {
            _AICallout_3.SetActive(false);
            AIUsageSensor_Callout_1.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                AIUsageSensor_Callout_1.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            OnDoorOpenCloseClicked_Top(true);
            yield return new WaitForSeconds(1f);
            OnDoorOpenCloseClicked_Top(true);
            yield return new WaitForSeconds(1f);
            OnDoorOpenCloseClicked_Bottom(true);
            yield return new WaitForSeconds(1f);
            OnDoorOpenCloseClicked_Bottom(true);
            

            yield return new WaitForSeconds(1f);

            OnDoorOpenCloseClicked_Top(true);
            yield return new WaitForSeconds(1f);
            OnDoorOpenCloseClicked_Top(true);
            yield return new WaitForSeconds(1f);
            OnDoorOpenCloseClicked_Bottom(true);
            yield return new WaitForSeconds(1f);
            OnDoorOpenCloseClicked_Bottom(true);

            yield return new WaitForSeconds(1f);
        }
        else
        {
            _AICallout_3.SetActive(false);
            AIUsageSensor_Callout_1.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                AIUsageSensor_Callout_1.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            OnDoorOpenCloseClicked_Top(true);
            yield return new WaitForSeconds(1f);
            OnDoorOpenCloseClicked_Top(true);
            yield return new WaitForSeconds(1f);
            OnDoorOpenCloseClicked_Bottom(true);
            yield return new WaitForSeconds(1f);
            OnDoorOpenCloseClicked_Bottom(true);


            yield return new WaitForSeconds(1f);

            OnDoorOpenCloseClicked_Top(true);
            yield return new WaitForSeconds(1f);
            OnDoorOpenCloseClicked_Top(true);
            yield return new WaitForSeconds(1f);
            OnDoorOpenCloseClicked_Bottom(true);
            yield return new WaitForSeconds(1f);
            OnDoorOpenCloseClicked_Bottom(true);

            yield return new WaitForSeconds(1f);
        }
    }

    public void OnUsageSensorButtonClicked()
    {
        ResetAIAction();
        usageSensorClicked = !usageSensorClicked;

        if (weatherSensorClicked && usageSensorClicked)
        {
            OnWeatherSensorButtonClicked();
        }
        if (loadSensorClicked && usageSensorClicked)
        {
            OnLoadSensorButtonClicked();
        }


        if (usageSensorClicked)
        {
            AIUsageSensor_Silver_Button.SetActive(true);
            AIUsageSensor_Gold_Button.SetActive(false);
            AIWeatherSensor_Silver_Button.SetActive(false);
            AIWeatherSensor_Gold_Button.SetActive(true);
            AILoadSensor_Silver_Button.SetActive(false);
            AILoadSensor_Gold_Button.SetActive(true);

            AIUsageSensor_Silver_Button_Portrait.SetActive(true);
            AIUsageSensor_Gold_Button_Portrait.SetActive(false);
            AIWeatherSensor_Silver_Button_Portrait.SetActive(false);
            AIWeatherSensor_Gold_Button_Portrait.SetActive(true);
            AILoadSensor_Silver_Button_Portrait.SetActive(false);
            AILoadSensor_Gold_Button_Portrait.SetActive(true);

            UsageSensorCoroutine = StartCoroutine(UsageSensorButtonTransition());
        }
        else
        {
            AIUsageSensor_Silver_Button.SetActive(false);
            AIUsageSensor_Gold_Button.SetActive(true);
            AIWeatherSensor_Silver_Button.SetActive(false);
            AIWeatherSensor_Gold_Button.SetActive(true);
            AILoadSensor_Silver_Button.SetActive(false);
            AILoadSensor_Gold_Button.SetActive(true);

            AIUsageSensor_Silver_Button_Portrait.SetActive(false);
            AIUsageSensor_Gold_Button_Portrait.SetActive(true);
            AIWeatherSensor_Silver_Button_Portrait.SetActive(false);
            AIWeatherSensor_Gold_Button_Portrait.SetActive(true);
            AILoadSensor_Silver_Button_Portrait.SetActive(false);
            AILoadSensor_Gold_Button_Portrait.SetActive(true);

            StopCoroutine(UsageSensorCoroutine);
            
            if (doorOpen_Top)
            {
                OnDoorOpenCloseClicked_Top(true);
            }
            if (doorOpen_Bottom)
            {
                OnDoorOpenCloseClicked_Bottom(true);
            }

        }

        if (GoogleAnalyticsAdapter.Instance != null)
        {
            GoogleAnalyticsAdapter.Instance.googleAnalyticsLogEvent("UsageSensor_Button", "Clicked");
        }
    }



    public bool weatherSensorClicked = false;
    Coroutine WeatherSensorCoroutine;

    IEnumerator WeatherSensorButtonTransition()
    {
        if (Screen.width > Screen.height)
        {
            _AICallout_3.SetActive(false);
            AIWeatherSensor_Callout_1.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                AIWeatherSensor_Callout_1.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            for(int i = 0; i < 2; i++)
            {
              
                AIWeatherSensor_Icon_1.SetActive(true);
                AIWeatherSensor_Icon_1.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
                {
                    Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                    AIWeatherSensor_Icon_1.GetComponent<SpriteRenderer>().color = newColor;
                    yield return null;
                }

                yield return new WaitForSeconds(1f);

                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
                {
                    Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
                    AIWeatherSensor_Icon_1.GetComponent<SpriteRenderer>().color = newColor;
                    yield return null;
                }
                AIWeatherSensor_Icon_1.SetActive(false);

                AIWeatherSensor_Icon_2.SetActive(true);
                AIWeatherSensor_Icon_2.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
                {
                    Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                    AIWeatherSensor_Icon_2.GetComponent<SpriteRenderer>().color = newColor;
                    yield return null;
                }

                yield return new WaitForSeconds(1f);

                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
                {
                    Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
                    AIWeatherSensor_Icon_2.GetComponent<SpriteRenderer>().color = newColor;
                    yield return null;
                }

            }
            

        }
        else
        {
            _AICallout_3.SetActive(false);
            AIWeatherSensor_Callout_1.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                AIWeatherSensor_Callout_1.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            for (int i = 0; i < 2; i++)
            {

                AIWeatherSensor_Icon_1.SetActive(true);
                AIWeatherSensor_Icon_1.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
                {
                    Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                    AIWeatherSensor_Icon_1.GetComponent<SpriteRenderer>().color = newColor;
                    yield return null;
                }

                yield return new WaitForSeconds(1f);

                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
                {
                    Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
                    AIWeatherSensor_Icon_1.GetComponent<SpriteRenderer>().color = newColor;
                    yield return null;
                }
                AIWeatherSensor_Icon_1.SetActive(false);

                AIWeatherSensor_Icon_2.SetActive(true);
                AIWeatherSensor_Icon_2.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
                {
                    Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                    AIWeatherSensor_Icon_2.GetComponent<SpriteRenderer>().color = newColor;
                    yield return null;
                }

                yield return new WaitForSeconds(1f);

                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
                {
                    Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
                    AIWeatherSensor_Icon_2.GetComponent<SpriteRenderer>().color = newColor;
                    yield return null;
                }

            }
        }

        yield return new WaitForSeconds(1f);
    }

    public void OnWeatherSensorButtonClicked()
    {
        ResetAIAction();
        weatherSensorClicked = !weatherSensorClicked;

        if (usageSensorClicked && weatherSensorClicked)
        {
            OnUsageSensorButtonClicked();
        }
        if (loadSensorClicked && weatherSensorClicked)
        {
            OnLoadSensorButtonClicked();
        }



        if (weatherSensorClicked)
        {
            AIUsageSensor_Silver_Button.SetActive(false);
            AIUsageSensor_Gold_Button.SetActive(true);
            AIWeatherSensor_Silver_Button.SetActive(true);
            AIWeatherSensor_Gold_Button.SetActive(false);
            AILoadSensor_Silver_Button.SetActive(false);
            AILoadSensor_Gold_Button.SetActive(true);


            AIUsageSensor_Silver_Button_Portrait.SetActive(false);
            AIUsageSensor_Gold_Button_Portrait.SetActive(true);
            AIWeatherSensor_Silver_Button_Portrait.SetActive(true);
            AIWeatherSensor_Gold_Button_Portrait.SetActive(false);
            AILoadSensor_Silver_Button_Portrait.SetActive(false);
            AILoadSensor_Gold_Button_Portrait.SetActive(true);

            WeatherSensorCoroutine = StartCoroutine(WeatherSensorButtonTransition());
        }
        else
        {
            AIUsageSensor_Silver_Button.SetActive(false);
            AIUsageSensor_Gold_Button.SetActive(true);
            AIWeatherSensor_Silver_Button.SetActive(false);
            AIWeatherSensor_Gold_Button.SetActive(true);
            AILoadSensor_Silver_Button.SetActive(false);
            AILoadSensor_Gold_Button.SetActive(true);

            AIUsageSensor_Silver_Button_Portrait.SetActive(false);
            AIUsageSensor_Gold_Button_Portrait.SetActive(true);
            AIWeatherSensor_Silver_Button_Portrait.SetActive(false);
            AIWeatherSensor_Gold_Button_Portrait.SetActive(true);
            AILoadSensor_Silver_Button_Portrait.SetActive(false);
            AILoadSensor_Gold_Button_Portrait.SetActive(true);

            StopCoroutine(WeatherSensorCoroutine);
        }

        if (GoogleAnalyticsAdapter.Instance != null)
        {
            GoogleAnalyticsAdapter.Instance.googleAnalyticsLogEvent("WeatherSensor_Button", "Clicked");
        }
    }




    public bool loadSensorClicked = false;
    Coroutine LoadSensorCoroutine;

    IEnumerator LoadSensorButtonTransition()
    {
        InsideObject_1.SetActive(false);
        InsideObject_2.SetActive(false);
        InsideObject_3.SetActive(false);
        InsideObject_4.SetActive(false);
        InsideObject_5.SetActive(false);
        InsideObject_6.SetActive(false);
        InsideObject_7.SetActive(false);
        InsideObject_8.SetActive(false);
        InsideObject_9.SetActive(false);
        InsideObject_10.SetActive(false);
        InsideObject_11.SetActive(false);
        InsideObject_12.SetActive(false);
        InsideObject_13.SetActive(false);
        InsideObject_14.SetActive(false);
        InsideObject_15.SetActive(false);
        InsideObject_16.SetActive(false);
        InsideObject_17.SetActive(false);
        InsideObject_18.SetActive(false);
        InsideObject_19.SetActive(false);
        InsideObject_20.SetActive(false);
        InsideObject_21.SetActive(false);
        InsideObject_22.SetActive(false);
        InsideObject_23.SetActive(false);
        InsideDoorObject_Top.SetActive(false);
        InsideDoorObject_Bottom.SetActive(false);

        if (Screen.width > Screen.height)
        {
            _AICallout_3.SetActive(false);
            AILoadSensor_Callout_1.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                AILoadSensor_Callout_1.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            OnDoorOpenCloseClicked(true);

            yield return new WaitForSeconds(1.5f);

            InsideDoorObject_Top.SetActive(true);
            InsideDoorObject_Bottom.SetActive(true);
            InsideObject_1.SetActive(true);
            InsideObject_2.SetActive(true);
            InsideObject_3.SetActive(true);
            InsideObject_4.SetActive(true);
            InsideObject_5.SetActive(true);
            yield return new WaitForSeconds(1f);
            InsideObject_6.SetActive(true);
            InsideObject_7.SetActive(true);
            InsideObject_8.SetActive(true);
            InsideObject_9.SetActive(true);
            InsideObject_10.SetActive(true);
            yield return new WaitForSeconds(1f);
            InsideObject_11.SetActive(true);
            InsideObject_12.SetActive(true);
            InsideObject_13.SetActive(true);
            InsideObject_14.SetActive(true);
            InsideObject_15.SetActive(true);
            yield return new WaitForSeconds(1f);
            InsideObject_16.SetActive(true);
            InsideObject_17.SetActive(true);
            InsideObject_18.SetActive(true);
            InsideObject_19.SetActive(true);
            yield return new WaitForSeconds(1f);
            InsideObject_20.SetActive(true);
            InsideObject_21.SetActive(true);
            InsideObject_22.SetActive(true);
            InsideObject_23.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
        else
        {
            _AICallout_3.SetActive(false);
            AILoadSensor_Callout_1.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                AILoadSensor_Callout_1.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            OnDoorOpenCloseClicked(true);

            yield return new WaitForSeconds(1.5f);

            InsideDoorObject_Top.SetActive(true);
            InsideDoorObject_Bottom.SetActive(true);
            InsideObject_1.SetActive(true);
            InsideObject_2.SetActive(true);
            InsideObject_3.SetActive(true);
            InsideObject_4.SetActive(true);
            InsideObject_5.SetActive(true);
            yield return new WaitForSeconds(1f);
            InsideObject_6.SetActive(true);
            InsideObject_7.SetActive(true);
            InsideObject_8.SetActive(true);
            InsideObject_9.SetActive(true);
            InsideObject_10.SetActive(true);
            yield return new WaitForSeconds(1f);
            InsideObject_11.SetActive(true);
            InsideObject_12.SetActive(true);
            InsideObject_13.SetActive(true);
            InsideObject_14.SetActive(true);
            InsideObject_15.SetActive(true);
            yield return new WaitForSeconds(1f);
            InsideObject_16.SetActive(true);
            InsideObject_17.SetActive(true);
            InsideObject_18.SetActive(true);
            InsideObject_19.SetActive(true);
            yield return new WaitForSeconds(1f);
            InsideObject_20.SetActive(true);
            InsideObject_21.SetActive(true);
            InsideObject_22.SetActive(true);
            InsideObject_23.SetActive(true);
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(1f);
    }

    public void OnLoadSensorButtonClicked()
    {
        ResetAIAction();
        loadSensorClicked = !loadSensorClicked;

        if (usageSensorClicked && loadSensorClicked)
        {
            OnUsageSensorButtonClicked();
        }
        if (weatherSensorClicked && loadSensorClicked)
        {
            OnWeatherSensorButtonClicked();
        }

        if (loadSensorClicked)
        {
            AIUsageSensor_Silver_Button.SetActive(false);
            AIUsageSensor_Gold_Button.SetActive(true);
            AIWeatherSensor_Silver_Button.SetActive(false);
            AIWeatherSensor_Gold_Button.SetActive(true);
            AILoadSensor_Silver_Button.SetActive(true);
            AILoadSensor_Gold_Button.SetActive(false);

            AIUsageSensor_Silver_Button_Portrait.SetActive(false);
            AIUsageSensor_Gold_Button_Portrait.SetActive(true);
            AIWeatherSensor_Silver_Button_Portrait.SetActive(false);
            AIWeatherSensor_Gold_Button_Portrait.SetActive(true);
            AILoadSensor_Silver_Button_Portrait.SetActive(true);
            AILoadSensor_Gold_Button_Portrait.SetActive(false);

            LoadSensorCoroutine = StartCoroutine(LoadSensorButtonTransition());
        }
        else
        {
            AIUsageSensor_Silver_Button.SetActive(false);
            AIUsageSensor_Gold_Button.SetActive(true);
            AIWeatherSensor_Silver_Button.SetActive(false);
            AIWeatherSensor_Gold_Button.SetActive(true);
            AILoadSensor_Silver_Button.SetActive(false);
            AILoadSensor_Gold_Button.SetActive(true);

            AIUsageSensor_Silver_Button_Portrait.SetActive(false);
            AIUsageSensor_Gold_Button_Portrait.SetActive(true);
            AIWeatherSensor_Silver_Button_Portrait.SetActive(false);
            AIWeatherSensor_Gold_Button_Portrait.SetActive(true);
            AILoadSensor_Silver_Button_Portrait.SetActive(false);
            AILoadSensor_Gold_Button_Portrait.SetActive(true);

            StopCoroutine(LoadSensorCoroutine);
            if (doorOpen)
            {
                OnDoorOpenCloseClicked(true);
            }
        }

        if (GoogleAnalyticsAdapter.Instance != null)
        {
            GoogleAnalyticsAdapter.Instance.googleAnalyticsLogEvent("LoadSensor_Button", "Clicked");
        }
    }



































































    [SerializeField]
    GameObject VariableTemperatureCallout_1, VariableTemperatureCallout_2, VariableTemperatureCallout_3, VariableTemperatureCallout_4, VariableTemperature_Tray, VariableTemperatureArrow,
               VariableTemperatureModel, VariableTemperature_ModelButton, VariableTemperature_DairyProduct, VariableTemperature_Fruits, VariableTemperature_Sprite_1, VariableTemperature_Sprite_2;
    Animator animatorVariableTemperature;

    IEnumerator VariableTemperatureTransition()
    {
        ObjectRotateScript.AllowRotation = 0;
        SceneZoomScript.AllowScaling = 0;

        InsideObject_1.SetActive(true);
        InsideObject_2.SetActive(true);
        InsideObject_3.SetActive(true);
        InsideObject_4.SetActive(true);
        InsideObject_5.SetActive(true);
        InsideObject_6.SetActive(true);
        InsideObject_7.SetActive(true);
        InsideObject_8.SetActive(true);
        InsideObject_9.SetActive(true);
        InsideObject_10.SetActive(true);
        InsideObject_11.SetActive(true);
        InsideObject_12.SetActive(true);
        InsideObject_13.SetActive(true);
        InsideObject_14.SetActive(true);
        InsideObject_15.SetActive(true);
        InsideObject_16.SetActive(true);
        InsideObject_17.SetActive(true);
        InsideObject_18.SetActive(true);
        InsideObject_19.SetActive(true);
        InsideObject_20.SetActive(true);
        InsideObject_21.SetActive(true);
        InsideObject_22.SetActive(true);
        InsideObject_23.SetActive(true);
        InsideDoorObject_Top.SetActive(true);
        InsideDoorObject_Bottom.SetActive(true);

        if (Screen.width > Screen.height)
        {
            VariableTemperatureCallout_1.transform.localPosition = new Vector3(-0.499f, 0.165f, 0.853f);
            VariableTemperatureCallout_1.transform.localScale = new Vector3(0.07480616f, 0.07480616f, 0.07480616f);
            VariableTemperatureCallout_2.transform.localPosition = new Vector3(-0.584f, 0.219f, 0.853f);
            VariableTemperatureCallout_2.transform.localScale = new Vector3(0.10138968f, 0.1013896f, 0.1013896f);
            VariableTemperatureCallout_3.transform.localPosition = new Vector3(-0.593f, -0.039f, 0.853f);
            VariableTemperatureCallout_3.transform.localScale = new Vector3(0.1069763f, 0.1069763f, 0.1069763f);
            VariableTemperatureCallout_4.transform.localPosition = new Vector3(-0.629f, 0.132f, 0.861f);
            VariableTemperatureCallout_4.transform.localScale = new Vector3(0.003453031f, 0.003453031f, 0.003453031f);


            OnDoorOpenCloseClicked(true);

            yield return new WaitForSeconds(1f);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 50)
            {
                _Virtual_Camera.transform.localPosition = Vector3.MoveTowards(_Virtual_Camera.transform.localPosition, new Vector3(-0.31f, 0.29f, -0.91f), t);
                yield return null;

                if (_Virtual_Camera.transform.localPosition == new Vector3(-0.31f, 0.29f, -0.91f))
                    break;
            }

            VariableTemperatureCallout_1.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                VariableTemperatureCallout_1.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            VariableTemperatureCallout_2.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                VariableTemperatureCallout_2.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            VariableTemperatureCallout_3.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                VariableTemperatureCallout_3.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);

            VariableTemperatureModel.SetActive(true);
            animatorVariableTemperature.Play("VariableTemperature_Animation");
            toggleSwitchEnabled = true;

            VariableTemperature_Fruits.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                VariableTemperature_Tray.transform.localPosition = Vector3.MoveTowards(VariableTemperature_Tray.transform.localPosition, new Vector3(0.0f, 0.0f, 0.25f), t);
                yield return null;

                if (VariableTemperature_Tray.transform.localPosition == new Vector3(0.0f, 0.0f, 0.25f))
                    break;
            }

            yield return new WaitForSeconds(0.75f);
            VariableTemperature_Sprite_2.SetActive(true);
            
            VariableTemperatureCallout_4.SetActive(true);
            VariableTemperatureArrow.SetActive(true);
            VariableTemperatureArrow.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            VariableTemperatureArrow.transform.localPosition = new Vector3(-2f, 73.3f, 5.375757f);
            VariableTemperatureArrow.transform.localScale = new Vector3(3.620229f, 3.620229f, 3.620229f);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                VariableTemperatureCallout_4.GetComponent<SpriteRenderer>().color = newColor;
                VariableTemperatureArrow.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            yield return new WaitForSeconds(1f);

            
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
                VariableTemperatureArrow.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
            VariableTemperatureArrow.SetActive(false);
            
            

            // _BackPanel_LandScape.GetComponent<RectTransform>().localPosition = new Vector3(-65f, -165f, 0);
            // _BackPanel_LandScape.SetActive(true);
            // _MenuPanel_Portrait.SetActive(false);
        }
        else
        {
            VariableTemperatureCallout_1.transform.localPosition = new Vector3(-0.543f, 0.178f, 0.861f);
            VariableTemperatureCallout_1.transform.localScale = new Vector3(0.08914935f, 0.08914935f, 0.08914935f);
            VariableTemperatureCallout_2.transform.localPosition = new Vector3(-0.593f, 0.249f, 0.861f);
            VariableTemperatureCallout_2.transform.localScale = new Vector3(0.1345849f, 0.1345849f, 0.1345849f);
            VariableTemperatureCallout_3.transform.localPosition = new Vector3(-0.608f, -0.151f, 0.861f);
            VariableTemperatureCallout_3.transform.localScale = new Vector3(0.1718593f, 0.1718593f, 0.1718593f);
            VariableTemperatureCallout_4.transform.localPosition = new Vector3(-0.629f, 0.132f, 0.861f);
            VariableTemperatureCallout_4.transform.localScale = new Vector3(0.003453031f, 0.003453031f, 0.003453031f);

            _MenuPanel_Portrait.SetActive(false);

            OnDoorOpenCloseClicked(true);

            yield return new WaitForSeconds(1f);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 10)
            {
                _Virtual_Camera.transform.localPosition = Vector3.MoveTowards(_Virtual_Camera.transform.localPosition, new Vector3(-0.31f, 0.23f, -1.63f), t);
                yield return null;

                if (_Virtual_Camera.transform.localPosition == new Vector3(-0.31f, 0.23f, -1.63f))
                    break;
            }

            VariableTemperatureCallout_1.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                VariableTemperatureCallout_1.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            VariableTemperatureCallout_2.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                VariableTemperatureCallout_2.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            VariableTemperatureCallout_3.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                VariableTemperatureCallout_3.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);

            VariableTemperatureModel.SetActive(true);
            animatorVariableTemperature.Play("VariableTemperature_Animation");
            toggleSwitchEnabled = true;

            VariableTemperature_Fruits.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 10)
            {
                VariableTemperature_Tray.transform.localPosition = Vector3.MoveTowards(VariableTemperature_Tray.transform.localPosition, new Vector3(0.0f, 0.0f, 0.25f), t);
                yield return null;

                if (VariableTemperature_Tray.transform.localPosition == new Vector3(0.0f, 0.0f, 0.25f))
                    break;
            }

            yield return new WaitForSeconds(0.75f);
            VariableTemperature_Sprite_2.SetActive(true);

            VariableTemperatureCallout_4.SetActive(true);
            VariableTemperatureArrow.SetActive(true);
            VariableTemperatureArrow.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            VariableTemperatureArrow.transform.localPosition = new Vector3(-4.2f, 73.3f, 5.375757f);
            VariableTemperatureArrow.transform.localScale = new Vector3(2.858226f, 2.858226f, 2.858226f);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                VariableTemperatureCallout_4.GetComponent<SpriteRenderer>().color = newColor;
                VariableTemperatureArrow.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            yield return new WaitForSeconds(1f);

            
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
                VariableTemperatureArrow.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
            VariableTemperatureArrow.SetActive(false);
            

            //_BackPanel_Portrait.GetComponent<RectTransform>().localPosition = new Vector3(-10f, -190f, 0);
            //_BackPanel_Portrait.SetActive(true);
        }

        _BackPanel_LandScape.GetComponent<RectTransform>().localPosition = new Vector3(-50f, -150f, 0);
        _BackPanel_LandScape.SetActive(true);
        // _BackPanel_Portrait.GetComponent<RectTransform>().localPosition = new Vector3(-35f, -215f, 0);
        _BackPanel_Portrait.SetActive(true);

    }

    public bool onVariableTemperatureClickedBool = false;

    public void OnVariableTemperatureClicked()
    {
        if (ARView)
        {

        }
        else
        {
            _SceneObject.transform.localScale = defaultScale;
        }

        onVariableTemperatureClickedBool = !onVariableTemperatureClickedBool;
        // ResetActions();
        ObjectRotateScript.resetRotation = true;

        if (Screen.width > Screen.height)
        {
            _AITechnology_White_Button_LandScape.SetActive(false);
            _AITechnology_Gold_Button_LandScape.SetActive(true);
            _variableTemperature_White_Button_LandScape.SetActive(true);
            _variableTemperature_Gold_Button_LandScape.SetActive(false);
            _3DAir_White_Button_LandScape.SetActive(false);
            _3DAir_Gold_Button_LandScape.SetActive(true);
            _PortableIce_White_Button_LandScape.SetActive(false);
            _PortableIce_Gold_Button_LandScape.SetActive(true);
            _MicroBlock_White_Button_LandScape.SetActive(false);
            _MicroBlock_Gold_Button_LandScape.SetActive(true);

            _BottomPanel_LandScape.SetActive(false);
            _MenuPanel_LandScape.SetActive(false);
            _InfoPanel_LandScape.SetActive(false);
            _ProductName.SetActive(false);
        }
        else
        {
            _AITechnology_White_Button_Portrait.SetActive(false);
            _AITechnology_Gold_Button_Portrait.SetActive(true);
            _variableTemperature_White_Button_Portrait.SetActive(true);
            _variableTemperature_Gold_Button_Portrait.SetActive(false);
            _3DAir_White_Button_Portrait.SetActive(false);
            _3DAir_Gold_Button_Portrait.SetActive(true);
            _PortableIce_White_Button_Portrait.SetActive(false);
            _PortableIce_Gold_Button_Portrait.SetActive(true);
            _MicroBlock_White_Button_Portrait.SetActive(false);
            _MicroBlock_Gold_Button_Portrait.SetActive(true);

            _BottomPanel_Portrait.SetActive(false);
            _MenuPanel_Portrait.SetActive(false);
            _InfoPanel_Portrait.SetActive(false);
        }


        StartCoroutine(VariableTemperatureTransition());


        if (GoogleAnalyticsAdapter.Instance != null)
        {
            GoogleAnalyticsAdapter.Instance.googleAnalyticsLogEvent("VariableTemperature_Button", "Clicked");
        }
    }






    [SerializeField]
    GameObject _3DFlow_Sprite, _3DFlowCallout_1, _3DFlowCallout_2, _3DFlowCallout_3, _3DFlowCallout_4, _3DFlow_BowlModel;
    Animator animator3DFlow;

    IEnumerator _3DFlowTransition()
    {
        ObjectRotateScript.AllowRotation = 0;
        SceneZoomScript.AllowScaling = 0;

        InsideObject_1.SetActive(true);
        InsideObject_2.SetActive(true);
        InsideObject_3.SetActive(true);
        InsideObject_4.SetActive(true);
        InsideObject_5.SetActive(true);
        InsideObject_6.SetActive(true);
        InsideObject_7.SetActive(true);
        InsideObject_8.SetActive(true);
        InsideObject_9.SetActive(true);
        InsideObject_10.SetActive(true);
        InsideObject_11.SetActive(true);
        InsideObject_12.SetActive(true);
        InsideObject_13.SetActive(true);
        InsideObject_14.SetActive(true);
        InsideObject_15.SetActive(true);
        InsideObject_16.SetActive(true);
        InsideObject_17.SetActive(true);
        InsideObject_18.SetActive(true);
        InsideObject_19.SetActive(true);
        InsideObject_20.SetActive(true);
        InsideObject_21.SetActive(true);
        InsideObject_22.SetActive(true);
        InsideObject_23.SetActive(true);
        InsideDoorObject_Top.SetActive(true);
        InsideDoorObject_Bottom.SetActive(true);

        if (Screen.width > Screen.height)
        {
            _3DFlowCallout_1.transform.localPosition = new Vector3(-0.445f, 0.396f, 1.125f);
            _3DFlowCallout_1.transform.localScale = new Vector3(0.1053922f, 0.1053922f, 0.1053922f);
            _3DFlowCallout_2.transform.localPosition = new Vector3(-0.58f, 0.472f, 1.125f);
            _3DFlowCallout_2.transform.localScale = new Vector3(0.129427f, 0.129427f, 0.129427f);
            _3DFlowCallout_3.transform.localPosition = new Vector3(-0.775f, 0.283f, 1.125f);
            _3DFlowCallout_3.transform.localScale = new Vector3(0.1104171f, 0.1104171f, 0.1104171f);
            _3DFlowCallout_4.transform.localPosition = new Vector3(-0.581f, 0.124f, 1.125f);
            _3DFlowCallout_4.transform.localScale = new Vector3(0.134945f, 0.134945f, 0.134945f);

            _3DFlow_BowlModel.SetActive(true);
            _3DFlow_Sprite.SetActive(true);
            // animator3DFlow.Play("_3DFlow_Animation");
            GameObject.Find("3D Air Flow Sequence Updated0").transform.localPosition = new Vector3(-3f, GameObject.Find("3D Air Flow Sequence Updated0").transform.localPosition.y, GameObject.Find("3D Air Flow Sequence Updated0").transform.localPosition.z);
            GameObject.Find("3D Air Flow Sequence Updated1").transform.localPosition = new Vector3(-3f, GameObject.Find("3D Air Flow Sequence Updated1").transform.localPosition.y, GameObject.Find("3D Air Flow Sequence Updated1").transform.localPosition.z);
            GameObject.Find("3D Air Flow Sequence Updated2").transform.localPosition = new Vector3(-3f, GameObject.Find("3D Air Flow Sequence Updated2").transform.localPosition.y, GameObject.Find("3D Air Flow Sequence Updated2").transform.localPosition.z);
            GameObject.Find("3D Air Flow Sequence Updated3").transform.localPosition = new Vector3(-3f, GameObject.Find("3D Air Flow Sequence Updated3").transform.localPosition.y, GameObject.Find("3D Air Flow Sequence Updated3").transform.localPosition.z);
            GameObject.Find("3D Air Flow Sequence Updated4").transform.localPosition = new Vector3(2.29f, GameObject.Find("3D Air Flow Sequence Updated4").transform.localPosition.y, GameObject.Find("3D Air Flow Sequence Updated4").transform.localPosition.z);
            GameObject.Find("3D Air Flow Sequence Updated5").transform.localPosition = new Vector3(2.29f, GameObject.Find("3D Air Flow Sequence Updated5").transform.localPosition.y, GameObject.Find("3D Air Flow Sequence Updated5").transform.localPosition.z);
            GameObject.Find("3D Air Flow Sequence Updated6").transform.localPosition = new Vector3(2.29f, GameObject.Find("3D Air Flow Sequence Updated6").transform.localPosition.y, GameObject.Find("3D Air Flow Sequence Updated6").transform.localPosition.z);
            GameObject.Find("3D Air Flow Sequence Updated7").transform.localPosition = new Vector3(2.29f, GameObject.Find("3D Air Flow Sequence Updated7").transform.localPosition.y, GameObject.Find("3D Air Flow Sequence Updated7").transform.localPosition.z);


            OnDoorOpenCloseClicked_Top(true);

            yield return new WaitForSeconds(1f);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 50)
            {
                _Virtual_Camera.transform.localPosition = Vector3.MoveTowards(_Virtual_Camera.transform.localPosition, new Vector3(-0.14f, 0.39f, -0.89f), t);
                yield return null;

                if (_Virtual_Camera.transform.localPosition == new Vector3(-0.14f, 0.39f, -0.89f))
                    break;
            }

            _3DFlowCallout_1.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _3DFlowCallout_1.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            _3DFlowCallout_2.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _3DFlowCallout_2.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            /*
            _3DFlowCallout_3.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _3DFlowCallout_3.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            yield return new WaitForSeconds(3f);

            _3DFlowCallout_3.SetActive(false);
            */

            _3DFlowCallout_4.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _3DFlowCallout_4.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            // _BackPanel_LandScape.GetComponent<RectTransform>().localPosition = new Vector3(-65f, -165f, 0);
            // _BackPanel_LandScape.SetActive(true);
            // _MenuPanel_Portrait.SetActive(false);
        }
        else
        {
            _3DFlowCallout_1.transform.localPosition = new Vector3(-0.535f, 0.282f, 1.124f);
            _3DFlowCallout_1.transform.localScale = new Vector3(0.1209419f, 0.1209419f, 0.1209419f);
            _3DFlowCallout_2.transform.localPosition = new Vector3(-0.653f, 0.366f, 1.124f);
            _3DFlowCallout_2.transform.localScale = new Vector3(0.1771887f, 0.1771887f, 0.1771887f);
            _3DFlowCallout_3.transform.localPosition = new Vector3(-0.779f, 0.154f, 1.124f);
            _3DFlowCallout_3.transform.localScale = new Vector3(0.1212172f, 0.1212172f, 0.1212172f);
            _3DFlowCallout_4.transform.localPosition = new Vector3(-0.657f, -0.077f, 1.124f);
            _3DFlowCallout_4.transform.localScale = new Vector3(0.183824f, 0.183824f, 0.183824f);

            _MenuPanel_Portrait.SetActive(false);


            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 10)
            {
                _Virtual_Camera.transform.localPosition = Vector3.MoveTowards(_Virtual_Camera.transform.localPosition, new Vector3(-0.32f, 0.12f, -1.58f), t);
                yield return null;

                if (_Virtual_Camera.transform.localPosition == new Vector3(-0.32f, 0.12f, -1.58f))
                    break;
            }


            _3DFlow_BowlModel.SetActive(true);
            _3DFlow_Sprite.SetActive(true);
            // animator3DFlow.Play("_3DFlow_Animation");
            GameObject.Find("3D Air Flow Sequence Updated0").transform.localPosition = new Vector3(-3.46f, GameObject.Find("3D Air Flow Sequence Updated0").transform.localPosition.y, GameObject.Find("3D Air Flow Sequence Updated0").transform.localPosition.z);
            GameObject.Find("3D Air Flow Sequence Updated1").transform.localPosition = new Vector3(-3.46f, GameObject.Find("3D Air Flow Sequence Updated1").transform.localPosition.y, GameObject.Find("3D Air Flow Sequence Updated1").transform.localPosition.z);
            GameObject.Find("3D Air Flow Sequence Updated2").transform.localPosition = new Vector3(-3.46f, GameObject.Find("3D Air Flow Sequence Updated2").transform.localPosition.y, GameObject.Find("3D Air Flow Sequence Updated2").transform.localPosition.z);
            GameObject.Find("3D Air Flow Sequence Updated3").transform.localPosition = new Vector3(-3.46f, GameObject.Find("3D Air Flow Sequence Updated3").transform.localPosition.y, GameObject.Find("3D Air Flow Sequence Updated3").transform.localPosition.z);
            GameObject.Find("3D Air Flow Sequence Updated4").transform.localPosition = new Vector3(1.87f, GameObject.Find("3D Air Flow Sequence Updated4").transform.localPosition.y, GameObject.Find("3D Air Flow Sequence Updated4").transform.localPosition.z);
            GameObject.Find("3D Air Flow Sequence Updated5").transform.localPosition = new Vector3(1.87f, GameObject.Find("3D Air Flow Sequence Updated5").transform.localPosition.y, GameObject.Find("3D Air Flow Sequence Updated5").transform.localPosition.z);
            GameObject.Find("3D Air Flow Sequence Updated6").transform.localPosition = new Vector3(1.87f, GameObject.Find("3D Air Flow Sequence Updated6").transform.localPosition.y, GameObject.Find("3D Air Flow Sequence Updated6").transform.localPosition.z);
            GameObject.Find("3D Air Flow Sequence Updated7").transform.localPosition = new Vector3(1.87f, GameObject.Find("3D Air Flow Sequence Updated7").transform.localPosition.y, GameObject.Find("3D Air Flow Sequence Updated7").transform.localPosition.z);


            OnDoorOpenCloseClicked_Top(true);

            yield return new WaitForSeconds(1f);

            _3DFlowCallout_1.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _3DFlowCallout_1.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            _3DFlowCallout_2.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _3DFlowCallout_2.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            /*
            _3DFlowCallout_3.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _3DFlowCallout_3.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            yield return new WaitForSeconds(3f);

            _3DFlowCallout_3.SetActive(false);
            */
            _3DFlowCallout_4.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _3DFlowCallout_4.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
            
            //_BackPanel_Portrait.GetComponent<RectTransform>().localPosition = new Vector3(-10f, -190f, 0);
            //_BackPanel_Portrait.SetActive(true);

        }

        _BackPanel_LandScape.GetComponent<RectTransform>().localPosition = new Vector3(-90f, -165f, 0);
        _BackPanel_LandScape.SetActive(true);
        // _BackPanel_Portrait.GetComponent<RectTransform>().localPosition = new Vector3(-10f, -190f, 0);
        _BackPanel_Portrait.SetActive(true);

    }

    public bool on3DFlowClickedBool = false;

    public void On3DFlowClicked()
    {
        if (ARView)
        {

        }
        else
        {
            _SceneObject.transform.localScale = defaultScale;
        }

        on3DFlowClickedBool = !on3DFlowClickedBool;
        // ResetActions();
        ObjectRotateScript.resetRotation = true;

        if (Screen.width > Screen.height)
        {
            _AITechnology_White_Button_LandScape.SetActive(false);
            _AITechnology_Gold_Button_LandScape.SetActive(true);
            _variableTemperature_White_Button_LandScape.SetActive(false);
            _variableTemperature_Gold_Button_LandScape.SetActive(true);
            _3DAir_White_Button_LandScape.SetActive(true);
            _3DAir_Gold_Button_LandScape.SetActive(false);
            _PortableIce_White_Button_LandScape.SetActive(false);
            _PortableIce_Gold_Button_LandScape.SetActive(true);
            _MicroBlock_White_Button_LandScape.SetActive(false);
            _MicroBlock_Gold_Button_LandScape.SetActive(true);

            _BottomPanel_LandScape.SetActive(false);
            _MenuPanel_LandScape.SetActive(false);
            _InfoPanel_LandScape.SetActive(false);
            _ProductName.SetActive(false);
        }
        else
        {
            _AITechnology_White_Button_Portrait.SetActive(false);
            _AITechnology_Gold_Button_Portrait.SetActive(true);
            _variableTemperature_White_Button_Portrait.SetActive(false);
            _variableTemperature_Gold_Button_Portrait.SetActive(true);
            _3DAir_White_Button_Portrait.SetActive(true);
            _3DAir_Gold_Button_Portrait.SetActive(false);
            _PortableIce_White_Button_Portrait.SetActive(false);
            _PortableIce_Gold_Button_Portrait.SetActive(true);
            _MicroBlock_White_Button_Portrait.SetActive(false);
            _MicroBlock_Gold_Button_Portrait.SetActive(true);

            _BottomPanel_Portrait.SetActive(false);
            _MenuPanel_Portrait.SetActive(false);
            _InfoPanel_Portrait.SetActive(false);
        }


        StartCoroutine(_3DFlowTransition());

        if (GoogleAnalyticsAdapter.Instance != null)
        {
            GoogleAnalyticsAdapter.Instance.googleAnalyticsLogEvent("3DFlow_Button", "Clicked");
        }
    }





    [SerializeField]
    GameObject PortableIce_1, PortableIce_2, PortableIce_3,PortableIce_Model, PortableIce_4;
    
    IEnumerator PortableIceTransition()
    {
        ObjectRotateScript.AllowRotation = 0;
        SceneZoomScript.AllowScaling = 0;
        InsideObject_4.transform.localPosition = new Vector3(-0.1516571f, -0.5090525f, 0.09480139f);
        InsideObject_5.transform.localPosition = new Vector3(-0.04314014f, -0.506731f, -0.01076187f);

        InsideObject_1.SetActive(true);
        InsideObject_2.SetActive(true);
        InsideObject_3.SetActive(true);
        InsideObject_4.SetActive(true);
        InsideObject_5.SetActive(true);
        InsideObject_6.SetActive(true);
        InsideObject_7.SetActive(true);
        InsideObject_8.SetActive(true);
        InsideObject_9.SetActive(true);
        InsideObject_10.SetActive(true);
        InsideObject_11.SetActive(true);
        InsideObject_12.SetActive(true);
        InsideObject_13.SetActive(true);
        InsideObject_14.SetActive(true);
        InsideObject_15.SetActive(true);
        InsideObject_16.SetActive(true);
        InsideObject_17.SetActive(true);
        InsideObject_18.SetActive(true);
        InsideObject_19.SetActive(true);
        InsideObject_20.SetActive(true);
        InsideObject_21.SetActive(true);
        InsideObject_22.SetActive(true);
        InsideObject_23.SetActive(true);
        InsideDoorObject_Top.SetActive(true);
        InsideDoorObject_Bottom.SetActive(true);

        if ( Screen.width > Screen.height)
        {
            PortableIce_1.transform.localPosition = new Vector3(-0.516f, -0.489f, 0.9473139f);
            PortableIce_1.transform.localScale = new Vector3(0.08771664f, 0.08771664f, 0.08771664f);
            PortableIce_2.transform.localPosition = new Vector3(-0.592f, -0.439f, 0.9473139f);
            PortableIce_2.transform.localScale = new Vector3(0.1500751f, 0.1500751f, 0.1500751f);
            PortableIce_3.transform.localPosition = new Vector3(-0.598f, -0.725f, 0.9473139f);
            PortableIce_3.transform.localScale = new Vector3(0.1249637f, 0.1249637f, 0.1249637f);
            PortableIce_4.transform.localPosition = new Vector3(-0.3921f, -0.489f, 0.9473139f);
            PortableIce_4.transform.localScale = new Vector3(0.1197297f, 0.1197297f, 0.1197297f);


            PortableIce_Model.transform.localPosition = new Vector3(0, 0, 0);
            PortableIce_Model.SetActive(true);

            OnDoorOpenCloseClicked_Bottom(true);

            yield return new WaitForSeconds(1f);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 50)
            {
                _Virtual_Camera.transform.localPosition = Vector3.MoveTowards(_Virtual_Camera.transform.localPosition, new Vector3(-0.32f, -0.4f, -0.99f), t);
                yield return null;

                if (_Virtual_Camera.transform.localPosition == new Vector3(-0.32f, -0.4f, -0.99f))
                    break;
            }

            PortableIce_1.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                PortableIce_1.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            PortableIce_2.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                PortableIce_2.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }


            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                PortableIce_Model.transform.localPosition = Vector3.MoveTowards(PortableIce_Model.transform.localPosition, new Vector3(0.0f, 0f, 0.295f), t);
                yield return null;

                if (PortableIce_Model.transform.localPosition == new Vector3(0.0f, 0f, 0.295f))
                    break;
            }

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                PortableIce_Model.transform.localPosition = Vector3.MoveTowards(PortableIce_Model.transform.localPosition, new Vector3(-0.211f, 0f, 0.295f), t);
                yield return null;

                if (PortableIce_Model.transform.localPosition == new Vector3(-0.211f, 0f, 0.295f))
                    break;
            }

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                InsideObject_4.transform.localPosition = Vector3.MoveTowards(InsideObject_4.transform.localPosition, new Vector3(0.089f, -0.5090525f, 0.09480139f), t);
                InsideObject_5.transform.localPosition = Vector3.MoveTowards(InsideObject_5.transform.localPosition, new Vector3(0.148f, -0.506731f, -0.01076187f), t);
                yield return null;

                if (InsideObject_4.transform.localPosition == new Vector3(0.089f, -0.5090525f, 0.09480139f))
                    if (InsideObject_5.transform.localPosition == new Vector3(0.148f, -0.506731f, -0.01076187f))
                        break;
            }

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                PortableIce_Model.transform.localPosition = Vector3.MoveTowards(PortableIce_Model.transform.localPosition, new Vector3(-0.211f, 0f, 0.0f), t);
                yield return null;

                if (PortableIce_Model.transform.localPosition == new Vector3(-0.211f, 0f, 0.0f))
                    break;
            }

            PortableIce_1.SetActive(false);
            PortableIce_4.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                PortableIce_4.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            PortableIce_3.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                PortableIce_3.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            // _BackPanel_LandScape.GetComponent<RectTransform>().localPosition = new Vector3(10f, -155f, 0);
            // _BackPanel_LandScape.SetActive(true);
        }
        else
        {
            PortableIce_1.transform.localPosition = new Vector3(-0.6f, -0.411f, 0.9473139f);
            PortableIce_1.transform.localScale = new Vector3(0.1188807f, 0.1188807f, 0.1188807f);
            PortableIce_2.transform.localPosition = new Vector3(-0.693f, -0.358f, 0.9473139f);
            PortableIce_2.transform.localScale = new Vector3(0.2244663f, 0.2244663f, 0.2244663f);
            PortableIce_3.transform.localPosition = new Vector3(-0.699f, -0.81f, 0.9473139f);
            PortableIce_3.transform.localScale = new Vector3(0.1948289f, 0.1948289f, 0.1948289f);
            PortableIce_4.transform.localPosition = new Vector3(-0.489f, -0.415f, 0.9473139f);
            PortableIce_4.transform.localScale = new Vector3(0.1485408f, 0.1485408f, 0.1485408f);


            PortableIce_Model.transform.localPosition = new Vector3(0, 0, 0);
            PortableIce_Model.SetActive(true);

            OnDoorOpenCloseClicked_Bottom(true);

            yield return new WaitForSeconds(1f);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 10)
            {
                _Virtual_Camera.transform.localPosition = Vector3.MoveTowards(_Virtual_Camera.transform.localPosition, new Vector3(-0.35f, -0.19f, -1.86f), t);
                yield return null;

                if (_Virtual_Camera.transform.localPosition == new Vector3(-0.35f, -0.19f, -1.86f))
                    break;
            }

            PortableIce_1.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                PortableIce_1.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            PortableIce_2.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                PortableIce_2.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }


            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 10)
            {
                PortableIce_Model.transform.localPosition = Vector3.MoveTowards(PortableIce_Model.transform.localPosition, new Vector3(0.0f, 0f, 0.295f), t);
                yield return null;

                if (PortableIce_Model.transform.localPosition == new Vector3(0.0f, 0f, 0.295f))
                    break;
            }

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 10)
            {
                PortableIce_Model.transform.localPosition = Vector3.MoveTowards(PortableIce_Model.transform.localPosition, new Vector3(-0.211f, 0f, 0.295f), t);
                yield return null;

                if (PortableIce_Model.transform.localPosition == new Vector3(-0.211f, 0f, 0.295f))
                    break;
            }

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 10)
            {
                InsideObject_4.transform.localPosition = Vector3.MoveTowards(InsideObject_4.transform.localPosition, new Vector3(0.089f, -0.5090525f, 0.09480139f), t);
                InsideObject_5.transform.localPosition = Vector3.MoveTowards(InsideObject_5.transform.localPosition, new Vector3(0.148f, -0.506731f, -0.01076187f), t);
                yield return null;

                if (InsideObject_4.transform.localPosition == new Vector3(0.089f, -0.5090525f, 0.09480139f))
                    if (InsideObject_5.transform.localPosition == new Vector3(0.148f, -0.506731f, -0.01076187f))
                        break;
            }

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 10)
            {
                PortableIce_Model.transform.localPosition = Vector3.MoveTowards(PortableIce_Model.transform.localPosition, new Vector3(-0.211f, 0f, 0.0f), t);
                yield return null;

                if (PortableIce_Model.transform.localPosition == new Vector3(-0.211f, 0f, 0.0f))
                    break;
            }

            PortableIce_1.SetActive(false);
            PortableIce_4.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                PortableIce_4.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            PortableIce_3.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                PortableIce_3.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            // _BackPanel_Portrait.GetComponent<RectTransform>().localPosition = new Vector3(-25f, -285f, 0);
            // _BackPanel_Portrait.SetActive(true);
        }

        _BackPanel_LandScape.GetComponent<RectTransform>().localPosition = new Vector3(-35f, -165f, 0);
        _BackPanel_LandScape.SetActive(true);
        // _BackPanel_Portrait.GetComponent<RectTransform>().localPosition = new Vector3(-25f, -285f, 0);
        _BackPanel_Portrait.SetActive(true);
    }


    public bool onPortableIceClickedBool = false;

    public void OnPortableIceClicked()
    {
        if (ARView)
        {

        }
        else
        {
            _SceneObject.transform.localScale = defaultScale;
        }

        onPortableIceClickedBool = !onPortableIceClickedBool;
        // ResetActions();
        ObjectRotateScript.resetRotation = true;

        if (Screen.width > Screen.height)
        {
            _AITechnology_White_Button_LandScape.SetActive(false);
            _AITechnology_Gold_Button_LandScape.SetActive(true);
            _variableTemperature_White_Button_LandScape.SetActive(false);
            _variableTemperature_Gold_Button_LandScape.SetActive(true);
            _3DAir_White_Button_LandScape.SetActive(false);
            _3DAir_Gold_Button_LandScape.SetActive(true);
            _PortableIce_White_Button_LandScape.SetActive(true);
            _PortableIce_Gold_Button_LandScape.SetActive(false);
            _MicroBlock_White_Button_LandScape.SetActive(false);
            _MicroBlock_Gold_Button_LandScape.SetActive(true);


            _BottomPanel_LandScape.SetActive(false);
            _MenuPanel_LandScape.SetActive(false);
            _InfoPanel_LandScape.SetActive(false);
            _ProductName.SetActive(false);
        }
        else
        {
            _AITechnology_White_Button_Portrait.SetActive(false);
            _AITechnology_Gold_Button_Portrait.SetActive(true);
            _variableTemperature_White_Button_Portrait.SetActive(false);
            _variableTemperature_Gold_Button_Portrait.SetActive(true);
            _3DAir_White_Button_Portrait.SetActive(false);
            _3DAir_Gold_Button_Portrait.SetActive(true);
            _PortableIce_White_Button_Portrait.SetActive(true);
            _PortableIce_Gold_Button_Portrait.SetActive(false);
            _MicroBlock_White_Button_Portrait.SetActive(false);
            _MicroBlock_Gold_Button_Portrait.SetActive(true);


            _BottomPanel_Portrait.SetActive(false);
            _MenuPanel_Portrait.SetActive(false);
            _InfoPanel_Portrait.SetActive(false);
        }

        StartCoroutine(PortableIceTransition());


        if (GoogleAnalyticsAdapter.Instance != null)
        {
            GoogleAnalyticsAdapter.Instance.googleAnalyticsLogEvent("PortableTray_Button", "Clicked");
        }
    }





    [SerializeField]
    GameObject MicroBlock_1, MicroBlock_2, MicroBlock_3, MicroBlock_4, MicroBlock_Disclaimer, MicroBlock_Model, MicroBlock_Sprite;
    [SerializeField]
    GameObject _TrayObject_1, _TrayObject_2, _TrayObject_3, _TrayObject_4, _TrayObject_5, _TrayObject_6, _TrayObject_7, _TrayObject_8;
    Animator animatorMicroBlock;

    IEnumerator MicroBlockTransition()
    {
        ObjectRotateScript.AllowRotation = 0;
        SceneZoomScript.AllowScaling = 0;

        InsideObject_1.SetActive(true);
        InsideObject_2.SetActive(true);
        InsideObject_3.SetActive(true);
        InsideObject_4.SetActive(true);
        InsideObject_5.SetActive(true);
        InsideObject_6.SetActive(true);
        InsideObject_7.SetActive(true);
        InsideObject_8.SetActive(true);
        InsideObject_9.SetActive(true);
        InsideObject_10.SetActive(true);
        InsideObject_11.SetActive(true);
        InsideObject_12.SetActive(true);
        InsideObject_13.SetActive(true);
        InsideObject_14.SetActive(true);
        InsideObject_15.SetActive(true);
        InsideObject_16.SetActive(false);
        InsideObject_17.SetActive(false);
        InsideObject_18.SetActive(false);
        InsideObject_19.SetActive(false);
        InsideObject_20.SetActive(false);
        InsideObject_21.SetActive(false);
        InsideObject_22.SetActive(false);
        InsideObject_23.SetActive(false);
        InsideDoorObject_Top.SetActive(true);
        InsideDoorObject_Bottom.SetActive(true);

        _MenuPanel_Portrait.SetActive(false);
        if(Screen.width > Screen.height)
        {
            MicroBlock_1.transform.localPosition = new Vector3(-0.498f, 0.027f, 0.9093139f);
            MicroBlock_1.transform.localScale = new Vector3(0.07705922f, 0.07705922f, 0.07705922f);
            MicroBlock_2.transform.localPosition = new Vector3(-0.555f, 0.087f, 0.9093139f);
            MicroBlock_2.transform.localScale = new Vector3(0.1133615f, 0.1133615f, 0.1133615f);
            MicroBlock_3.transform.localPosition = new Vector3(-0.699f, -0.06099993f, 0.9093139f);
            MicroBlock_3.transform.localScale = new Vector3(0.1092711f, 0.1092711f, 0.1092711f);
            MicroBlock_4.transform.localPosition = new Vector3(-0.563f, -0.219f, 0.9093139f);
            MicroBlock_4.transform.localScale = new Vector3(0.1295968f, 0.1295968f, 0.1295968f);
            MicroBlock_Disclaimer.transform.localPosition = new Vector3(-1.13f, -0.335f, 0.9093139f);
            MicroBlock_Disclaimer.transform.localScale = new Vector3(0.1153411f, 0.1153411f, 0.1153411f);


            MicroBlock_Model.transform.localPosition = new Vector3(0, 0.7986788f, -0.144f);
            MicroBlock_Model.SetActive(true);

            OnDoorOpenCloseClicked_Top(true);

            yield return new WaitForSeconds(1f);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 50)
            {
                _Virtual_Camera.transform.localPosition = Vector3.MoveTowards(_Virtual_Camera.transform.localPosition, new Vector3(-0.3f, 0.17f, -1.01f), t);
                yield return null;

                if (_Virtual_Camera.transform.localPosition == new Vector3(-0.3f, 0.17f, -1.01f))
                    break;
            }

            MicroBlock_1.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                MicroBlock_1.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }


            MicroBlock_2.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                MicroBlock_2.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            MicroBlock_Disclaimer.SetActive(true);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                MicroBlock_Model.transform.localPosition = Vector3.MoveTowards(MicroBlock_Model.transform.localPosition, new Vector3(0f, 0.7986788f, 0.222f), t);
                yield return null;

                if (MicroBlock_Model.transform.localPosition == new Vector3(0f, 0.7986788f, 0.222f))
                    break;
            }

            /*
            MicroBlock_3.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                MicroBlock_3.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
            */

            yield return new WaitForSeconds(1f);
            

            _TrayObject_1.SetActive(true);
            // yield return new WaitForSeconds(0.5f);
            _TrayObject_2.SetActive(true);
            // yield return new WaitForSeconds(0.5f);
            _TrayObject_3.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _TrayObject_4.SetActive(true);
            // yield return new WaitForSeconds(0.5f);
            _TrayObject_5.SetActive(true);
            // yield return new WaitForSeconds(0.5f);
            _TrayObject_6.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _TrayObject_7.SetActive(true);
            // yield return new WaitForSeconds(0.5f);
            _TrayObject_8.SetActive(true);
            yield return new WaitForSeconds(1f);


            // MicroBlock_3.SetActive(false);
            MicroBlock_4.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                MicroBlock_4.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                MicroBlock_Model.transform.localPosition = Vector3.MoveTowards(MicroBlock_Model.transform.localPosition, new Vector3(0, 0.7986788f, -0.144f), t);
                yield return null;

                if (MicroBlock_Model.transform.localPosition == new Vector3(0, 0.7986788f, -0.144f))
                    break;
            }

            MicroBlock_Sprite.SetActive(true);
            MicroBlock_Sprite.transform.localEulerAngles = new Vector3(0, -180, 0);
            animatorMicroBlock.Play("MicroBlock_Animation");
            MicroBlock_Sprite.transform.localEulerAngles = new Vector3(0, -180, 0);
            yield return new WaitForSeconds(1.083f);
            MicroBlock_Sprite.transform.localEulerAngles = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(1.083f);
            MicroBlock_Sprite.transform.localEulerAngles = new Vector3(0, -180, 0);
            yield return new WaitForSeconds(1.083f);
            MicroBlock_Sprite.transform.localEulerAngles = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(1.083f);
            MicroBlock_Sprite.SetActive(false);

            // _BackPanel_LandScape.GetComponent<RectTransform>().localPosition = new Vector3(10f, -155f, 0);
            // _BackPanel_LandScape.SetActive(true);
        }
        else
        {
            MicroBlock_1.transform.localPosition = new Vector3(-0.548f, -0.00999999f, 0.9093139f);
            MicroBlock_1.transform.localScale = new Vector3(0.1107652f, 0.1107652f, 0.1107652f);
            MicroBlock_2.transform.localPosition = new Vector3(-0.6439999f, 0.067f, 0.9093139f);
            MicroBlock_2.transform.localScale = new Vector3(0.1668694f, 0.1668694f, 0.1668694f);
            MicroBlock_3.transform.localPosition = new Vector3(-0.678f, -0.08499998f, 0.9093139f);
            MicroBlock_3.transform.localScale = new Vector3(0.1174958f, 0.1174958f, 0.1174958f);
            MicroBlock_4.transform.localPosition = new Vector3(-0.654f, -0.382f, 0.9093139f);
            MicroBlock_4.transform.localScale = new Vector3(0.1920505f, 0.1920505f, 0.1920505f);
            MicroBlock_Disclaimer.transform.localPosition = new Vector3(-0.54f, -0.62f, 0.9093139f);
            MicroBlock_Disclaimer.transform.localScale = new Vector3(0.1315773f, 0.1315773f, 0.1315773f);
            

            MicroBlock_Model.transform.localPosition = new Vector3(0, 0.7986788f, -0.144f);
            MicroBlock_Model.SetActive(true);

            OnDoorOpenCloseClicked_Top(true);

            yield return new WaitForSeconds(1f);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 10)
            {
                _Virtual_Camera.transform.localPosition = Vector3.MoveTowards(_Virtual_Camera.transform.localPosition, new Vector3(-0.32f, 0.1f, -1.73f), t);
                yield return null;

                if (_Virtual_Camera.transform.localPosition == new Vector3(-0.32f, 0.1f, -1.73f))
                    break;
            }

            MicroBlock_1.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                MicroBlock_1.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            MicroBlock_2.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                MicroBlock_2.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            MicroBlock_Disclaimer.SetActive(true);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 10)
            {
                MicroBlock_Model.transform.localPosition = Vector3.MoveTowards(MicroBlock_Model.transform.localPosition, new Vector3(0f, 0.7986788f, 0.222f), t);
                yield return null;

                if (MicroBlock_Model.transform.localPosition == new Vector3(0f, 0.7986788f, 0.222f))
                    break;
            }

            /*
            MicroBlock_3.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                MicroBlock_3.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
            */

            yield return new WaitForSeconds(1f);
            

            
            _TrayObject_1.SetActive(true);
            // yield return new WaitForSeconds(0.5f);
            _TrayObject_2.SetActive(true);
            // yield return new WaitForSeconds(0.5f);
            _TrayObject_3.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _TrayObject_4.SetActive(true);
            // yield return new WaitForSeconds(0.5f);
            _TrayObject_5.SetActive(true);
            // yield return new WaitForSeconds(0.5f);
            _TrayObject_6.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _TrayObject_7.SetActive(true);
            // yield return new WaitForSeconds(0.5f);
            _TrayObject_8.SetActive(true);
            yield return new WaitForSeconds(1f);



            // MicroBlock_3.SetActive(false);
            MicroBlock_4.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                MicroBlock_4.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 10)
            {
                MicroBlock_Model.transform.localPosition = Vector3.MoveTowards(MicroBlock_Model.transform.localPosition, new Vector3(0, 0.7986788f, -0.144f), t);
                yield return null;

                if (MicroBlock_Model.transform.localPosition == new Vector3(0, 0.7986788f, -0.144f))
                    break;
            }

            MicroBlock_Sprite.SetActive(true);
            MicroBlock_Sprite.transform.localEulerAngles = new Vector3(0, -180, 0);
            animatorMicroBlock.Play("MicroBlock_Animation");
            MicroBlock_Sprite.transform.localEulerAngles = new Vector3(0, -180, 0);
            yield return new WaitForSeconds(1.083f);
            MicroBlock_Sprite.transform.localEulerAngles = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(1.083f);
            MicroBlock_Sprite.transform.localEulerAngles = new Vector3(0, -180, 0);
            yield return new WaitForSeconds(1.083f);
            MicroBlock_Sprite.transform.localEulerAngles = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(1.083f);
            MicroBlock_Sprite.SetActive(false);


            // _BackPanel_Portrait.GetComponent<RectTransform>().localPosition = new Vector3(25f, -265f, 0);
            // _BackPanel_Portrait.SetActive(true);
        }

        _BackPanel_LandScape.GetComponent<RectTransform>().localPosition = new Vector3(-40f, -160f, 0);
        _BackPanel_LandScape.SetActive(true);
        // _BackPanel_Portrait.GetComponent<RectTransform>().localPosition = new Vector3(25f, -265f, 0);
        _BackPanel_Portrait.SetActive(true);
    }

    public bool onMicroBlockClickedBool = false;

    public void OnMicroBlockClicked()
    {
        if (ARView)
        {

        }
        else
        {
            _SceneObject.transform.localScale = defaultScale;
        }

        onMicroBlockClickedBool = !onMicroBlockClickedBool;
        // ResetActions();
        ObjectRotateScript.resetRotation = true;

        if (Screen.width > Screen.height)
        {
            _AITechnology_White_Button_LandScape.SetActive(false);
            _AITechnology_Gold_Button_LandScape.SetActive(true);
            _variableTemperature_White_Button_LandScape.SetActive(false);
            _variableTemperature_Gold_Button_LandScape.SetActive(true);
            _3DAir_White_Button_LandScape.SetActive(false);
            _3DAir_Gold_Button_LandScape.SetActive(true);
            _PortableIce_White_Button_LandScape.SetActive(false);
            _PortableIce_Gold_Button_LandScape.SetActive(true);
            _MicroBlock_White_Button_LandScape.SetActive(true);
            _MicroBlock_Gold_Button_LandScape.SetActive(false);


            _BottomPanel_LandScape.SetActive(false);
            _MenuPanel_LandScape.SetActive(false);
            _InfoPanel_LandScape.SetActive(false);
            _ProductName.SetActive(false);
        }
        else
        {
            _AITechnology_White_Button_Portrait.SetActive(false);
            _AITechnology_Gold_Button_Portrait.SetActive(true);
            _variableTemperature_White_Button_Portrait.SetActive(false);
            _variableTemperature_Gold_Button_Portrait.SetActive(true);
            _3DAir_White_Button_Portrait.SetActive(false);
            _3DAir_Gold_Button_Portrait.SetActive(true);
            _PortableIce_White_Button_Portrait.SetActive(false);
            _PortableIce_Gold_Button_Portrait.SetActive(true);
            _MicroBlock_White_Button_Portrait.SetActive(true);
            _MicroBlock_Gold_Button_Portrait.SetActive(false);

            _BottomPanel_Portrait.SetActive(false);
            _MenuPanel_Portrait.SetActive(false);
            _InfoPanel_Portrait.SetActive(false);
        }

        StartCoroutine(MicroBlockTransition());

        if (GoogleAnalyticsAdapter.Instance != null)
        {
            GoogleAnalyticsAdapter.Instance.googleAnalyticsLogEvent("MicroBlock_Button", "Clicked");
        }
    }




}
