using UnityEngine;
using System.Collections.Generic;

namespace SpaceQuestListScene
{
    public class QuestManager : MonoBehaviour
    {
        public List<QuestProgress> activeQuests;
        public QuestUIManager questUIManager;

        public void AddProgressToQuest(QuestData questData)
        {
            QuestProgress quest = activeQuests.Find(q => q.questData == questData);
            if (quest != null)
            {
                quest.AddProgress();
                if (questUIManager != null)
                    questUIManager.UpdateQuestUI();
            }
        }
    }
}