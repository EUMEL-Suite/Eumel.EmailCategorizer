using System.Collections.Generic;
using System.ComponentModel;
using Eumel.EmailCategorizer.WpfUI.Storage;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Eumel.EmailCategorizer.WpfUI.Model
{
    public class ConfigModel
    {

        [Category("Categories")]
        [DisplayName("List of Forward Marker")]
        [Description("List of markers which are prefixed if an email is forwarded e.g. 'FW:'")]
        public List<string> ForwardMarker { get; set; }

        [Category("Categories")]
        [DisplayName("List of Reply Marker")]
        [Description("List of markers which are prefixed if an email is replied e.g. 'RE:'")]
        public List<string> ReplyMarker { get; set; }

        [Category("Storage")]
        [DisplayName("Use Outlook PST file")]
        [Description("Use the Outlook PST file to read categories")]
        public bool UseOutlookPst { get; set; }

        [Category("Storage")]
        [DisplayName("Use plain files")]
        [Description("Use plain files in %localappdata% or 'Storage Folder' to store a file per configuration setting")]
        public bool UsePlainFileStorage { get; set; }

        [Category("Storage")]
        [DisplayName("Use JSON file")]
        [Description("Use a single json file to store the settings.")]
        public bool UseJsonFileStorage { get; set; }

        [Category("Storage")]
        [DisplayName("Use HTTP/S source")]
        [Description("Use a http or https source which contains the data.")]
        public bool UseHttpSource { get; set; }
 
        [Category("Storage")]
        [DisplayName("HTTP/S URI")]
        [Description("Endpoint for the HTTP storage")]
        public string HttpSource { get; set; }


        [Category("Storage")]
        [DisplayName("Storage Folder")]
        [Description("Local folder to store configuration. Default: %localappdata%")]
        public string StorageFolder { get; set; }
        
        [Category("Storage")]
        [DisplayName("Write Storage")]
        [Description("Storage or Endpoint which is used to store data. The write storage is always used as read source, too.")]
        [ItemsSource(typeof(EumelStorageWriteSources))]
        public string WriteStorage { get; set; }

        public static ConfigModel Default()
        {
            return new ConfigModel()
            {
                WriteStorage = nameof(JsonFileEumelStorage),
                UseOutlookPst = false,
                UsePlainFileStorage = false,
                UseHttpSource = false,
                ReplyMarker = new List<string>() {"RE:", "AW:"},
                ForwardMarker = new List<string>() {"FW:", "WG:"}
            };
        }
    }
}
