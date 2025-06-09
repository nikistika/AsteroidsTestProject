using _Project.Scripts.UI.MenuScene;
using Zenject;

namespace _Project.Scripts.EntryPoints
{
    public class MenuEntryPoint : IInitializable
    {
        private readonly MenuUISpawner _menuUISpawner;

        public MenuEntryPoint(
            MenuUISpawner menuUISpawner)
        {
            _menuUISpawner = menuUISpawner;
        }

        public async void Initialize()
        {
            await _menuUISpawner.StartWork();
        }
    }
}