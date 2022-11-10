using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MenuHandlerUI : MonoBehaviour
{

    public GameObject signMenu;
    public GameObject scrollNames;

    public GameObject helloText;
    private Text hello_msj;

    public GameObject listNames;
    public GameObject choicePlayer;

    public GameObject playerButton;
    private GameObject content;

    public Text nameInputText;

    private string currentPlayer;

 
    



    // Start is called before the first frame update
    void Start()
    {
        
        hello_msj = helloText.GetComponent<Text>();
     

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Go to main scene
    public void StartGame()
    {
        if (string.IsNullOrEmpty(currentPlayer))
        {
            hello_msj.text = "Please choose player";
        }
        else
        {
            SceneManager.LoadScene("main");
        }
    }



    public void SaveName()
    {
       
        currentPlayer = nameInputText.text;
        hello_msj.text = "Hello " + currentPlayer + " !";
        MainManagerX.Player player = new MainManagerX.Player(MainManagerX.players.Count,currentPlayer, 0);
        MainManagerX.players.Add(player.SavetoString());
        MainManagerX.currentPlayer = player;
        MainManagerX.SaveList();
        signMenu.SetActive(false);
        helloText.SetActive(true);
        choicePlayer.SetActive(true);
       

    }

 
    public void GetPlayerFromList(MainManagerX.Player current)//
    {
        
        currentPlayer = current.name; 
        hello_msj.text = "Hello " + currentPlayer + " !";

        MainManagerX.currentPlayer = current;
        listNames.SetActive(false);
        helloText.SetActive(true);
        choicePlayer.SetActive(true);
    }


 

    void LoadPlayers()
    {
        int positionY = -20;
        foreach(string player in MainManagerX.players)
        {
            InstantiateBottonPlayer(player, positionY);
            positionY -= 30;
        }
    }

    void InstantiateBottonPlayer(string player, int positionY)
    {
        Vector3 position = new Vector3(0, positionY, 0);
        
        MainManagerX.Player current = MainManagerX.ChangeFormat(player);
        playerButton.transform.GetChild(0).GetComponentInChildren<Text>().text = current.name.ToUpper();
        playerButton.transform.GetChild(1).GetComponentInChildren<Text>().text = current.score.ToString();
    
        GameObject a = Instantiate(playerButton, position, playerButton.transform.rotation); // objeto instanciado
        Button b = a.GetComponent<Button>();
        b.onClick.AddListener(delegate { GetPlayerFromList(current); });
        a.transform.SetParent(content.transform, false);
    }

    public void ChoicePlayer()
    {
        listNames.SetActive(true);
        content = GameObject.Find("Content");
        helloText.SetActive(false);
        choicePlayer.SetActive(false);
        LoadPlayers();
    }

    public void CreatePlayer()
    {
        listNames.SetActive(false);
        signMenu.SetActive(true);
   
        
    }

  /*  MainManagerX.Player ChangeFormat(string original)
    {
       
        return JsonUtility.FromJson<MainManagerX.Player>(original);
    }*/
    

}
