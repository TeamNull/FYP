using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using System.Text;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{

    #region Variable
    SqlConnectionStringBuilder builder;
    PlayerAttribute player;

    public bool saveBtnClicked = false;
    public bool loadBtnClicked = false;
    #endregion

    #region LifeCycle
    // Use this for initialization
    void Start()
    {
        player = StaticVarAndFunction.player.GetComponent<PlayerAttribute>();
        builder = new SqlConnectionStringBuilder();
        builder.DataSource = "team-null-fyp.database.windows.net";
        builder.UserID = "yungaiyin";
        builder.Password = "IamFish624";
        builder.InitialCatalog = "fyp";
    }

    // Update is called once per frame
    void Update()
    {
        if (saveBtnClicked) {
            //Todo: Show save success message;
            //Save();
            Debug.Log("Save Btn Clicked");
            saveBtnClicked = false;
        }

        if (loadBtnClicked) {
            //Todo: Show load success message;
            //Load();
            Debug.Log("Load Btn Clicked");
            loadBtnClicked = false;
        }
    }
    #endregion

    #region Method

    public void SaveBtnOnClick() {
        saveBtnClicked = true;
    }

    public void LoadBtnOnClick() {
        loadBtnClicked = true;
    }

    bool Load()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                Debug.Log("DB Connection Start");

                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * FROM Player");
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if (command.ExecuteNonQuery() == 0) return false;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PlayerAttributeParser(reader);
                        }
                    }
                }
                Debug.Log("DB Connection End");
            }
        }
        catch (SqlException e)
        {
            Debug.Log(e.ToString());
        }

        return true;
    }

    void PlayerAttributeParser(SqlDataReader reader) {
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

    bool Save()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                Debug.Log("DB Connection Start");

                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("Update Player Set PlayerLevel = " + player.currentLevel.ToString() + ", Exp = " + player.currentExp.ToString() + 
                          ", STR = " + player.str.ToString() + ", _Int = " + player._int.ToString() + ", Agi = " + player.agi.ToString() + 
                          ", AVBLPOINT = " + player.AvailablePoint.ToString() + ", Position_x = " + StaticVarAndFunction.player.transform.position.x.ToString() + 
                          ", Position_y = " + StaticVarAndFunction.player.transform.position.y.ToString() + ", Position_z = " + StaticVarAndFunction.player.transform.position.z.ToString() + 
                          ", Scene = " + SceneManager.GetActiveScene().name + ", HP = " + player.currentHP.ToString() + ", MP = " + player.currentMP.ToString() + ", Job = " + player.job.ToString());
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if (command.ExecuteNonQuery() == 0) return false;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                    }
                }

                Debug.Log("DB Connection End");
            }

        }
        catch (SqlException e)
        {
            Debug.Log(e.ToString());
        }

        return true;
    }
    #endregion
}
