using System;
using System.Collections.Generic;

namespace QuestEngine
{
    /// <summary>
    /// Represents any type of Scene for quest
    /// </summary>
    public interface IScene
    {
        /// <summary>
        /// Scene ID (has to be unique for a quest)
        /// </summary>
        long Id { get; }

        /// <summary>
        /// Scene's content
        /// </summary>
        IContent Content { get; }

        /// <summary>
        /// Seems to be called before scene's bein' leaved
        /// </summary>
        Action OnEnter { get; }

        /// <summary>
        /// Will be called after scene's been activated
        /// </summary>
        Action OnExit { get; }

        /// <summary>
        /// list of selections, which you can make for this scene
        /// </summary>
        IList<string> Selections { get; }
    }
}