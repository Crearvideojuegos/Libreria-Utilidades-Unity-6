using UnityEngine;

namespace SpaceQuestListScene
{
    [System.Serializable]
    public class QuestProgress
    {
        public QuestData questData;
        public int currentCount;

        public bool IsCompleted => currentCount >= questData.targetCount;

        public void AddProgress()
        {
            if (!IsCompleted)
            {
                currentCount++;
                Debug.Log($"Progreso: {currentCount}/{questData.targetCount}");

                if (IsCompleted)
                    Debug.Log($"¡Quest '{questData.questName}' completada!");
            }
        }
    }
}