using System.Collections.Generic;
using _Project.Scripts.Enums;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine;

namespace _Project.Scripts.Save.CloudSave
{
    public class UnityCloudSave : ISave
    {
        public async UniTask Initialize()
        {
            await SetupAndSignIn();
        }

        private async UniTask SetupAndSignIn()
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        public async UniTask SaveData(SaveData saveData)
        {
            string jsonSave = JsonUtility.ToJson(saveData);

            var data = new Dictionary<string, object>
            {
                { SaveKeys.GeneralData2.ToString(), jsonSave },
            };
            try
            {
                Debug.Log("Attempting to save data...");
                await CloudSaveService.Instance.Data.ForceSaveAsync(data);
                Debug.Log("Save data success!");
            }
            catch (ServicesInitializationException e)
            {
                Debug.LogError(e);
            }
            catch (CloudSaveValidationException e)
            {
                Debug.LogError(e);
            }
            catch (CloudSaveRateLimitedException e)
            {
                Debug.LogError(e);
            }
            catch (CloudSaveException e)
            {
                Debug.LogError(e);
            }
        }

        public async UniTask<SaveData> GetData()
        {
            SaveData saveData = new SaveData();
            string saveKey = SaveKeys.GeneralData2.ToString();

            var keysToLoad = new HashSet<string>
            {
                saveKey
            };
            try
            {
                var loadedData = await CloudSaveService.Instance.Data.LoadAsync(keysToLoad);

                if (loadedData.TryGetValue(saveKey, out var data))
                {
                    saveData = JsonUtility.FromJson<SaveData>(data);
                    Debug.Log("Loaded saved data: " + data);
                }
                else
                {
                    Debug.Log("Level not found in cloud save");
                }
            }

            catch (ServicesInitializationException e)
            {
                Debug.LogError(e);
            }
            catch (CloudSaveValidationException e)
            {
                Debug.LogError(e);
            }
            catch (CloudSaveRateLimitedException e)
            {
                Debug.LogError(e);
            }
            catch (CloudSaveException e)
            {
                Debug.LogError(e);
            }

            return saveData;
        }
    }
}