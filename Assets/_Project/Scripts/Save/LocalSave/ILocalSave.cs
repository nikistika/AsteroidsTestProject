using _Project.Scripts.RemoteConfig;

namespace _Project.Scripts.Save.LocalSave
{
    public interface ILocalSave
    {
        public void SetSaveData(SaveConfig saveConfig);
        public SaveConfig GetSaveData();

    }
}