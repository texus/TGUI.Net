/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// TGUI - Texus' Graphical User Interface
// Copyright (C) 2012-2024 Bruno Van de Velde (vdv_b@tgui.eu)
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

namespace TGUI
{
    /// <summary>
    /// ObjectBase class is an abstract base for every TGUI object.
    /// It provides the Dispose interface and the CPointer to the related CTGUI object.
    /// </summary>
    public abstract class ObjectBase : IDisposable
    {
        /// <summary>
        /// Construct the object from a pointer to the CTGUI object
        /// </summary>
        /// <param name="cPointer">Internal pointer to the object in CTGUI</param>
        public ObjectBase(IntPtr cPointer)
        {
            if (cPointer == IntPtr.Zero)
                throw new Exception("CPointer can't be Zero when constructing TGUI object");

            _cPointer = cPointer;

            if (!(this is Gui))
                Util.RegisterDisposableObject(this);
        }

        /// <summary>
        /// Dispose the object
        /// </summary>
        ~ObjectBase()
        {
            Dispose(false);
        }

        /// <summary>
        /// Access to the internal pointer of the object.
        /// </summary>
        public IntPtr CPointer
        {
            get
            {
                if (_cPointer == IntPtr.Zero)
                    throw new ObjectDisposedException($"This {GetType().Name} instance has been disposed and should not be used");

                return _cPointer;
            }
        }

        /// <summary>
        /// Explicitly dispose the object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Destroy the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
        private void Dispose(bool disposing)
        {
            if (_cPointer != IntPtr.Zero)
            {
                if (!(this is Gui))
                    Util.UnregisterDisposableObject(this);

                Destroy(disposing);
                _cPointer = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Compare C object and object and checks if they are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>Objects are equal</returns>
        public override bool Equals(object? obj) => (obj is ObjectBase) && Equals((ObjectBase)obj);

        /// <summary>
        /// Compare two C objects and checks if they are equal
        /// </summary>
        /// <param name="other">Object to check</param>
        /// <returns>Objects are equal</returns>
        public bool Equals(ObjectBase other) => _cPointer == other._cPointer;

        /// <summary>
        /// Provide a integer describing the object
        /// </summary>
        /// <returns>Integer description of the object</returns>
        public override int GetHashCode() => _cPointer.GetHashCode();

        /// <summary>
        /// Destroy the object (implementation is left to each derived class)
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
        protected abstract void Destroy(bool disposing);

        private IntPtr _cPointer = IntPtr.Zero;
    }
}
