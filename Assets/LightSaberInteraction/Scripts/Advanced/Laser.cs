using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace LightSaber.advanced
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] private GameObject laserRoot;
        [SerializeField] private float resizeSpeed = 0.1f;
        [SerializeField] private AudioClip laserOnFbx;

        private Vector3 fullLaserSize;
        private AudioSource audioSource;
        private XRBaseInteractor grabbingController;

        public bool LaserOn { get; set; }

        private void Start()
        {
            InitializeLaser();
        }

        private void Update()
        {
            AdjustLaserScale();
            CheckControllerVelocity();
        }

        private void InitializeLaser()
        {
            fullLaserSize = laserRoot.transform.localScale;
            laserRoot.transform.localScale = new Vector3(fullLaserSize.x, 0f, fullLaserSize.z);

            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1;
        }

        private void OnSelectEntered(SelectEnterEventArgs arg0)
        {
            grabbingController = (XRBaseInteractor)arg0.interactorObject;
        }

        private void AdjustLaserScale()
        {
            if (LaserOn && laserRoot.transform.localScale.y < fullLaserSize.y)
            {
                IncreaseLaserScale();
            }
            else if (!LaserOn && laserRoot.transform.localScale.y > 0)
            {
                DecreaseLaserScale();
            }
            else
            {
                return;
            }
        }

        private void IncreaseLaserScale()
        {
            laserRoot.transform.localScale += new Vector3(0f, Time.deltaTime * resizeSpeed, 0f);
            PlayLaserSound(laserOnFbx);
        }

        private void DecreaseLaserScale()
        {
            laserRoot.transform.localScale -= new Vector3(0f, Time.deltaTime * resizeSpeed, 0f);
            PlayLaserSound(laserOnFbx);
        }

        private void PlayLaserSound(AudioClip audioClip)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(audioClip);
            }
        }

        private void CheckControllerVelocity()
        {
            if (grabbingController != null)
            {
                var xrController = grabbingController.GetComponent<XRController>();
                if (xrController != null)
                {
                    //Vector3 controllerAngularVelocity = xrController.velocity;
                    //Debug.Log("Controller Angular Velocity: " + controllerAngularVelocity);
                    // Perform further actions with the controller's angular velocity as needed.
                }
            }
        }

        private void OnSelectEntered(XRBaseInteractor interactor)
        {
            grabbingController = interactor;
        }
    }
}
