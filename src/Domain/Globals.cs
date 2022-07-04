using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UexCorpDataRunner.Domain;
public static class Globals
{
    public const string SimpleCipherKey = "UnitedExpressCorporationFoundedIn2947";

    public static class Settings
    {
        static Settings()
        {
            var entryAssembly = System.Reflection.Assembly.GetEntryAssembly();
            var versionInfo = entryAssembly?.GetName()?.Version;
            Version = $"v{versionInfo?.Major}.{versionInfo?.Minor}.{versionInfo?.Build}.{versionInfo?.Revision}";
        }

        public const string Light = "Light";
        public const string Dark = "Dark";

        public const string TopLeft = "TopLeft";
        public const string TopCenter = "TopCenter";
        public const string TopRight = "TopRight";
        public const string CenterLeft = "CenterLeft";
        public const string CenterRight = "CenterRight";
        public const string BottomLeft = "BottomLeft";
        public const string BottomCenter = "BottomCenter";
        public const string BottomRight = "BottomRight";

        public const string Horizontal = "Horizontal";
        public const string Vertical = "Vertical";

        public const string Always = "Always";
        public const string Minimized = "Minimized";
        public const string Never = "Never";

        public const string ShowAll = "Show All";
        public const string HideTemporary = "Hide Temporary";

        public static readonly string? Version;

    }
}
