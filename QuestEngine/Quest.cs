using System;
using System.Collections.Generic;

namespace QuestEngine
{
    /// <summary>
    /// Represent base abstract class for all quests
    /// </summary>
    /// <typeparam name="TScene">Scene type</typeparam>
    /// <typeparam name="TTrigger">Trigger type to change scenes (WARN: type should correctly override GetHashCode method!)</typeparam>
    public abstract class Quest<TScene, TTrigger> where TScene : IScene
    {
        private readonly IDictionary<SceneTransition<TTrigger>, TScene> _sceneTriggerConfiguration;


        /// <summary>
        /// Creates new instance of quest, initialized with some scene
        /// </summary>
        /// <param name="initialScene">initial scene</param>
        protected Quest(TScene initialScene)
        {
            _sceneTriggerConfiguration 
                = new Dictionary<SceneTransition<TTrigger>, TScene>();

            CurrentScene = initialScene;
        }

        /// <summary>
        /// Configures scene transition
        /// </summary>
        /// <param name="from">scene, you are making transition from</param>
        /// <param name="selection">actually - trigger, which causes scene transition</param>
        /// <param name="target">scene, you want to be activated with specified trigger</param>
        public virtual void Configure(TScene from, TTrigger selection, TScene target)
        {            
            this._sceneTriggerConfiguration.Add(new SceneTransition<TTrigger>(from.Id, selection), target);
        }

        /// <summary>
        /// returns currently activated scene
        /// </summary>
        public TScene CurrentScene { get; private set; }

        /// <summary>
        /// Activated selection (trigger)
        /// </summary>
        /// <param name="fromId">current scene id</param>
        /// <param name="selection">selection (trigger)</param>
        /// <exception cref="ArgumentException"/>
        public void Activate(long fromId, TTrigger selection)
        {            
            if (!_sceneTriggerConfiguration.TryGetValue(
                new SceneTransition<TTrigger>(fromId, selection), out TScene resultScene))
                    throw new ArgumentException(
                        $"Unable to activate next scene due to invalid scene trigger is being called. Check quest configuration!");

            CurrentScene.OnExit?.Invoke();

            this.CurrentScene = resultScene;

            CurrentScene.OnEnter?.Invoke();
        }
    }
}
