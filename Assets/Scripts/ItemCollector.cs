using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int items = 0;

    [SerializeField] private TextMeshProUGUI itemsText;

    [SerializeField] private AudioSource collectionSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Items")) 
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            items++;
            itemsText.text = "Items: " + items;
        }
    }
}
