using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BattleToCity : MonoBehaviour
{
#if UNITY_EDITOR
    public SceneAsset sceneAsset;  // �ν����Ϳ��� �巡���� �� �ڻ�
#endif

    [HideInInspector]
    public string sceneName;       // ��Ÿ�ӿ� ����� �� �̸�

    void OnValidate()
    {
#if UNITY_EDITOR
        if (sceneAsset != null)
        {
            string path = AssetDatabase.GetAssetPath(sceneAsset);
            sceneName = System.IO.Path.GetFileNameWithoutExtension(path);
        }
#endif
    }

    public void GoToBattleScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("�� �̸��� �������� �ʾҽ��ϴ�!");
        }
    }
}
