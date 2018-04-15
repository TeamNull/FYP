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
    //private int counter = 0;
    private string playername = "Player";
    private int time = 1;

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

            storytext.text = playername + ": I have told the experience to him.";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: That’s great. By the way, are you interested to be one of the warriors of our team?";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": Thank you for your invitation, but I think I can have more freedom and help more people if I am an adventurer.";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: Ok, you must take care yourself. We always welcome you.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": I understand. Thank you!";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: For now, we need to know more about the unknown in order to have better planning.";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: Go out to get some intelligence about them.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": Ok!";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: I hope the peace will come soon.";

            yield return new WaitForSeconds(time);

        }


        if (mission == 9)
        {
            storytext.text = "Chief of the warrior: Great! After we have that intelligence, we can set up a perfect plan to fight against them.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": Good.";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: Next, we are going to the preparation stage for the war. We need you to collect some materials.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": OK.";

            yield return new WaitForSeconds(time);

        }

        if (mission == 10)
        {
            storytext.text = "Chief of the warrior: Thank you for your contribution. We have already finished the setup of our front line base.";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: Go to the front line base and find the commander there. He needs your help.";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: Go! May the god be with you!";

            yield return new WaitForSeconds(time);

        }

        if (mission == 11)
        {
            storytext.text = playername + ": I heard from Chief of the warrior that you request for an assistant, what can I help you ?";

            yield return new WaitForSeconds(time);

            storytext.text = "Commander: Great! We do not have enough people here so can you help us to find out some material that we lost in the forest?";

            yield return new WaitForSeconds(time);

            storytext.text = "Commander: We lost the material because of the heavy rainfall a few days ago and they are important for us.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": Ok!";

            yield return new WaitForSeconds(time);

        }

        if (mission == 12)
        {
            storytext.text = "Commander: Wow! You did it.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": Just luck.";

            yield return new WaitForSeconds(time);

            storytext.text = "Commander: Ok! As we can finish the construction work of the front line base earlier, I am more confident that we can handle the attack from Unknown.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": That’s great.";

            yield return new WaitForSeconds(time);

            storytext.text = "Commander: But the situation is still not that positive.";

            yield return new WaitForSeconds(time);

            storytext.text = "Commander: Here is the last defense line for human - being, you should go back to the village as this base will under military control very soon.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": I want to work for you and all the people.";

            yield return new WaitForSeconds(time);

            storytext.text = "Commander: There is no mission for you! Go and find the Chief of the warrior now.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": OK…. See you later.";

            yield return new WaitForSeconds(time);

        }

        if (mission == 13)
        {
            storytext.text = "Chief of the warrior: You come back again.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": Yes.";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: ............";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": ............"; 

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: he war is coming. Do you have anything want to do before the war begin ? ";

            yield return new WaitForSeconds(time);


            storytext.text = "Chief of the warrior: You cannot regret after you have joined the war.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": To find out where I come from.";

            yield return new WaitForSeconds(time);

            storytext.text = "Chief of the warrior: You should ask father. He may know something about it.";

            yield return new WaitForSeconds(time);

        }

        if (mission == 14)
        {
            storytext.text = playername + ": Father, do you know where am I come from?";

            yield return new WaitForSeconds(time);

            storytext.text = "Father: How can I know, my son.";

            yield return new WaitForSeconds(time);

            storytext.text = "Father: Ah! You have mentioned that you came from the forest and owned a weapon, maybe you came from the front line ? ";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": I don’t know…I can’t remember anything about my born.";

            yield return new WaitForSeconds(time);

            storytext.text = "Father: Never mind! Just look forward right now, focus on the thing you need to do now!";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": But I don’t have any mission now, I feel lost.";

            yield return new WaitForSeconds(time);

            storytext.text = "Father: God will lead your way, Son. Go to the front line and find what you want to find.";

            yield return new WaitForSeconds(time);

        }

        if (mission == 15)
        {
            storytext.text = playername + ": I want to go into front line.";

            yield return new WaitForSeconds(time);

            storytext.text = "Commander: It is very dangerous. Why do you want to go inside?";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": (sharing)";

            yield return new WaitForSeconds(time);

            storytext.text = "Commander: Although you may find some information there, what is happening now is more important than the past.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": I have made my decision; I want to go!";

            yield return new WaitForSeconds(time);

            storytext.text = "Commander: OK! There is a mission of collecting some material on the front line.";

            yield return new WaitForSeconds(time);

            storytext.text = "Commander: You need to take this mission in order to pass throughout defense line. Will you accept it?";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": Yes, I will.";

            yield return new WaitForSeconds(time);

            storytext.text = "Commander: Be careful with unknown.";

            yield return new WaitForSeconds(time);

        }

        if (mission == 16)
        {
            storytext.text = playername + ": Why this place looks so familiar? I have never come to here before.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": Let me check out the reason behind it.";

            yield return new WaitForSeconds(time);

        }

        if (mission == 17)
        {
            storytext.text = playername + ": Why there is just some useless paperwork?";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": Never mind, let me continue to find. Oh NO! There is an unknown!";

            yield return new WaitForSeconds(time);

        }


        if (mission == 18)
        {
            storytext.text = playername + ": Wow, the unknown is dead and I am still alive.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": Why there is a brand on the floor? Why my name has print on it ? ";

            yield return new WaitForSeconds(time);

        }

        if(mission == 19)
        {
            storytext.text = playername + ": The unknown is dead and I am still alive.";

            yield return new WaitForSeconds(time);

            storytext.text = playername + ": Commander, why there is a brand on the floor? Why my name has print on it ? ";

            yield return new WaitForSeconds(time);
            storytext.text = playername + ": Do you what this brand is?";

            yield return new WaitForSeconds(time);

            storytext.text = "Commander: This..........Oh.........., You are..........";

            yield return new WaitForSeconds(time);
        }

        storyBoard.SetActive(false);
        playerUI.SetActive(true);

      
    }
}
