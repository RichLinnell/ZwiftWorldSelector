using System;

using AppKit;
using Foundation;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ZwiftWorldSelector
{
    public partial class ViewController : NSViewController
    {
        private readonly string _filePath;
        public ViewController(IntPtr handle) : base(handle)
        {
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			_filePath = Path.Combine(documentsPath, "Documents/Zwift/prefs.xml");
}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Save.Activated += (sender, e) => SaveSelection();
            LoadExistingSettings();
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

        private void SaveSelection()
        {
            var xmlDoc = LoadPrefsFile();
			var root = xmlDoc.Descendants().First();
            if(!root.Descendants("WORLD").Any()){
                XElement worldNode = new XElement("WORLD", "1");
                root.Add(worldNode);
            }
            root.Descendants("WORLD").Single().Value = SelectedWorld.SelectedItem.Tag.ToString();
            if(SelectedWorld.SelectedItem.Tag == 0){
                root.Descendants("WORLD").Remove();
            }

            xmlDoc.Save(_filePath);
            NSApplication.SharedApplication.Terminate(this);
        }

        private XDocument LoadPrefsFile()
        {
			
            if (!File.Exists(_filePath))
			{
				FileMissing.Hidden = false;
				return null;
			}
			FileMissing.Hidden = true;
			return XDocument.Load(_filePath);
		}

        private void LoadExistingSettings()
        {
            var xmlDoc = LoadPrefsFile();
			var root = xmlDoc.Descendants().First();
            if (root.Descendants("WORLD").Any())
            {
                var selectedVal = root.Descendants("WORLD").Single().Value;
                nint valAsInt;
                if (nint.TryParse(selectedVal, out valAsInt))
                {
                    var listItem = SelectedWorld.Items().SingleOrDefault(i => i.Tag == valAsInt);
                    SelectedWorld.SelectItem(listItem);
                }
            }
        }
			
    }
}
