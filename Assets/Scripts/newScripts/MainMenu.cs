using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private Text StartGame;
    private Text Controls;
    private Text Quit;
    private Text BackToMenu;

    private bool aButtonIsPressed;

    private GameObject ControlsCanvas;

    private bool ControlsCanvasUp;

    private enum HoveringOver
    {
        StartButton,
        ControlsButton,
        QuitButton
    }
    private HoveringOver PlayerisHoveringOver;

    // Start is called before the first frame update
    void Start()
    {
        StartGame = GameObject.Find("Start_Game_Text").GetComponent<Text>();
        Controls = GameObject.Find("Controls_Text").GetComponent<Text>();
        Quit = GameObject.Find("Quit_Text").GetComponent<Text>();
        ControlsCanvas = GameObject.Find("Controls_Canvas");
        BackToMenu = GameObject.Find("Back_To_Menu_Text").GetComponent<Text>();
        ControlsCanvas.gameObject.SetActive(false);
        PlayerisHoveringOver = HoveringOver.StartButton;
        StartGame.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (!aButtonIsPressed && !ControlsCanvasUp)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                ShuffleUp();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                ShuffleDown();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ButtonPressed();
            }
        } else if (Input.GetKeyDown(KeyCode.Space) && ControlsCanvasUp)
        {
            ButtonPressed();
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
            Quit.color = Color.white;
            PlayerisHoveringOver = HoveringOver.QuitButton;
        }
    }

    void ShuffleUp()
    {
        if (PlayerisHoveringOver.Equals(HoveringOver.ControlsButton))
        {
            Controls.color = Color.black;
            StartGame.color = Color.white;
            PlayerisHoveringOver = HoveringOver.StartButton;
        } else if (PlayerisHoveringOver.Equals(HoveringOver.QuitButton))
        {
            Quit.color = Color.black;
            Controls.color = Color.white;
            PlayerisHoveringOver = HoveringOver.ControlsButton;
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
        }
        StartCoroutine(ActivateAction());
    }

    IEnumerator ActivateAction()
    {
        yield return new WaitForSeconds(0.4f);
        if (PlayerisHoveringOver.Equals(HoveringOver.StartButton))
        {
            //SceneManager.LoadScene("FightingRing");
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
        }
    }


}
