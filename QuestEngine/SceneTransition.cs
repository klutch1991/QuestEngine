namespace QuestEngine
{
    /// <summary>
    /// Represents a key for scene transitions configuration dictionary
    /// </summary>
    /// <typeparam name="TTrigger">trigger type</typeparam>
    internal class SceneTransition<TTrigger>
    {
        /// <summary>
        /// Id of scene, from which we're making a transition
        /// </summary>
        public long FromSceneId { get; }

        /// <summary>
        /// Trigger to make transition
        /// </summary>
        public TTrigger Selection { get; }


        /// <summary>
        /// Create a new instance of SceneTransition class
        /// </summary>
        /// <param name="fromSceneId">Id of scene, from which we're making a transition</param>
        /// <param name="selection">Trigger to make transition</param>
        public SceneTransition(long fromSceneId, TTrigger selection)
        {
            this.FromSceneId = fromSceneId;
            this.Selection = selection;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return 340 * (int)this.FromSceneId * this.Selection.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as SceneTransition<TTrigger>;
            if (other == null) return false;

            return this.FromSceneId.Equals(other.FromSceneId) && this.Selection.Equals(other.Selection);
        }
    }
}