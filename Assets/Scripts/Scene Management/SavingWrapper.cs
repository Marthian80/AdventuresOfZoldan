using AdventureOfZoldan.Core;
using AdventureOfZoldan.Core.Saving;
using UnityEngine;

namespace AdventureOfZoldan.SceneManagement
{
    public class SavingWrapper : Singleton<SavingWrapper>
    {
        [SerializeField] KeyCode saveKey = KeyCode.Q;
        [SerializeField] KeyCode loadKey = KeyCode.L;
        [SerializeField] KeyCode deleteKey = KeyCode.Delete;
        private const string defaultSaveFile = "save";

        protected override void Awake()
        {
            base.Awake();
        }

        private void Update()
        {
            if (Input.GetKeyDown(saveKey))
            {
                Save();
            }
            if (Input.GetKeyDown(loadKey))
            {
                Load();
            }
            if (Input.GetKeyDown(deleteKey))
            {
                Delete();
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
        }
    }
}