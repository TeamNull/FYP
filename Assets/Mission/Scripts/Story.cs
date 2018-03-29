using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Story : MonoBehaviour
{

    public Scene UI;
    public GameObject playerUI;
    public GameObject storyBoard;
    public Text storytext;
    public Button nextline;
    private int counter = 0;
    private string playername = "Player";
    private int time = 1;
    void Start()
    {
      
    }

    public void Callstory(int mission)
    {
        StartCoroutine(Loadstory(mission));
    }

    IEnumerator Loadstory(int mission)
    {
        playerUI.SetActive(false);
        storyBoard.SetActive(true);

        if (mission == 0)
        {
            storytext.text = playername + ": Where should I go?";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": Oh, maybe I should go to the church and ask the father.";

            yield return new WaitForSeconds(time);
        }

        if(mission == 1)
        {
            storytext.text = "Father: You look so distressed, young gentleman. What happens?";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": Can you help me?";

            yield return new WaitForSeconds(time);

            storytext.text = "Father: Everyone can get help from here. So, what problem are you facing?";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": (sharing)";

            yield return new WaitForSeconds(time);

            storytext.text = "Father: Alright. Um…You stay here first and continue to think what you should do next.";

            yield return new WaitForSeconds(time);

            storytext.text = "Father: There are so many people become homeless since the beginning of the attack from enemies.";

            yield return new WaitForSeconds(time);

            storytext.text = "Father:  Find something to work on, you will be fine later.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": ...............";

            yield return new WaitForSeconds(time);

            storytext.text = "Father: I believe you should have the ability to fight as you come to the village alone.";

            yield return new WaitForSeconds(time);

            storytext.text = "Father: Go to find the Chief of the warrior to ask if there is any job for you.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": Ok!";

            yield return new WaitForSeconds(time);

        }


        
        if(mission == 2)
        {
            storytext.text = "Chief of the warrior: Youngster, What's happening?";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": Is there any job for me to finish?";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: Only a warrior can get a job from here.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": How to become a warrior?";

            yield return new WaitForSeconds(time);

            storytext.text = "If you go to the forest and hunt 3 monsters, I will admit you are a warrior.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": OK! Wait for me.";

            yield return new WaitForSeconds(time);

        }

        if (mission == 3)
        {
            storytext.text = "Chief of the warrior: Unbelievable! How come you can finish it?";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: To keep my promise, I admit you as a warrior from now on.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": So, what can I get from here?";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: For now, I do not have any jobs for you.";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: There is a collection mission, will you accept it?";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": Sure!";

            yield return new WaitForSeconds(time);

        }

        if (mission == 4)
        {
            storytext.text = "Chief of the warrior: You are a qualified warrior now.";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: I have received a news from the front line of the military.";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: One unknown has passed through our defense line.";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: Can you go to find out the unknown and bring me some information about it ?";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": This is my honor.";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: Go my son. Be careful, don’t lose your life.";

            yield return new WaitForSeconds(time);

        }

        if (mission == 4)
        {
            storytext.text = "Chief of the warrior: You are a qualified warrior now.";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: I have received a news from the front line of the military.";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: One unknown has passed through our defense line.";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: Can you go to find out the unknown and bring me some information about it ?";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": This is my honor.";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: Go my son. Be careful, don’t lose your life.";

            yield return new WaitForSeconds(time);

        }

        if (mission == 5)
        {
            storytext.text = "Chief of the warrior: Well, nice to see you. You should take more rest.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": I am fine. Is the situation very bad right now?";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: The people you killed is a scout.";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior:  We have lost the first defense line and this village will become the front line very soon.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": No, I am a warrior! Protecting the village is my responsibility!";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: Great, are you ready to receive the mission?";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": Yes, I am ready.";

            yield return new WaitForSeconds(time);

        }

        if (mission == 6)
        {
            storytext.text = playername + ": Here is the map. However, nothing there...";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: You are right. That's what unknown is. They know only destroying.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": Well.... Will here become the front line?";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: Yes.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": What else should I do?";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: Go to find Chief of the army and discuss with him about what happened to you.";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: You are the only one have those experience. Well, hoping both of you can save more soldiers.";

            yield return new WaitForSeconds(time);

        }

        if (mission == 7)
        {
            storytext.text = playername + ": Hello, I am....";

            yield return new WaitForSeconds(time);

            storytext.text = "Army chief: I know who you are, our hero! I know that you come here is to share things happened to you.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": (sharing)";

            yield return new WaitForSeconds(time);

            storytext.text = "Army chief: Ok, I understand what you mean. I will pass it to our soldiers.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": May the god be with you.";

            yield return new WaitForSeconds(time);

        }

        if (mission == 8)
        {
            storytext.text = "Chief of the warrior: Welcome back! All things go right?";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": I am fine. Is the situation very bad right now?";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: The people you killed is a scout.";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior:  We have lost the first defense line and this village will become the front line very soon.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": No, I am a warrior! Protecting the village is my responsibility!";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: Great, are you ready to receive the mission?";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": Yes, I am ready.";

            yield return new WaitForSeconds(time);

        }



        storyBoard.SetActive(false);
        playerUI.SetActive(true);

      
    }
}
