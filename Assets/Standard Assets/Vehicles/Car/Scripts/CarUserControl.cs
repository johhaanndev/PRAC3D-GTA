using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use

        public bool isDriving = false;
        private float minimumTimeToLeave = 1f;
        private float timer = 0;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }


        private void FixedUpdate()
        {
            Debug.Log("CarUserControl isDriving: " + isDriving);
            if (isDriving == true)
            {
                timer += Time.deltaTime;

                Debug.Log("you can drive");
                // pass the input to the car!
                float h = CrossPlatformInputManager.GetAxis("Horizontal");
                float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
                float handbrake = CrossPlatformInputManager.GetAxis("Jump");
                m_Car.Move(h, v, v, handbrake);
#else
                m_Car.Move(h, v, v, 0f);
#endif
                if (timer >= minimumTimeToLeave)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        timer = 0;
                        Debug.Log("Leave car");
                        h = 0;
                        v = 0;
                        isDriving = false;
                    }
                }
            }
        }
    }
}
