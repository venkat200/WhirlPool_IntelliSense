using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowControl : MonoBehaviour
{
    [SerializeField]
    GameObject Plus_GlowWhite_LandScape, Plus_GlowGold_LandScape, CMS_GlowWhite_LandScape, CMS_GlowGold_LandScape,
               TopDoor_GlowWhite_LandScape, TopDoor_GlowGold_LandScape, BottomDoor_GlowWhite_LandScape, BottomDoor_GlowGold_LandScape;
    [SerializeField]
    GameObject AITech_GlowWhite_LandScape, AITech_GlowGold_LandScape, VTZ_GlowWhite_LandScape, VTZ_GlowGold_LandScape, _3DFlow_GlowWhite_LandScape, _3DFlow_GlowGold_LandScape, 
               Portable_GlowWhite_LandScape, Portable_GlowGold_LandScape, Micro_GlowWhite_LandScape, Micro_GlowGold_LandScape;
    [SerializeField]
    GameObject Usage_GlowWhite_LandScape, Usage_GlowGold_LandScape, Weather_GlowWhite_LandScape, Weather_GlowGold_LandScape, Load_GlowWhite_LandScape, Load_GlowGold_LandScape;

    [SerializeField]
    GameObject Plus_GlowWhite_Portrait, Plus_GlowGold_Portrait, CMS_GlowWhite_Portrait, CMS_GlowGold_Portrait,
               TopDoor_GlowWhite_Portrait, TopDoor_GlowGold_Portrait, BottomDoor_GlowWhite_Portrait, BottomDoor_GlowGold_Portrait;
    [SerializeField]
    GameObject AITech_GlowWhite_Portrait, AITech_GlowGold_Portrait, VTZ_GlowWhite_Portrait, VTZ_GlowGold_Portrait, _3DFlow_GlowWhite_Portrait, _3DFlow_GlowGold_Portrait,
               Portable_GlowWhite_Portrait, Portable_GlowGold_Portrait, Micro_GlowWhite_Portrait, Micro_GlowGold_Portrait;
    [SerializeField]
    GameObject Usage_GlowWhite_Portrait, Usage_GlowGold_Portrait, Weather_GlowWhite_Portrait, Weather_GlowGold_Portrait, Load_GlowWhite_Portrait, Load_GlowGold_Portrait;



    // Start is called before the first frame update
    void Start()
    {
        PlusGlow();
        CMSGlow();
        TopDoorGlow();
        BottomDoorGlow();
        AITechnologyGlow();
        VTZoneGlow();
        _3DAirFlowGlow();
        PortableTrayGlow();
        MicroBlockGlow();
        UsageGlow();
        WeatherGlow();
        LoadGlow();
    }

    // Update is called once per frame
    void Update()
    {
    }



    bool plusGlowClicked = false, plusGlow= false;
    Coroutine PlusCoroutine;
    IEnumerator PlusGlowTransition()
    {
        while (plusGlow)
        {
            Plus_GlowWhite_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Plus_GlowGold_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Plus_GlowWhite_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Plus_GlowGold_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                Plus_GlowWhite_LandScape.GetComponent<Image>().color = newColor;
                Plus_GlowGold_LandScape.GetComponent<Image>().color = newColor;
                Plus_GlowWhite_Portrait.GetComponent<Image>().color = newColor;
                Plus_GlowGold_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
                Plus_GlowWhite_LandScape.GetComponent<Image>().color = newColor;
                Plus_GlowGold_LandScape.GetComponent<Image>().color = newColor;
                Plus_GlowWhite_Portrait.GetComponent<Image>().color = newColor;
                Plus_GlowGold_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }
        }
    }

    public void PlusGlow()
    {
        plusGlowClicked = !plusGlowClicked;
        plusGlow = true;

        StartCoroutine(PlusGlowTransition());
    }



    bool cmsGlowClicked = false, cmsGlow = false;
    Coroutine CMSCoroutine;
    IEnumerator CMSGlowTransition()
    {
        while (cmsGlow)
        {
            CMS_GlowWhite_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            CMS_GlowGold_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            CMS_GlowWhite_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            CMS_GlowGold_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                CMS_GlowWhite_LandScape.GetComponent<Image>().color = newColor;
                CMS_GlowGold_LandScape.GetComponent<Image>().color = newColor;
                CMS_GlowWhite_Portrait.GetComponent<Image>().color = newColor;
                CMS_GlowGold_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
                CMS_GlowWhite_LandScape.GetComponent<Image>().color = newColor;
                CMS_GlowGold_LandScape.GetComponent<Image>().color = newColor;
                CMS_GlowWhite_Portrait.GetComponent<Image>().color = newColor;
                CMS_GlowGold_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }
        }
    }

    public void CMSGlow()
    {
        cmsGlowClicked = !cmsGlowClicked;
        cmsGlow = true;

        StartCoroutine(CMSGlowTransition());
    }



    bool topDoorGlowClicked = false, topDoorGlow = false;
    Coroutine TopDoorCoroutine;
    IEnumerator TopDoorGlowTransition()
    {
        while (topDoorGlow)
        {
            TopDoor_GlowWhite_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            TopDoor_GlowGold_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            TopDoor_GlowWhite_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            TopDoor_GlowGold_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                TopDoor_GlowWhite_LandScape.GetComponent<Image>().color = newColor;
                TopDoor_GlowGold_LandScape.GetComponent<Image>().color = newColor;
                TopDoor_GlowWhite_Portrait.GetComponent<Image>().color = newColor;
                TopDoor_GlowGold_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
                TopDoor_GlowWhite_LandScape.GetComponent<Image>().color = newColor;
                TopDoor_GlowGold_LandScape.GetComponent<Image>().color = newColor;
                TopDoor_GlowWhite_Portrait.GetComponent<Image>().color = newColor;
                TopDoor_GlowGold_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }
        }
    }

    public void TopDoorGlow()
    {
        topDoorGlowClicked = !topDoorGlowClicked;
        topDoorGlow = true;

        StartCoroutine(TopDoorGlowTransition());
    }



    bool bottomDoorGlowClicked = false, bottomDoorGlow = false;
    Coroutine BottomDoorCoroutine;
    IEnumerator BottomDoorGlowTransition()
    {
        while (bottomDoorGlow)
        {
            BottomDoor_GlowWhite_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            BottomDoor_GlowGold_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            BottomDoor_GlowWhite_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            BottomDoor_GlowGold_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                BottomDoor_GlowWhite_LandScape.GetComponent<Image>().color = newColor;
                BottomDoor_GlowGold_LandScape.GetComponent<Image>().color = newColor;
                BottomDoor_GlowWhite_Portrait.GetComponent<Image>().color = newColor;
                BottomDoor_GlowGold_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
                BottomDoor_GlowWhite_LandScape.GetComponent<Image>().color = newColor;
                BottomDoor_GlowGold_LandScape.GetComponent<Image>().color = newColor;
                BottomDoor_GlowWhite_Portrait.GetComponent<Image>().color = newColor;
                BottomDoor_GlowGold_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }
        }
    }

    public void BottomDoorGlow()
    {
        bottomDoorGlowClicked = !bottomDoorGlowClicked;
        bottomDoorGlow = true;

        StartCoroutine(BottomDoorGlowTransition());
    }



    bool AITechGlowClicked = false, AITechGlow = false;
    Coroutine AITechnologyCoroutine;
    IEnumerator AITechnologyGlowTransition()
    {
        while (AITechGlow)
        {
            AITech_GlowWhite_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            AITech_GlowGold_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            AITech_GlowWhite_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            AITech_GlowGold_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);            
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                AITech_GlowWhite_LandScape.GetComponent<Image>().color = newColor;
                AITech_GlowGold_LandScape.GetComponent<Image>().color = newColor;
                AITech_GlowWhite_Portrait.GetComponent<Image>().color = newColor;
                AITech_GlowGold_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
                AITech_GlowWhite_LandScape.GetComponent<Image>().color = newColor;
                AITech_GlowGold_LandScape.GetComponent<Image>().color = newColor;
                AITech_GlowWhite_Portrait.GetComponent<Image>().color = newColor;
                AITech_GlowGold_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }
        }
    }

    public void AITechnologyGlow()
    {
        AITechGlowClicked = !AITechGlowClicked;
        AITechGlow = true;

        StartCoroutine(AITechnologyGlowTransition());
    }



    bool VTZGlowClicked = false, VTZGlow = false;
    Coroutine VTZoneCoroutine;
    IEnumerator VTZoneGlowTransition()
    {
        while (VTZGlow)
        {
            VTZ_GlowWhite_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            VTZ_GlowGold_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            VTZ_GlowWhite_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            VTZ_GlowGold_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);            
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                VTZ_GlowWhite_LandScape.GetComponent<Image>().color = newColor;
                VTZ_GlowGold_LandScape.GetComponent<Image>().color = newColor;
                VTZ_GlowWhite_Portrait.GetComponent<Image>().color = newColor;
                VTZ_GlowGold_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
                VTZ_GlowWhite_LandScape.GetComponent<Image>().color = newColor;
                VTZ_GlowGold_LandScape.GetComponent<Image>().color = newColor;
                VTZ_GlowWhite_Portrait.GetComponent<Image>().color = newColor;
                VTZ_GlowGold_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }
        }
    }

    public void VTZoneGlow()
    {
        VTZGlowClicked = !VTZGlowClicked;
        VTZGlow = true;

        StartCoroutine(VTZoneGlowTransition());
    }



    bool _3DFlowGlowClicked = false, _3DFlowGlow = false;
    Coroutine _3DAirFlowCoroutine;
    IEnumerator _3DAirFlowGlowTransition()
    {
        while (_3DFlowGlow)
        {
            _3DFlow_GlowWhite_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            _3DFlow_GlowGold_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            _3DFlow_GlowWhite_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            _3DFlow_GlowGold_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);            
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _3DFlow_GlowWhite_LandScape.GetComponent<Image>().color = newColor;
                _3DFlow_GlowGold_LandScape.GetComponent<Image>().color = newColor;
                _3DFlow_GlowWhite_Portrait.GetComponent<Image>().color = newColor;
                _3DFlow_GlowGold_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
                _3DFlow_GlowWhite_LandScape.GetComponent<Image>().color = newColor;
                _3DFlow_GlowGold_LandScape.GetComponent<Image>().color = newColor;
                _3DFlow_GlowWhite_Portrait.GetComponent<Image>().color = newColor;
                _3DFlow_GlowGold_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }
        }
    }

    public void _3DAirFlowGlow()
    {
        _3DFlowGlowClicked = !_3DFlowGlowClicked;
        _3DFlowGlow = true;

        StartCoroutine(_3DAirFlowGlowTransition());
    }



    bool PortableGlowClicked = false, PortableGlow = false;
    Coroutine PortableTrayCoroutine;
    IEnumerator PortableTrayGlowTransition()
    {
        while (PortableGlow)
        {
            Portable_GlowWhite_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Portable_GlowGold_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Portable_GlowWhite_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Portable_GlowGold_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);            
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                Portable_GlowWhite_LandScape.GetComponent<Image>().color = newColor;
                Portable_GlowGold_LandScape.GetComponent<Image>().color = newColor;
                Portable_GlowWhite_Portrait.GetComponent<Image>().color = newColor;
                Portable_GlowGold_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
                Portable_GlowWhite_LandScape.GetComponent<Image>().color = newColor;
                Portable_GlowGold_LandScape.GetComponent<Image>().color = newColor;
                Portable_GlowWhite_Portrait.GetComponent<Image>().color = newColor;
                Portable_GlowGold_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }
        }
    }

    public void PortableTrayGlow()
    {
        PortableGlowClicked = !PortableGlowClicked;
        PortableGlow = true;

        StartCoroutine(PortableTrayGlowTransition());
    }



    bool MicroGlowClicked = false, MicroGlow = false;
    Coroutine MicroBlockCoroutine;
    IEnumerator MicroBlockGlowTransition()
    {
        while (MicroGlow)
        {
            Micro_GlowWhite_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Micro_GlowGold_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Micro_GlowWhite_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Micro_GlowGold_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);            
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                Micro_GlowWhite_LandScape.GetComponent<Image>().color = newColor;
                Micro_GlowGold_LandScape.GetComponent<Image>().color = newColor;
                Micro_GlowWhite_Portrait.GetComponent<Image>().color = newColor;
                Micro_GlowGold_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
                Micro_GlowWhite_LandScape.GetComponent<Image>().color = newColor;
                Micro_GlowGold_LandScape.GetComponent<Image>().color = newColor;
                Micro_GlowWhite_Portrait.GetComponent<Image>().color = newColor;
                Micro_GlowGold_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }
        }
    }

    public void MicroBlockGlow()
    {
        MicroGlowClicked = !MicroGlowClicked;
        MicroGlow = true;

        StartCoroutine(MicroBlockGlowTransition());
    }




    bool UsageGlowClicked = false, usageGlow = false;
    Coroutine UsageCoroutine;
    IEnumerator UsageGlowTransition()
    {
        while (usageGlow)
        {
            Usage_GlowWhite_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Usage_GlowGold_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Usage_GlowWhite_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Usage_GlowGold_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                Usage_GlowWhite_LandScape.GetComponent<Image>().color = newColor;
                Usage_GlowGold_LandScape.GetComponent<Image>().color = newColor;
                Usage_GlowWhite_Portrait.GetComponent<Image>().color = newColor;
                Usage_GlowGold_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
                Usage_GlowWhite_LandScape.GetComponent<Image>().color = newColor;
                Usage_GlowGold_LandScape.GetComponent<Image>().color = newColor;
                Usage_GlowWhite_Portrait.GetComponent<Image>().color = newColor;
                Usage_GlowGold_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }
        }
    }

    public void UsageGlow()
    {
        UsageGlowClicked = !UsageGlowClicked;
        usageGlow = true;

        StartCoroutine(UsageGlowTransition());
    }



    bool WeatherGlowClicked = false, weatherGlow = false;
    Coroutine WeatherCoroutine;
    IEnumerator WeatherGlowTransition()
    {
        while (weatherGlow)
        {
            Weather_GlowWhite_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Weather_GlowGold_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Weather_GlowWhite_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Weather_GlowGold_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                Weather_GlowWhite_LandScape.GetComponent<Image>().color = newColor;
                Weather_GlowGold_LandScape.GetComponent<Image>().color = newColor;
                Weather_GlowWhite_Portrait.GetComponent<Image>().color = newColor;
                Weather_GlowGold_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
                Weather_GlowWhite_LandScape.GetComponent<Image>().color = newColor;
                Weather_GlowGold_LandScape.GetComponent<Image>().color = newColor;
                Weather_GlowWhite_Portrait.GetComponent<Image>().color = newColor;
                Weather_GlowGold_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }
        }
    }

    public void WeatherGlow()
    {
        WeatherGlowClicked = !WeatherGlowClicked;
        weatherGlow = true;

        StartCoroutine(WeatherGlowTransition());
    }



    bool LoadGlowClicked = false, loadGlow = false;
    Coroutine LoadCoroutine;
    IEnumerator LoadGlowTransition()
    {
        while (loadGlow)
        {
            Load_GlowWhite_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Load_GlowGold_LandScape.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Load_GlowWhite_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Load_GlowGold_Portrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                Load_GlowWhite_LandScape.GetComponent<Image>().color = newColor;
                Load_GlowGold_LandScape.GetComponent<Image>().color = newColor;
                Load_GlowWhite_Portrait.GetComponent<Image>().color = newColor;
                Load_GlowGold_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
                Load_GlowWhite_LandScape.GetComponent<Image>().color = newColor;
                Load_GlowGold_LandScape.GetComponent<Image>().color = newColor;
                Load_GlowWhite_Portrait.GetComponent<Image>().color = newColor;
                Load_GlowGold_Portrait.GetComponent<Image>().color = newColor;
                yield return null;
            }
        }
    }

    public void LoadGlow()
    {
        LoadGlowClicked = !LoadGlowClicked;
        loadGlow = true;

        StartCoroutine(LoadGlowTransition());
    }
}
