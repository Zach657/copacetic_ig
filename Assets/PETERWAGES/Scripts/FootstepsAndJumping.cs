using System.Collections;
using UnityEngine;

/** 
 * Copyright (C) 2016 - Peter Wages & Unity
 **/

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class FootstepsAndJumping : MonoBehaviour
    {
        private float m_StepInterval;
        private float crouchingStepInterval = 0.6f;
        private float runningStepInterval = 0.3f;
        [SerializeField]
        private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField]
        private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        [SerializeField]
        private AudioClip m_LandSound;           // the sound played when character touches back on ground.
        private AudioSource m_AudioSource;
        private bool m_IsGrounded;
        private bool m_Crouching;

        // Peter Wages
        private bool m_PlayLandingSound = false;
        private bool StepsPlaying = false;
        private bool footstepsSpeedChange = false;
        private IEnumerator footStepsCoroutine;
        private ThirdPersonCharacter playerController;
        // End Peter Wages

        void Start()
        {
            m_AudioSource = GetComponent<AudioSource>();
            playerController = this.GetComponent<ThirdPersonCharacter>();
        }

        // Peter Wages
        private void Update()
        {
            m_IsGrounded = playerController.m_IsGrounded;
            bool previousState = m_Crouching;
            m_Crouching = playerController.m_Crouching;
            if (previousState != m_Crouching)
            {
                footstepsSpeedChange = true;
            }
            if (!m_IsGrounded && playerController.m_PlayJumpSound)
            {
                PlayJumpSound();
                playerController.m_PlayJumpSound = false;
                m_PlayLandingSound = true;
            }
            else if (m_IsGrounded && m_PlayLandingSound)
            {
                PlayLandingSound();
                m_PlayLandingSound = false;
            }
        }

        private void FixedUpdate()
        { 
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && (!StepsPlaying && !footstepsSpeedChange))
            {
                if (m_Crouching) m_StepInterval = crouchingStepInterval;
                else m_StepInterval = runningStepInterval;
                InvokeRepeating("PlayFootStepAudio", 0f, m_StepInterval);
                StepsPlaying = true;
            }
            else if ((!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && StepsPlaying) || (footstepsSpeedChange && StepsPlaying))
            {
                CancelInvoke("PlayFootStepAudio");
                StepsPlaying = false;
                footstepsSpeedChange = false;
            }
        }
        // Peter Wages

        private void PlayJumpSound()
        {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
        }

        private void PlayLandingSound()
        {
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.Play();
        }

        private void PlayFootStepAudio()
        {
            if (!m_IsGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, m_FootstepSounds.Length);
            m_AudioSource.clip = m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            m_FootstepSounds[n] = m_FootstepSounds[0];
            m_FootstepSounds[0] = m_AudioSource.clip;
        }
    }
}