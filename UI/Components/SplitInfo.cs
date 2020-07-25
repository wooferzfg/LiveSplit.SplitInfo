using LiveSplit.Model;
using LiveSplit.TimeFormatters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace LiveSplit.UI.Components
{
    public class SplitInfo : LogicComponent
    {
        public override string ComponentName => "Split Info";

        public SplitInfo()
        {
        }

        public override Control GetSettingsControl(LayoutMode mode) => null;

        public override void SetSettings(System.Xml.XmlNode settings)
        {
        }

        public override System.Xml.XmlNode GetSettings(System.Xml.XmlDocument document)
        {
            return document.CreateElement("SplitInfoSettings");
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
        }

        public override void Dispose()
        {
        }

        public int GetSettingsHashCode() => 1;
    }
}
