using System;
using System.Linq;
using Mt.Core;
using System;
using System.Linq;

namespace SanXing.Web.Framework.Themes
{
    /// <summary>
    /// Theme context
    /// </summary>
    public partial class ThemeContext : IThemeContext
    {

        private readonly IThemeProvider _themeProvider;

        private bool _desktopThemeIsCached;
        private string _cachedDesktopThemeName;

        private bool _mobileThemeIsCached;
        private string _cachedMobileThemeName;

        public ThemeContext(
            IThemeProvider themeProvider)
        {
            this._themeProvider = themeProvider;
        }

        /// <summary>
        /// Get or set current theme for desktops
        /// </summary>
        public string WorkingDesktopTheme
        {
            get
            {
                return "Default";
            }
            set
            {
                _cachedDesktopThemeName = value;
            }
        }

        /// <summary>
        /// Get current theme for mobile (e.g. Mobile)
        /// </summary>
        public string WorkingMobileTheme
        {
            get
            {
                return "Mobile";
            }
        }
    }
}
