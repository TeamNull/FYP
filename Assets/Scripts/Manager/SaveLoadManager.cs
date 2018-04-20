using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using System.Text;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveLoadManager : MonoBehaviour
{
    #region Variable
    public GameObject LoadingScreen;
    public GameObject Bag;
    public GameObject Shortcut;
    public GameObject shopObject;
    public GameObject skill;
    public GameObject TemplateItem;
    public GameObject PlayerInfo;

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
        player = GameManager.player.GetComponent<PlayerAttribute>();
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
            LoadingScreen.SetActive(true);
            GameManager.isLoading = true;
            saveBtnClicked = false;
            bool isSuccess = Save();
            GameManager.isLoading = false;
            LoadingScreen.SetActive(false);
            Debug.Log("Save" + isSuccess);
        }

        if (loadBtnClicked)
        {
            LoadingScreen.SetActive(true);
            GameManager.isLoading = true;
            loadBtnClicked = false;
            bool isSuccess = Load();
            GameManager.isLoading = false;
            LoadingScreen.SetActive(false);
            // Update all relavent UI;
            skill.GetComponent<UIController>().InitSkillUI();
            player.UpdatePlayerValueByPoint();
            PlayerInfo.GetComponent<UIinfo>().updateHP(player.currentHP, player.maxHP);
            PlayerInfo.GetComponent<UIinfo>().updateMP(player.currentMP, player.maxMP);
            PlayerInfo.GetComponent<UIinfo>().updateEXP(player.currentLevel, player.currentExp, player.needExp, false);
            Debug.Log("Load" + isSuccess);

        }
    }
    #endregion

    #region Method
    public void SaveBtnOnClick()
    {
        GameManager.AudioManager.GetComponent<BGMcontrol>().Playsound("ClickButton");
        saveBtnClicked = true;
    }

    public void LoadBtnOnClick()
    {
        GameManager.AudioManager.GetComponent<BGMcontrol>().Playsound("ClickButton");
        loadBtnClicked = true;
    }

    bool Load()
    {
        bool paLoaded = true;
        bool bagLoaded = true;
        bool skillLoaded = true;
        bool missionLoaded = true;
        bool shortcutLoaded = true;

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
                            Debug.Log("Player Attribute Load Success");
                        }
                    }
                }
                sb.Remove(0, sb.Length);

                //Load Bag
                sb.Append("SELECT * FROM Bag");
                string bag = sb.ToString();
                using (SqlCommand command = new SqlCommand(bag, connection))
                {
                    if (command.ExecuteNonQuery() == 0)
                    {
                        Debug.Log("Bag Load Fail");
                        bagLoaded = false;
                    }
                    else
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BagParser(reader);
                            }
                            Debug.Log("Bag Load Success");
                        }
                    }
                }
                sb.Remove(0, sb.Length);

                //Load Skill
                sb.Append("SELECT * FROM Skill");
                string skillSQL = sb.ToString();
                using (SqlCommand command = new SqlCommand(skillSQL, connection))
                {
                    if (command.ExecuteNonQuery() == 0)
                    {
                        Debug.Log("Skill Load Fail");
                        skillLoaded = false;
                    }
                    else
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SkillParser(reader);
                            }
                            Debug.Log("Skill Load Success");
                        }
                    }
                }
                sb.Remove(0, sb.Length);

                //Load Mission
                sb.Append("SELECT * FROM Mission");
                string mission = sb.ToString();
                using (SqlCommand command = new SqlCommand(mission, connection))
                {
                    if (command.ExecuteNonQuery() == 0)
                    {
                        Debug.Log("Mission Load Fail");
                        missionLoaded = false;
                    }
                    else
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MissionParser(reader);
                            }
                            Debug.Log("Mission Load Success");
                        }
                    }
                }
                sb.Remove(0, sb.Length);

                //Load Shortcut
                sb.Append("SELECT * FROM Shortcut");
                string shortcut = sb.ToString();
                using (SqlCommand command = new SqlCommand(shortcut, connection))
                {
                    if (command.ExecuteNonQuery() == 0)
                    {
                        Debug.Log("Shortcut Load Fail");
                        shortcutLoaded = false;
                    }
                    else
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ShortcutParser(reader);
                            }
                            Debug.Log("Shortcut Load Success");
                        }
                    }
                }
                sb.Remove(0, sb.Length);

                connection.Close();
                Debug.Log("DB Connection End");
            }
        }
        catch (SqlException e)
        {
            Debug.Log(e.ToString());
        }

        return paLoaded && skillLoaded && bagLoaded && missionLoaded && shortcutLoaded;
    }

    bool Save()
    {
        bool paSaved = false;
        bool bagSaved = false;
        bool skillSaved = false;
        bool missionSaved = false;
        bool shortcutSaved = false;

        try
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                Debug.Log("DB Connection Start");

                connection.Open();
                StringBuilder sb = new StringBuilder();

                //Save Player Attribute
                sb.Append("Update Player Set PlayerLevel = " + player.currentLevel.ToString() + ", PlayerName = '" + player.playerName + "', Exp = " + player.currentExp.ToString() +
                          ", STR = " + player.str.ToString() + ", _Int = " + player._int.ToString() + ", Agi = " + player.agi.ToString() +
                          ", AVBLPOINT = " + player.AvailablePoint.ToString() + ", SKILLPOINT = " + player.SkillPoint.ToString() + ", Job = '" + player.job.ToString() + "' WHERE ID = '" + id + "'");
                string attribute = sb.ToString();
                using (SqlCommand command = new SqlCommand(attribute, connection))
                {
                    if (command.ExecuteNonQuery() > 0)
                    {
                        Debug.Log("Player Attribute Save Success");
                        paSaved = true;
                    }
                    /*else
                    {
                        command.ExecuteReader();
                    }*/
                }
                sb.Remove(0, sb.Length);

                //Save Player Bag
                sb.Append("Update Bag Set ");
                bool isFirst = true;
                Equipment[] eqArray = Bag.transform.GetChild(1).GetComponent<ArmedEquipment>().equipmentList;
                for (int i = 0; i < eqArray.Length; i++)
                {
                    if (!isFirst) sb.Append(", ");
                    switch (i)
                    {
                        case 0:
                            if (eqArray[i] != null) sb.Append("Head = " + eqArray[i].id);
                            else sb.Append("Head = -1");
                            break;
                        case 1:
                            if (eqArray[i] != null) sb.Append("Weapon = " + eqArray[i].id);
                            else sb.Append("Weapon = -1");
                            break;
                        case 2:
                            if (eqArray[i] != null) sb.Append("Body = " + eqArray[i].id);
                            else sb.Append("Body = -1");
                            break;
                        case 3:
                            if (eqArray[i] != null) sb.Append("Hand = " + eqArray[i].id);
                            else sb.Append("Hand = -1");
                            break;
                        case 4:
                            if (eqArray[i] != null) sb.Append("Leg = " + eqArray[i].id);
                            else sb.Append("Leg = -1");
                            break;
                    }
                    isFirst = false;
                }
                List<Item> itemList = Bag.transform.GetChild(2).GetComponent<Bag>().itemList;
                for (int i = 0; i < 24; i++)
                {
                    if (!isFirst) sb.Append(", ");
                    if (i < itemList.Count)
                    {
                        sb.Append(string.Format("Storage_{0}_ID = {1}, ", i + 1, itemList[i].id.ToString()));
                        sb.Append(string.Format("Storage_{0}_Unit = {1}", i + 1, itemList[i].unit.ToString()));
                        isFirst = false;
                    }
                    else
                    {
                        sb.Append(string.Format("Storage_{0}_ID = -1, ", i + 1));
                        sb.Append(string.Format("Storage_{0}_Unit = -1", i + 1));
                        isFirst = false;
                    }
                }
                sb.Append(", Money = " + Bag.transform.GetChild(2).GetComponent<Bag>().coin.unit.ToString());
                sb.Append(" Where ID = 1");
                string bag = sb.ToString();
                using (SqlCommand command = new SqlCommand(bag, connection))
                {
                    if (command.ExecuteNonQuery() > 0)
                    {
                        Debug.Log("Bag Save Success");
                        bagSaved = true;
                    }
                }
                sb.Remove(0, sb.Length);
                isFirst = true;

                //Save SKill
                sb.Append("Update Skill Set ");

                for (int i = 0; i < player.Skill.Length; i++)
                {
                    if (!isFirst) sb.Append(", ");
                    sb.Append(string.Format("Skill_{0}_Point = {1}", i + 1, player.Skill[i]));
                    isFirst = false;
                }
                sb.Append(" Where ID = 1");
                string skill = sb.ToString();
                using (SqlCommand command = new SqlCommand(skill, connection))
                {
                    if (command.ExecuteNonQuery() > 0)
                    {
                        Debug.Log("Skill Save Success");
                        skillSaved = true;
                    }
                }
                sb.Remove(0, sb.Length);
                isFirst = true;

                //Save Shortcut
                sb.Append("Update Shortcut Set ");
                GameObject[] temp = Shortcut.GetComponent<Shortcut>().ShortCutArray;
                for (int i = 0; i < temp.Length; i++)
                {
                    if (!isFirst) sb.Append(", ");
                    if (temp[i].transform.childCount > 0)
                    {
                        sb.Append(string.Format("Shortcut_{0}_ID = {1}", i + 1, temp[i].transform.GetChild(0).GetComponent<DragAndDropItem>().ItemId));
                    }
                    else
                    {
                        sb.Append(string.Format("Shortcut_{0}_ID = -1", i + 1));
                    }
                    isFirst = false;
                }
                sb.Append(" Where ID = 1");
                string shortcut = sb.ToString();
                using (SqlCommand command = new SqlCommand(shortcut, connection))
                {
                    if (command.ExecuteNonQuery() > 0)
                    {
                        Debug.Log("Shortcut Save Success");
                        shortcutSaved = true;
                    }
                }
                sb.Remove(0, sb.Length);
                isFirst = true;

                //Save Mission
                sb.Append("Update Mission Set ");
                MissionSystem ms = GameManager.player.GetComponent<MissionSystem>();
                for (int i = 0; i < ms.mission.Length; i++)
                {
                    if (!isFirst) sb.Append(", ");
                    sb.Append(string.Format("Mission_{0}_progress = {1}", i + 1, ms.mission[i].Getcomplete() ? 1 : 0));
                    isFirst = false;
                }
                sb.Append(" Where ID = 1");
                string mission = sb.ToString();
                using (SqlCommand command = new SqlCommand(mission, connection))
                {
                    if (command.ExecuteNonQuery() > 0)
                    {
                        Debug.Log("Mission Save Success");
                        missionSaved = true;
                    }
                }
                sb.Remove(0, sb.Length);

                connection.Close();
                Debug.Log("DB Connection End");
            }

        }
        catch (SqlException e)
        {
            Debug.Log(e.ToString());
        }

        return paSaved && bagSaved && skillSaved && shortcutSaved && missionSaved;
    }
    #endregion

    #region Parser
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
                    case "job":
                        player.job = reader.GetString(i).ToLower() == "warrior" ? PlayerAttribute.Classes.Warrior : reader.GetString(i).ToLower() == "archer" ? PlayerAttribute.Classes.Archer : PlayerAttribute.Classes.Magician;
                        break;
                }
            }
        }
    }

    void BagParser(SqlDataReader reader)
    {
        ArmedEquipment ae = Bag.transform.GetChild(1).GetComponent<ArmedEquipment>();
        //Clear equipment list
        for (int i = 0; i < 5; i++)
        {
            if (ae.equipmentList[i] != null) ae.RemoveEquipment(i);
        }
        //Clear bag List
        Bag.transform.GetChild(2).GetComponent<Bag>().RemoveAllItemInBag();
        Shop shop = shopObject.GetComponent<Shop>();
        int[] itemList = new int[24];
        int[] itemUnitList = new int[24];
        for (int i = 0; i < reader.FieldCount; i++)
        {
            switch (reader.GetName(i).ToLower())
            {
                case "head":
                    if (reader.GetInt32(i) != -1)
                    {
                        ae.ApplyEquipment((Equipment)shop.findItemByID(reader.GetInt32(i)) ?? null);
                    }
                    break;
                case "weapon":
                    if (reader.GetInt32(i) != -1)
                    {
                        ae.ApplyEquipment((Equipment)shop.findItemByID(reader.GetInt32(i)) ?? null);
                    }
                    break;
                case "body":
                    if (reader.GetInt32(i) != -1)
                    {
                        ae.ApplyEquipment((Equipment)shop.findItemByID(reader.GetInt32(i)) ?? null);
                    }
                    break;
                case "hand":
                    if (reader.GetInt32(i) != -1)
                    {
                        ae.ApplyEquipment((Equipment)shop.findItemByID(reader.GetInt32(i)) ?? null);
                    }
                    break;
                case "leg":
                    if (reader.GetInt32(i) != -1)
                    {
                        ae.ApplyEquipment((Equipment)shop.findItemByID(reader.GetInt32(i)) ?? null);
                    }
                    break;
                case "storage_1_id":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemList[0] = reader.GetInt32(i);
                    }
                    break;
                case "storage_2_id":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemList[1] = reader.GetInt32(i);
                    }
                    break;
                case "storage_3_id":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemList[2] = reader.GetInt32(i);
                    }
                    break;
                case "storage_4_id":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemList[3] = reader.GetInt32(i);
                    }
                    break;
                case "storage_5_id":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemList[4] = reader.GetInt32(i);
                    }
                    break;
                case "storage_6_id":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemList[5] = reader.GetInt32(i);
                    }
                    break;
                case "storage_7_id":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemList[6] = reader.GetInt32(i);
                    }
                    break;
                case "storage_8_id":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemList[7] = reader.GetInt32(i);
                    }
                    break;
                case "storage_9_id":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemList[8] = reader.GetInt32(i);
                    }
                    break;
                case "storage_10_id":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemList[9] = reader.GetInt32(i);
                    }
                    break;
                case "storage_11_id":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemList[10] = reader.GetInt32(i);
                    }
                    break;
                case "storage_12_id":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemList[11] = reader.GetInt32(i);
                    }
                    break;
                case "storage_13_id":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemList[12] = reader.GetInt32(i);
                    }
                    break;
                case "storage_14_id":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemList[13] = reader.GetInt32(i);
                    }
                    break;
                case "storage_15_id":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemList[14] = reader.GetInt32(i);
                    }
                    break;
                case "storage_16_id":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemList[15] = reader.GetInt32(i);
                    }
                    break;
                case "storage_17_id":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemList[16] = reader.GetInt32(i);
                    }
                    break;
                case "storage_18_id":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemList[17] = reader.GetInt32(i);
                    }
                    break;
                case "storage_19_id":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemList[18] = reader.GetInt32(i);
                    }
                    break;
                case "storage_20_id":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemList[19] = reader.GetInt32(i);
                    }
                    break;
                case "storage_21_id":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemList[20] = reader.GetInt32(i);
                    }
                    break;
                case "storage_22_id":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemList[21] = reader.GetInt32(i);
                    }
                    break;
                case "storage_23_id":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemList[22] = reader.GetInt32(i);
                    }
                    break;
                case "storage_24_id":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemList[23] = reader.GetInt32(i);
                    }
                    break;
                case "storage_1_unit":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemUnitList[0] = reader.GetInt32(i);
                    }
                    break;
                case "storage_2_unit":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemUnitList[1] = reader.GetInt32(i);
                    }
                    break;
                case "storage_3_unit":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemUnitList[2] = reader.GetInt32(i);
                    }
                    break;
                case "storage_4_unit":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemUnitList[3] = reader.GetInt32(i);
                    }
                    break;
                case "storage_5_unit":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemUnitList[4] = reader.GetInt32(i);
                    }
                    break;
                case "storage_6_unit":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemUnitList[5] = reader.GetInt32(i);
                    }
                    break;
                case "storage_7_unit":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemUnitList[6] = reader.GetInt32(i);
                    }
                    break;
                case "storage_8_unit":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemUnitList[7] = reader.GetInt32(i);
                    }
                    break;
                case "storage_9_unit":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemUnitList[8] = reader.GetInt32(i);
                    }
                    break;
                case "storage_10_unit":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemUnitList[9] = reader.GetInt32(i);
                    }
                    break;
                case "storage_11_unit":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemUnitList[10] = reader.GetInt32(i);
                    }
                    break;
                case "storage_12_unit":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemUnitList[11] = reader.GetInt32(i);
                    }
                    break;
                case "storage_13_unit":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemUnitList[12] = reader.GetInt32(i);
                    }
                    break;
                case "storage_14_unit":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemUnitList[13] = reader.GetInt32(i);
                    }
                    break;
                case "storage_15_unit":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemUnitList[14] = reader.GetInt32(i);
                    }
                    break;
                case "storage_16_unit":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemUnitList[15] = reader.GetInt32(i);
                    }
                    break;
                case "storage_17_unit":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemUnitList[16] = reader.GetInt32(i);
                    }
                    break;
                case "storage_18_unit":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemUnitList[17] = reader.GetInt32(i);
                    }
                    break;
                case "storage_19_unit":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemUnitList[18] = reader.GetInt32(i);
                    }
                    break;
                case "storage_20_unit":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemUnitList[19] = reader.GetInt32(i);
                    }
                    break;
                case "storage_21_unit":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemUnitList[20] = reader.GetInt32(i);
                    }
                    break;
                case "storage_22_unit":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemUnitList[21] = reader.GetInt32(i);
                    }
                    break;
                case "storage_23_unit":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemUnitList[22] = reader.GetInt32(i);
                    }
                    break;
                case "storage_24_unit":
                    if (reader.GetInt32(i) != -1)
                    {
                        itemUnitList[23] = reader.GetInt32(i);
                    }
                    break;
                case "money":
                    if (reader.GetInt32(i) != -1)
                    {
                        Bag.transform.GetChild(2).GetComponent<Bag>().coin.unit = reader.GetInt32(i);

                    }
                    break;
            }
        }
        for (int i = 0; i < itemList.Length; i++)
        {
            Item temp = shop.findItemByID(itemList[i]);
            if (temp != null)
            {
                Bag.transform.GetChild(2).GetComponent<Bag>().AddItem(temp, itemUnitList[i] < 0 ? 1 : itemUnitList[i]);
            }
        }
    }

    void SkillParser(SqlDataReader reader)
    {
        for (int i = 0; i < reader.FieldCount; i++)
        {
            if (reader.GetValue(i) != null)
            {
                switch (reader.GetName(i).ToLower())
                {
                    case "skill_1_point":
                        player.Skill[0] = reader.GetInt32(i);
                        break;
                    case "skill_2_point":
                        player.Skill[1] = reader.GetInt32(i);
                        break;
                    case "skill_3_point":
                        player.Skill[2] = reader.GetInt32(i);
                        break;
                    case "skill_4_point":
                        player.Skill[3] = reader.GetInt32(i);
                        break;
                    case "skill_5_point":
                        player.Skill[4] = reader.GetInt32(i);
                        break;
                }
            }
        }
    }

    void MissionParser(SqlDataReader reader)
    {
        MissionSystem ms = player.gameObject.GetComponent<MissionSystem>();
        for (int i = 0; i < reader.FieldCount; i++)
        {
            if (reader.GetValue(i) != null)
            {
                switch (reader.GetName(i).ToLower())
                {
                    case "mission_1_progress":
                        if (reader.GetBoolean(i) != false)
                        {
                            ms.mission[0].Setcomplete(true);
                            ms.MissionStart(1);
                        }
                        break;
                    case "mission_2_progress":
                        if (reader.GetBoolean(i) != false)
                        {
                            ms.mission[1].Setcomplete(true);
                            ms.MissionStart(2);
                        }
                        break;
                    case "mission_3_progress":
                        if (reader.GetBoolean(i) != false)
                        {
                            ms.mission[2].Setcomplete(true);
                            ms.MissionStart(3);
                        }
                        break;
                    case "mission_4_progress":
                        if (reader.GetBoolean(i) != false)
                        {
                            ms.mission[3].Setcomplete(true);
                            ms.MissionStart(4);
                        }
                        break;
                    case "mission_5_progress":
                        if (reader.GetBoolean(i) != false)
                        {
                            ms.mission[4].Setcomplete(true);
                            ms.MissionStart(5);
                        }
                        break;
                    case "mission_6_progress":
                        if (reader.GetBoolean(i) != false)
                        {
                            ms.mission[5].Setcomplete(true);
                            ms.MissionStart(6);
                        }
                        break;
                    case "mission_7_progress":
                        if (reader.GetBoolean(i) != false)
                        {
                            ms.mission[6].Setcomplete(true);
                            ms.MissionStart(7);
                        }
                        break;
                    case "mission_8_progress":
                        if (reader.GetBoolean(i) != false)
                        {
                            ms.mission[7].Setcomplete(true);
                            ms.MissionStart(8);
                        }
                        break;
                    case "mission_9_progress":
                        if (reader.GetBoolean(i) != false)
                        {
                            ms.mission[8].Setcomplete(true);
                            ms.MissionStart(9);
                        }
                        break;
                    case "mission_10_progress":
                        if (reader.GetBoolean(i) != false)
                        {
                            ms.mission[9].Setcomplete(true);
                            ms.MissionStart(10);
                        }
                        break;
                    case "mission_11_progress":
                        if (reader.GetBoolean(i) != false)
                        {
                            ms.mission[10].Setcomplete(true);
                            ms.MissionStart(11);
                        }
                        break;
                    case "mission_12_progress":
                        if (reader.GetBoolean(i) != false)
                        {
                            ms.mission[11].Setcomplete(true);
                            ms.MissionStart(12);
                        }
                        break;
                    case "mission_13_progress":
                        if (reader.GetBoolean(i) != false)
                        {
                            ms.mission[12].Setcomplete(true);
                            ms.MissionStart(13);
                        }
                        break;
                    case "mission_14_progress":
                        if (reader.GetBoolean(i) != false)
                        {
                            ms.mission[13].Setcomplete(true);
                            ms.MissionStart(14);
                        }
                        break;
                    case "mission_15_progress":
                        if (reader.GetBoolean(i) != false)
                        {
                            ms.mission[14].Setcomplete(true);
                            ms.MissionStart(15);
                        }
                        break;
                    case "mission_16_progress":
                        if (reader.GetBoolean(i) != false)
                        {
                            ms.mission[15].Setcomplete(true);
                            ms.MissionStart(16);
                        }
                        break;
                    case "mission_17_progress":
                        if (reader.GetBoolean(i) != false)
                        {
                            ms.mission[16].Setcomplete(true);
                            ms.MissionStart(17);
                        }
                        break;
                    case "mission_18_progress":
                        if (reader.GetBoolean(i) != false)
                        {
                            ms.mission[17].Setcomplete(true);
                            ms.MissionStart(18);
                        }
                        break;
                    case "mission_19_progress":
                        if (reader.GetBoolean(i) != false)
                        {
                            ms.mission[18].Setcomplete(true);
                            ms.MissionStart(19);
                        }
                        break;
                    case "mission_20_progress":
                        if (reader.GetBoolean(i) != false)
                        {
                            ms.mission[19].Setcomplete(true);
                        }
                        break;
                }
            }
        }
    }

    void ShortcutParser(SqlDataReader reader)
    {
        GameObject[] temp = Shortcut.GetComponent<Shortcut>().ShortCutArray;
        UIController ctrl = skill.GetComponent<UIController>();
        Shop shop = shopObject.GetComponent<Shop>();
        for (int i = 0; i < reader.FieldCount; i++)
        {
            if (reader.GetValue(i) != null)
            {
                switch (reader.GetName(i).ToLower())
                {
                    case "shortcut_1_id":
                        if (reader.GetInt32(i) > 200 && reader.GetInt32(i) < 300)
                        {
                            temp[0].GetComponent<DragAndDropCell>().PlaceItem(Instantiate(ctrl.FindSkillDADItemByID(reader.GetInt32(i))));
                        }
                        else if (reader.GetInt32(i) != -1)
                        {
                            Item item = shop.findItemByID(reader.GetInt32(i));
                            GameObject itemTemplate = Instantiate(TemplateItem);
                            DragAndDropItem tempDadItem = itemTemplate.transform.GetChild(0).GetComponent<DragAndDropItem>();
                            tempDadItem.gameObject.GetComponent<Image>().sprite = item.sprite;
                            tempDadItem.item = item;
                            temp[0].GetComponent<DragAndDropCell>().PlaceItem(tempDadItem);
                        }
                        break;
                    case "shortcut_2_id":
                        if (reader.GetInt32(i) > 200 && reader.GetInt32(i) < 300)
                        {
                            temp[1].GetComponent<DragAndDropCell>().PlaceItem(Instantiate(ctrl.FindSkillDADItemByID(reader.GetInt32(i))));
                        }
                        else if (reader.GetInt32(i) != -1)
                        {
                            Item item = shop.findItemByID(reader.GetInt32(i));
                            GameObject itemTemplate = Instantiate(TemplateItem);
                            DragAndDropItem tempDadItem = itemTemplate.transform.GetChild(0).GetComponent<DragAndDropItem>();
                            tempDadItem.gameObject.GetComponent<Image>().sprite = item.sprite;
                            tempDadItem.item = item;
                            temp[1].GetComponent<DragAndDropCell>().PlaceItem(tempDadItem);
                        }
                        break;
                    case "shortcut_3_id":
                        if (reader.GetInt32(i) > 200 && reader.GetInt32(i) < 300)
                        {
                            temp[2].GetComponent<DragAndDropCell>().PlaceItem(Instantiate(ctrl.FindSkillDADItemByID(reader.GetInt32(i))));
                        }
                        else if (reader.GetInt32(i) != -1)
                        {
                            Item item = shop.findItemByID(reader.GetInt32(i));
                            GameObject itemTemplate = Instantiate(TemplateItem);
                            DragAndDropItem tempDadItem = itemTemplate.transform.GetChild(0).GetComponent<DragAndDropItem>();
                            tempDadItem.gameObject.GetComponent<Image>().sprite = item.sprite;
                            tempDadItem.item = item;
                            temp[2].GetComponent<DragAndDropCell>().PlaceItem(tempDadItem);
                        }
                        break;
                    case "shortcut_4_id":
                        if (reader.GetInt32(i) > 200 && reader.GetInt32(i) < 300)
                        {
                            temp[3].GetComponent<DragAndDropCell>().PlaceItem(Instantiate(ctrl.FindSkillDADItemByID(reader.GetInt32(i))));
                        }
                        else if (reader.GetInt32(i) != -1)
                        {
                            Item item = shop.findItemByID(reader.GetInt32(i));
                            GameObject itemTemplate = Instantiate(TemplateItem);
                            DragAndDropItem tempDadItem = itemTemplate.transform.GetChild(0).GetComponent<DragAndDropItem>();
                            tempDadItem.gameObject.GetComponent<Image>().sprite = item.sprite;
                            tempDadItem.item = item;
                            temp[3].GetComponent<DragAndDropCell>().PlaceItem(tempDadItem);
                        }
                        break;
                    case "shortcut_5_id":
                        if (reader.GetInt32(i) > 200 && reader.GetInt32(i) < 300)
                        {
                            temp[4].GetComponent<DragAndDropCell>().PlaceItem(Instantiate(ctrl.FindSkillDADItemByID(reader.GetInt32(i))));
                        }
                        else if (reader.GetInt32(i) != -1)
                        {
                            Item item = shop.findItemByID(reader.GetInt32(i));
                            GameObject itemTemplate = Instantiate(TemplateItem);
                            DragAndDropItem tempDadItem = itemTemplate.transform.GetChild(0).GetComponent<DragAndDropItem>();
                            tempDadItem.gameObject.GetComponent<Image>().sprite = item.sprite;
                            tempDadItem.item = item;
                            temp[4].GetComponent<DragAndDropCell>().PlaceItem(tempDadItem);
                        }
                        break;
                    case "shortcut_6_id":
                        if (reader.GetInt32(i) > 200 && reader.GetInt32(i) < 300)
                        {
                            temp[5].GetComponent<DragAndDropCell>().PlaceItem(Instantiate(ctrl.FindSkillDADItemByID(reader.GetInt32(i))));
                        }
                        else if (reader.GetInt32(i) != -1)
                        {
                            Item item = shop.findItemByID(reader.GetInt32(i));
                            GameObject itemTemplate = Instantiate(TemplateItem);
                            DragAndDropItem tempDadItem = itemTemplate.transform.GetChild(0).GetComponent<DragAndDropItem>();
                            tempDadItem.gameObject.GetComponent<Image>().sprite = item.sprite;
                            tempDadItem.item = item;
                            temp[5].GetComponent<DragAndDropCell>().PlaceItem(tempDadItem);
                        }
                        break;
                    case "shortcut_7_id":
                        if (reader.GetInt32(i) > 200 && reader.GetInt32(i) < 300)
                        {
                            temp[6].GetComponent<DragAndDropCell>().PlaceItem(Instantiate(ctrl.FindSkillDADItemByID(reader.GetInt32(i))));
                        }
                        else if (reader.GetInt32(i) != -1)
                        {
                            Item item = shop.findItemByID(reader.GetInt32(i));
                            GameObject itemTemplate = Instantiate(TemplateItem);
                            DragAndDropItem tempDadItem = itemTemplate.transform.GetChild(0).GetComponent<DragAndDropItem>();
                            tempDadItem.gameObject.GetComponent<Image>().sprite = item.sprite;
                            tempDadItem.item = item;
                            temp[6].GetComponent<DragAndDropCell>().PlaceItem(tempDadItem);
                        }
                        break;
                }
            }
        }
    }
    #endregion
}
