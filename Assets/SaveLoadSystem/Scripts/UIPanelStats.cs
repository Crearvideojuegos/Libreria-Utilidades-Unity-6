using TMPro;
using UnityEngine;


namespace SpaceSaveLoadSystem
{
    public class UIPanelStats : MonoBehaviour
    {
        public static UIPanelStats Instance; //Singleton
        [SerializeField] private TMP_Text _actualCoins;
        [SerializeField] private TMP_Text _actualTime;
        [SerializeField] private TMP_Text _savedCoins;
        [SerializeField] private TMP_Text _savedTime;
        private int coinsSaved;
        private float timeSaved;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            LoadData();
        }

        private void Update()
        {
            ActualTime();
            _actualCoins.text = "Coins: " + PlayerController.Instance.GetCoins();
        }

        private void ActualTime()
        {
            float actualTime = PlayerController.Instance.GetTimePlayed();
            _actualTime.text = "Seconds: " + actualTime.ToString("f0");
        }

        public void LoadData()
        {
            string myData = GameDataManager.Instance.GenerateKey(1, 1);
            if (GameDataManager.Instance.gameDataDictionary.TryGetValue(myData, out GameData myDataLoaded))
            {
                coinsSaved = myDataLoaded.coins;
                timeSaved = myDataLoaded.timePlayed;
                PlayerController.Instance.ResetCoins();            
                PlayerController.Instance.ResetTimePlayed();
                
                _actualCoins.text = "Coins: 0";
                _actualTime.text = "Seconds: 0";
                _savedCoins.text = "Coins: " + myDataLoaded.coins.ToString();
                _savedTime.text = "Seconds: " + myDataLoaded.timePlayed.ToString("f0");
            }
            else
            {
                coinsSaved = 0;
                timeSaved = 0f;
                PlayerController.Instance.ResetCoins();
                PlayerController.Instance.ResetTimePlayed();

                _actualCoins.text = "Coins: 0";
                _actualTime.text = "Seconds: 0";
                _savedCoins.text = "Coins: 0";
                _savedTime.text = "Seconds: 0";
            }
        }

        public void SaveData()
        {
            if(PlayerController.Instance.GetSave())
            {
                int coins = PlayerController.Instance.GetCoins() + coinsSaved;
                float timePlayed = PlayerController.Instance.GetTimePlayed() + timeSaved;

                GameDataManager.Instance.AddOrUpdateGameData(1, 1, coins, timePlayed);
                PlayerController.Instance.ResetCoins();            
                PlayerController.Instance.ResetTimePlayed();
                LoadData();
                PlayerController.Instance.YouSave();
            }

        }

    }
}