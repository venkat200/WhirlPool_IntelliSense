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


    // Start is called before the first frame update
    void Start()
    {
        animatorStartFridge = _FridgeModel.GetComponent<Animator>();
        animatorAITechSequence = _AITech_Sprite.GetComponent<Animator>();
        animator3DFlow = _3DFlow_Sprite.GetComponent<Animator>();

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

    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.width > Screen.height || ARView)
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
            yield return new WaitForSeconds(0.5f);

            animatorStartFridge.Play("StartFridge_Animation");

            yield return new WaitForSeconds(4f);

            _ProductName_Portrait.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _ProductName_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }

            yield return new WaitForSeconds(1f);

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
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
        {
            _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, initialPosition, t);
            yield return null;

            if (_Virtual_Camera.transform.position == initialPosition)
                break;
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
        }
        else
        {
            _Zappar_Camera.SetActive(ARView);
            _InstantTracker.SetActive(ARView);
            _Virtual_Camera.SetActive(VirtualView);
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
            yield return new WaitForSeconds(0.1f);
            _variableTemperature_Gold_Button_LandScape.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            _3DAir_Gold_Button_LandScape.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            _PortableIce_Gold_Button_LandScape.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            _MicroBlock_Gold_Button_LandScape.SetActive(true);

            _MenuPanel_Portrait.SetActive(true);

            _AITechnology_Gold_Button_Portrait.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            _variableTemperature_Gold_Button_Portrait.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            _3DAir_Gold_Button_Portrait.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            _PortableIce_Gold_Button_Portrait.SetActive(true);
            yield return new WaitForSeconds(0.1f);
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
                    _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(0.25f, 0f, -2.2f), t);
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

    public void CMButtonClicked()
    {

    }


    bool doorOpen = false;

    public void OnDoorOpenCloseClicked()
    {
        // ResetActions(false);
        doorOpen = !doorOpen;

        if (doorOpen)
        {
            animatorStartFridge.Play("DoorOpen_Animation");

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
        else
        {
            animatorStartFridge.Play("DoorClose_Animation");

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



    public void BackButtonClicked()
    {
        if(Screen.width > Screen.height || ARView)
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
            OnDoorOpenCloseClicked();
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

    }

    [SerializeField]
    GameObject _AITech_Sprite, _AICallout_1, _AICallout_2, _AICallout_3;
    Animator animatorAITechSequence;

    IEnumerator AITechnologyTransition()
    {
        if(Screen.width > Screen.height || ARView)
        {
            _AICallout_1.transform.localPosition = new Vector3(-1.175f, 0.77f, 1.370314f);
            _AICallout_1.transform.localScale = new Vector3(0.1969543f, 0.1969543f, 0.1969543f);
            _AICallout_2.transform.localPosition = new Vector3(-1.025f, 0.549f, 1.370314f);
            _AICallout_2.transform.localScale = new Vector3(0.2502712f, 0.2502712f, 0.2502712f);
            _AICallout_3.transform.localPosition = new Vector3(-1.177f, 0.051f, 1.370314f);
            _AICallout_3.transform.localScale = new Vector3(0.2213244f, 0.2213244f, 0.2213244f);

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

        _BackPanel_LandScape.SetActive(true);
        _BackPanel_Portrait.SetActive(true);

    }

    public void OnAITechnologyClicked()
    {
        // ResetActions();
        if(Screen.width > Screen.height || ARView)
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



    [SerializeField]
    GameObject _3DFlow_Sprite, _3DFlowCallout_1, _3DFlowCallout_2, _3DFlowCallout_3, _3DFlowCallout_4, _3DFlow_BowlModel;
    Animator animator3DFlow;

    IEnumerator _3DFlowTransition()
    {
        if(Screen.width > Screen.height || ARView)
        {
            _3DFlowCallout_1.transform.localPosition = new Vector3(-0.631f, 0.282f, 0.318f);
            _3DFlowCallout_1.transform.localScale = new Vector3(0.217749f, 0.217749f, 0.217749f);
            _3DFlowCallout_2.transform.localPosition = new Vector3(-0.959f, 0.442f, 0.318f);
            _3DFlowCallout_2.transform.localScale = new Vector3(0.1807317f, 0.1807317f, 0.1807317f);
            _3DFlowCallout_3.transform.localPosition = new Vector3(-0.857f, 0.03199999f, 0.318f);
            _3DFlowCallout_3.transform.localScale = new Vector3(0.217749f, 0.217749f, 0.217749f);
            _3DFlowCallout_4.transform.localPosition = new Vector3(-0.804f, - 0.097f, 0.318f);
            _3DFlowCallout_4.transform.localScale = new Vector3(0.2213244f, 0.2213244f, 0.2213244f);


            _3DFlow_BowlModel.SetActive(true);
            _3DFlow_Sprite.SetActive(true);
            animator3DFlow.Play("_3DFlow_Animation");

            OnDoorOpenCloseClicked();

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

            _BackPanel_LandScape.SetActive(true);
            _MenuPanel_Portrait.SetActive(false);
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

            OnDoorOpenCloseClicked();

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

            _BackPanel_Portrait.SetActive(true);

        }

        

    }


    public void On3DFlowClicked()
    {
        // ResetActions();

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

        StartCoroutine(_3DFlowTransition());
    }





    [SerializeField]
    GameObject PortableIce_1, PortableIce_2, PortableIce_3,PortableIce_Model;
    
    IEnumerator PortableIceTransition()
    {
        PortableIce_Model.transform.localPosition = new Vector3(0, 0, 0);
        PortableIce_Model.SetActive(true);

        OnDoorOpenCloseClicked();

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

        _BackPanel_LandScape.SetActive(true);
    }


    public void OnPortableIceClicked()
    {
        ResetActions();

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

        StartCoroutine(PortableIceTransition());
    }





    [SerializeField]
    GameObject MicroBlock_1, MicroBlock_2, MicroBlock_3, MicroBlock_4, MicroBlock_Model;
    [SerializeField]
    GameObject _TrayObject_1, _TrayObject_2, _TrayObject_3, _TrayObject_4, _TrayObject_5, _TrayObject_6, _TrayObject_7, _TrayObject_8;

    IEnumerator MicroBlockTransition()
    {
        MicroBlock_Model.transform.localPosition = new Vector3(0, 0.2192179f, -0.06651523f);
        MicroBlock_Model.SetActive(true);

        OnDoorOpenCloseClicked();

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


        yield return new WaitForSeconds(0.5f);
        _TrayObject_1.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _TrayObject_2.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _TrayObject_3.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _TrayObject_4.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _TrayObject_5.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _TrayObject_6.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _TrayObject_7.SetActive(true);
        yield return new WaitForSeconds(0.5f);
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

        _BackPanel_LandScape.SetActive(true);
    }


    public void OnMicroBlockClicked()
    {
        // ResetActions();

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

        StartCoroutine(MicroBlockTransition());
    }




}
