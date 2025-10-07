/* Copyright (c) 2025 Rick (rick 'at' gibbed 'dot' us)
 *
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;

namespace Gibbed.IvaliceChronicles.ScriptFormats
{
    public static class OperandTypeHelpers
    {
        public static int GetSize(this OperandType type) => type switch
        {
            OperandType.Bool8 or
            OperandType.Bool8OnOff or
            OperandType.Bool8OffOn or
            OperandType.Int8 or
            OperandType.UInt8 => 1,
            OperandType.Int16 or
            OperandType.UInt16 or
            OperandType.UInt16MessageIndex => 2,
            OperandType.Int32 or
            OperandType.UInt32 => 4,
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };
    }
}
