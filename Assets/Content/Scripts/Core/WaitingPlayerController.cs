using UnityEngine.Video;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using UnityEngine.Networking;

public enum WaitingType
{
    Image,
    Video
}

public class WaitingPlayerController : MonoBehaviour
{
    [SerializeField] private RenderTexture rt;
    private VideoPlayer vp;
    public delegate void VoidDelegate();
    public static VoidDelegate PlayVideo, PauseVideo, StopVideo, DisposeTexture;
    public static bool VideoEnded;
    private WaitingType waitingType;

    private void Awake()
    {
        // waitingType = UniversalConfigParser
        // .GetStringParam(UniversalConfigParser.
        // GetNodesByTag("appParams"), "waiting_type") == "image" ? WaitingType.Image : WaitingType.Video;

        // switch (waitingType)
        // {
        //     case WaitingType.Image:
        //         var path = Path.Combine(Application.persistentDataPath, UniversalConfigParser.GetStringParam(UniversalConfigParser.GetNodesByTag("appParams"), "waiting_image_name"));
        //         GetComponent<Image>().sprite = LoadSprite(path);
        //         break;
        //     case WaitingType.Video:
        //         rt = new RenderTexture((int)UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), "video_width"),
        // (int)UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), "video_height"),
        // 0);
        //         GetComponent<RawImage>().texture = rt;
        //         vp = GetComponent<VideoPlayer>();
        //         vp.renderMode = VideoRenderMode.RenderTexture;
        //         vp.targetTexture = rt;
        //         vp.url = Path.Combine(Application.persistentDataPath, UniversalConfigParser.GetStringParam(UniversalConfigParser.GetNodesByTag("appParams"), "waiting_video_name"));
        //         vp.Prepare();
        //         PlayVideo = () => vp.Play();
        //         PauseVideo = () => vp.Pause();
        //         StopVideo = () => vp.Stop();

        //         vp.loopPointReached += (s) =>
        //         {
        //             VideoEnded = true;
        //             Debug.Log("video was ended");
        //         };
        //         break;
        // }

    }

    public static Sprite LoadSprite(string path)
    {
        if (string.IsNullOrEmpty(path)) return null;

        if (System.IO.File.Exists(path))
        {
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }

        return null;
    }
}