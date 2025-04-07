using UnityEngine;

namespace SpaceQuestListScene
{
    [CreateAssetMenu(fileName = "QuestData", menuName = "Scriptable Objects/QuestData")]
    public class QuestData : ScriptableObject
    {
        public string questName;
        public string description;
        public int targetCount;
    }
}