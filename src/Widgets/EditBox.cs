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
using System.Security;
using System.Runtime.InteropServices;

namespace TGUI
{
    /// <summary>
    /// Edit box widget
    /// </summary>
    public class EditBox : ClickableWidget
    {
        /// <summary>Validator for the InputValidator property to accept all input (default)</summary>
        public const string ValidatorAll = ".*";

        /// <summary>Validator for the InputValidator property to only accept integers</summary>
        public const string ValidatorInt = "[+-]?[0-9]*";

        /// <summary>Validator for the InputValidator property to only accept unsigned integers</summary>
        public const string ValidatorUInt = "[0-9]*";

        /// <summary>Validator for the InputValidator property to only accept numbers</summary>
        public const string ValidatorFloat = "[+-]?[0-9]*\\.?[0-9]*";

        /// <summary>
        /// Default constructor
        /// </summary>
        public EditBox()
            : base(tguiEditBox_create())
        {
        }

        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal EditBox(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">Object to copy</param>
        public EditBox(EditBox copy)
            : base(copy)
        {
        }

        /// <summary>
        /// Gets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        /// <remarks>
        /// After calling this function, the widget has its own copy of the renderer and it will no longer be shared.
        /// </remarks>
        public new EditBoxRenderer Renderer
        {
            get { return new EditBoxRenderer(tguiWidget_getRenderer(CPointer)); }
        }

        /// <summary>
        /// Gets the renderer, which gives access to properties that determine how the widget is displayed
        /// </summary>
        public new EditBoxRenderer SharedRenderer
        {
            get { return new EditBoxRenderer(tguiWidget_getSharedRenderer(CPointer)); }
        }

        /// <summary>
        /// Gets or sets the text of the editbox
        /// </summary>
        /// <remarks>
        /// The last characters of the text might be removed in the following situations:
        /// - You have set a character limit and this text contains too much characters.
        /// - You have limited the text width and the text does not fit inside the EditBox.
        /// </remarks>
        public string Text
        {
            get { return Util.GetStringFromC_UTF32(tguiEditBox_getText(CPointer)); }
            set { tguiEditBox_setText(CPointer, Util.ConvertStringForC_UTF32(value)); }
        }

        /// <summary>
        /// Gets or sets the default text of the editbox. This is the text drawn when the edit box is empty.
        /// </summary>
        /// <remarks>
        /// This text is not affected by the password character.
        /// </remarks>
        public string DefaultText
        {
            get { return Util.GetStringFromC_UTF32(tguiEditBox_getDefaultText(CPointer)); }
            set { tguiEditBox_setDefaultText(CPointer, Util.ConvertStringForC_UTF32(value)); }
        }

        /// <summary>
        /// Selects text in the edit box
        /// </summary>
        /// <param name="start">The index of the first character to select</param>
        /// <param name="length">Amount of character to select</param>
        public void SetSelectedText(uint start = 0, uint length = uint.MaxValue)
        {
            tguiEditBox_selectText(CPointer, start, length);
        }

        /// <summary>
        /// Gets the currently selected text (not affected by the password character)
        /// </summary>
        public string SelectedText
        {
            get { return Util.GetStringFromC_UTF32(tguiEditBox_getSelectedText(CPointer)); }
        }

        /// <summary>
        /// Gets or sets the character size of the text
        /// </summary>
        public uint TextSize
        {
            get { return tguiEditBox_getTextSize(CPointer); }
            set { tguiEditBox_setTextSize(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the password character (0 to not use a password character, default)
        /// </summary>
        public char PasswordCharacter
        {
            get { return tguiEditBox_getPasswordCharacter(CPointer); }
            set { tguiEditBox_setPasswordCharacter(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the character limit (0 to disable, default)
        /// </summary>
        public uint MaximumCharacters
        {
            get { return tguiEditBox_getMaximumCharacters(CPointer); }
            set { tguiEditBox_setMaximumCharacters(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the text alignment
        /// </summary>
        public HorizontalAlignment Alignment
        {
            get { return tguiEditBox_getAlignment(CPointer); }
            set { tguiEditBox_setAlignment(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whether the text width is limited or whether you are able to type even if the edit box is full
        /// </summary>
        public bool LimitTextWidth
        {
            get { return tguiEditBox_isTextWidthLimited(CPointer); }
            set { tguiEditBox_limitTextWidth(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets whether the text box is read-only
        /// </summary>
        /// <remarks>
        /// When the edit box is read-only, you can no longer delete characters and type text.
        /// Selecting text, copying text and even calling the setText function will still work.
        /// </remarks>
        public bool ReadOnly
        {
            get { return tguiEditBox_isReadOnly(CPointer); }
            set { tguiEditBox_setReadOnly(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets after which character the blinking cursor is located
        /// </summary>
        public uint CaretPosition
        {
            get { return tguiEditBox_getCaretPosition(CPointer); }
            set { tguiEditBox_setCaretPosition(CPointer, value); }
        }

        /// <summary>
        /// Gets or sets the regex used to decide whether the entered text is accepted or dropped
        /// </summary>
        /// <remarks>
        /// When the regex does not match when calling the setText function then the edit box contents will be cleared.
        /// When it does not match when the user types a character in the edit box, then the input character is rejected.
        /// </remarks>
        /// <example>
        /// <code>
        /// editBox.InputValidator = EditBox.ValidatorInt;
        /// editBox.InputValidator = "[a-zA-Z][a-zA-Z0-9]*";
        /// </code>
        /// </example>
        public string InputValidator
        {
            get { return Util.GetStringFromC_ASCII(tguiEditBox_getInputValidator(CPointer)); }
            set { tguiEditBox_setInputValidator(CPointer, Util.ConvertStringForC_ASCII(value)); }
        }

        /// <summary>
        /// Gets or sets a suffix displayed at the right side of the edit box
        /// </summary>
        public string Suffix
        {
            get { return Util.GetStringFromC_UTF32(tguiEditBox_getSuffix(CPointer)); }
            set { tguiEditBox_setSuffix(CPointer, Util.ConvertStringForC_UTF32(value)); }
        }

        /// <summary>
        /// Initializes the signals
        /// </summary>
        protected override void InitSignals()
        {
            base.InitSignals();

            TextChangedCallback = new CallbackActionString(ProcessTextChangedSignal);
            if (tguiWidget_connectString(CPointer, Util.ConvertStringForC_ASCII("TextChanged"), TextChangedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));

            ReturnKeyPressedCallback = new CallbackActionString(ProcessReturnKeyPressedSignal);
            if (tguiWidget_connectString(CPointer, Util.ConvertStringForC_ASCII("ReturnKeyPressed"), ReturnKeyPressedCallback) == 0)
                throw new TGUIException(Util.GetStringFromC_ASCII(tgui_getLastError()));
        }

        private void ProcessTextChangedSignal(IntPtr text)
        {
            TextChanged?.Invoke(this, new SignalArgsString(Util.GetStringFromC_UTF32(text)));
        }

        private void ProcessReturnKeyPressedSignal(IntPtr text)
        {
            ReturnKeyPressed?.Invoke(this, new SignalArgsString(Util.GetStringFromC_UTF32(text)));
        }

        /// <summary>Event handler for the TextChanged signal</summary>
        public event EventHandler<SignalArgsString> TextChanged = null;

        /// <summary>Event handler for the ReturnKeyPressed signal</summary>
        public event EventHandler<SignalArgsString> ReturnKeyPressed = null;

        private CallbackActionString TextChangedCallback;
        private CallbackActionString ReturnKeyPressedCallback;


        #region Imports

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiEditBox_create();

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBox_setText(IntPtr cPointer, IntPtr text);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiEditBox_getText(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBox_setDefaultText(IntPtr cPointer, IntPtr defaultText);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiEditBox_getDefaultText(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBox_selectText(IntPtr cPointer, uint start, uint length);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiEditBox_getSelectedText(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBox_setTextSize(IntPtr cPointer, uint textSize);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiEditBox_getTextSize(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBox_setPasswordCharacter(IntPtr cPointer, char passwordChar);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private char tguiEditBox_getPasswordCharacter(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBox_setMaximumCharacters(IntPtr cPointer, uint maximumCharacters);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiEditBox_getMaximumCharacters(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBox_setAlignment(IntPtr cPointer, HorizontalAlignment alignment);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private HorizontalAlignment tguiEditBox_getAlignment(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBox_limitTextWidth(IntPtr cPointer, bool limitWidth);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiEditBox_isTextWidthLimited(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBox_setReadOnly(IntPtr cPointer, bool readOnly);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private bool tguiEditBox_isReadOnly(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBox_setCaretPosition(IntPtr cPointer, uint caretPosition);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private uint tguiEditBox_getCaretPosition(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBox_setInputValidator(IntPtr cPointer, IntPtr validator);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiEditBox_getInputValidator(IntPtr cPointer);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private void tguiEditBox_setSuffix(IntPtr cPointer, IntPtr suffix);

        [DllImport(Global.CTGUI, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern private IntPtr tguiEditBox_getSuffix(IntPtr cPointer);

        #endregion
    }
}
