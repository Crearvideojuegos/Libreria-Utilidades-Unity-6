using System.Collections;
using UnityEngine;

namespace SpaceSaveLoadSystem
{
    public class Coin : MonoBehaviour
    {
        private MeshRenderer meshRenderer;
        private CapsuleCollider capsuleCollider;
        private bool coinPicked;


        private void Awake()
        {
            coinPicked = false;
            meshRenderer = GetComponent<MeshRenderer>();
            capsuleCollider = GetComponent<CapsuleCollider>();
        }

        private void Start()
        {
            meshRenderer.enabled = true;
            capsuleCollider.enabled = true;
        }

        private void FixedUpdate()
        {
            transform.Rotate(0, 0, 50f * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!coinPicked && other.CompareTag("Player"))
            {
                
                coinPicked = true;
                meshRenderer.enabled = false;
                capsuleCollider.enabled = false;
                other.GetComponent<PlayerController>().CoinPicked();
                StartCoroutine(ShowAgain());
            }
        }

        private IEnumerator ShowAgain()
        {
            yield return new WaitForSeconds(5f);
            coinPicked = false;
            meshRenderer.enabled = true;
            capsuleCollider.enabled = true;
        }


    }
}