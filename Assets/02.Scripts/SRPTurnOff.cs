using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
public class DisableSRPBatcher : MonoBehaviour
{
    void OnEnable()
    {
        GraphicsSettings.useScriptableRenderPipelineBatching = false;
    }

    void OnDisable()
    {
        GraphicsSettings.useScriptableRenderPipelineBatching = true; // 원상복구 (선택사항)
    }
}

