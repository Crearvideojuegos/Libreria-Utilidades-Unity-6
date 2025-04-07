using TMPro;
using UnityEngine;

namespace SpaceQuestListScene
{
    public class QuestUIManager : MonoBehaviour
    {
        public QuestManager questManager; // Asigna en el Inspector
        public TextMeshProUGUI questListText;

        void Start()
        {
            UpdateQuestUI();
        }

        public void UpdateQuestUI()
        {
            questListText.text = ""; // Limpia el texto actual

            foreach (var quest in questManager.activeQuests)
            {
                string progress = $"{quest.currentCount}/{quest.questData.targetCount}";
                string questName = quest.questData.questName;

                string line;

                if (quest.IsCompleted)
                    line = $"<s>{questName}: {progress}</s>"; // Tachado
                else
                    line = $"{questName}: {progress}";

                questListText.text += line + "\n";
            }
        }
    }
}