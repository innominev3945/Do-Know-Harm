/*

The MIT License (MIT)

Copyright (c) 2015-2017 Secret Lab Pty. Ltd. and Yarn Spinner contributors.

Permission is hereby granted, free of charge, to any person obtaining a
copy of this software and associated documentation files (the "Software"),
to deal in the Software without restriction, including without limitation
the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
DEALINGS IN THE SOFTWARE.



using UnityEngine;

namespace Yarn.Unity
{
    /// <summary>
    /// A <see cref="MonoBehaviour"/> that a <see cref="DialogueRunner"/>
    /// uses to store and retrieve variables.
    /// </summary>
    /// <remarks>
    /// This abstract class inherits from <see cref="MonoBehaviour"/>,
    /// which means that subclasses of this class can be attached to <see
    /// cref="GameObject"/>s.
    /// </remarks>
    public abstract class CustomVariableStorage : MonoBehaviour, Yarn.IVariableStorage
    {
        [SerializeField]
        GameControl saveData;
        /// <inheritdoc/>
        public bool TryGetValue<T>(string variableName, out T result)
        {
            if(variableName == "$deaths")
            {
                result = (T) saveData.deaths;
                return true;
            }
            return false;
        }

        /// <inheritdoc/>
        public void SetValue(string variableName, string stringValue)
        {
            if(variableName == "$deaths")
            {
                saveData.deaths = Int32.Parse(stringValue);
                return;
            }
            return;
        }

        /// <inheritdoc/>
        public abstract void SetValue(string variableName, float floatValue);

        /// <inheritdoc/>
        public abstract void SetValue(string variableName, bool boolValue);

        /// <inheritdoc/>
        public abstract void Clear();

        /// <summary>
        /// Returns a boolean value representing if a particular variable is
        /// inside the variable storage.
        /// </summary>
        /// <param name="variableName">The name of the variable to check
        /// for.</param>
        /// <returns><see langword="true"/> if this variable storage contains a
        /// value for the variable named <paramref name="variableName"/>; <see
        /// langword="false"/> otherwise.</returns>
        public bool Contains(string variableName)
        {
            if(variableName == "$deaths")
            {
                //result = saveData.deaths;
                return true;
            }
            return false;
        }
    }
}*/