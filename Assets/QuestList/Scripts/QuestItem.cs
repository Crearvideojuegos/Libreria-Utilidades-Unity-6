using UnityEngine;

namespace SpaceQuestListScene
{
    public class QuestItem : MonoBehaviour
    {
        public QuestData questToAffect;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                FindFirstObjectByType<QuestManager>().AddProgressToQuest(questToAffect);
                Destroy(gameObject);
            }
        }
    }
}