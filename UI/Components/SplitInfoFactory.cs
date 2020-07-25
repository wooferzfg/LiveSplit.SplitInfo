using LiveSplit.Model;
using System;

namespace LiveSplit.UI.Components
{
    public class SplitInfoFactory : IComponentFactory
    {
        public string ComponentName => "Split Info";

        public string Description => "Outputs info about splits to a text file";

        public ComponentCategory Category => ComponentCategory.Other;

        public IComponent Create(LiveSplitState state) => new SplitInfo();

        public string UpdateName => ComponentName;

        public string XMLURL => "http://livesplit.org/update/Components/noupdates.xml";

        public string UpdateURL => "http://livesplit.org/update/";

        public Version Version => Version.Parse("1.0.0");
    }
}
