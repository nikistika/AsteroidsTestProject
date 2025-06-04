using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameLogic.Enums;
using GameLogic.SaveLogic.SaveData;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine;

namespace SaveLogic
{
    public class UnityCloudSave : IUnityCloudSave
    {
        public async UniTask Initialize()
        {
           await SetupAndSignIn();
        }

        // This part of the code should be done at the beginning of your game flow (i.e. Main Menu)
        private async UniTask SetupAndSignIn()
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        public async UniTask SaveData(SaveConfig saveData)
        {
            string jsonSave = JsonUtility.ToJson(saveData);
            
            var data = new Dictionary<string, object>
            {
                { SaveKeys.GeneralData1.ToString(), jsonSave },
            };
            try
            {
                Debug.Log("Attempting to save data...");
                await CloudSaveService.Instance.Data.ForceSaveAsync(data);
                Debug.Log("Save data success!");
            }
            catch (ServicesInitializationException e)
            {
                // service not initialized
                Debug.LogError(e);
            }
            catch (CloudSaveValidationException e)
            {
                // validation error
                Debug.LogError(e);
            }
            catch (CloudSaveRateLimitedException e)
            {
                // rate limited
                Debug.LogError(e);
            }
            catch (CloudSaveException e)
            {
                Debug.LogError(e);
            }
        }

        public async UniTask<SaveConfig> LoadData()
        {
            SaveConfig saveData = new SaveConfig();
            string saveKey = SaveKeys.GeneralData1.ToString();
            
            var keysToLoad = new HashSet<string>
            {
                saveKey
            };
            try
            {
                var loadedData = await CloudSaveService.Instance.Data.LoadAsync(keysToLoad);

                if (loadedData.TryGetValue(saveKey, out var data))
                {
                    saveData = JsonUtility.FromJson<SaveConfig>(data);
                    Debug.Log("Loaded saved data: " + data);
                }
                else
                {
                    Debug.Log("Level not found in cloud save");
                }
            }
            
            catch (ServicesInitializationException e)
            {
                // service not initialized
                Debug.LogError(e);
            }
            catch (CloudSaveValidationException e)
            {
                // validation error
                Debug.LogError(e);
            }
            catch (CloudSaveRateLimitedException e)
            {
                // rate limited
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