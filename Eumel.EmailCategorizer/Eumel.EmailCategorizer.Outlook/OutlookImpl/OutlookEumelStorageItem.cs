using System;
using Microsoft.Office.Interop.Outlook;

namespace Eumel.EmailCategorizer.Outlook.OutlookImpl
{
    public class OutlookEumelStorageItem : IEumelStorage
    {
        private const string StorageIdentifier = "Eumel.EmailCategorizer";
        private readonly StorageItem _storage;

        public OutlookEumelStorageItem(MAPIFolder folder)
        {
            if (folder == null) throw new ArgumentNullException(nameof(folder));

            _storage = folder.GetStorage(StorageIdentifier, OlStorageIdentifierType.olIdentifyBySubject);

            if (_storage == null) throw new ArgumentException(@"The returned storage is null.");
        }

        public string this[string name]
        {
            get
            {
                if (_storage.Size != 0) return _storage.UserProperties[name].Value;

                // empty init
                _storage.UserProperties.Add(name, OlUserPropertyType.olText);
                _storage.UserProperties[name].Value = string.Empty;
                _storage.Save();

                return _storage.UserProperties[name].Value;
            }
            set
            {
                _storage.UserProperties[name].Value = value;
                _storage.Save();
            }
        }
    }
}