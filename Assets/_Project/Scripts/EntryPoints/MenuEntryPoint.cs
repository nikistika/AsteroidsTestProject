using _Project.Scripts.UI.MenuScene;
using Zenject;

namespace _Project.Scripts.EntryPoints
{
    public class MenuEntryPoint : IInitializable
    {
        private readonly MenuUIFactory _menuUIFactory;

        public MenuEntryPoint(
            MenuUIFactory menuUIFactory)
        {
            _menuUIFactory = menuUIFactory;
        }

        public async void Initialize()
        {
            await _menuUIFactory.StartWork();
        }
    }
}