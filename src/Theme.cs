// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    public class Theme : ObjectBase
    {
        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal Theme(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Theme()
            : base(tguiTheme_create())
        {
        }

        /// <summary>
        /// Construct the theme given a filename
        /// </summary>
        /// <param name="filename">Filename of the theme to load</param>
        public Theme(string filename)
            : base(tguiTheme_create())
        {
            Load(filename);
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        public Theme(Theme copy)
            : base(tguiTheme_copy(copy.CPointer))
        {
        }

        /// <summary>
        /// Destroy the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
        protected override void Destroy(bool disposing)
        {
            tguiTheme_destroy(CPointer);
        }

        /// <summary>
        /// Changes the file from which the theme is loaded
        /// </summary>
        /// <param name="filename">Filename of the theme to load</param>
        /// <remarks>
        /// When the theme was loaded before and a renderer with the same name is encountered,
        /// the widgets that were using the old renderer will be reloaded with the new renderer.
        /// </remarks>
        public void Load(string filename)
        {
            tguiTheme_load(CPointer, Util.ConvertStringForC_UTF32(filename));
        }

        /// <summary>
        /// Replaced this theme with another one, while updating all connected widgets to the new renderers
        /// </summary>
        /// <param name="otherTheme">The theme to copy</param>
        /// <remarks>
        /// The renderers are copied, meaning that all widgets connected to the other theme will remain connected to it.
        /// Any widgets connected to this theme will however be updated with new renderers when the same name is encountered.
        /// </remarks>
        public void Replace(Theme otherTheme)
        {
            tguiTheme_replace(CPointer, otherTheme.CPointer);
        }

        /// <summary>
        /// Gets data for the renderers
        /// </summary>
        /// <param name="id">Name of the section in the theme file</param>
        /// <returns>Shared renderer data</returns>
        public RendererData GetRenderer(string id)
        {
            return new RendererData(tguiTheme_getRenderer(CPointer, Util.ConvertStringForC_UTF32(id)));
        }

        /// <summary>
        /// Sets the theme class that widgets use by default
        /// </summary>
        /// <param name="theme">Theme to use as default</param>
        /// <remarks>
        /// The theme will be reset to the built-in White theme when passing null to this function.
        /// </remarks>
        public static void SetDefault(Theme? theme)
        {
            tguiTheme_setDefault(theme?.CPointer);
        }

        /// <summary>
        /// Gets the theme that is currently used as the default when creating new widgets
        /// </summary>
        /// <returns>Current default theme</returns>
        public static Theme GetDefault()
        {
            return new Theme(tguiTheme_getDefault());
        }

        /// <summary>
        /// Check if a global property exists in the theme
        /// </summary>
        /// <param name="property">Global property to check</param>
        /// <returns>Was the global property was found in the theme?</returns>
        public bool HasGlobalProperty(string property)
        {
            return tguiTheme_hasGlobalProperty(CPointer, Util.ConvertStringForC_UTF32(property)) != 0;
        }

        /// <summary>
        /// Returns the value of a global property in the theme
        /// </summary>
        /// <param name="property">Name of the global property to retrieve</param>
        /// <returns>Value of the global property</returns>
        /// <exception cref="Exception">The property didn't exist or had a different type</exception>
        public bool GetGlobalPropertyBool(string property)
        {
            byte val;
            if (tguiTheme_getGlobalPropertyBool(CPointer, Util.ConvertStringForC_UTF32(property), out val) != 0)
                return val != 0;
            else
                throw new Exception("No bool property with name '" + property + "' was found");
        }

        /// <summary>
        /// Returns the value of a global property in the theme
        /// </summary>
        /// <param name="property">Name of the global property to retrieve</param>
        /// <returns>Value of the global property</returns>
        /// <exception cref="Exception">The property didn't exist or had a different type</exception>
        public Font GetGlobalPropertyFont(string property)
        {
            IntPtr val;
            if (tguiTheme_getGlobalPropertyFont(CPointer, Util.ConvertStringForC_UTF32(property), out val) != 0)
                return new Font(val);
            else
                throw new Exception("No Font property with name '" + property + "' was found");
        }

        /// <summary>
        /// Returns the value of a global property in the theme
        /// </summary>
        /// <param name="property">Name of the global property to retrieve</param>
        /// <returns>Value of the global property</returns>
        /// <exception cref="Exception">The property didn't exist or had a different type</exception>
        public Color? GetGlobalPropertyColor(string property)
        {
            ColorCTGUI val;
            if (tguiTheme_getGlobalPropertyColor(CPointer, Util.ConvertStringForC_UTF32(property), out val) != 0)
                return Util.GetColorFromC(val);
            else
                throw new Exception("No Color property with name '" + property + "' was found");
        }

        /// <summary>
        /// Returns the value of a global property in the theme
        /// </summary>
        /// <param name="property">Name of the global property to retrieve</param>
        /// <returns>Value of the global property</returns>
        /// <exception cref="Exception">The property didn't exist or had a different type</exception>
        public string GetGlobalPropertyString(string property)
        {
            IntPtr val;
            if (tguiTheme_getGlobalPropertyString(CPointer, Util.ConvertStringForC_UTF32(property), out val) != 0)
                return Util.GetStringFromC_UTF32(val);
            else
                throw new Exception("No string property with name '" + property + "' was found");
        }

        /// <summary>
        /// Returns the value of a global property in the theme
        /// </summary>
        /// <param name="property">Name of the global property to retrieve</param>
        /// <returns>Value of the global property</returns>
        /// <exception cref="Exception">The property didn't exist or had a different type</exception>
        public float GetGlobalPropertyNumber(string property)
        {
            float val;
            if (tguiTheme_getGlobalPropertyNumber(CPointer, Util.ConvertStringForC_UTF32(property), out val) != 0)
                return val;
            else
                throw new Exception("No float property with name '" + property + "' was found");
        }

        /// <summary>
        /// Returns the value of a global property in the theme
        /// </summary>
        /// <param name="property">Name of the global property to retrieve</param>
        /// <returns>Value of the global property</returns>
        /// <exception cref="Exception">The property didn't exist or had a different type</exception>
        public Outline GetGlobalPropertyOutline(string property)
        {
            IntPtr val;
            if (tguiTheme_getGlobalPropertyOutline(CPointer, Util.ConvertStringForC_UTF32(property), out val) != 0)
                return new Outline(val);
            else
                throw new Exception("No Outline property with name '" + property + "' was found");
        }

        /// <summary>
        /// Returns the value of a global property in the theme
        /// </summary>
        /// <param name="property">Name of the global property to retrieve</param>
        /// <returns>Value of the global property</returns>
        /// <exception cref="Exception">The property didn't exist or had a different type</exception>
        public Texture GetGlobalPropertyTexture(string property)
        {
            IntPtr val;
            if (tguiTheme_getGlobalPropertyTexture(CPointer, Util.ConvertStringForC_UTF32(property), out val) != 0)
                return new Texture(val);
            else
                throw new Exception("No Texture property with name '" + property + "' was found");
        }

        /// <summary>
        /// Returns the value of a global property in the theme
        /// </summary>
        /// <param name="property">Name of the global property to retrieve</param>
        /// <returns>Value of the global property</returns>
        /// <exception cref="Exception">The property didn't exist or had a different type</exception>
        public TextStyles GetGlobalPropertyTextStyle(string property)
        {
            TextStyles val;
            if (tguiTheme_getGlobalPropertyTextStyle(CPointer, Util.ConvertStringForC_UTF32(property), out val) != 0)
                return val;
            else
                throw new Exception("No TextStyles property with name '" + property + "' was found");
        }

        /// <summary>
        /// Returns the value of a global property in the theme
        /// </summary>
        /// <param name="property">Name of the global property to retrieve</param>
        /// <returns>Value of the global property</returns>
        /// <exception cref="Exception">The property didn't exist or had a different type</exception>
        public RendererData GetGlobalPropertyRendererData(string property)
        {
            IntPtr val;
            if (tguiTheme_getGlobalPropertyRendererData(CPointer, Util.ConvertStringForC_UTF32(property), out val) != 0)
                return new RendererData(val);
            else
                throw new Exception("No RendererData property with name '" + property + "' was found");
        }

        /// <summary>
        /// Inform the theme that a custom renderer has subwidgets that need a default value
        /// </summary>
        /// <param name="widgetType">Type name of the custom widget</param>
        /// <param name="property">Property of the renderer which should refer to another section in the renderer</param>
        /// <param name="propertyWidgetType">Type name of subwidget, which specifies the section in the theme file to refer to</param>
        public static void AddRendererDefaultSubwidget(string widgetType, string property, string propertyWidgetType)
        {
            tguiTheme_addRendererDefaultSubwidget(Util.ConvertStringForC_UTF32(widgetType), Util.ConvertStringForC_UTF32(property), Util.ConvertStringForC_UTF32(propertyWidgetType));
        }

        /// <summary>
        /// Returns the defaulted subwidgets that a renderer has for the given property
        /// </summary>
        /// <param name="widgetType">Type name of the custom widget</param>
        /// <param name="property">Property of the renderer which refers to another section in the renderer</param>
        /// <returns>Corresponding renderer section, or an empty string if the widget type or property were not found</returns>
        public static string GetRendererDefaultSubwidget(string widgetType, string property)
        {
            return Util.GetStringFromC_UTF32(tguiTheme_getRendererDefaultSubwidget(Util.ConvertStringForC_UTF32(widgetType), Util.ConvertStringForC_UTF32(property)));
        }

        /// <summary>
        /// Inform the theme that a custom renderer has properties that can use a default value from the global properties
        /// </summary>
        /// <param name="widgetType">Type name of the custom widget</param>
        /// <param name="property">Property of the renderer which refers to the global property</param>
        /// <param name="globalProperty">Global property in the theme that would be copied as default value for the renderer property</param>
        public static void AddRendererInheritedGlobalProperty(string widgetType, string property, string globalProperty)
        {
            tguiTheme_addRendererInheritedGlobalProperty(Util.ConvertStringForC_UTF32(widgetType), Util.ConvertStringForC_UTF32(property), Util.ConvertStringForC_UTF32(globalProperty));
        }

        /// <summary>
        /// Returns the property which a renderer inherits from the global theme properties
        /// </summary>
        /// <param name="widgetType">Type name of the widget of which we are looking up the properties</param>
        /// <param name="property">Property of the renderer which refers to the global property</param>
        /// <returns>Corresponding global property, or an empty string if the widget type or property were not found</returns>
        public static string GetRendererInheritedGlobalProperty(string widgetType, string property)
        {
            return Util.GetStringFromC_UTF32(tguiTheme_getRendererInheritedGlobalProperty(Util.ConvertStringForC_UTF32(widgetType), Util.ConvertStringForC_UTF32(property)));
        }

        public void AddRenderer(string id, RendererData renderer)
        {
            tguiTheme_addRenderer(CPointer, Util.ConvertStringForC_UTF32(id), renderer.CPointer);
        }

        public bool RemoveRenderer(string id)
        {
            return tguiTheme_removeRenderer(CPointer, Util.ConvertStringForC_UTF32(id)) != 0;
        }

        public string GetPrimary()
        {
            return Util.GetStringFromC_UTF32(tguiTheme_getPrimary(CPointer));
        }

        public static void AddRendererInheritanceParent(string widgetType, string parentType)
        {
            tguiTheme_addRendererInheritanceParent(Util.ConvertStringForC_UTF32(widgetType), Util.ConvertStringForC_UTF32(parentType));
        }

        public static string GetRendererInheritanceParent(string widgetType)
        {
            return Util.GetStringFromC_UTF32(tguiTheme_getRendererInheritanceParent(Util.ConvertStringForC_UTF32(widgetType)));
        }

        #region Imports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTheme_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTheme_copy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTheme_destroy(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTheme_load(IntPtr cPointer, IntPtr filename);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTheme_replace(IntPtr cPointer, IntPtr otherTheme);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTheme_getRenderer(IntPtr cPointer, IntPtr id);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTheme_setDefault(IntPtr? cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTheme_getDefault();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTheme_hasGlobalProperty(IntPtr cPointer, IntPtr property);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTheme_getGlobalPropertyBool(IntPtr cPointer, IntPtr property, out byte val);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTheme_getGlobalPropertyFont(IntPtr cPointer, IntPtr property, out IntPtr val);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTheme_getGlobalPropertyColor(IntPtr cPointer, IntPtr property, out ColorCTGUI val);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTheme_getGlobalPropertyString(IntPtr cPointer, IntPtr property, out IntPtr val);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTheme_getGlobalPropertyNumber(IntPtr cPointer, IntPtr property, out float val);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTheme_getGlobalPropertyOutline(IntPtr cPointer, IntPtr property, out IntPtr val);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTheme_getGlobalPropertyTexture(IntPtr cPointer, IntPtr property, out IntPtr val);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTheme_getGlobalPropertyTextStyle(IntPtr cPointer, IntPtr property, out TextStyles val);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTheme_getGlobalPropertyRendererData(IntPtr cPointer, IntPtr property, out IntPtr val);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTheme_addRendererDefaultSubwidget(IntPtr widgetType, IntPtr property, IntPtr propertyWidgetType);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTheme_getRendererDefaultSubwidget(IntPtr widgetType, IntPtr property);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTheme_addRendererInheritedGlobalProperty(IntPtr widgetType, IntPtr property, IntPtr globalProperty);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTheme_getRendererInheritedGlobalProperty(IntPtr widgetType, IntPtr property);

        #endregion

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTheme_addRenderer(IntPtr cPointer, IntPtr id, IntPtr renderer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiTheme_removeRenderer(IntPtr cPointer, IntPtr id);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTheme_getPrimary(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiTheme_addRendererInheritanceParent(IntPtr widgetType, IntPtr parentType);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiTheme_getRendererInheritanceParent(IntPtr widgetType);

        #endregion
    }
}
