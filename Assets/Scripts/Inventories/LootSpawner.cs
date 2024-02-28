using AdventureOfZoldan.Core.Saving;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureOfZoldan.Inventories
{
    public class LootSpawner : MonoBehaviour, ISaveable
    {
        // CONFIG DATA
        [SerializeField] private List<InventoryItem> randomLootItems = new List<InventoryItem>();

        private Pickup spawnedPickup = null;

        public void DropLoot()
        {
            if (spawnedPickup == null)
            {
                Debug.Log("Attempt to dtop loot");
                SpawnPickup();
            }            
        }

        public Pickup GetPickup()
        {
            return GetComponentInChildren<Pickup>();
        }

        public bool isCollected()
        {
            return GetPickup() == null;
        }

        private void SpawnPickup()
        {
            var randomNumber = Random.Range(0, randomLootItems.Count);           
            
            spawnedPickup = randomLootItems[randomNumber].SpawnPickup(transform.position);
            Debug.Log($"Going to drop {spawnedPickup.GetItem().GetItemID()}");
            spawnedPickup.transform.SetParent(transform);            
        }

        private void DestroyPickup()
        {
            if (GetPickup())
            {
                Destroy(GetPickup().gameObject);
            }
        }

        public object CaptureState()
        {
            Dictionary<string, bool> data = new Dictionary<string, bool>();
            data["isCollected"] = isCollected();
            data["hasSpawned"] = spawnedPickup != null;
            return data;
        }

        public void RestoreState(object state)
        {
            Dictionary<string, bool> data = (Dictionary<string, bool>)state;
            bool shouldBeCollected = data["isCollected"];
            bool pickupHasSpawned = data["hasSpawned"];

            if (!pickupHasSpawned) { return; }
            else
            {
                if (shouldBeCollected && !isCollected())
                {
                    DestroyPickup();
                }
                if (!shouldBeCollected && isCollected())
                {
                    SpawnPickup();
                }
            }
        }
    }
}