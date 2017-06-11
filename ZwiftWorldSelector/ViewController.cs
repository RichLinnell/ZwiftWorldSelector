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
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Save.Activated += (sender, e) => SaveSelection();
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

        public void SaveSelection(){
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var filePath = Path.Combine(documentsPath, "Documents/Zwift/prefs.xml");
            if (!File.Exists(filePath)){
                FileMissing.Hidden = false;
                return;
            }
            FileMissing.Hidden = true;
            var xmlDoc = XDocument.Load(filePath);
            var root = xmlDoc.Descendants().First();
            if(!root.Descendants("WORLD").Any()){
                XElement worldNode = new XElement("WORLD", "1");
                root.Add(worldNode);
            }
            root.Descendants("WORLD").Single().Value = SelectedWorld.SelectedItem.Tag.ToString();
            if(SelectedWorld.SelectedItem.Tag == 0){
                root.Descendants("WORLD").Remove();
            }

            xmlDoc.Save(filePath);
            NSApplication.SharedApplication.Terminate(this);
        }
    }
}
