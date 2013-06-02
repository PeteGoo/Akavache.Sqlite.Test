using System.IO;
using Akavache.Sqlite.Test.ViewModels;
using Akavache.Sqlite3;
using Windows.Storage;

namespace Akavache.Sqlite.Test {
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;
    using Microsoft.Phone.Controls;
    using Caliburn.Micro;

    public class AppBootstrapper : PhoneBootstrapper
    {
        PhoneContainer container;

        protected override void Configure()
        {
            container = new PhoneContainer(RootFrame);

            BlobCache.ApplicationName = "TestApp";

            var localPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "settings.db");
            //var roamingPath = Path.Combine(ApplicationData.Current.RoamingFolder.Path, "settings.db");
            //var secretPath = Path.Combine(ApplicationData.Current.RoamingFolder.Path, "secret.db");

            BlobCache.LocalMachine = new SqlitePersistentBlobCache(localPath);
            //BlobCache.UserAccount = new SqlitePersistentBlobCache(localPath);
            //BlobCache.Secure = new Akavache.Sqlite3.EncryptedBlobCache(localPath);

			container.RegisterPhoneServices();
            container.PerRequest<MainPageViewModel>();

            AddCustomConventions();
        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = container.GetInstance(service, key);
            if (instance != null)
                return instance;

            throw new Exception("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        static void AddCustomConventions()
        {
            ConventionManager.AddElementConvention<Pivot>(Pivot.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
                (viewModelType, path, property, element, convention) => {
                    if (ConventionManager
                        .GetElementConvention(typeof(ItemsControl))
                        .ApplyBinding(viewModelType, path, property, element, convention))
                    {
                        ConventionManager
                            .ConfigureSelectedItem(element, Pivot.SelectedItemProperty, viewModelType, path);
                        ConventionManager
                            .ApplyHeaderTemplate(element, Pivot.HeaderTemplateProperty, null, viewModelType);
                        return true;
                    }

                    return false;
                };

            ConventionManager.AddElementConvention<Panorama>(Panorama.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
                (viewModelType, path, property, element, convention) => {
                    if (ConventionManager
                        .GetElementConvention(typeof(ItemsControl))
                        .ApplyBinding(viewModelType, path, property, element, convention))
                    {
                        ConventionManager
                            .ConfigureSelectedItem(element, Panorama.SelectedItemProperty, viewModelType, path);
                        ConventionManager
                            .ApplyHeaderTemplate(element, Panorama.HeaderTemplateProperty, null, viewModelType);
                        return true;
                    }

                    return false;
                };
        }
    }
}