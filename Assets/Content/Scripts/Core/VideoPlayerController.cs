using UnityEngine.Video;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class VideoPlayerController : MonoBehaviour
{
    [SerializeField] private RenderTexture rt, rt_alpha;
    [SerializeField] private VideoPlayer vp, vp_alpha;
    public delegate void VoidDelegate();
    public static VoidDelegate PlayVideo, PauseVideo, StopVideo;
    public static bool VideoEnded;

    private void Awake()
    {
        // rt = new RenderTexture((int)UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), "video_width"),
        // (int)UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), "video_height"),
        // 0);
        // GetComponent<RawImage>().texture = rt;
        // vp = GetComponent<VideoPlayer>();
        // vp.renderMode = VideoRenderMode.RenderTexture;
        // vp.targetTexture = rt;
        // vp.url = Path.Combine(Application.persistentDataPath, UniversalConfigParser.GetStringParam(UniversalConfigParser.GetNodesByTag("appParams"), "main_video_name"));
        vp.Prepare();
        // rt_alpha = new RenderTexture((int)UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), "video_width"),
        // (int)UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), "video_height"),
        // 0);
        // vp_alpha.renderMode = VideoRenderMode.RenderTexture;
        // vp_alpha.targetTexture = rt_alpha;
        // vp_alpha.url = Path.Combine(Application.persistentDataPath, UniversalConfigParser.GetStringParam(UniversalConfigParser.GetNodesByTag("appParams"), "alpha_video_name"));
        vp_alpha.Prepare();
        PlayVideo = () =>
        {
            vp_alpha.Play();
            vp.Play();
        };
        PauseVideo = () =>
        {
            vp_alpha.Pause();
            vp.Pause();
        };
        StopVideo = () => vp.Stop();
        {
            vp_alpha.Stop();
            vp.Stop();
        };

        vp.loopPointReached += (s) =>
        {
            VideoEnded = true;
            Debug.Log("video was ended");
            rt.Release();
            rt_alpha.Release();
        };
    }
}
