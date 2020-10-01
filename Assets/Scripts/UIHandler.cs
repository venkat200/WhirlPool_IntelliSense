using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{

    [SerializeField]
    GameObject _Zappar_Camera, _InstantTracker, _Virtual_Camera;
    Vector3 initialPosition = new Vector3(0, 0, -2.2f);
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
    GameObject _FridgeModel;
    Animator animatorStartFridge;

    [SerializeField]
    GameObject _ProductName;


    // Start is called before the first frame update
    void Start()
    {
        animatorStartFridge = _FridgeModel.GetComponent<Animator>();
        animatorAITechSequence = _AITech_Sprite.GetComponent<Animator>();
        // animator3DFlow = _3DFlow_Sprite.GetComponent<Animator>();

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
        }
        else
        {
            _Panel_LandScape.SetActive(false);
            _Panel_Portrait.SetActive(true);
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
        yield return new WaitForSeconds(0.5f);

        animatorStartFridge.Play("StartFridge_Animation");

        yield return new WaitForSeconds(4f);

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
        _ProductName.SetActive(true);
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
            _ProductName.GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        _BottomPanel_LandScape.SetActive(true);
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
        PortableIce_Model.SetActive(false);

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
    }


    IEnumerator InfoButtonTransition()
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

    public void OnInfoButtonClicked()
    {
        InfoClicked = !InfoClicked;
        StartCoroutine(InfoButtonTransition());
    }


    [SerializeField]
    GameObject _MenuPanel_LandScape, _AITechnology_White_Button_LandScape, _AITechnology_Gold_Button_LandScape, _variableTemperature_White_Button_LandScape, _variableTemperature_Gold_Button_LandScape,
                                     _3DAir_White_Button_LandScape, _3DAir_Gold_Button_LandScape, _PortableIce_White_Button_LandScape, _PortableIce_Gold_Button_LandScape,
                                     _MicroBlock_White_Button_LandScape, _MicroBlock_Gold_Button_LandScape;
    public bool plusClicked = false;

    IEnumerator PlusButtonTransition()
    {
        if (plusClicked)
        {
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
        }
    }

    public void OnPlusButtonClicked()
    {
        // ResetActions();
        plusClicked = !plusClicked;
        if (plusClicked)
        {
            _Plus_Gold_LandScape.SetActive(true);
            _Plus_White_LandScape.SetActive(false);
            _CM_Gold_LandScape.SetActive(false);
            _CM_White_LandScape.SetActive(true);
            _Door_Gold_LandScape.SetActive(false);
            _Door_White_LandScape.SetActive(true);
        }
        else
        {
            _Plus_Gold_LandScape.SetActive(false);
            _Plus_White_LandScape.SetActive(true);
            _CM_Gold_LandScape.SetActive(false);
            _CM_White_LandScape.SetActive(true);
            _Door_Gold_LandScape.SetActive(false);
            _Door_White_LandScape.SetActive(true);
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
        }
    }



    public void BackButtonClicked()
    {
        _BottomPanel_LandScape.SetActive(true);
        _MenuPanel_LandScape.SetActive(true);
        _ProductName.SetActive(true);
        _InfoPanel_LandScape.SetActive(true);

        _BackPanel_LandScape.SetActive(false);
        StopAllCoroutines();
        // ResetPosition = true;
        StartCoroutine(BackTransition());

        _AITech_Sprite.SetActive(false);
        _AICallout_1.SetActive(false);
        _AICallout_2.SetActive(false);
        _AICallout_3.SetActive(false);


        // _3DFlow_Sprite.SetActive(false);
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




    }

    [SerializeField]
    GameObject _AITech_Sprite, _AICallout_1, _AICallout_2, _AICallout_3;
    Animator animatorAITechSequence;

    IEnumerator AITechnologyTransition()
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
        {
            _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(-0.32f, 0.36f, -1.38f), t);
            yield return null;

            if (_Virtual_Camera.transform.position == new Vector3(-0.32f, 0.36f, -1.38f))
                break;
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

    }

    public void OnAITechnologyClicked()
    {
        // ResetActions();

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

        _BackPanel_LandScape.SetActive(true);

        _BottomPanel_LandScape.SetActive(false);
        _MenuPanel_LandScape.SetActive(false);
        _InfoPanel_LandScape.SetActive(false);
        _ProductName.SetActive(false);

        StartCoroutine(AITechnologyTransition());
    }



    [SerializeField]
    GameObject _3DFlow_Sprite, _3DFlowCallout_1, _3DFlowCallout_2, _3DFlowCallout_3, _3DFlowCallout_4, _3DFlow_BowlModel;
    Animator animator3DFlow;

    IEnumerator _3DFlowTransition()
    {
        _3DFlow_BowlModel.SetActive(true);
        // animator3DFlow.Play("_3DFlow_Animation");

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

        _BackPanel_LandScape.SetActive(true);

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
            PortableIce_Model.transform.localPosition = Vector3.MoveTowards(PortableIce_Model.transform.localPosition, new Vector3(-0.211f, 0f, 0f), t);
            yield return null;

            if (PortableIce_Model.transform.localPosition == new Vector3(-0.211f, 0f, 0f))
                break;
        }


        PortableIce_3.SetActive(true);
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
            PortableIce_3.GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }
    }


    public void OnPortableIceClicked()
    {
        // ResetActions();

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

        _BackPanel_LandScape.SetActive(true);

        _BottomPanel_LandScape.SetActive(false);
        _MenuPanel_LandScape.SetActive(false);
        _InfoPanel_LandScape.SetActive(false);
        _ProductName.SetActive(false);

        StartCoroutine(PortableIceTransition());
    }





    [SerializeField]
    GameObject MicroBlock_1, MicroBlock_2, MicroBlock_3, MicroBlock_4, MicroBlock_Model;

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

        yield return new WaitForSeconds(2f);

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

        _BackPanel_LandScape.SetActive(true);

        _BottomPanel_LandScape.SetActive(false);
        _MenuPanel_LandScape.SetActive(false);
        _InfoPanel_LandScape.SetActive(false);
        _ProductName.SetActive(false);

        StartCoroutine(MicroBlockTransition());
    }




}
