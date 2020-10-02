using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    GameObject _Plus_White_LandScape, _Plus_Gold_LandScape, _CM_White_LandScape, _CM_Gold_LandScape, _Door_White_LandScape, _Door_Gold_LandScape;
    [SerializeField]
    GameObject _BackPanel_LandScape;

    [SerializeField]
    GameObject _InfoPanel_Portrait, _InfoButton_Portrait, _InfoImage_Portrait;
    [SerializeField]
    GameObject _BottomPanel_Portrait;
    [SerializeField]
    GameObject _Plus_White_Portrait, _Plus_Gold_Portrait, _CM_White_Portrait, _CM_Gold_Portrait, _Door_White_Portrait, _Door_Gold_Portrait;
    [SerializeField]
    GameObject _BackPanel_Portrait;


    [SerializeField]
    GameObject _FridgeModel;
    Animator animatorStartFridge;

    [SerializeField]
    GameObject _ProductName, _ProductName_Portrait;

    [SerializeField]
    GameObject ObjectRotateGameObject;
    ObjectRotate ObjectRotateScript;

    Vector3 defaultScale;




    // Start is called before the first frame update
    void Start()
    {
        animatorStartFridge = _FridgeModel.GetComponent<Animator>();
        animatorAITechSequence = _AITech_Sprite.GetComponent<Animator>();
        animator3DFlow = _3DFlow_Sprite.GetComponent<Animator>();
        animatorMicroBlock = MicroBlock_Sprite.GetComponent<Animator>();

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


    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.width > Screen.height)
        {
            _Panel_LandScape.SetActive(true);
            _Panel_Portrait.SetActive(false);

            _Virtual_Camera.GetComponent<Camera>().fieldOfView = 70f;
            initialPosition = new Vector3(0, 0, -2.2f);
        }
        else
        {
            _Panel_LandScape.SetActive(false);
            _Panel_Portrait.SetActive(true);

            _Virtual_Camera.GetComponent<Camera>().fieldOfView = 75f;
            initialPosition = new Vector3(0.25f, 0, -2.2f);

        }

        if (ResetPosition)
        {
            _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, initialPosition, 5 * Time.deltaTime);

            if (_Virtual_Camera.transform.position == initialPosition)
            {
                ResetPosition = false;
            }
        }

        if (ARView)
        {
            defaultScale = new Vector3(3f, 3f, 3f);
        }
        else
        {
            defaultScale = new Vector3(1f, 1f, 1f);
        }
    }

    IEnumerator StartTransition()
    {
        if(Screen.width > Screen.height || ARView)
        {
            yield return new WaitForSeconds(0.5f);

            animatorStartFridge.Play("StartFridge_Animation");

            yield return new WaitForSeconds(4f);

            _ProductName.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _ProductName.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            yield return new WaitForSeconds(1f);

            _BottomPanel_LandScape.SetActive(true);
            _InfoPanel_LandScape.SetActive(true);
            _InfoImage_LandScape.SetActive(true);
            _InfoImage_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _InfoImage_LandScape.GetComponent<Image>().color = newColor;
                yield return null;
            }

            yield return new WaitForSeconds(2f);

            _InfoImage_LandScape.SetActive(false);
        }
        else
        {
            animatorStartFridge.Play("StartFridge_Animation");

            yield return new WaitForSeconds(4f);


            _BottomPanel_Portrait.SetActive(true);
            _InfoPanel_Portrait.SetActive(true);
            _InfoImage_Portrait.SetActive(true);
            _InfoImage_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _InfoImage_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }

            yield return new WaitForSeconds(2f);

            _InfoImage_Portrait.SetActive(false);
        }


    }


    public void ResetActions(bool DoorFunction = true)
    {
        animatorStartFridge.Play("Still");
        _SceneObject.transform.localScale = defaultScale;

        if (DoorFunction == true)
        {
            doorOpen = false;
        }

        _BackPanel_LandScape.SetActive(false);
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

        _AITech_Sprite.SetActive(false);
        _AICallout_1.SetActive(false);
        _AICallout_2.SetActive(false);
        _AICallout_3.SetActive(false);

        _3DFlow_Sprite.SetActive(false);
        _3DFlowCallout_1.SetActive(false);
        _3DFlowCallout_2.SetActive(false);
        _3DFlowCallout_3.SetActive(false);
        _3DFlowCallout_4.SetActive(false);

        PortableIce_1.SetActive(false);
        PortableIce_2.SetActive(false);
        PortableIce_3.SetActive(false);
        // PortableIce_Model.SetActive(false);

    }

    IEnumerator BackTransition()
    {
        if (plusClicked)
        {
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, initialPosition, t);
                yield return null;

                if (_Virtual_Camera.transform.position == initialPosition)
                    break;
            }
        }
        else
        {
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(0f, 0f, -2.2f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(0f, 0f, -2.2f))
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
            _SceneObject.transform.localScale = new Vector3(1f, 1f, 1f);
            BackButtonClicked();
        }
        else
        {
            _Zappar_Camera.SetActive(ARView);
            _InstantTracker.SetActive(ARView);
            _Virtual_Camera.SetActive(VirtualView);
            _SceneObject.transform.localScale = new Vector3(3f, 3f, 3f);
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

    IEnumerator PlusButtonTransition()
    {   

        if (plusClicked)
        {
            if (Screen.width < Screen.height )
            {
                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 50)
                {
                    _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(0.25f, 0f, -2.2f), t);
                    yield return null;

                    if (_Virtual_Camera.transform.position == new Vector3(0.25f, 0f, -2.2f))
                        break;
                }

            }

            _MenuPanel_LandScape.SetActive(true);

            _AITechnology_Gold_Button_LandScape.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            _variableTemperature_Gold_Button_LandScape.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            _3DAir_Gold_Button_LandScape.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            _PortableIce_Gold_Button_LandScape.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            _MicroBlock_Gold_Button_LandScape.SetActive(true);

            _MenuPanel_Portrait.SetActive(true);

            _ProductName_Portrait.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _ProductName_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }

            _AITechnology_Gold_Button_Portrait.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            _variableTemperature_Gold_Button_Portrait.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            _3DAir_Gold_Button_Portrait.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            _PortableIce_Gold_Button_Portrait.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            _MicroBlock_Gold_Button_Portrait.SetActive(true);

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

            if (Screen.width < Screen.height)
            {
                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 50)
                {
                    _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(0f, 0f, -2.2f), t);
                    yield return null;

                    if (_Virtual_Camera.transform.position == new Vector3(0f, 0f, -2.2f))
                        break;
                }
            }
        }
    }

    public void OnPlusButtonClicked()
    {
        ResetActions();
        plusClicked = !plusClicked;
        ObjectRotateScript.resetRotation = true;

        if (dimensionClick && plusClicked)
        {
            CMButtonClicked();
            ObjectRotateScript.OnDimensionClicked();
        }
        if (doorOpen && plusClicked)
        {
            // OnDoorOpenCloseClicked();
        }

        if (plusClicked)
        {
            _Plus_Gold_LandScape.SetActive(true);
            _Plus_White_LandScape.SetActive(false);
            _CM_Gold_LandScape.SetActive(false);
            _CM_White_LandScape.SetActive(true);
            _Door_Gold_LandScape.SetActive(false);
            _Door_White_LandScape.SetActive(true);

            _Plus_Gold_Portrait.SetActive(true);
            _Plus_White_Portrait.SetActive(false);
            _CM_Gold_Portrait.SetActive(false);
            _CM_White_Portrait.SetActive(true);
            _Door_Gold_Portrait.SetActive(false);
            _Door_White_Portrait.SetActive(true);
        }
        else
        {
            _Plus_Gold_LandScape.SetActive(false);
            _Plus_White_LandScape.SetActive(true);
            _CM_Gold_LandScape.SetActive(false);
            _CM_White_LandScape.SetActive(true);
            _Door_Gold_LandScape.SetActive(false);
            _Door_White_LandScape.SetActive(true);

            _Plus_Gold_Portrait.SetActive(false);
            _Plus_White_Portrait.SetActive(true);
            _CM_Gold_Portrait.SetActive(false);
            _CM_White_Portrait.SetActive(true);
            _Door_Gold_Portrait.SetActive(false);
            _Door_White_Portrait.SetActive(true);
        }
        StartCoroutine(PlusButtonTransition());
    }


    [SerializeField]
    GameObject Arrow_L, Arrow_B, Arrow_H;

    bool dimensionClick = false;

    public void CMButtonClicked()
    {
        ResetActions();
        dimensionClick = !dimensionClick;
        ObjectRotateScript.resetRotation = true;

        if (dimensionClick)
        {
            _Plus_Gold_LandScape.SetActive(false);
            _Plus_White_LandScape.SetActive(true);
            _CM_Gold_LandScape.SetActive(true);
            _CM_White_LandScape.SetActive(false);
            _Door_Gold_LandScape.SetActive(false);
            _Door_White_LandScape.SetActive(true);

            _Plus_Gold_Portrait.SetActive(false);
            _Plus_White_Portrait.SetActive(true);
            _CM_Gold_Portrait.SetActive(true);
            _CM_White_Portrait.SetActive(false);
            _Door_Gold_Portrait.SetActive(false);
            _Door_White_Portrait.SetActive(true);
        }
        else
        {
            _Plus_Gold_LandScape.SetActive(false);
            _Plus_White_LandScape.SetActive(true);
            _CM_Gold_LandScape.SetActive(false);
            _CM_White_LandScape.SetActive(true);
            _Door_Gold_LandScape.SetActive(false);
            _Door_White_LandScape.SetActive(true);

            _Plus_Gold_Portrait.SetActive(false);
            _Plus_White_Portrait.SetActive(true);
            _CM_Gold_Portrait.SetActive(false);
            _CM_White_Portrait.SetActive(true);
            _Door_Gold_Portrait.SetActive(false);
            _Door_White_Portrait.SetActive(true);
        }

        if (plusClicked && dimensionClick)
        {
            OnPlusButtonClicked();
        }
        if (doorOpen && dimensionClick)
        {
            // OnDoorOpenCloseClicked();
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

    }


    public bool doorOpen = false;

    public void OnDoorOpenCloseClicked(bool check = false)
    {
        // ResetActions(false);

        doorOpen = !doorOpen;
        ObjectRotateScript.resetRotation = true;


        if (plusClicked && doorOpen && !check)
        {
            OnPlusButtonClicked();
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

            // Only change the bottom icon color when other bottom icon is clicked 
            if (!check)
            {
                _Plus_Gold_LandScape.SetActive(false);
                _Plus_White_LandScape.SetActive(true);
                _Door_Gold_LandScape.SetActive(true);
                _Door_White_LandScape.SetActive(false);
                _CM_Gold_LandScape.SetActive(false);
                _CM_White_LandScape.SetActive(true);

                _Plus_Gold_Portrait.SetActive(false);
                _Plus_White_Portrait.SetActive(true);
                _Door_Gold_Portrait.SetActive(true);
                _Door_White_Portrait.SetActive(false);
                _CM_Gold_Portrait.SetActive(false);
                _CM_White_Portrait.SetActive(true);
            }
        }
        else
        {
            animatorStartFridge.Play("DoorClose_Animation");

            if (!check)
            {
                _Plus_Gold_LandScape.SetActive(false);
                _Plus_White_LandScape.SetActive(true);
                _Door_Gold_LandScape.SetActive(false);
                _Door_White_LandScape.SetActive(true);
                _CM_Gold_LandScape.SetActive(false);
                _CM_White_LandScape.SetActive(true);

                _Plus_Gold_Portrait.SetActive(false);
                _Plus_White_Portrait.SetActive(true);
                _Door_Gold_Portrait.SetActive(false);
                _Door_White_Portrait.SetActive(true);
                _CM_Gold_Portrait.SetActive(false);
                _CM_White_Portrait.SetActive(true);
            }
            
        }
    }


    public void BackButtonClicked()
    {
        _SceneObject.transform.localScale = defaultScale;
        if (Screen.width > Screen.height)
        {
            _BottomPanel_LandScape.SetActive(true);
            _MenuPanel_LandScape.SetActive(true);
            _ProductName.SetActive(true);
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
        on3DFlowClickedBool = false;
        onPortableIceClickedBool = false;
        onMicroBlockClickedBool = false;


        StopAllCoroutines();
        // ResetPosition = true;
        StartCoroutine(BackTransition());

        _AITech_Sprite.SetActive(false);
        _AICallout_1.SetActive(false);
        _AICallout_2.SetActive(false);
        _AICallout_3.SetActive(false);


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

        PortableIce_1.SetActive(false);
        PortableIce_2.SetActive(false);
        PortableIce_3.SetActive(false);
        PortableIce_Model.SetActive(false);
        PortableIce_Model.transform.localPosition = new Vector3(0, 0, 0);

        MicroBlock_1.SetActive(false);
        MicroBlock_2.SetActive(false);
        MicroBlock_3.SetActive(false);
        MicroBlock_4.SetActive(false);
        MicroBlock_Sprite.SetActive(false);
        // MicroBlock_Model.SetActive(false);
        MicroBlock_Model.transform.localPosition = new Vector3(0, 0.2192179f, -0.06651523f);
        _TrayObject_1.SetActive(false);
        _TrayObject_2.SetActive(false);
        _TrayObject_3.SetActive(false);
        _TrayObject_4.SetActive(false);
        _TrayObject_5.SetActive(false);
        _TrayObject_6.SetActive(false);
        _TrayObject_7.SetActive(false);
        _TrayObject_8.SetActive(false);

        ObjectRotateScript.resetRotation = true;
    }

    [SerializeField]
    GameObject _AITech_Sprite, _AICallout_1, _AICallout_2, _AICallout_3;
    Animator animatorAITechSequence;

    IEnumerator AITechnologyTransition()
    {
        if(Screen.width > Screen.height || ARView)
        {
            _AICallout_1.transform.localPosition = new Vector3(-1.175f, 0.77f, 1.370314f);
            _AICallout_1.transform.localScale = new Vector3(0.1649768f, 0.1649768f, 0.1649768f);
            _AICallout_2.transform.localPosition = new Vector3(-1.057f, 0.543f, 1.370314f);
            _AICallout_2.transform.localScale = new Vector3(0.2096372f, 0.2096372f, 0.2096372f);
            _AICallout_3.transform.localPosition = new Vector3(-1.177f, 0.051f, 1.370314f);
            _AICallout_3.transform.localScale = new Vector3(0.1853902f, 0.1853902f, 0.1853902f);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(-0.32f, 0.36f, -1.38f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(-0.32f, 0.36f, -1.38f))
                    break;
            }
        }
        else
        {

            _AICallout_1.transform.localPosition = new Vector3(-1.04f, 0.77f, 1.66f);
            _AICallout_1.transform.localScale = new Vector3(0.1969543f, 0.1969543f, 0.1969543f);
            _AICallout_2.transform.localPosition = new Vector3(-0.9f, 0.55f, 1.66f);
            _AICallout_2.transform.localScale = new Vector3(0.2502712f, 0.2502712f, 0.2502712f);
            _AICallout_3.transform.localPosition = new Vector3(-1.04f, -0.06f, 1.66f);
            _AICallout_3.transform.localScale = new Vector3(0.2213244f, 0.2213244f, 0.2213244f);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(-0.55f, -0.06f, -2.42f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(-0.55f, -0.06f, -2.42f))
                    break;
            }

        }
        

        _AITech_Sprite.SetActive(true);
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
            _AITech_Sprite.GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        animatorAITechSequence.Play("AITech_Animation");
        

        yield return new WaitForSeconds(1f);

        _AICallout_1.SetActive(true);
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
            _AICallout_1.GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        _AICallout_2.SetActive(true);
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
            _AICallout_2.GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        _AICallout_3.SetActive(true);
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
            _AICallout_3.GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }

        _BackPanel_LandScape.GetComponent<RectTransform>().localPosition = new Vector3(-80f, -175f, 0f);
        _BackPanel_LandScape.SetActive(true);
        _BackPanel_Portrait.GetComponent<RectTransform>().localPosition = new Vector3(5f, -115f, 0);
        _BackPanel_Portrait.SetActive(true);

    }

    bool onAITechnologyClickedBool = false;

    public void OnAITechnologyClicked()
    {
        _SceneObject.transform.localScale = defaultScale;
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
    }











    /*

    [SerializeField]
    GameObject VariableTemperature_Sprite, VariableTemperatureCallout_1, VariableTemperatureCallout_2, VariableTemperatureCallout_3, VariableTemperatureCallout_4, VariableTemperatureModel;
    Animator animatorVariableTemperature;

    IEnumerator VariableTemperatureTransition()
    {
        if (Screen.width > Screen.height || ARView)
        {
            VariableTemperatureCallout_1.transform.localPosition = new Vector3(-0.576f, 0.433f, 1.125f);
            VariableTemperatureCallout_1.transform.localScale = new Vector3(0.1481054f, 0.1481054f, 0.1481054f);
            VariableTemperatureCallout_2.transform.localPosition = new Vector3(-0.823f, 0.508f, 1.125f);
            VariableTemperatureCallout_2.transform.localScale = new Vector3(0.1065419f, 0.1065419f, 0.1065419f);
            VariableTemperatureCallout_3.transform.localPosition = new Vector3(-0.748f, 0.283f, 1.125f);
            VariableTemperatureCallout_3.transform.localScale = new Vector3(0.1330327f, 0.1330327f, 0.1330327f);
            VariableTemperatureCallout_4.transform.localPosition = new Vector3(-0.68f, 0.163f, 1.125f);
            VariableTemperatureCallout_4.transform.localScale = new Vector3(0.1308552f, 0.1308552f, 0.1308552f);


            VariableTemperatureModel.SetActive(true);
            VariableTemperature_Sprite.SetActive(true);
            animator3DFlow.Play("_3DFlow_Animation");

            OnDoorOpenCloseClicked(true);

            yield return new WaitForSeconds(1f);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(-0.26f, 0.21f, -1.11f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(-0.26f, 0.21f, -1.11f))
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

            yield return new WaitForSeconds(3f);

            VariableTemperatureCallout_3.SetActive(false);
            VariableTemperatureCallout_4.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                VariableTemperatureCallout_4.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            // _BackPanel_LandScape.GetComponent<RectTransform>().localPosition = new Vector3(-65f, -165f, 0);
            // _BackPanel_LandScape.SetActive(true);
            // _MenuPanel_Portrait.SetActive(false);
        }
        else
        {
            VariableTemperatureCallout_1.transform.localPosition = new Vector3(-0.512f, 0.282f, 1.124f);
            VariableTemperatureCallout_1.transform.localScale = new Vector3(0.1722803f, 0.1722803f, 0.1722803f);
            VariableTemperatureCallout_2.transform.localPosition = new Vector3(-0.749f, 0.379f, 1.124f);
            VariableTemperatureCallout_2.transform.localScale = new Vector3(0.1429926f, 0.1429926f, 0.1429926f);
            VariableTemperatureCallout_3.transform.localPosition = new Vector3(-0.726f, 0.03199999f, 1.428f);
            VariableTemperatureCallout_3.transform.localScale = new Vector3(0.217749f, 0.217749f, 0.217749f);
            VariableTemperatureCallout_4.transform.localPosition = new Vector3(-0.6850001f, -0.097f, 1.124f);
            VariableTemperatureCallout_4.transform.localScale = new Vector3(0.1343786f, 0.1343786f, 0.1343786f);

            _MenuPanel_Portrait.SetActive(false);


            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(-0.4f, 0f, -1.7f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(-0.4f, 0f, -1.7f))
                    break;
            }


            VariableTemperatureModel.SetActive(true);
            VariableTemperature_Sprite.SetActive(true);
            animatorVariableTemperature.Play("VariableTemperature_Animation");

            OnDoorOpenCloseClicked(true);

            yield return new WaitForSeconds(1f);

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

            yield return new WaitForSeconds(3f);

            VariableTemperatureCallout_3.SetActive(false);
            VariableTemperatureCallout_4.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                VariableTemperatureCallout_4.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            //_BackPanel_Portrait.GetComponent<RectTransform>().localPosition = new Vector3(-10f, -190f, 0);
            //_BackPanel_Portrait.SetActive(true);

        }

        _BackPanel_LandScape.GetComponent<RectTransform>().localPosition = new Vector3(-50f, -165f, 0);
        _BackPanel_LandScape.SetActive(true);
        _BackPanel_Portrait.GetComponent<RectTransform>().localPosition = new Vector3(-10f, -190f, 0);
        _BackPanel_Portrait.SetActive(true);

    }

    bool onVariableTemperatureClickedBool = false;

    public void On3DFlowClicked()
    {
        _SceneObject.transform.localScale = defaultScale;
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
    }

    */



















































































































    [SerializeField]
    GameObject _3DFlow_Sprite, _3DFlowCallout_1, _3DFlowCallout_2, _3DFlowCallout_3, _3DFlowCallout_4, _3DFlow_BowlModel;
    Animator animator3DFlow;

    IEnumerator _3DFlowTransition()
    {
        if(Screen.width > Screen.height || ARView)
        {
            _3DFlowCallout_1.transform.localPosition = new Vector3(-0.576f, 0.433f, 1.125f);
            _3DFlowCallout_1.transform.localScale = new Vector3(0.1481054f, 0.1481054f, 0.1481054f);
            _3DFlowCallout_2.transform.localPosition = new Vector3(-0.823f, 0.508f, 1.125f);
            _3DFlowCallout_2.transform.localScale = new Vector3(0.1065419f, 0.1065419f, 0.1065419f);
            _3DFlowCallout_3.transform.localPosition = new Vector3(-0.748f, 0.283f, 1.125f);
            _3DFlowCallout_3.transform.localScale = new Vector3(0.1330327f, 0.1330327f, 0.1330327f);
            _3DFlowCallout_4.transform.localPosition = new Vector3(-0.68f, 0.163f, 1.125f);
            _3DFlowCallout_4.transform.localScale = new Vector3(0.1308552f, 0.1308552f, 0.1308552f);


            _3DFlow_BowlModel.SetActive(true);
            _3DFlow_Sprite.SetActive(true);
            animator3DFlow.Play("_3DFlow_Animation");

            OnDoorOpenCloseClicked(true);

            yield return new WaitForSeconds(1f);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(-0.26f, 0.21f, -1.11f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(-0.26f, 0.21f, -1.11f))
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

            _3DFlowCallout_3.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _3DFlowCallout_3.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            yield return new WaitForSeconds(3f);

            _3DFlowCallout_3.SetActive(false);
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
            _3DFlowCallout_1.transform.localPosition = new Vector3(-0.512f, 0.282f, 1.124f);
            _3DFlowCallout_1.transform.localScale = new Vector3(0.1722803f, 0.1722803f, 0.1722803f);
            _3DFlowCallout_2.transform.localPosition = new Vector3(-0.749f, 0.379f, 1.124f);
            _3DFlowCallout_2.transform.localScale = new Vector3(0.1429926f, 0.1429926f, 0.1429926f);
            _3DFlowCallout_3.transform.localPosition = new Vector3(-0.726f, 0.03199999f, 1.428f);
            _3DFlowCallout_3.transform.localScale = new Vector3(0.217749f, 0.217749f, 0.217749f);
            _3DFlowCallout_4.transform.localPosition = new Vector3(-0.6850001f, -0.097f, 1.124f);
            _3DFlowCallout_4.transform.localScale = new Vector3(0.1343786f, 0.1343786f, 0.1343786f);

            _MenuPanel_Portrait.SetActive(false);


            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(-0.4f, 0f, -1.7f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(-0.4f, 0f, -1.7f))
                    break;
            }


            _3DFlow_BowlModel.SetActive(true);
            _3DFlow_Sprite.SetActive(true);
            animator3DFlow.Play("_3DFlow_Animation");

            OnDoorOpenCloseClicked(true);

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

            _3DFlowCallout_3.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _3DFlowCallout_3.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            yield return new WaitForSeconds(3f);

            _3DFlowCallout_3.SetActive(false);
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

        _BackPanel_LandScape.GetComponent<RectTransform>().localPosition = new Vector3(-50f, -165f, 0);
        _BackPanel_LandScape.SetActive(true);
        _BackPanel_Portrait.GetComponent<RectTransform>().localPosition = new Vector3(-10f, -190f, 0);
        _BackPanel_Portrait.SetActive(true);

    }

    bool on3DFlowClickedBool = false;

    public void On3DFlowClicked()
    {
        _SceneObject.transform.localScale = defaultScale;
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
    }





    [SerializeField]
    GameObject PortableIce_1, PortableIce_2, PortableIce_3,PortableIce_Model;
    
    IEnumerator PortableIceTransition()
    {
        if( Screen.width > Screen.height || ARView)
        {
            PortableIce_1.transform.localPosition = new Vector3(-0.502f, -0.489f, 0.9473139f);
            PortableIce_1.transform.localScale = new Vector3(0.100825f, 0.100825f, 0.100825f);
            PortableIce_2.transform.localPosition = new Vector3(-0.575f, -0.445f, 0.9473139f);
            PortableIce_2.transform.localScale = new Vector3(0.100825f, 0.100825f, 0.100825f);
            PortableIce_3.transform.localPosition = new Vector3(-0.587f, -0.726f, 0.9473139f);
            PortableIce_3.transform.localScale = new Vector3(0.100825f, 0.100825f, 0.100825f);


            PortableIce_Model.transform.localPosition = new Vector3(0, 0, 0);
            PortableIce_Model.SetActive(true);

            OnDoorOpenCloseClicked(true);

            yield return new WaitForSeconds(1f);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(-0.32f, -0.55f, -1.21f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(-0.32f, -0.55f, -1.21f))
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
                PortableIce_Model.transform.localPosition = Vector3.MoveTowards(PortableIce_Model.transform.localPosition, new Vector3(0.0f, 0f, 0.37f), t);
                yield return null;

                if (PortableIce_Model.transform.localPosition == new Vector3(0.0f, 0f, 0.37f))
                    break;
            }

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                PortableIce_Model.transform.localPosition = Vector3.MoveTowards(PortableIce_Model.transform.localPosition, new Vector3(-0.211f, 0f, 0.37f), t);
                yield return null;

                if (PortableIce_Model.transform.localPosition == new Vector3(-0.211f, 0f, 0.37f))
                    break;
            }

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                PortableIce_Model.transform.localPosition = Vector3.MoveTowards(PortableIce_Model.transform.localPosition, new Vector3(-0.211f, 0f, 0.0f), t);
                yield return null;

                if (PortableIce_Model.transform.localPosition == new Vector3(-0.211f, 0f, 0.0f))
                    break;
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
            PortableIce_1.transform.localPosition = new Vector3(-0.55f, -0.3845918f, 0.9473139f);
            PortableIce_1.transform.localScale = new Vector3(0.1260312f, 0.1260312f, 0.1260312f);
            PortableIce_2.transform.localPosition = new Vector3(-0.6380324f, - 0.3275917f,  0.9473139f);
            PortableIce_2.transform.localScale = new Vector3(0.1260312f, 0.1260312f, 0.1260312f);
            PortableIce_3.transform.localPosition = new Vector3(-0.6480324f, - 0.6375917f, 0.9473139f);
            PortableIce_3.transform.localScale = new Vector3(0.1260312f, 0.1260312f, 0.1260312f);


            


            PortableIce_Model.transform.localPosition = new Vector3(0, 0, 0);
            PortableIce_Model.SetActive(true);

            OnDoorOpenCloseClicked(true);

            yield return new WaitForSeconds(1f);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(-0.35f, -0.19f, -1.86f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(-0.35f, -0.19f, -1.86f))
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
                PortableIce_Model.transform.localPosition = Vector3.MoveTowards(PortableIce_Model.transform.localPosition, new Vector3(0.0f, 0f, 0.37f), t);
                yield return null;

                if (PortableIce_Model.transform.localPosition == new Vector3(0.0f, 0f, 0.37f))
                    break;
            }

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                PortableIce_Model.transform.localPosition = Vector3.MoveTowards(PortableIce_Model.transform.localPosition, new Vector3(-0.211f, 0f, 0.37f), t);
                yield return null;

                if (PortableIce_Model.transform.localPosition == new Vector3(-0.211f, 0f, 0.37f))
                    break;
            }

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                PortableIce_Model.transform.localPosition = Vector3.MoveTowards(PortableIce_Model.transform.localPosition, new Vector3(-0.211f, 0f, 0.0f), t);
                yield return null;

                if (PortableIce_Model.transform.localPosition == new Vector3(-0.211f, 0f, 0.0f))
                    break;
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

        _BackPanel_LandScape.GetComponent<RectTransform>().localPosition = new Vector3(10f, -155f, 0);
        _BackPanel_LandScape.SetActive(true);
        _BackPanel_Portrait.GetComponent<RectTransform>().localPosition = new Vector3(-25f, -285f, 0);
        _BackPanel_Portrait.SetActive(true);
    }


    bool onPortableIceClickedBool = false;

    public void OnPortableIceClicked()
    {
        _SceneObject.transform.localScale = defaultScale;
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
    }





    [SerializeField]
    GameObject MicroBlock_1, MicroBlock_2, MicroBlock_3, MicroBlock_4, MicroBlock_Model, MicroBlock_Sprite;
    [SerializeField]
    GameObject _TrayObject_1, _TrayObject_2, _TrayObject_3, _TrayObject_4, _TrayObject_5, _TrayObject_6, _TrayObject_7, _TrayObject_8;
    Animator animatorMicroBlock;

    IEnumerator MicroBlockTransition()
    {
        _MenuPanel_Portrait.SetActive(false);
        if(Screen.width > Screen.height || ARView)
        {
            MicroBlock_1.transform.localPosition = new Vector3(-0.533f, -0.551f, 0.9093139f);
            MicroBlock_1.transform.localScale = new Vector3(0.1159296f, 0.1159296f, 0.1159296f);
            MicroBlock_2.transform.localPosition = new Vector3(-0.724f, -0.475f, 0.9093139f);
            MicroBlock_2.transform.localScale = new Vector3(0.09725131f, 0.09725131f, 0.09725131f);
            MicroBlock_3.transform.localPosition = new Vector3(-0.699f, -0.6259999f, 0.9093139f);
            MicroBlock_3.transform.localScale = new Vector3(0.1092711f, 0.1092711f, 0.1092711f);
            MicroBlock_4.transform.localPosition = new Vector3(-0.636f, -0.728f, 0.9093139f);
            MicroBlock_4.transform.localScale = new Vector3(0.1092711f, 0.1092711f, 0.1092711f);


            MicroBlock_Model.transform.localPosition = new Vector3(0, 0.2192179f, -0.06651523f);
            MicroBlock_Model.SetActive(true);

            OnDoorOpenCloseClicked(true);

            yield return new WaitForSeconds(1f);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(-0.32f, -0.55f, -1.21f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(-0.32f, -0.55f, -1.21f))
                    break;
            }

            MicroBlock_1.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                MicroBlock_1.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            MicroBlock_Sprite.SetActive(true);
            animatorMicroBlock.Play("MicroBlock_Animation");

            MicroBlock_2.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                MicroBlock_2.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                MicroBlock_Model.transform.localPosition = Vector3.MoveTowards(MicroBlock_Model.transform.localPosition, new Vector3(0f, 0.2192f, 0.222f), t);
                yield return null;

                if (MicroBlock_Model.transform.localPosition == new Vector3(0f, 0.2192f, 0.222f))
                    break;
            }


            MicroBlock_3.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                MicroBlock_3.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

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



            MicroBlock_3.SetActive(false);
            MicroBlock_4.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                MicroBlock_4.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                MicroBlock_Model.transform.localPosition = Vector3.MoveTowards(MicroBlock_Model.transform.localPosition, new Vector3(0, 0.2192179f, -0.06651523f), t);
                yield return null;

                if (MicroBlock_Model.transform.localPosition == new Vector3(0, 0.2192179f, -0.06651523f))
                    break;
            }

            // _BackPanel_LandScape.GetComponent<RectTransform>().localPosition = new Vector3(10f, -155f, 0);
            // _BackPanel_LandScape.SetActive(true);
        }
        else
        {
            MicroBlock_1.transform.localPosition = new Vector3(-0.531f, -0.5f, 0.9093139f);
            MicroBlock_1.transform.localScale = new Vector3(0.1174958f, 0.1174958f, 0.1174958f);
            MicroBlock_2.transform.localPosition = new Vector3(-0.697f, -0.414f, 0.9093139f);
            MicroBlock_2.transform.localScale = new Vector3(0.1174958f, 0.1174958f, 0.1174958f);
            MicroBlock_3.transform.localPosition = new Vector3(-0.678f, -0.575f, 0.9093139f);
            MicroBlock_3.transform.localScale = new Vector3(0.1174958f, 0.1174958f, 0.1174958f);
            MicroBlock_4.transform.localPosition = new Vector3(-0.607f, -0.778f, 0.9093139f);
            MicroBlock_4.transform.localScale = new Vector3(0.1174958f, 0.1174958f, 0.1174958f);


            MicroBlock_Model.transform.localPosition = new Vector3(0, 0.2192179f, -0.06651523f);
            MicroBlock_Model.SetActive(true);

            OnDoorOpenCloseClicked(true);

            yield return new WaitForSeconds(1f);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(-0.32f, -0.2f, -1.85f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(-0.32f, -0.2f, -1.85f))
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

            MicroBlock_Sprite.SetActive(true);
            animatorMicroBlock.Play("MicroBlock_Animation");

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                MicroBlock_Model.transform.localPosition = Vector3.MoveTowards(MicroBlock_Model.transform.localPosition, new Vector3(0f, 0.2192f, 0.222f), t);
                yield return null;

                if (MicroBlock_Model.transform.localPosition == new Vector3(0f, 0.2192f, 0.222f))
                    break;
            }


            MicroBlock_3.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                MicroBlock_3.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

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
                MicroBlock_Model.transform.localPosition = Vector3.MoveTowards(MicroBlock_Model.transform.localPosition, new Vector3(0, 0.2192179f, -0.06651523f), t);
                yield return null;

                if (MicroBlock_Model.transform.localPosition == new Vector3(0, 0.2192179f, -0.06651523f))
                    break;
            }

            // _BackPanel_Portrait.GetComponent<RectTransform>().localPosition = new Vector3(25f, -265f, 0);
            // _BackPanel_Portrait.SetActive(true);
        }

        _BackPanel_LandScape.GetComponent<RectTransform>().localPosition = new Vector3(10f, -155f, 0);
        _BackPanel_LandScape.SetActive(true);
        _BackPanel_Portrait.GetComponent<RectTransform>().localPosition = new Vector3(25f, -265f, 0);
        _BackPanel_Portrait.SetActive(true);
    }

    bool onMicroBlockClickedBool = false;

    public void OnMicroBlockClicked()
    {
        _SceneObject.transform.localScale = defaultScale;
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
        
    }




}
