using System.Collections;
using UnityEngine;

/** 
 * Copyright (C) 2016 - Peter Wages & Unity - Third Person Character
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

        //Gets needed components
    void Start()
        {
            audioSource = GetComponent<AudioSource>();
            playerController = GetComponent<CharacterController>();
        input = GetComponent<FPSInput>();
        }

        // Peter Wages
        // Calculates when to change footstep speed
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
            SceneController.PlaySoundAtPoint(jumpSound, Utilities.playerCharacter.transform.position);
                input.playJumpSound = false;
                playLandingSound = true;
            }
            else if (isGrounded && playLandingSound)
            {
            SceneController.PlaySoundAtPoint(landSound, Utilities.playerCharacter.transform.position);
            playLandingSound = false;
            }
        }

    // Detects if player is walking and needs to change speed
        private void FixedUpdate()
        { 
            if (PlayerWalking() && (!stepsPlaying && !footstepsSpeedChange))
            {
                if (crouching) stepInterval = crouchingStepInterval;
                else stepInterval = runningStepInterval;
                InvokeRepeating("PlayFootStepAudio", 0f, stepInterval);
                stepsPlaying = true;
            }
            else if (!(PlayerWalking() && stepsPlaying) || (footstepsSpeedChange && stepsPlaying))
            {
                CancelInvoke("PlayFootStepAudio");
                stepsPlaying = false;
                footstepsSpeedChange = false;
            }
        }
        // Peter Wages

    // Plays footstep sounds
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

    // Checks if player is walking
    private bool PlayerWalking()
    {
        return ((Input.GetAxis("Horizontal") > 0 || (Input.GetAxis("Vertical") > 0)));
    }
    }