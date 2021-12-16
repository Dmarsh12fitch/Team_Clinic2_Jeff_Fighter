using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioSource au;

    private Text StartGame;
    private Text Controls;
    private Text Credits;
    private Text Quit;
    private Text BackToMenu;
    private Text BackToMenu2;

    private bool aButtonIsPressed;

    private GameObject ControlsCanvas;
    private GameObject CreditsCanvas;

    private bool ControlsCanvasUp;
    private bool CreditsCanvasUp;

    private enum HoveringOver
    {
        StartButton,
        ControlsButton,
        CreditsButton,
        QuitButton
    }
    private HoveringOver PlayerisHoveringOver;

    // Start is called before the first frame update
    void Start()
    {
        StartGame = GameObject.Find("Start_Game_Text").GetComponent<Text>();
        Controls = GameObject.Find("Controls_Text").GetComponent<Text>();
        Credits = GameObject.Find("Credits_Text").GetComponent<Text>();
        Quit = GameObject.Find("Quit_Text").GetComponent<Text>();
        ControlsCanvas = GameObject.Find("Controls_Canvas");
        CreditsCanvas = GameObject.Find("Credits_Canvas");
        au = GetComponent<AudioSource>();
        BackToMenu = GameObject.Find("Back_To_Menu_Text").GetComponent<Text>();
        BackToMenu2 = GameObject.Find("Back_To_Menu_Text2").GetComponent<Text>();
        ControlsCanvas.gameObject.SetActive(false);
        CreditsCanvas.gameObject.SetActive(false);
        //PlayerisHoveringOver = HoveringOver.StartButton;
        //StartGame.color = Color.white;
        PlayerisHoveringOver = HoveringOver.ControlsButton;
        Controls.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (!aButtonIsPressed && !ControlsCanvasUp && !CreditsCanvasUp)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                ShuffleUp();
                au.pitch = 3;
                au.Play();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                ShuffleDown();
                au.pitch = 3;
                au.Play();
            }
        }
        if (!aButtonIsPressed && Input.GetKeyDown(KeyCode.Space))
        {
            ButtonPressed();
            au.pitch = 1.7f;
            au.Play();
        }
    }

    void ShuffleDown()
    {
        if (PlayerisHoveringOver.Equals(HoveringOver.StartButton))
        {
            StartGame.color = Color.black;
            Controls.color = Color.white;
            PlayerisHoveringOver = HoveringOver.ControlsButton;
        } else if (PlayerisHoveringOver.Equals(HoveringOver.ControlsButton))
        {
            Controls.color = Color.black;
            Credits.color = Color.white;
            PlayerisHoveringOver = HoveringOver.CreditsButton;
        } else if (PlayerisHoveringOver.Equals(HoveringOver.CreditsButton))
        {
            Credits.color = Color.black;
            Quit.color = Color.white;
            PlayerisHoveringOver = HoveringOver.QuitButton;
        }
    }

    void ShuffleUp()
    {
        if (PlayerisHoveringOver.Equals(HoveringOver.QuitButton))
        {
            Credits.color = Color.white;
            Quit.color = Color.black;
            PlayerisHoveringOver = HoveringOver.CreditsButton;
        }
        else if (PlayerisHoveringOver.Equals(HoveringOver.CreditsButton))
        {
            Controls.color = Color.white;
            Credits.color = Color.black;
            PlayerisHoveringOver = HoveringOver.ControlsButton;
        }
        else if (PlayerisHoveringOver.Equals(HoveringOver.ControlsButton))
        {
            StartGame.color = Color.white;
            Controls.color = Color.black;
            PlayerisHoveringOver = HoveringOver.StartButton;
        }
    }

    void ButtonPressed()
    {
        aButtonIsPressed = true;
        if (PlayerisHoveringOver.Equals(HoveringOver.StartButton))
        {
            StartGame.color = Color.grey;
        }
        else if (PlayerisHoveringOver.Equals(HoveringOver.ControlsButton) && !ControlsCanvasUp)
        {
            Controls.color = Color.grey;
        }
        else if (PlayerisHoveringOver.Equals(HoveringOver.ControlsButton) && ControlsCanvasUp)
        {
            BackToMenu.color = Color.grey;
        }
        else if (PlayerisHoveringOver.Equals(HoveringOver.QuitButton))
        {
            Quit.color = Color.grey;
        } else if (PlayerisHoveringOver.Equals(HoveringOver.CreditsButton) && !CreditsCanvasUp)
        {
            Credits.color = Color.grey;
        } else if(PlayerisHoveringOver.Equals(HoveringOver.CreditsButton) && CreditsCanvasUp)
        {
            BackToMenu2.color = Color.grey;
        }
        StartCoroutine(ActivateAction());
    }

    IEnumerator ActivateAction()
    {
        yield return new WaitForSeconds(0.4f);
        if (PlayerisHoveringOver.Equals(HoveringOver.StartButton))
        {
            //SceneManager.LoadScene("FightingRing");
            //DontDestroyOnLoad(GameObject.Find("musicMaker"));
            SceneManager.LoadScene("CharacterSelection");
        }
        else if (PlayerisHoveringOver.Equals(HoveringOver.ControlsButton) && !ControlsCanvasUp)
        {
            ControlsCanvas.gameObject.SetActive(true);
            Controls.color = Color.black;
            ControlsCanvasUp = true;
            aButtonIsPressed = false;
        }
        else if(PlayerisHoveringOver.Equals(HoveringOver.ControlsButton) && ControlsCanvasUp)
        {
            BackToMenu.color = Color.white;
            ControlsCanvas.gameObject.SetActive(false);
            Controls.color = Color.white;
            ControlsCanvasUp = false;
            aButtonIsPressed = false;
        }
        else if (PlayerisHoveringOver.Equals(HoveringOver.QuitButton))
        {
            Application.Quit();
        } else if (PlayerisHoveringOver.Equals(HoveringOver.CreditsButton) && !CreditsCanvasUp)
        {
            CreditsCanvas.gameObject.SetActive(true);
            Credits.color = Color.black;
            CreditsCanvasUp = true;
            aButtonIsPressed = false;

        } else if(PlayerisHoveringOver.Equals(HoveringOver.CreditsButton) && CreditsCanvasUp)
        {
            BackToMenu2.color = Color.white;
            CreditsCanvas.gameObject.SetActive(false);
            Credits.color = Color.white;
            CreditsCanvasUp = false;
            aButtonIsPressed = false;
        }
    }


}
