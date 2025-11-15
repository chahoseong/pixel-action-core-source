using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace Animation
{
    [CreateAssetMenu(menuName = "Gameplay/Animation/Montage", fileName = "New AnimMontage")]
    public class AnimMontage : ScriptableObject
    {
        [field: SerializeField] public AnimationClip[] Clips { get; private set; }
    }

    public class AnimMontageInstance : System.IDisposable
    {
        private AnimMontage definition;
        
        private PlayableGraph graph;
        private AnimationMixerPlayable mixer;
        private AnimationPlayableOutput output;

        private List<AnimationClipPlayable> tracks = new();
        private int currentTrackIndex = 0;

        private bool disposed = false;
        
        public bool IsPlaying { get; private set; }
        public bool IsFinished { get; private set; }

        public UnityAction OnFinished;

        public AnimMontageInstance(AnimMontage animMontage)
        {
            definition = animMontage;
            
            graph = PlayableGraph.Create();
            graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
            
            mixer = AnimationMixerPlayable.Create(graph, definition.Clips.Length);
            for (int i = 0; i < definition.Clips.Length; ++i)
            {
                var clip = definition.Clips[i];
                var track = AnimationClipPlayable.Create(graph, clip);
                track.SetDuration(clip.length);
                tracks.Add(track);
                
                mixer.SetInputWeight(i, 0.0f);
                
                graph.Connect(track, 0, mixer, i);
            }
        }
        
        ~AnimMontageInstance()
        {
            Dispose(false);
        }

        public void Play(Animator animator)
        {
            output = AnimationPlayableOutput.Create(graph, "AnimMontage", animator);
            output.SetSourcePlayable(mixer);
            
            graph.Play();
            
            IsPlaying = true;
        }

        public void Update(float deltaTime)
        {
            if (!graph.IsValid())
            {
                return;
            }

            if (currentTrackIndex >= tracks.Count)
            {
                return;
            }

            var currentTrack = tracks[currentTrackIndex];
            
            if (IsTrackFinished(currentTrack))
            {
                mixer.SetInputWeight(currentTrackIndex, 0.0f);
                
                ++currentTrackIndex;

                if (currentTrackIndex < tracks.Count)
                {
                    mixer.SetInputWeight(currentTrackIndex, 1.0f);
                }
                else
                {
                    IsFinished = true;
                }
            }
        }

        private bool IsTrackFinished(AnimationClipPlayable track)
        {
            return track.GetTime() >= track.GetDuration();
        }


        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            disposed = true;

            if (disposing)
            {
                // Release managed resources.
            }
            
            // Release unmanaged resources.
            graph.Destroy();
        }
    }
}
