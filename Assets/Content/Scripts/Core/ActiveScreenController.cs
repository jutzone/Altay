using UnityEngine;
using DG.Tweening;
interface IShow
{
    public void ShowScreen()
    {

    }

    public void HideScreen()
    {

    }
}


public class ActiveScreenController : MonoBehaviour, IShow
{
    public void ShowScreen(System.Action callback = null)
    {
        GetComponent<CanvasGroup>()
        .DOFade(1, UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), "show_active_screen"))
        .OnComplete(() => callback?.Invoke());

        VideoPlayerController.PlayVideo.Invoke();
    }

    public void HideScreen(System.Action callback = null)
    {
        GetComponent<CanvasGroup>()
        .DOFade(0, UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), "hide_active_screen"))
        .OnComplete(() => callback?.Invoke()); 

    }
}
