namespace EtVK.Event_Module.Event_Types
{
    public struct InteractUiData
    {
        public string PressLabel { get; set; }
        public string InteractLabel { get; set; }
        public bool Display { get; set; }
        public bool UpdateOnly { get; set; }

        public InteractUiData(string pressLabel, string interactLabel, bool display)
        {
            PressLabel = pressLabel;
            InteractLabel = interactLabel;
            Display = display;
            UpdateOnly = false;
        }

        public InteractUiData(bool display) : this()
        {
            Display = display;
            PressLabel = null;
            InteractLabel = null;
            UpdateOnly = false;
        }

        public InteractUiData(string pressLabel, string interactLabel) : this()
        {
            PressLabel = pressLabel;
            InteractLabel = interactLabel;
            UpdateOnly = true;
        }
    }
}