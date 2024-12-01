// This file is generated, it should not be edited directly.

using System.Runtime.InteropServices;
using System.Security;
using System;

namespace TGUI
{
    public enum SpriteScalingType
    {
        Normal,
        Horizontal,
        Vertical,
        NineSlice,
    }

    public class Sprite : ObjectBase
    {
        /// <summary>
        /// Constructor that creates the object from its C pointer
        /// </summary>
        /// <param name="cPointer">Pointer to object in C code</param>
        protected internal Sprite(IntPtr cPointer)
            : base(cPointer)
        {
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Sprite()
            : base(tguiSprite_createNull())
        {
        }

        /// <summary>
        /// Construct from a texture
        /// </summary>
        public Sprite(Texture texture)
            : base(tguiSprite_createFromTexture(texture.CPointer))
        {
        }

        /// <summary>
        /// Destroy the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
        protected override void Destroy(bool disposing)
        {
            tguiSprite_destroy(CPointer);
        }

        public bool IsSet()
        {
            return tguiSprite_isSet(CPointer) != 0;
        }

        public Texture Texture
        {
            get => new Texture(tguiSprite_getTexture(CPointer));
            set => tguiSprite_setTexture(CPointer, value.CPointer);
        }

        public Vector2f Position
        {
            get => tguiSprite_getPosition(CPointer);
            set => tguiSprite_setPosition(CPointer, value);
        }

        public Vector2f Size
        {
            get => tguiSprite_getSize(CPointer);
            set => tguiSprite_setSize(CPointer, value);
        }

        public float Opacity
        {
            get => tguiSprite_getOpacity(CPointer);
            set => tguiSprite_setOpacity(CPointer, value);
        }

        public FloatRect VisibleRect
        {
            get => tguiSprite_getVisibleRect(CPointer);
            set => tguiSprite_setVisibleRect(CPointer, value);
        }

        public float Rotation
        {
            get => tguiSprite_getRotation(CPointer);
            set => tguiSprite_setRotation(CPointer, value);
        }

        public bool IsTransparentPixel(Vector2f pos)
        {
            return tguiSprite_isTransparentPixel(CPointer, pos) != 0;
        }

        public SpriteScalingType GetScalingType()
        {
            return tguiSprite_getScalingType(CPointer);
        }

        #region Imports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSprite_createNull();

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSprite_createFromTexture(IntPtr texture);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSprite_destroy(IntPtr cPointer);

        #endregion

        #region GeneratedImports

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiSprite_isSet(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr tguiSprite_getTexture(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSprite_setTexture(IntPtr cPointer, IntPtr value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiSprite_getPosition(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSprite_setPosition(IntPtr cPointer, Vector2f value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f tguiSprite_getSize(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSprite_setSize(IntPtr cPointer, Vector2f value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiSprite_getOpacity(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSprite_setOpacity(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern FloatRect tguiSprite_getVisibleRect(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSprite_setVisibleRect(IntPtr cPointer, FloatRect value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float tguiSprite_getRotation(IntPtr cPointer);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void tguiSprite_setRotation(IntPtr cPointer, float value);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern byte tguiSprite_isTransparentPixel(IntPtr cPointer, Vector2f pos);

        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern SpriteScalingType tguiSprite_getScalingType(IntPtr cPointer);

        #endregion
    }
}
