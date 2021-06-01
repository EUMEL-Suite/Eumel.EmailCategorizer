﻿using System;
using Eumel.EmailCategorizer.WpfUI;
using Microsoft.Office.Interop.Outlook;

namespace Eumel.EmailCategorizer.Outlook.OutlookImpl
{
    public class OutlookEumelStorageItem : IEumelStorage
    {
        private const string StorageIdentifier = "Eumel";
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
                foreach (UserProperty item in _storage.UserProperties)
                    if (string.Compare(name, item.Name, StringComparison.InvariantCultureIgnoreCase) == 0)
                        return item.Value;

                return null;
            }
            set
            {
                var prop = (UserProperty)null;
                
                foreach (UserProperty item in _storage.UserProperties)
                    if (string.Compare(name, item.Name, StringComparison.InvariantCultureIgnoreCase) == 0)
                        prop = item;

                if (prop == null)
                {
                    prop = _storage.UserProperties.Add(name, OlUserPropertyType.olText);
                    _storage.Save();
                }

                prop.Value = value;
                _storage.Save();
            }
        }
    }
}