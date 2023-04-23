using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class QuestGiver : NPC
{
    public bool AssignedQuest { get; set; }
    public bool Helped { get; set; }

    [SerializeField]
    private GameObject quests;

    [SerializeField]
    private string questType;
    private Quest Quest { get; set; }
    public override void Interact()
    {
        
        if (!AssignedQuest && !Helped)
        {
            base.Interact();
            Debug.Log("Interacting with QuestGiver.");
            AssignQuest();
            Debug.Log("Quest Assigned.");
        }
        else if(AssignedQuest && !Helped)
        {
            CheckQuest();
            Debug.Log("Quest Checked.");
        }
        else
        {
            DialogueSystem.Instance.AddNewDialogue(new string[] { "Thanks for that stuff that one time." }, name);
        }
    }

    void AssignQuest()
    {
        AssignedQuest = true;
        Quest = (Quest)quests.AddComponent(System.Type.GetType(questType));
    }

    void CheckQuest()
    {
        if (Quest.Completed)
        {
            Helped = true;
            AssignedQuest = false;
            DialogueSystem.Instance.AddNewDialogue(new string[] {"Thanks for that! Here's your reward.", "More dialogue"}, name);
        }
        else
        {
            DialogueSystem.Instance.AddNewDialogue(new string[] { "You're still in the middle of helping me. Get back at it!"}, name);
        }
    }
}
