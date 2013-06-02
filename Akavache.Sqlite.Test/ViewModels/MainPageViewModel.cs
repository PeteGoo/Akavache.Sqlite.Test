using System.Reactive.Linq;
using Caliburn.Micro;

namespace Akavache.Sqlite.Test.ViewModels {
    public class MainPageViewModel : Screen {
        private string status;
        public string Status {
            get { return status; }
            set {
                if (value == status) {
                    return;
                }
                status = value;
                NotifyOfPropertyChange(() => Status);
            }
        }

        public async void AddCacheValue() {
            await BlobCache.LocalMachine.InsertObject("MyCacheKey", new CacheValueContainer() {Foo = "Foo"});
            Status = "Value added to cache";
        }

        public async void GetCacheValue() {
            var cacheValue =
                await BlobCache.LocalMachine.GetOrCreateObject<CacheValueContainer>("MyCacheKey", () => null);
            Status = cacheValue == null ? "No cache value found" : "Cache value successfully retrieved";

        }

        public class CacheValueContainer {
            public string Foo { get; set; }
        }
    }
}