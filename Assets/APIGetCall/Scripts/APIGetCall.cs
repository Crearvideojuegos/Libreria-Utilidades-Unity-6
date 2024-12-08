using Newtonsoft.Json;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using System;

namespace SpaceAPIGetCall
{

    public class Fact
    {
        public string fact { get; set; }
    }

    public class APIGetCall : MonoBehaviour
    {
        public TextMeshProUGUI text;

        private void Start()
        {
            OnRefresh();
        }

        public void OnRefresh()
        {
            StartCoroutine(GetRequest("https://catfact.ninja/fact"));
        }

        private IEnumerator GetRequest(String uri)
        {
            using(UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                yield return webRequest.SendWebRequest();

                switch(webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError(String.Format("Something went wrong: {0}", webRequest.error));
                        break;
                    case UnityWebRequest.Result.Success:
                        Fact fact = JsonConvert.DeserializeObject<Fact>(webRequest.downloadHandler.text);
                        text.text = fact.fact;
                        break;
                }
            }
        }
    }
}

