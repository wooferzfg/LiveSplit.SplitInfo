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

        private LiveSplitState currentState;

        public SplitInfo(LiveSplitState state)
        {
            state.OnSplit += State_OnSplit;
            state.OnStart += State_OnStart;
            state.OnUndoSplit += State_OnUndoSplit;
            state.OnSkipSplit += State_OnSkipSplit;
            state.OnReset += State_OnReset;

            currentState = state;
        }

        private void State_OnReset(object sender, TimerPhase value)
        {
            GenerateFile();
        }

        private void State_OnSkipSplit(object sender, EventArgs e)
        {
            GenerateFile();
        }

        private void State_OnUndoSplit(object sender, EventArgs e)
        {
            GenerateFile();
        }

        private void State_OnStart(object sender, EventArgs e)
        {
            GenerateFile();
        }

        private void State_OnSplit(object sender, EventArgs e)
        {
            GenerateFile();
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            currentState = state;
        }

        public override void Dispose()
        {
            currentState.OnSplit -= State_OnSplit;
            currentState.OnStart -= State_OnStart;
            currentState.OnUndoSplit -= State_OnUndoSplit;
            currentState.OnSkipSplit -= State_OnSkipSplit;
            currentState.OnReset -= State_OnReset;
        }

        private void GenerateFile()
        {
            var previousSplitName = "";
            if (currentState.CurrentSplitIndex > 0)
            {
                previousSplitName = currentState.Run[currentState.CurrentSplitIndex - 1].Name;
            }
            var previousSplitLine = GenerateLine("prevSplitName", previousSplitName);

            var currentSplitName = currentState.CurrentSplit?.Name ?? "";
            var currentSplitLine = GenerateLine("splitName", currentSplitName);

            var formattedSplitTime = "";
            var formattedDelta = "";
            if (currentState.CurrentSplitIndex > 0)
            {
                var previousSplit = currentState.Run[currentState.CurrentSplitIndex - 1];
                var splitTime = previousSplit.SplitTime[currentState.CurrentTimingMethod];
                var delta = splitTime - previousSplit.Comparisons[currentState.CurrentComparison][currentState.CurrentTimingMethod];
                formattedSplitTime = FormatTime(splitTime);
                formattedDelta = FormatDelta(delta);
            }
            var splitTimeLine = GenerateLine("splitTime", formattedSplitTime);
            var deltaLine = GenerateLine("splitComparison", formattedDelta);

            string[] result = { previousSplitLine, currentSplitLine, deltaLine, splitTimeLine };
            System.IO.File.WriteAllLines(@"SplitInfo.txt", result);
        }

        private string GenerateLine(string key, string value)
        {
            return $"{key}={value.Replace("=", "&#61;")}";
        }

        private string FormatDelta(TimeSpan? delta)
        {
            if (!delta.HasValue)
            {
                return "";
            }

            var result = FormatTimeRaw(delta.Value);

            return delta < TimeSpan.Zero ? $"-{result}" : $"+{result}";
        }

        private string FormatTime(TimeSpan? time)
        {
            if (!time.HasValue)
            {
                return "";
            }

            var result = FormatTimeRaw(time.Value);

            return time < TimeSpan.Zero ? $"-{result}" : result;
        }

        private string FormatTimeRaw(TimeSpan time)
        {
            return time.ToString(@"dd\:hh\:mm\:ss\.fff");
        }

        public int GetSettingsHashCode() => 1;

        public override Control GetSettingsControl(LayoutMode mode) => null;

        public override void SetSettings(System.Xml.XmlNode settings) { }

        public override System.Xml.XmlNode GetSettings(System.Xml.XmlDocument document)
        {
            return document.CreateElement("SplitInfoSettings");
        }
    }
}
