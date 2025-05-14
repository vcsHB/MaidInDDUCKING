using Core.StageController;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TitleScene
{

    public class TitleSceneManager : MonoBehaviour
    {
        [SerializeField] private StageSO _bossStage;
        private string _folderPath = Path.Combine(Application.dataPath, "Save");

        public void HandleStart()
        {
            StageManager.Instance.LoadCurrentScene();
            //SceneManager.LoadScene(_startConnectSceneName);
        }

        public void HandleStartBoss()
        {
            StageManager.Instance.currentStage = _bossStage;
            StageManager.Instance.LoadCurrentScene();
        }

        public void ResetData()
        {
            if(Directory.Exists(_folderPath))
            {
                string[] files = Directory.GetFiles(_folderPath);

                 Debug.Log("������ ���ϵ� : " + files.Length);
                foreach (string file in files)
                    File.Delete(file);

                StageManager.Instance.Save();
            }
            else
            {
                Debug.Log("�ֵ�");
            }
        }

        public void HandleQuit()
        {
            Application.Quit();
        }
    }

}