using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BattleToCity : MonoBehaviour
{
#if UNITY_EDITOR
    public SceneAsset sceneAsset;  // 인스펙터에서 드래그할 씬 자산
#endif

    [HideInInspector]
    public string sceneName;       // 런타임에 사용할 씬 이름

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
            Debug.LogWarning("씬 이름이 설정되지 않았습니다!");
        }
    }
}
