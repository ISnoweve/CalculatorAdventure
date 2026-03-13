using System.Runtime.CompilerServices;
using _Main.GameSceneSys.LoadPanelSys.UI_ScenePanelView.Event;
using Animancer;
using MessagePipe;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.GameSceneSys.LoadPanelSys.UI_ScenePanelView
{
    public class ScenePanelView : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private AnimancerComponent animancerComponent;
        [SerializeField] private AnimationClip fadeInAnimationClip, fadeOutAnimationClip;
        [SerializeField] private float fadeDuration = 1f;
        private AnimancerLayer _baseLayer;

        private void Awake()
        {
            panel.SetActive(false);
        }

        private void OnDestroy()
        {
            _baseLayer = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CreateBaseLayer()
        {
            _baseLayer ??= animancerComponent.Layers.Add();
        }
        
        public void PanelFadeInAnimation()
        {
            panel.SetActive(true);
            CreateBaseLayer();
            _baseLayer.Play(fadeInAnimationClip, fadeDuration, FadeMode.FromStart);
        }

        public void PanelFadeOutAnimation()
        {
            CreateBaseLayer();
            _baseLayer.Play(fadeOutAnimationClip, fadeDuration);
        }

        public void FadeInAnimationEnd()
        {
            var data = new Event_FadeInAnimationEnd();
            GlobalMessagePipe.GetPublisher<Event_FadeInAnimationEnd>().Publish(data);
        }

        public void FadeOutAnimationEnd()
        {
            Event_FadeOutAnimationEnd data = new Event_FadeOutAnimationEnd();
            GlobalMessagePipe.GetPublisher<Event_FadeOutAnimationEnd>().Publish(data);
            panel.SetActive(false);
        }
    }
}