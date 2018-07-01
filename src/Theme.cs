/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI - Texus' Graphical User Interface
// Copyright (C) 2012-2016 Bruno Van de Velde (vdv_b@tgui.eu)
//
// This software is provided 'as-is', without any express or implied warranty.
// In no event will the authors be held liable for any damages arising from the use of this software.
//
// Permission is granted to anyone to use this software for any purpose,
// including commercial applications, and to alter it and redistribute it freely,
// subject to the following restrictions:
//
// 1. The origin of this software must not be misrepresented;
//    you must not claim that you wrote the original software.
//    If you use this software in a product, an acknowledgment
//    in the product documentation would be appreciated but is not required.
//
// 2. Altered source versions must be plainly marked as such,
//    and must not be misrepresented as being the original software.
//
// 3. This notice may not be removed or altered from any source distribution.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Text;
using System.Security;
using System.Runtime.InteropServices;
using SFML.System;

namespace TGUI
{
    public class Theme : SFML.ObjectBase
    {
        public Theme(string filename)
            : base(tguiTheme_create())
        {
            if (filename.Length > 0)
                load(filename);
        }

        protected internal Theme(IntPtr cPointer)
            : base(cPointer)
        {
        }

        public Theme(Theme copy)
            : base(tguiTheme_copy(copy.CPointer))
        {
        }

        protected override void Destroy(bool disposing)
        {
            tguiTheme_destroy(CPointer);
        }

        public void load(string filename)
        {
            if (!tguiTheme_load(CPointer, Util.ConvertStringForC_ASCII(filename)))
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
        }

        public RendererData getRenderer(string id)
        {
            IntPtr data = tguiTheme_getRenderer(CPointer, Util.ConvertStringForC_ASCII(id));
            if (data == IntPtr.Zero)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
            
            return new RendererData(data);
        }

        public void addRenderer(string id, RendererData renderer)
        {
            tguiTheme_addRenderer(CPointer, Util.ConvertStringForC_ASCII(id), renderer.CPointer);
        }

        public bool removeRenderer(string id)
        {
            return tguiTheme_removeRenderer(CPointer, Util.ConvertStringForC_ASCII(id));
        }

        public string Filename
        {
            get { return Util.GetStringFromC_ASCII(tguiTheme_getFilename(CPointer)); }
        }

        public static Theme Default
        {
            get { return new Theme(tguiTheme_getDefault()); }
            set { tguiTheme_setDefault(value.CPointer); }
        }

        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tgui_getLastError();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiTheme_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiTheme_copy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiTheme_destroy(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiTheme_load(IntPtr cPointer, IntPtr filename);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiTheme_getRenderer(IntPtr cPointer, IntPtr id);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiTheme_addRenderer(IntPtr cPointer, IntPtr id, IntPtr renderer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected bool tguiTheme_removeRenderer(IntPtr cPointer, IntPtr id);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiTheme_getFilename(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected void tguiTheme_setDefault(IntPtr defaultThemeCPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern protected IntPtr tguiTheme_getDefault();

        #endregion
    }
}
