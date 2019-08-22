using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CameraControl : MonoBehaviour
{
	[Header("Target Players")]
	public GameObject targetPlayer1;
	public GameObject targetPlayer2;
	public GameObject targetPlayer3;
	public GameObject targetPlayer4;
	private Move player1, player2, player3, player4;

	[Header("Camera Stuff")]
	public GameObject cam1;
	public GameObject trueTarget;
	public GameObject targetZoom;
	[Range(0f, 1f)]public float cameraSmooth;

	private float desiredDistance = 3f;
	private float pitch = 0f;                                   // controls up and down
	private float pitchMin = -10f;
	private float pitchMax = 60f;
	private float yaw = 0f;                                     // controls side to side
	private float roll = 0f;                                    // controls camera rotation
	[Range(1f, 15f)] public float sensitivity = 5f;             // create the sensitivity of the mouse

    public GameObject UI;                                       // to hid UI while the video is playing

    //for playing characters intro
    public VideoClip videoClipAva, videoClipHazMad, videoClipRox, videoClipTempest;
    public VideoPlayer videoPlayer;
    private bool avaPlayed = false, hazmadPlayed = false, roxPlayed = false, tempestPlayed = false;
   
    [SerializeField] private bool isPlayingVideo = false;

    public PlayerInControl playerInControl;

	void Awake()
	{
		trueTarget = targetPlayer1;
		playerInControl = trueTarget.GetComponent<Move>().inControl;
		cam1.SetActive(true);

		player1 = targetPlayer1.GetComponent<Move>();
		player2 = targetPlayer2.GetComponent<Move>();
		player3 = targetPlayer3.GetComponent<Move>();
		player4 = targetPlayer4.GetComponent<Move>();

        //videoPlayer = GetComponent<VideoPlayer>();

		player1.isControlling = true;
	}

	void Update()
	{
        if(isPlayingVideo == false)
        {
            // multiplying these movements by the sensitivity which let's us control how much moving to the mouse effects the camera angle.
            pitch -= sensitivity * Input.GetAxis("Mouse Y");
            yaw += sensitivity * Input.GetAxis("Mouse X");

            // Clamp prevents a value from going below a minimum or above a maximum value.
            pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

            transform.localEulerAngles = new Vector3(pitch, yaw, roll);

            if (Input.GetMouseButton(1))
            {
                transform.position = targetZoom.transform.position;
            }

            // Section handling character swapping
            if (Input.GetKeyDown("1"))
            {
                //plays cinematic when ava is used for the first time
                if (!avaPlayed)
                {
                    StartCoroutine(CoWait(videoClipAva));

                    avaPlayed = true;
                }

                cam1.SetActive(true);
                trueTarget = targetPlayer1;
                playerInControl = trueTarget.GetComponent<Move>().inControl;

                SwapCharacters(playerInControl);
            }
            else if (Input.GetKeyDown("2"))
            {
                //plays cinematic when hazmad is used for the first time
                if (!hazmadPlayed)
                {
                    StartCoroutine(CoWait(videoClipHazMad));

                    hazmadPlayed = true;
                }

                cam1.SetActive(true);
                trueTarget = targetPlayer2;
                playerInControl = trueTarget.GetComponent<Move>().inControl;

                SwapCharacters(playerInControl);
            }
            else if (Input.GetKeyDown("3"))
            {

                //plays cinematic when rox is used for the first time
                if (!roxPlayed)
                {
                    StartCoroutine(CoWait(videoClipRox));

                    roxPlayed = true;
                }

                cam1.SetActive(true);
                trueTarget = targetPlayer3;
                playerInControl = trueTarget.GetComponent<Move>().inControl;

                SwapCharacters(playerInControl);
            }

            if (Input.GetKeyDown("4"))
            {

                //plays cinematic when tempest is used for the first time
                if (!tempestPlayed)
                {
                    StartCoroutine(CoWait(videoClipTempest));

                    tempestPlayed = true;
                }

                cam1.SetActive(true);
                trueTarget = targetPlayer4;
                playerInControl = trueTarget.GetComponent<Move>().inControl;

                SwapCharacters(playerInControl);
            }
        }
	}

	void SwapCharacters(PlayerInControl player)
	{
		switch (player)
		{
			case PlayerInControl.Ava:
				player1.isControlling = true;
				player2.isControlling = false;
				player3.isControlling = false;
				player4.isControlling = false;
				break;
			case PlayerInControl.Hazmat:
				player1.isControlling = false;
				player2.isControlling = true;
				player3.isControlling = false;
				player4.isControlling = false;
				break;
			case PlayerInControl.Rox:
				player1.isControlling = false;
				player2.isControlling = false;
				player3.isControlling = true;
				player4.isControlling = false;
				break;
			case PlayerInControl.Tempest:
				player1.isControlling = false;
				player2.isControlling = false;
				player3.isControlling = false;
				player4.isControlling = true;
				break;
		}
	}

    private void LateUpdate()
    {
        // Used to be
        // transform.position = trueTarget.transform.position - desiredDistance * transform.forward + Vector3.up * 1.5f;
        // Changed to lerp to achieve a smoother followup on the character

        // Camera's position - Start from the player's position and go backwards a distance desiredDistance from the player.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, trueTarget.transform.position - desiredDistance * transform.forward + Vector3.up * 1.5f, cameraSmooth);
        transform.position = smoothedPosition;
    }

    IEnumerator CoWait(VideoClip clip)
    {
        //hides UI
        UI.SetActive(false);
        isPlayingVideo = true;
        videoPlayer.gameObject.SetActive(true);

        videoPlayer.Stop();
        videoPlayer.clip = clip;
        videoPlayer.Play();

        yield return new WaitForSeconds((float)clip.length);

        isPlayingVideo = false;
        UI.SetActive(true);
        videoPlayer.gameObject.SetActive(false);
    }
}
