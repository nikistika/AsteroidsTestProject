using UI.View;

namespace UI
{
    public class GameplayUIRepository
    {
        public GameplayUIView GameplayUIObject { get; private set; }

        public void GetGameplayUIObject(GameplayUIView gameplayUIObject)
        {
            GameplayUIObject = gameplayUIObject;
        }
    }
}