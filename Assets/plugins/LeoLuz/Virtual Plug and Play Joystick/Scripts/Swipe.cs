using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using LeoLuz.PropertyAttributes;
using LeoLuz;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LeoLuz.PlugAndPlayJoystick
{
    [RequireComponent(typeof(Rect))]
    public class Swipe : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [InputAxesListDropdown]
        public string SwipeAxisHorizontal = "Horizontal";
        [InputAxesListDropdown]
        public string SwipeAxisVertical = "Vertical";
        private bool pressed;

#if UNITY_EDITOR
        private bool OrderOfScriptChanged;

        public void OnDrawGizmosSelected()
        {
            if (!OrderOfScriptChanged)
            {
                // Get the name of the script we want to change it's execution order
                string scriptName = typeof(Swipe).Name;

                // Iterate through all scripts (Might be a better way to do this?)
                foreach (MonoScript monoScript in MonoImporter.GetAllRuntimeMonoScripts())
                {
                    // If found our script
                    if (monoScript.name == scriptName)
                    {
                        MonoImporter.SetExecutionOrder(monoScript, -2000);
                    }
                }
                OrderOfScriptChanged = true;
            }
        }
#endif

        void Start()
        {
            Input.RegisterAxisMobile(SwipeAxisHorizontal);
            Input.RegisterAxisMobile(SwipeAxisVertical);

            Image image = GetComponent<Image>();
            Color _color = image.color;
            _color.a = 0f;
            image.color = _color;
        }

        public void Update()
        {
            if (pressed)
            {
                Vector2 ResultPosition;

                if (Input.touchCount > 0)
                {
                    Touch AnalogTouch = Input.GetTouch(Input.touchCount-1);
                        //verifica touchs
                    ResultPosition = (Vector2)AnalogTouch.position - StartPosition;
                } else
                {
                    return; 
                }

                Input.PressButtonMobile(SwipeAxisHorizontal);
                Input.PressButtonMobile(SwipeAxisVertical);
                Input.SetAxisMobile(SwipeAxisHorizontal, ResultPosition.x);
                Input.SetAxisMobile(SwipeAxisVertical, ResultPosition.y);
            }
        }

        public Vector2 StartPosition;
        public Vector2 AxisResult;

        public virtual void OnPointerDown(PointerEventData data)
        {
            //   print("DOWN");
            Input.PressButtonDownMobile(SwipeAxisHorizontal);
            Input.SetAxisMobile(SwipeAxisHorizontal, 0f);
            Input.PressButtonDownMobile(SwipeAxisVertical);
            Input.SetAxisMobile(SwipeAxisVertical, 0f);
            pressed = true;


                if (Input.touchCount > 0)
                {
                    Touch AnalogTouch = Input.GetTouch(Input.touchCount-1);
                        //verifica touchs
                    StartPosition = (Vector2)AnalogTouch.position - StartPosition;
                }
        }

        public virtual void OnPointerUp(PointerEventData data)
        {
            pressed = false;
            Vector2 ResultPosition;

            if (Input.touchCount > 0)
            {
                Touch AnalogTouch = Input.GetTouch(Input.touchCount-1);
                    //verifica touchs
                ResultPosition = (Vector2)AnalogTouch.position - StartPosition;
            } else
            {
                return;
            }

            this.AxisResult = ResultPosition;
            Input.PressButtonUpMobile(SwipeAxisHorizontal);
            Input.PressButtonUpMobile(SwipeAxisVertical);
            Input.SetAxisMobile(SwipeAxisHorizontal, ResultPosition.x);
            Input.SetAxisMobile(SwipeAxisVertical, ResultPosition.y);



        }
    }

}