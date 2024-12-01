// This file is generated, it should not be edited directly.

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    /// <summary>
    /// FileDialog widget
    /// </summary>
    public class FileDialog : ChildWindow
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public FileDialog()
            : base(tguiFileDialog_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal FileDialog(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public FileDialog(FileDialog copy)
            : base(copy)
        {
        }

        public struct Filter
        {
            public string name;
            public List<string> expressions;
        };

        public string Path
        {
            get => Util.GetStringFromC_UTF32(tguiFileDialog_getPath(CPointer));
            set => tguiFileDialog_setPath(CPointer, Util.ConvertStringForC_UTF32(value));
        }

        public unsafe IReadOnlyList<string> SelectedPaths
        {
            get
            {
                IntPtr* returnStringsC = tguiFileDialog_getSelectedPaths(CPointer, out UIntPtr returnCount);
                string[] returnStrings = new string[(int)returnCount];
                for (int i = 0; i < (int)returnCount; ++i)
                    returnStrings[i] = Util.GetStringFromC_UTF32(returnStringsC[i]) ?? throw new ArgumentNullException();

                return returnStrings;
            }
        }

        public unsafe IReadOnlyList<Filter> FileTypeFilters
        {
            get
            {
                IntPtr* filtersC = tguiFileDialog_getFileTypeFilters(CPointer, out UIntPtr filtersCount);

                Filter[] filters = new Filter[(int)filtersCount];
                for (int i = 0; i < (int)filtersCount; ++i)
                {
                    filters[i].name = Util.GetStringFromC_UTF32(tguiFileDialogFilter_getName(filtersC[i]));

                    IntPtr* expressionsC = tguiFileDialogFilter_getExpressions(filtersC[i], out UIntPtr expressionsCount);
                    filters[i].expressions = new List<string>();
                    for (int j = 0; j < (int)expressionsCount; ++j)
                        filters[i].expressions.Add(Util.GetStringFromC_UTF32(expressionsC[i]));

                    tguiFileDialogFilter_destroy(filtersC[i]);
                }

                return filters;
            }

            set
            {
                IntPtr[] filtersC = new IntPtr[value.Count];
                for (int i = 0; i < (int)value.Count; ++i)
                {
                    filtersC[i] = tguiFileDialogFilter_create(Util.ConvertStringForC_UTF32(value[i].name));
                    if (!(value[i].expressions is null))
                    {
                        foreach (string expression in value[i].expressions)
                            tguiFileDialogFilter_addExpression(filtersC[i], Util.ConvertStringForC_UTF32(expression));
                    }
                }

                tguiFileDialog_setFileTypeFilters(CPointer, filtersC, (UIntPtr)filtersC.Length, (UIntPtr)0);

                for (int i = 0; i < (int)value.Count; ++i)
                    tguiFileDialogFilter_destroy(filtersC[i]);
            }
        }

        public unsafe int DefaultFilterIndex
        {
            get => (int)tguiFileDialog_getFileTypeFiltersIndex(CPointer);
            set
            {
                IntPtr* filters = tguiFileDialog_getFileTypeFilters(CPointer, out UIntPtr filtersCount);
                IntPtr[] filtersArray = new IntPtr[(int)filtersCount];
                for (int i = 0; i < (int)filtersCount; ++i)
                    filtersArray[i] = filters[i];
                tguiFileDialog_setFileTypeFilters(CPointer, filtersArray, filtersCount, (UIntPtr)value);
                for (int i = 0; i < (int)filtersCount; ++i)
                    tguiFileDialogFilter_destroy(filters[i]);
            }
        }

        public (string name, string size, string modified) ColumnCaptions
        {
            get => (Util.GetStringFromC_UTF32(tguiFileDialog_getListViewColumnCaptionsName(CPointer)),
                    Util.GetStringFromC_UTF32(tguiFileDialog_getListViewColumnCaptionsSize(CPointer)),
                    Util.GetStringFromC_UTF32(tguiFileDialog_getListViewColumnCaptionsModified(CPointer)));
            set => tguiFileDialog_setListViewColumnCaptions(CPointer,
                                                            Util.ConvertStringForC_UTF32(value.name),
                                                            Util.ConvertStringForC_UTF32(value.size),
                                                            Util.ConvertStringForC_UTF32(value.modified));
        }

        public new FileDialogRenderer Renderer
        {
            get => new FileDialogRenderer(tguiWidget_getRenderer(CPointer));
            set => SetRenderer(value.Data);
        }

        public  new FileDialogRenderer SharedRenderer => new FileDialogRenderer(tguiWidget_getSharedRenderer(CPointer));

        public string Filename
        {
            get => Util.GetStringFromC_UTF32(tguiFileDialog_getFilename(CPointer));
            set => tguiFileDialog_setFilename(CPointer, Util.ConvertStringForC_UTF32(value));
        }

        public string ConfirmButtonText
        {
            get => Util.GetStringFromC_UTF32(tguiFileDialog_getConfirmButtonText(CPointer));
            set => tguiFileDialog_setConfirmButtonText(CPointer, Util.ConvertStringForC_UTF32(value));
        }

        public string CancelButtonText
        {
            get => Util.GetStringFromC_UTF32(tguiFileDialog_getCancelButtonText(CPointer));
            set => tguiFileDialog_setCancelButtonText(CPointer, Util.ConvertStringForC_UTF32(value));
        }

        public string CreateFolderButtonText
        {
            get => Util.GetStringFromC_UTF32(tguiFileDialog_getCreateFolderButtonText(CPointer));
            set => tguiFileDialog_setCreateFolderButtonText(CPointer, Util.ConvertStringForC_UTF32(value));
        }

        public string FilenameLabelText
        {
            get => Util.GetStringFromC_UTF32(tguiFileDialog_getFilenameLabelText(CPointer));
            set => tguiFileDialog_setFilenameLabelText(CPointer, Util.ConvertStringForC_UTF32(value));
        }

        public bool AllowCreateFolder
        {
            get => tguiFileDialog_getAllowCreateFolder(CPointer) != 0;
            set => tguiFileDialog_setAllowCreateFolder(CPointer, value ? (byte)1 : (byte)0);
        }

        public bool FileMustExist
        {
            get => tguiFileDialog_getFileMustExist(CPointer) != 0;
            set => tguiFileDialog_setFileMustExist(CPointer, value ? (byte)1 : (byte)0);
        }

        public bool SelectingDirectory
        {
            get => tguiFileDialog_getSelectingDirectory(CPointer) != 0;
            set => tguiFileDialog_setSelectingDirectory(CPointer, value ? (byte)1 : (byte)0);
        }

        public bool MultiSelect
        {
            get => tguiFileDialog_getMultiSelect(CPointer) != 0;
            set => tguiFileDialog_setMultiSelect(CPointer, value ? (byte)1 : (byte)0);
        }

        public class FileSelectEventArgs : EventArgs
        {
            public FileSelectEventArgs(string[] paths)
            {
                Paths = paths;
            }
            public string[] Paths { get; }
        }
        public unsafe event EventHandler<FileSelectEventArgs> OnFileSelect
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallbackFileDialogPaths func = (UIntPtr count, IntPtr* strings) => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    string[] stringArray = new string[(int)count];
                    for (int i = 0; i < (int)count; ++i)
                        stringArray[i] = Util.GetStringFromC_UTF32(strings[i]);
                    value(sender, new FileSelectEventArgs(stringArray));
                };
                uint id = tguiWidget_signalFileDialogPathsConnect(CPointer, Util.ConvertStringForC_UTF32("FileSelected"), func);
                ConnectEventHandler(id, "FileSelected", value, func);
            }
            remove
            {
                DisconnectEventHandler("FileSelected", value);
            }
        }

        public event EventHandler OnCancel
        {
            add
            {
                var selfCPointer = CPointer;
                var selfType = GetType();
                UnmanagedCallback func = () => {
                    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);
                    value(sender, EventArgs.Empty);
                };
                uint id = tguiWidget_signalConnect(CPointer, Util.ConvertStringForC_UTF32("Cancelled"), func);
                ConnectEventHandler(id, "Cancelled", value, func);
            }
            remove
            {
                DisconnectEventHandler("Cancelled", value);
            }
        }

        #region Imports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFileDialogFilter_create(IntPtr name);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFileDialogFilter_destroy(IntPtr filter);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFileDialogFilter_getName(IntPtr filter);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFileDialogFilter_addExpression(IntPtr filter, IntPtr expression);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe IntPtr* tguiFileDialogFilter_getExpressions(IntPtr filter, out UIntPtr count);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFileDialog_setPath(IntPtr cPointer, IntPtr path);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFileDialog_getPath(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe IntPtr* tguiFileDialog_getSelectedPaths(IntPtr cPointer, out UIntPtr count);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void tguiFileDialog_setFileTypeFilters(IntPtr cPointer, IntPtr[] filters, UIntPtr filterCount, UIntPtr defaultFilterIndex);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe IntPtr* tguiFileDialog_getFileTypeFilters(IntPtr cPointer, out UIntPtr count);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr tguiFileDialog_getFileTypeFiltersIndex(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFileDialog_setListViewColumnCaptions(IntPtr cPointer, IntPtr nameColumnText, IntPtr sizeColumnText, IntPtr modifiedColumnText);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFileDialog_getListViewColumnCaptionsName(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFileDialog_getListViewColumnCaptionsSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFileDialog_getListViewColumnCaptionsModified(IntPtr cPointer);

        #endregion

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFileDialog_create();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFileDialog_getFilename(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFileDialog_setFilename(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFileDialog_getConfirmButtonText(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFileDialog_setConfirmButtonText(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFileDialog_getCancelButtonText(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFileDialog_setCancelButtonText(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFileDialog_getCreateFolderButtonText(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFileDialog_setCreateFolderButtonText(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiFileDialog_getFilenameLabelText(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFileDialog_setFilenameLabelText(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiFileDialog_getAllowCreateFolder(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFileDialog_setAllowCreateFolder(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiFileDialog_getFileMustExist(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFileDialog_setFileMustExist(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiFileDialog_getSelectingDirectory(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFileDialog_setSelectingDirectory(IntPtr cPointer, byte value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiFileDialog_getMultiSelect(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiFileDialog_setMultiSelect(IntPtr cPointer, byte value);

        #endregion
    }
}
