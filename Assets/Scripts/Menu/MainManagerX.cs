using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManagerX : MonoBehaviour
{
    public static MainManagerX Instance;
    public static Player currentPlayer;
    public static Player bestPlayer;
    public static List<string> players;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        //LoadColor();//carga de memoria el color 
    }
    // Start is called before the first frame update
    void Start()
    {
        //File.Delete(Application.persistentDataPath + "/savelist.json");
        LoadBestPlayerdata();
        /*if(bestPlayer == null)
        {
            bestPlayer = new Player();
        }*/
        LoadListData();
     /*   if (players == null)
        {
            players = new List<string>();

        }*/
    }

    void LoadBestPlayerdata()
    {
        string path = Application.persistentDataPath + "/saveBestPlayer.json";//ruta de guardado
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);//lee la clase
            string data = JsonUtility.FromJson<string>(json);//la transforma en la modelo

            bestPlayer = ChangeFormat(data);//toma el dato para recuperarlo
        }
        if (bestPlayer == null)
        {
            bestPlayer = new Player(0,"",0);
        }
    }
        // Update is called once per frame
        void Update()
    {

    }

    
    public class Player
    {
        public int index;
        public string name;
        public int score;
        

        public Player(int index,string name, int score)
        {
            this.index = index;
            this.name = name;
            this.score = score;
        }

        public string SavetoString()
        {
            return JsonUtility.ToJson(this);
        }
    }

    public void LoadListData()
    {
        
        string path = Application.persistentDataPath + "/savelist.json";//ruta de guardado
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);//lee la clase
            SaveListData data = JsonUtility.FromJson<SaveListData>(json);//la transforma en la modelo

            players = data.names;//toma el dato para recuperarlo
        }

        if (players == null)
        {
            players = new List<string>();

        }

    }

    public static void SaveList()
    {

        SaveListData data = new SaveListData(MainManagerX.players);//crea clase modelo con datos

        string json = JsonUtility.ToJson(data);//la serializa

        File.WriteAllText(Application.persistentDataPath + "/savelist.json", json);//la guarda
    }

    public static void SaveBestPlayer()
    {

        File.WriteAllText(Application.persistentDataPath + "/saveBestPlayer.json", bestPlayer.SavetoString());//la guarda
    }

    [System.Serializable]
    public class SaveListData
    {
        public List<string> names;

        public SaveListData(List<string> list)
        {
            names = list;
        }


    }

    public static Player ChangeFormat(string original)
    {

        return JsonUtility.FromJson<MainManagerX.Player>(original);
    }

}
