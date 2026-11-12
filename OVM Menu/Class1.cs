using System;
using UnityEngine;
using HarmonyLib;
using BepInEx;

namespace OVM_Menu
{
    [BepInPlugin("com.OceanView", "VerityMod", "0.1.9")]
    public class Class1 :   BaseUnityPlugin
    {
        bool menuimput;
        bool isMenuCreated;

        GameObject menuObj;
        GameObject btnObj;

        void Awake()
        {
            Harmony harmony = new Harmony("OVM.Menu");
            harmony.PatchAll();

            Debug.Log("Your mod has successfully loaded");
        }

        void Start()
        {
            isMenuCreated = false;
        }

        void Update()
        {
            menuimput = ControllerInputPoller.instance.leftControllerPrimaryButton;

            if (menuimput && !isMenuCreated)
            {
                CreateMenu();
            }
            else if (menuimput && isMenuCreated)
            {
                DestroyMenu();
            } 
        }

        void CreateMenu()
        {
            GorillaLocomotion.GTPlayer player = GorillaLocomotion.GTPlayer.Instance;

            isMenuCreated = true;

            Transform leftControllerTransform = player.GetControllerTransform(true);

            menuObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            menuObj.transform.parent = leftControllerTransform;
            menuObj.transform.position = leftControllerTransform.position;
            menuObj.transform.rotation = leftControllerTransform.rotation;

            menuObj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            menuObj.transform.localPosition = new Vector3(0f, 0f, -0.08f);


            Renderer rend = menuObj.GetComponent<Renderer>();
            rend.material.color = Color.yellow;
            rend.material.shader = Shader.Find("GorillaTag/UberShader");

            GameObject.Destroy(menuObj.GetComponent<Collider>());
        }

        void DestroyMenu()
        {
            GameObject.Destroy(menuObj);
            isMenuCreated = false;
        }
    }
}
