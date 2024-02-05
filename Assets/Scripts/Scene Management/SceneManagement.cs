using AdventureOfZoldan.Core;

namespace AdventureOfZoldan.SceneManagement
{
    public class SceneManagement : Singleton<SceneManagement>
    {
        public string SceneTransitionName { get; private set; }

        protected override void Awake()
        {
            base.Awake();
        }

        public void SetTransitionName(string sceneTransitionName)
        {
            this.SceneTransitionName = sceneTransitionName;
        }
    }
}