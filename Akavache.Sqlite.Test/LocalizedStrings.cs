﻿using Akavache.Sqlite.Test.Resources;

namespace Akavache.Sqlite.Test {
    /// <summary>
    /// Provides access to string resources.
    /// </summary>
    public class LocalizedStrings {
        private static AppResources _localizedResources = new AppResources();

        public AppResources LocalizedResources { get { return _localizedResources; } }
    }
}