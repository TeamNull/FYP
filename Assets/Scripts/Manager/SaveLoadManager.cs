using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using System.Text;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    #region Variable
    public GameObject LoadingScreen;
    public GameObject Grid;

    const string id = "BF1D24BE7DF041E4A40170B1E940BBD4";

    bool saveBtnClicked;
    bool loadBtnClicked;

    SqlConnectionStringBuilder builder;
    PlayerAttribute player;
    #endregion

    #region LifeCycle
    // Use this for initialization
    void Start()
    {
        player = StaticVarAndFunction.player.GetComponent<PlayerAttribute>();
        builder = new SqlConnectionStringBuilder
        {
            DataSource = "team-null-fyp.database.windows.net",
            UserID = "yungaiyin",
            Password = "IamFish624",
            InitialCatalog = "fyp"
        };
        saveBtnClicked = false;
        loadBtnClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (saveBtnClicked)
        {
            //Todo: Show save success message;
            LoadingScreen.SetActive(true);
            StaticVarAndFunction.isLoading = true;
            saveBtnClicked = false;
            bool isSuccess = Save();
            StaticVarAndFunction.isLoading = false;
            LoadingScreen.SetActive(false);
            Debug.Log("Save" + isSuccess);

        }

        if (loadBtnClicked)
        {
            //Todo: Show load success message;
            LoadingScreen.SetActive(true);
            StaticVarAndFunction.isLoading = true;
            loadBtnClicked = false;
            bool isSuccess = Load();
            StaticVarAndFunction.isLoading = false;
            LoadingScreen.SetActive(false);
            Debug.Log("Load" + isSuccess);

        }
    }
    #endregion

    #region Method
    public void SaveBtnOnClick()
    {
        saveBtnClicked = true;
    }

    public void LoadBtnOnClick()
    {
        loadBtnClicked = true;
    }

    bool Load()
    {
        bool paLoaded = true;
        //bool bagLoaded = true;
        //bool skillLoaded = true;
        //bool missionLoaded = true;
        //bool shortcutLoaded = true;
        //bool warehouseLoaded = true;

        try
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                Debug.Log("DB Connection Start");

                connection.Open();
                StringBuilder sb = new StringBuilder();

                //Load Player Attribute
                sb.Append("SELECT * FROM Player");
                string attribute = sb.ToString();
                using (SqlCommand command = new SqlCommand(attribute, connection))
                {
                    if (command.ExecuteNonQuery() == 0)
                    {
                        Debug.Log("Player Attribute Load Fail");
                        paLoaded = false;
                    }
                    else
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PlayerAttributeParser(reader);
                            }
                        }
                    }
                }
                sb.Remove(0, sb.Length);

                ////Load Bag
                //sb.Append("SELECT * FROM Bag");
                //string bag = sb.ToString();
                //using (SqlCommand command = new SqlCommand(bag, connection))
                //{
                //    if (command.ExecuteNonQuery() == 0)
                //    {
                //        Debug.Log("Bag Load Fail");
                //        bagLoaded = false;
                //    }
                //    using (SqlDataReader reader = command.ExecuteReader())
                //    {
                //        while (reader.Read())
                //        {
                //            //Todo: Add back loader
                //        }
                //    }
                //}
                //sb.Remove(0, sb.Length);

                ////Load Skill
                //sb.Append("SELECT * FROM Skill");
                //string skill = sb.ToString();
                //using (SqlCommand command = new SqlCommand(skill, connection))
                //{
                //    if (command.ExecuteNonQuery() == 0)
                //    {
                //        Debug.Log("Skill Load Fail");
                //        skillLoaded = false;
                //    }
                //    using (SqlDataReader reader = command.ExecuteReader())
                //    {
                //        while (reader.Read())
                //        {
                //            //Todo: Add back loader
                //        }
                //    }
                //}
                //sb.Remove(0, sb.Length);

                ////Load Mission
                //sb.Append("SELECT * FROM Mission");
                //string mission = sb.ToString();
                //using (SqlCommand command = new SqlCommand(mission, connection))
                //{
                //    if (command.ExecuteNonQuery() == 0)
                //    {
                //        Debug.Log("Mission Load Fail");
                //        missionLoaded = false;
                //    }
                //    using (SqlDataReader reader = command.ExecuteReader())
                //    {
                //        while (reader.Read())
                //        {
                //            //Todo: Add back loader
                //        }
                //    }
                //}
                //sb.Remove(0, sb.Length);

                ////Load Shortcut
                //sb.Append("SELECT * FROM Shortcut");
                //string shortcut = sb.ToString();
                //using (SqlCommand command = new SqlCommand(shortcut, connection))
                //{
                //    if (command.ExecuteNonQuery() == 0)
                //    {
                //        Debug.Log("Shortcut Load Fail");
                //        shortcutLoaded = false;
                //    }
                //    using (SqlDataReader reader = command.ExecuteReader())
                //    {
                //        while (reader.Read())
                //        {
                //            //Todo: Add back loader
                //        }
                //    }
                //}
                //sb.Remove(0, sb.Length);

                ////Load Warehouse
                //sb.Append("SELECT * FROM Warehouse");
                //string warehouse = sb.ToString();
                //using (SqlCommand command = new SqlCommand(warehouse, connection))
                //{
                //    if (command.ExecuteNonQuery() == 0)
                //    {
                //        Debug.Log("Warehouse Load Fail");
                //        warehouseLoaded = false;
                //    }
                //    using (SqlDataReader reader = command.ExecuteReader())
                //    {
                //        while (reader.Read())
                //        {
                //              //Todo: Add back loader
                //              UnityEngine.UI.Button[] temp = Grid.GetComponentsInChildren<UnityEngine.UI.Button>();
                //              foreach (UnityEngine.UI.Button button in temp)
                //              {
                //                  Debug.Log(button.name);
                //              }
                //        }
                //    }
                //}
                //sb.Remove(0, sb.Length);

                connection.Close();
                Debug.Log("DB Connection End");
            }
        }
        catch (SqlException e)
        {
            Debug.Log(e.ToString());
        }

        return paLoaded/* && skillLoaded && bagLoaded && missionLoaded && warehouseLoaded && shortcutLoaded*/;
    }

    bool Save()
    {
        bool paSaved = true;
        //bool bagSaved = true;
        //bool skillSaved = true;
        //bool missionSaved = true;
        //bool shortcutSaved = true;
        //bool warehouseSaved = true;

        try
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                Debug.Log("DB Connection Start");

                connection.Open();
                StringBuilder sb = new StringBuilder();

                //Save Player Attribute
                sb.Append("Update Player Set PlayerLevel = " + player.currentLevel.ToString() + ", Exp = " + player.currentExp.ToString() +
                          ", STR = " + player.str.ToString() + ", _Int = " + player._int.ToString() + ", Agi = " + player.agi.ToString() +
                          ", AVBLPOINT = " + player.AvailablePoint.ToString() + ", Position_x = " + StaticVarAndFunction.player.transform.position.x.ToString() +
                          ", Position_y = " + StaticVarAndFunction.player.transform.position.y.ToString() + ", Position_z = " + StaticVarAndFunction.player.transform.position.z.ToString() +
                          ", Scene = '" + SceneManager.GetActiveScene().name + "', HP = " + player.currentHP.ToString() + ", MP = " + player.currentMP.ToString() +
                          ", Job = '" + player.job.ToString() + "' WHERE ID = '" + id + "'");
                string attribute = sb.ToString();
                using (SqlCommand command = new SqlCommand(attribute, connection))
                {
                    if (command.ExecuteNonQuery() == 0)
                    {
                        Debug.Log("Player Attribute Load Fail");
                        paSaved = false;
                    }
                    /*else
                    {
                        command.ExecuteReader();
                    }*/
                }
                sb.Remove(0, sb.Length);

                ////Save Player Bag
                //sb.Append("Update Bag Set ");
                ////Todo: Add back reader
                //string bag = sb.ToString();
                //using (SqlCommand command = new SqlCommand(bag, connection))
                //{
                //    if (command.ExecuteNonQuery() == 0)
                //    {
                //        Debug.Log("Bag Save Fail");
                //        bagSaved = false;
                //    }
                //}
                //sb.Remove(0, sb.Length);

                ////Save SKill
                //sb.Append("Update Skill Set ");
                ////Todo: Add back reader
                //string skill = sb.ToString();
                //using (SqlCommand command = new SqlCommand(skill, connection))
                //{
                //    if (command.ExecuteNonQuery() == 0)
                //    {
                //        Debug.Log("Skill Save Fail");
                //        bagSaved = false;
                //    }
                //}
                //sb.Remove(0, sb.Length);

                ////Save Shortcut
                //sb.Append("Update Shortcut Set ");
                ////Todo: Add back reader
                //string shortcut = sb.ToString();
                //using (SqlCommand command = new SqlCommand(shortcut, connection))
                //{
                //    if (command.ExecuteNonQuery() == 0)
                //    {
                //        Debug.Log("Shortcut Save Fail");
                //        shortcutSaved = false;
                //    }
                //}
                //sb.Remove(0, sb.Length);

                ////Save Mission
                //sb.Append("Update Shortcut Set ");
                ////Todo: Add back reader
                //string mission = sb.ToString();
                //using (SqlCommand command = new SqlCommand(mission, connection))
                //{
                //    if (command.ExecuteNonQuery() == 0)
                //    {
                //        Debug.Log("Mission Save Fail");
                //        missionSaved = false;
                //    }
                //}
                //sb.Remove(0, sb.Length);

                ////Save Warehouse
                //sb.Append("Update Warehouse Set ");
                ////Todo: Add back reader
                //UnityEngine.UI.Button[] temp = Grid.GetComponentsInChildren<UnityEngine.UI.Button>();
                //foreach (UnityEngine.UI.Button button in temp)
                //{
                //    Debug.Log(button.name);
                //}
                //string warehouse = sb.ToString();
                //using (SqlCommand command = new SqlCommand(warehouse, connection))
                //{
                //    if (command.ExecuteNonQuery() == 0)
                //    {
                //        Debug.Log("Warehouse Save Fail");
                //        warehouseSaved = false;
                //    }
                //}
                //sb.Remove(0, sb.Length);

                connection.Close();
                Debug.Log("DB Connection End");
            }

        }
        catch (SqlException e)
        {
            Debug.Log(e.ToString());
        }

        return paSaved/* && bagSaved && skillSaved && shortcutSaved && missionSaved && warehouseSaved*/;
    }

    void PlayerAttributeParser(SqlDataReader reader)
    {
        for (int i = 1; i < reader.FieldCount; i++)
        {
            if (reader.GetValue(i) != null)
            {
                switch (reader.GetName(i).ToLower())
                {
                    case "playername":
                        player.playerName = reader.GetString(i);
                        break;
                    case "playerlevel":
                        player.currentLevel = reader.GetInt32(i);
                        break;
                    case "exp":
                        player.currentExp = reader.GetInt32(i);
                        break;
                    case "str":
                        player.str = reader.GetInt32(i);
                        break;
                    case "_int":
                        player._int = reader.GetInt32(i);
                        break;
                    case "agi":
                        player.agi = reader.GetInt32(i);
                        break;
                    case "avblpoint":
                        player.AvailablePoint = reader.GetInt32(i);
                        break;
                    case "hp":
                        player.currentHP = reader.GetInt32(i) > player.maxHP ? player.maxHP : reader.GetInt32(i);
                        break;
                    case "mp":
                        player.currentMP = reader.GetInt32(i) > player.maxMP ? player.maxMP : reader.GetInt32(i);
                        break;
                    case "job":
                        player.job = reader.GetString(i).ToLower() == "warrior" ? PlayerAttribute.Classes.Warrior : reader.GetString(i).ToLower() == "archer" ? PlayerAttribute.Classes.Archer : PlayerAttribute.Classes.Magician;
                        break;
                    //Todo: Load Position
                    case "position_x":
                    case "position_y":
                    case "position_z":
                    case "scene":
                    //Todo: Load equipment
                    case "head":
                    case "body":
                    case "hand":
                    case "leg":
                    case "weapon":
                        break;
                }
            }
        }
    }
    #endregion
}
