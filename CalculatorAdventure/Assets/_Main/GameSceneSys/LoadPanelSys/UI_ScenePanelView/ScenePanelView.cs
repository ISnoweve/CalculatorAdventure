using System.Runtime.CompilerServices;
using _Main.GameSceneSys.LoadPanelSys.UI_ScenePanelView.Event;
using Animancer;
using MessagePipe;
using UnityEngine;

namespace _Main.GameSceneSys.LoadPanelSys.UI_ScenePanelView
{
    public class ScenePanelView : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private AnimancerComponent animancerComponent;
        [SerializeField] private AnimationClip fadeInAnimationClip,fadeOutAnimationClip;
        [SerializeField] private float fadeDuration = 1f;
        private AnimancerLayer _baseLayer;
        
        private void Awake()
        {
            panel.SetActive(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CreateBaseLayer()
        {
            _baseLayer ??= animancerComponent.Layers.Add();
        }

        private void OnDestroy()
        {
            _baseLayer = null;
        }


        public void PanelFadeInAnimation()
        {
            CreateBaseLayer();
            panel.SetActive(true);
            _baseLayer.Play(fadeInAnimationClip, fadeDuration);
        }

        public void PanelFadeOutAnimation()
        {
            // 負責最主要的場景切換
            
            panel.SetActive(false);
            //CreateBaseLayer();
            //_baseLayer.Play(fadeOutAnimationClip, fadeDuration);
        }

        public void FadeInAnimationEnd()
        {
            Event_FadeInAnimationEnd data = new Event_FadeInAnimationEnd();
            GlobalMessagePipe.GetPublisher<Event_FadeInAnimationEnd>().Publish(data);
        }

        public void FadeOutAnimationEnd()
        {
            // 這裡沒有使用，是因為讓每個場景都有自己的入場動畫。
            
            //Event_FadeOutAnimationEnd data = new Event_FadeOutAnimationEnd();
            //GlobalMessagePipe.GetPublisher<Event_FadeOutAnimationEnd>().Publish(data);
            //panel.SetActive(false);
        }
    }
}