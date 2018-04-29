using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttribute : MonoBehaviour
{
    #region Variable
    public enum Classes { Warrior, Archer, Magician }
    public delegate void LevelUpHandler();
    public event LevelUpHandler LevelUp;
    public UIinfo playerUiScript;
    public Attribute attributeScript;
    public GameObject levelUp;
    public int[] Skill = new int[5];
    public int currentHP;
    public int currentMP;
    public int maxHP;           //Include additionalHP
    public int maxMP;           //Include additionalMP
    public int str;
    public int _int;
    public int agi;
    public int additionalAtk;   //used for add
    public int additionalDef;   //used for add
    public int additionalSpeed; //used for add
    public int additionalHP;    //used for add
    public int additionalMP;    //used for add
    public int atk;             //total included additional
    public int def;             //total included additional
    public float attackSpeed;   //total included additional
    public string playerName;
    public Classes job;
    public int AvailablePoint;
    public int SkillPoint;
    public int currentLevel;
    public int currentExp;
    //public int hpCoe;
    //public int mpCoe;
    //public int atkCoe;
    //public int atkLvCoe;
    //public int defCoe;
    //public int defLvCoe;
    int totalExp;
    public int needExp;
    const int baseExp = 100;
    Animator anim;
    GameObject loadingScene;
    GameObject deadScene;
    float timer;
    #endregion

    #region LifeCycle
    // Use this for initialization
    void Awake()
    {
        //attribute initialization
        InitialPlayerAttribute();
        //player anim
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        GameObject[] uiGameObjectArray = SceneManager.GetSceneByName("UI").GetRootGameObjects();

        foreach (GameObject go in uiGameObjectArray)
        {
            if (go.name == "PlayerUI")
            {
                loadingScene = go.transform.Find("Loading Scene").gameObject;
                deadScene = go.transform.Find("Loading Scene (1)").gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1 && !GameManager.PlayerIsDead) {
            currentHP = currentHP + 1 > maxHP ? maxHP : currentHP + 1;
            currentMP = currentMP + 1 > maxMP ? maxMP : currentMP + 1;
            playerUiScript.updateHP(currentHP, maxHP);
            playerUiScript.updateMP(currentMP, maxMP);
            timer = 0;
        }
    }
    #endregion

    #region Method

    public void TakeDamge(int damage)
    {
        if (GameManager.PlayerIsDead) return;

        currentHP -= ((damage - def) > 1) ? (damage - def) : 1;
        playerUiScript.updateHP(currentHP, maxHP);
        anim.SetTrigger("Damaged");
        if (currentHP <= 0)
        {
            GameManager.PlayerIsDead = true;
            anim.SetTrigger("Dead");
            GameManager.inGameLog.AddLog("You are dead.", Color.red);
            deadScene.SetActive(true);
            StartCoroutine(dieAction());
            //loadingScene.SetActive(true);
            //if (SceneManager.GetActiveScene().name.Equals("Forest")) this.transform.position = new Vector3(57.46f, 1.572f,-4.608f);
            //if (SceneManager.GetActiveScene().name.Equals("Runis")) this.transform.position = new Vector3(27.87f,6.18f,0.89f);
        }
    }

    IEnumerator dieAction()
    {
        yield return new WaitForSeconds(3f);
        loadingScene.SetActive(true);
        deadScene.SetActive(false);
        GameManager.PlayerIsDead = false;
        currentHP = maxHP;
        UpdatePlayerValueByPoint();
        anim.SetTrigger("Attack");
        GameManager.inGameLog.AddLog("You are recovered.", Color.white);
        if (SceneManager.GetActiveScene().name.Equals("Forest")) this.transform.position = new Vector3(57.46f, 1.572f, -4.608f);
        if (SceneManager.GetActiveScene().name.Equals("Ruins")) this.transform.position = new Vector3(27.87f, 6.18f, 0.89f);
    }

    public bool ConsumeMP(int value)
    {
        if (GameManager.PlayerIsDead) return false;
        if (currentMP < value) return false;
        currentMP -= value;
        playerUiScript.updateMP(currentMP, maxMP);
        return true;
    }

    public void GainExp(int sourceExp, int sourceLevel)
    {
        if (GameManager.PlayerIsDead) return;
        //penalty and bonus for the level difference between player and monster
        //if sourceLevel==0 which is mission, no penaly or bonus will be apply
        float temp = sourceExp;
        bool isLvUp = false;
        temp *= (!(sourceLevel == 0)) ? (((float)(sourceLevel - currentLevel)) / 100f) : 1;
        sourceExp += (int)temp;
        currentExp += sourceExp;
        totalExp += sourceExp;
        //check if level up
        string tempLogText = "You have earned " + sourceExp + " EXP.";
        GameManager.inGameLog.AddLog("You have earned " + sourceExp + " EXP.", Color.magenta);
        while (currentExp >= needExp)
        {
            GameManager.AudioManager.GetComponent<BGMcontrol>().Playsound("LevelUp");
            currentExp -= needExp;
            //Debug.Log("cur lv " + currentLevel+ " cur lv/10 " + (currentLevel / 10)+ " cur lv%10 " + (currentLevel % 10));
            float expCoeLvRange = 3 - (currentLevel / 10);
            float expCoeLvTune = 10 - (currentLevel % 10);
            if (currentLevel >= 30)
            {
                expCoeLvRange = (currentLevel / 10);
                expCoeLvTune = (currentLevel % 10);
            }
            float expCoefficient = 1 + expCoeLvRange * 0.05f + expCoeLvTune * 0.005f;
            needExp = (int)Mathf.Floor(needExp * expCoefficient);
            Debug.Log("lv " + currentLevel + "needed " + needExp);
            currentLevel++;
            isLvUp = true;
            AvailablePoint++;
            SkillPoint++;
            currentHP = maxHP;
            currentMP = maxMP;
            if (LevelUp != null) LevelUp();
        }
        playerUiScript.updateEXP(currentLevel, currentExp, needExp, isLvUp);
        playerUiScript.updateHP(currentHP, maxHP);
        if (isLvUp) levelUp.SetActive(true);
        isLvUp = false;
    }

    //public void SetCoe() {
    //    switch (job)
    //    {
    //        case Classes.Warrior:
    //            hpCoe = 7;
    //            mpCoe = 10;
    //            atkCoe = 5;
    //            atkLvCoe = 1;
    //            defCoe=1;
    //            defLvCoe =3;
    //            break;
    //        case Classes.Archer:
    //            hpCoe = 5;
    //            mpCoe = 12;
    //            atkCoe = 4;
    //            atkLvCoe = 2;
    //            defCoe = 2;
    //            defLvCoe = 2;
    //            break;
    //        case Classes.Magician:
    //            hpCoe = 5;
    //            mpCoe = 15;
    //            atkCoe = 2;
    //            atkLvCoe = 1;
    //            defCoe = 1;
    //            defLvCoe = 2;
    //            break;
    //    }
    //}

    //initial
    void InitialPlayerAttribute()
    {
        //playerName = "Fish";
        //job = Classes.Warrior;

        //set name
        //set job,enable character,set staticvarandfunc
        playerName = CreateNewCharacter.nameOfPlayer;
        //switch by name later
        switch (CreateNewCharacter.numOfSelectedCharacter) {
            case 0:
                job = Classes.Warrior;
                str = 5;
                _int = 5;
                agi = 3;
                maxHP = 300;
                break;
            case 1:
                job = Classes.Archer;
                str = 3;
                _int = 7;
                agi = 3;
                maxHP = 270;
                break;
            case 2:
                job = Classes.Magician;
                str = 3;
                _int = 3;
                agi = 7;
                maxHP = 250;
                break;
        }
        maxMP = 100;
        //SetCoe();

        additionalAtk = 0;
        additionalDef = 0;
        additionalSpeed = 0;
        additionalHP = 0;
        additionalMP = 0;
        
        UpdatePlayerValueByPoint(); //could be deleted if the save is loaded

        currentLevel = 1;
        currentExp = 0;
        needExp = baseExp;
        AvailablePoint = 0;
        SkillPoint = 0;

        //exp ui update
        playerUiScript.updateEXP(currentLevel, currentExp, needExp, false);

        currentHP = maxHP;
        currentMP = maxMP;
        //hp ui update
        playerUiScript.updateHP(currentHP, maxHP);

        //mp ui update
        playerUiScript.updateMP(currentMP, maxMP);
        Skill.Initialize();

    }

    //call after point change,initial,equipment,skill
    public void UpdatePlayerValueByPoint()
    {
        switch (job)
        {
            case Classes.Warrior:
                atk = 10 + str * 5 + currentLevel + additionalAtk;
                def = agi + currentLevel * 2+additionalDef;
                maxHP = 300 + str * 7 + currentLevel * 10 + additionalHP;
                maxMP = 100 + _int * 10+additionalMP;
                break;
            case Classes.Archer:
                atk = 10 + agi * 4 + currentLevel*2+ additionalAtk;
                def = agi * 2 + currentLevel * 2+additionalDef;
                maxHP = 270 + str * 5 + currentLevel * 10+ additionalHP;
                maxMP = 100 + _int * 13 + additionalMP;
                break;
            case Classes.Magician:
                atk = _int * 2 + additionalAtk;
                def = agi + currentLevel * 2+additionalDef;
                maxHP = 250 + str * 5 + currentLevel * 10 + additionalHP;
                maxMP = 100 + _int * 15 + additionalMP;
                break;
        }
        //def = Mathf.CeilToInt(agi * 0.5f) + additionalDef;
        //attackSpeed = 1 + additionalSpeed;    //Todo: Calculate attackSpeed by agi
        //maxHP = 100 + str * 8 +additionalHP;
        //maxMP = 100 + _int * 5 +additionalMP;
        currentHP = (currentHP < maxHP) ? currentHP : maxHP;
        currentMP = (currentMP < maxMP) ? currentMP : maxMP;

        //update ui and attribute page after value and point update        

        //hp ui update
        playerUiScript.updateHP(currentHP, maxHP);

        //mp ui update
        playerUiScript.updateMP(currentMP, maxMP);

        //update attribute page
        //attributeScript.UpdatePlayerInfo();
    }
    #endregion
}
