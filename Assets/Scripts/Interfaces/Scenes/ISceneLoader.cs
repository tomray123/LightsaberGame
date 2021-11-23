using UnityEngine.UI;

public interface ISceneLoader
{
    void LoadScene(int sceneIndex);

    // Gets scene index from its name.
    void LoadScene(Text sceneName);
}
