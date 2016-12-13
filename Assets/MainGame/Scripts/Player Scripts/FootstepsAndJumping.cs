using System.Collections;
using UnityEngine;

/** 
 * Copyright (C) 2016 - Peter Wages & Unity
 **/

    public class FootstepsAndJumping : MonoBehaviour
    {
        private float stepInterval;
        private float crouchingStepInterval = 0.6f;
        private float runningStepInterval = 0.3f;
        [SerializeField]
        private AudioClip[] footstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField]
        private AudioClip jumpSound;           // the sound played when character leaves the ground.
        [SerializeField]
        private AudioClip landSound;           // the sound played when character touches back on ground.
        private AudioSource audioSource;
        private bool isGrounded;
        private bool crouching;

        // Peter Wages
        private bool playLandingSound = false;
        private bool stepsPlaying = false;
        private bool footstepsSpeedChange = false;
        private IEnumerator footStepsCoroutine;
        private CharacterController playerController;
        private FPSInput input;
    // End Peter Wages

    void Start()
        {
            audioSource = GetComponent<AudioSource>();
            playerController = GetComponent<CharacterController>();
        input = GetComponent<FPSInput>();
        }

        // Peter Wages
        private void Update()
        {
            isGrounded = playerController.isGrounded;
            bool previousState = crouching;
            crouching = input.crouching;
            if (previousState != crouching)
            {
                footstepsSpeedChange = true;
            }
            if (!isGrounded && input.playJumpSound)
            {
                PlayJumpSound();
                input.playJumpSound = false;
                playLandingSound = true;
            }
            else if (isGrounded && playLandingSound)
            {
                PlayLandingSound();
                playLandingSound = false;
            }
        }

        private void FixedUpdate()
        { 
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && (!stepsPlaying && !footstepsSpeedChange))
            {
                if (crouching) stepInterval = crouchingStepInterval;
                else stepInterval = runningStepInterval;
                InvokeRepeating("PlayFootStepAudio", 0f, stepInterval);
                stepsPlaying = true;
            }
            else if ((!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && stepsPlaying) || (footstepsSpeedChange && stepsPlaying))
            {
                CancelInvoke("PlayFootStepAudio");
                stepsPlaying = false;
                footstepsSpeedChange = false;
            }
        }
        // Peter Wages

        private void PlayJumpSound()
        {
            audioSource.clip = jumpSound;
            audioSource.Play();
        }

        private void PlayLandingSound()
        {
            audioSource.clip = landSound;
            audioSource.Play();
        }

        private void PlayFootStepAudio()
        {
            if (!isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, footstepSounds.Length);
            audioSource.clip = footstepSounds[n];
            audioSource.PlayOneShot(audioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            footstepSounds[n] = footstepSounds[0];
            footstepSounds[0] = audioSource.clip;
        }
    }