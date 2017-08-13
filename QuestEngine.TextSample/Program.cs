using System;
using System.Collections.Generic;

namespace QuestEngine.TextSample
{
    internal class TextContent : IContent
    {
        private string _text;

        public TextContent(string text)
        {
            _text = text;
        }
        public string Print()
            => _text;
    }

    internal class TextScene : IScene
    {
        public long Id { get; }

        public IContent Content { get; }

        public Action OnEnter { get; set; }

        public Action OnExit { get; set; }

        public IList<string> Selections { get; }

        public TextScene(long id, string text, params string[] selections)
        {
            this.Id = id;
            this.Content = new TextContent(text);
            this.Selections = new List<string>();
            (this.Selections as List<string>).AddRange(selections);
        }

        public override string ToString()
            => Content.Print();
    }

    internal class TextQuest : Quest<TextScene, string>
    {
        public TextQuest(TextScene initialScene)
            : base(initialScene)
        {
        }
    }

    internal class Program
    {
        private static void Main()
        {
            var firstScene = new TextScene(1, "Hello, what do you want to do?", "Play", "Leave");
            var playScene = new TextScene(2, "Okay, let's play");
            var leaveScene = new TextScene(3, "You sure! Wanna restart?", "Yes", "No");
            var lastScene = new TextScene(4, "Bye!");

            var quest = new TextQuest(firstScene);
            quest.Configure(firstScene, "Play", playScene);
            quest.Configure(firstScene, "Leave", leaveScene);
            quest.Configure(leaveScene, "Yes", firstScene);
            quest.Configure(leaveScene, "No", lastScene);

            Console.WriteLine(quest.CurrentScene);

            quest.Activate(quest.CurrentScene.Id, "Leave");

            Console.WriteLine(quest.CurrentScene);

            quest.Activate(quest.CurrentScene.Id, "Yes");

            Console.WriteLine(quest.CurrentScene);

            quest.Activate(quest.CurrentScene.Id, "Leave");

            Console.WriteLine(quest.CurrentScene);

            quest.Activate(quest.CurrentScene.Id, "No");

            Console.WriteLine(quest.CurrentScene);

            Console.ReadKey();
        }
    }
}