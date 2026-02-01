#################################################################################################################################
#
# TGUI - Texus' Graphical User Interface
# Copyright (C) 2012-2024 Bruno Van de Velde (vdv_b@tgui.eu)
#
# This software is provided 'as-is', without any express or implied warranty.
# In no event will the authors be held liable for any damages arising from the use of this software.
#
# Permission is granted to anyone to use this software for any purpose,
# including commercial applications, and to alter it and redistribute it freely,
# subject to the following restrictions:
#
# 1. The origin of this software must not be misrepresented;
#    you must not claim that you wrote the original software.
#    If you use this software in a product, an acknowledgment
#    in the product documentation would be appreciated but is not required.
#
# 2. Altered source versions must be plainly marked as such,
#    and must not be misrepresented as being the original software.
#
# 3. This notice may not be removed or altered from any source distribution.
#
#################################################################################################################################

import os
from CTGUI.FileParser import *

CTGUI_TEMPLATES_DIR = os.path.join('CTGUI', 'templates')

TYPE_MAP_C = {
    'void' : 'void',
    'bool' : 'byte',
    'int' : 'int',
    'uint' : 'uint',
    'float' : 'float',
    'size_t' : 'UIntPtr',
    'string' : 'IntPtr',
    'TextStyle' : 'TextStyles',
    'Char32' : 'uint',
    'Color' : 'ColorCTGUI',
    'Texture' : 'IntPtr',
    'Outline' : 'IntPtr',
    'Layout' : 'IntPtr',
    'Layout2d' : 'IntPtr',
    'Font' : 'IntPtr',
    'RendererData' : 'IntPtr',
    'Widget' : 'IntPtr',
    'ConstWidget' : 'IntPtr',
    'AnyObject' : 'IntPtr',
    'Vector2f' : 'Vector2f',
    'Vector2u' : 'Vector2u',
    'Vector2i' : 'Vector2i',
    'FloatRect' : 'FloatRect',
    'UIntRect' : 'UIntRect',
    'IntRect' : 'IntRect',
    'List<string>' : 'IntPtr[]_or_IntPtr*',
    'List<Widget>' : 'IntPtr[]_or_IntPtr*',
    'Set<size_t>' : 'UIntPtr[]_or_UIntPtr*',
    'ScrollbarPolicy': 'ScrollbarPolicy',
    'VerticalAlignment': 'VerticalAlignment',
    'HorizontalAlignment': 'HorizontalAlignment',
    'Orientation': 'Orientation',
    'CursorType': 'Cursor.Type',
    'Duration': 'Duration',
}

TYPE_MAP_CS = {
    'void' : 'void',
    'bool' : 'bool',
    'int' : 'int',
    'uint' : 'int',
    'float' : 'float',
    'size_t' : 'int',
    'string' : 'string',
    'TextStyle' : 'TextStyles',
    'Char32' : 'string',
    'Color' : 'Color?',
    'Texture' : 'Texture',
    'Outline' : 'Outline',
    'Layout' : 'Layout',
    'Layout2d' : 'Layout2d',
    'Font' : 'Font',
    'RendererData' : 'RendererData',
    'Widget' : 'Widget?',
    'ConstWidget' : 'Widget?',
    'AnyObject' : 'string',
    'Vector2f' : 'Vector2f',
    'Vector2u' : 'Vector2u',
    'Vector2i' : 'Vector2i',
    'FloatRect' : 'FloatRect',
    'UIntRect' : 'UIntRect',
    'IntRect' : 'IntRect',
    'List<string>' : 'ReadOnlySpan_or_IReadOnlyList<string>',
    'List<Widget>' : 'ReadOnlySpan_or_IReadOnlyList<Widget>',
    'Set<size_t>' : 'HashSet<int>',
    'ScrollbarPolicy': 'ScrollbarPolicy',
    'VerticalAlignment': 'VerticalAlignment',
    'HorizontalAlignment': 'HorizontalAlignment',
    'Orientation': 'Orientation',
    'CursorType': 'Cursor.Type',
    'Duration': 'Duration',
}


def generateExportedSymbolCSharpToC(className, enums, funcNameC, static, returnType, params):
    paramsC = '' if static else 'IntPtr cPointer'
    for param in params:
        paramType, paramName, paramDefaultValue = param
        if paramType in enums:
            paramsC += ', ' + className + paramType + ' ' + paramName
        else:
            if TYPE_MAP_C[paramType] == 'IntPtr[]_or_IntPtr*':
                paramsC += ', IntPtr[] ' + paramName
            elif TYPE_MAP_C[paramType] == 'UIntPtr[]_or_UIntPtr*':
                paramsC += ', UIntPtr[] ' + paramName
            else:
                paramsC += ', ' + TYPE_MAP_C[paramType] + ' ' + paramName
            if paramType.startswith('List<') or paramType.startswith('Set<'):
                paramsC += ', UIntPtr ' + paramName + 'Length'

    if returnType.startswith('List<') or returnType.startswith('Set<'):
        paramsC += ', out UIntPtr returnCount'

    if paramsC.startswith(', '): # Static function with at least one parameter
        paramsC = paramsC[2:]

    if returnType in enums:
        return className + returnType + ' tgui' + className + '_' + funcNameC + '(' + paramsC + ')'
    else:
        if TYPE_MAP_C[returnType] == 'IntPtr[]_or_IntPtr*':
            return 'IntPtr* tgui' + className + '_' + funcNameC + '(' + paramsC + ')'
        if TYPE_MAP_C[returnType] == 'UIntPtr[]_or_UIntPtr*':
            return 'UIntPtr* tgui' + className + '_' + funcNameC + '(' + paramsC + ')'
        else:
            return TYPE_MAP_C[returnType] + ' tgui' + className + '_' + funcNameC + '(' + paramsC + ')'


def generateFunctionBodyCSharp(className, enums, funcNameC, static, returnType, params):
    bodyLines = []
    unsafeCode = False

    funcCallParams = [] if static else ['CPointer']
    for param in params:
        paramType, paramName, defaultValue = param
        if paramType in enums or paramType == 'Vector2f' or paramType == 'Vector2u' or paramType == 'Vector2i' \
        or paramType == 'FloatRect' or paramType == 'UIntRect' or paramType == 'IntRect' or paramType == 'int' \
        or paramType == 'float' or paramType == 'TextStyle' or paramType == 'VerticalAlignment' or paramType == 'HorizontalAlignment' \
        or paramType == 'ScrollbarPolicy' or paramType == 'Orientation' or paramType == 'CursorType' or paramType == 'Duration':
            funcCallParams.append(paramName)
        elif paramType == 'size_t':
            funcCallParams.append('(UIntPtr)' + paramName)
        elif paramType == 'uint':
            funcCallParams.append('(uint)' + paramName)
        elif paramType == 'bool':
            funcCallParams.append(paramName + ' ? (byte)1 : (byte)0')
        elif paramType == 'Char32':
            funcCallParams.append('(uint)Char.ConvertToUtf32(' + paramName + ', 0)')
        elif paramType == 'string' or paramType == 'AnyObject':
            funcCallParams.append('Util.ConvertStringForC_UTF32(' + paramName + ')')
        elif paramType == 'Color':
            funcCallParams.append('Util.ConvertColorForC(' + paramName + ')')
        elif paramType == 'Texture' or paramType == 'Font' \
        or paramType == 'Outline' or paramType == 'Layout' or paramType == 'Layout2d' or paramType == 'RendererData':
            funcCallParams.append(paramName + '.CPointer')
        elif paramType == 'Widget' or  paramType == 'ConstWidget':
            funcCallParams.append(paramName + ' is null ? IntPtr.Zero : ' + paramName + '.CPointer')
        elif paramType == 'List<string>':
            bodyLines.append('IntPtr[] ' + paramName + 'ForC = new IntPtr[' + paramName + '.Length];')
            bodyLines.append('for (int i = 0; i < ' + paramName + '.Length; ++i)')
            bodyLines.append('    ' + paramName + 'ForC[i] = Util.ConvertStringForC_UTF32(' + paramName + '[i]);')
            bodyLines.append('')
            funcCallParams.append(paramName + 'ForC')
            funcCallParams.append('(UIntPtr)' + paramName + 'ForC.Length')
        elif paramType == 'Set<size_t>':
            bodyLines.append('UIntPtr[] ' + paramName + 'Array = Array.ConvertAll(' + paramName + '.ToArray(), val => (UIntPtr)val);')
            bodyLines.append('')
            funcCallParams.append(paramName + 'Array')
            funcCallParams.append('(UIntPtr)' + paramName + 'Array.Length')
        else:
            raise RuntimeError('Type ' + paramType + ' is not supported as function parameter')

    if returnType.startswith('List<') or returnType.startswith('Set<'):
        funcCallParams.append('out UIntPtr returnCount')

    funcCall = 'tgui' + className + '_' + funcNameC + '(' + ', '.join(funcCallParams) + ')'

    if returnType in enums or returnType == 'Vector2f' or returnType == 'Vector2u' or returnType == 'Vector2i' or returnType == 'int' \
    or returnType == 'float' or returnType == 'TextStyle' or returnType == 'FloatRect' \
    or returnType == 'UIntRect' or returnType == 'IntRect' or returnType == 'HorizontalAlignment' or returnType == 'VerticalAlignment' \
    or returnType == 'ScrollbarPolicy' or returnType == 'Orientation' or returnType == 'CursorType' or returnType == 'Duration':
        bodyLines.append('return ' + funcCall + ';')
    elif returnType == 'void':
        bodyLines.append(funcCall + ';')
    elif returnType == 'uint' or returnType == 'size_t':
        bodyLines.append('return (int)' + funcCall + ';')
    elif returnType == 'bool':
        bodyLines.append('return ' + funcCall + ' != 0;')
    elif returnType == 'Char32':
        bodyLines.append('return Char.ConvertFromUtf32((int)' + funcCall + ');')
    elif returnType == 'Widget':
        bodyLines.append('return Util.GetWidgetFromC(' + funcCall + ');')
    elif returnType == 'string' or returnType == 'AnyObject':
        bodyLines.append('return Util.GetStringFromC_UTF32(' + funcCall + ');')
    elif returnType == 'Color':
        bodyLines.append('return Util.GetColorFromC(' + funcCall + ');')
    elif returnType == 'Outline':
        bodyLines.append('return new Outline(' + funcCall + ');')
    elif returnType == 'Layout':
        bodyLines.append('return new Layout(' + funcCall + ');')
    elif returnType == 'Layout2d':
        bodyLines.append('return new Layout2d(' + funcCall + ');')
    elif returnType == 'Texture':
        bodyLines.append('return new Texture(' + funcCall + ');')
    elif returnType == 'Font':
        bodyLines.append('return new Font(' + funcCall + ');')
    elif returnType == 'RendererData':
        bodyLines.append('return new RendererData(' + funcCall + ');')
    elif returnType == 'List<Widget>':
        unsafeCode = True
        bodyLines.extend([
            'IntPtr* returnWidgetsC = ' + funcCall + ';',
            'Widget[] returnWidgets = new Widget[(int)returnCount];',
            'for (int i = 0; i < (int)returnCount; ++i)',
            '    returnWidgets[i] = Util.GetWidgetFromC(returnWidgetsC[i]) ?? throw new ArgumentNullException();',
            '',
            'return returnWidgets;'
        ])
    elif returnType == 'List<string>':
        unsafeCode = True
        bodyLines.extend([
            'IntPtr* returnStringsC = ' + funcCall + ';',
            'string[] returnStrings = new string[(int)returnCount];',
            'for (int i = 0; i < (int)returnCount; ++i)',
            '    returnStrings[i] = Util.GetStringFromC_UTF32(returnStringsC[i]) ?? throw new ArgumentNullException();',
            '',
            'return returnStrings;'
        ])
    elif returnType == 'List<size_t>':
        unsafeCode = True
        bodyLines.extend([
            'IntPtr* returnIntsC = ' + funcCall + ';',
            'int[] returnInts = new int[(int)returnCount];',
            'for (int i = 0; i < (int)returnCount; ++i)',
            '    returnInts[i] = returnIntsC[i];',
            '',
            'return returnInts;'
        ])
    elif returnType == 'Set<size_t>':
        unsafeCode = True
        bodyLines.extend([
            'UIntPtr* returnIntsC = ' + funcCall + ';',
            'HashSet<int> returnInts = new HashSet<int>();',
            'for (int i = 0; i < (int)returnCount; ++i)',
            '    returnInts.Add((int)returnIntsC[i]);',
            '',
            'return returnInts;'
        ])
    else:
        raise RuntimeError('function return type ' + returnType + ' is not supported')

    return bodyLines, unsafeCode


def generateReturnTypeCSharp(className, enums, returnType):
    if returnType in enums:
        return className + returnType
    elif 'ReadOnlySpan_or_IReadOnlyList' in TYPE_MAP_CS[returnType]:
        return TYPE_MAP_CS[returnType].replace('ReadOnlySpan_or_IReadOnlyList', 'IReadOnlyList')
    else:
        return TYPE_MAP_CS[returnType]


def generatePropertyCSharp(className, segment, enums):
    propertyType = segment.type
    propertyName = segment.name

    getterPrefix = 'is' if segment.getterUsesIsPrefix else 'get'
    getterBody, unsafeCode = generateFunctionBodyCSharp(className, enums, getterPrefix + propertyName, segment.static, propertyType, [])
    exportedSymbols = [generateExportedSymbolCSharpToC(className, enums, getterPrefix + propertyName, segment.static, propertyType, [])]
    if not segment.getterOnly:
        setterBody, setterUnsafeCode = generateFunctionBodyCSharp(className, enums, 'set' + propertyName, segment.static, 'void', [(propertyType, 'value', None)])
        exportedSymbols.append(generateExportedSymbolCSharpToC(className, enums, 'set' + propertyName, segment.static, 'void', [(propertyType, 'value', None)]))
        if setterUnsafeCode:
            unsafeCode = True

    returnType = generateReturnTypeCSharp(className, enums, propertyType)
    staticStr = 'static ' if segment.static else ''

    generatedLines = []
    generatedLines.append('public ' + staticStr + ('unsafe ' if unsafeCode else '') + returnType + ' ' + propertyName)
    generatedLines.append('{')

    if len(getterBody) == 1:
        assert(getterBody[0].startswith('return '))
        generatedLines.append('    get => ' + getterBody[0][7:])
    else:
        generatedLines.append('    get')
        generatedLines.append('    {')
        for line in getterBody:
            if line:
                generatedLines.append('        ' + line)
            else:
                generatedLines.append('')
        generatedLines.append('    }')

    if not segment.getterOnly:
        if len(setterBody) == 1:
            generatedLines.append('    set => ' + setterBody[0])
        else:
            generatedLines.append('    set')
            generatedLines.append('    {')
            for line in setterBody:
                if line:
                    generatedLines.append('        ' + line)
                else:
                    generatedLines.append('')
            generatedLines.append('    }')

    generatedLines.append('}')
    return generatedLines, exportedSymbols


def generateFunctionCSharp(className, segment, enums):
    generatedLines = []
    exportedSymbols = [generateExportedSymbolCSharpToC(className, enums, segment.nameC, segment.static, segment.returnType, segment.params)]
    usingDeclarations = set()

    params = ''
    if segment.params:
        firstParam = True
        for param in segment.params:
            if firstParam:
                firstParam = False
            else:
                params += ', '

            paramType, paramName, paramDefaultValue = param
            if paramType in enums:
                params += className + paramType + ' ' + paramName
                if paramDefaultValue is not None:
                    params += ' = ' + className + paramType + '.' + paramDefaultValue
            else:
                if paramType not in TYPE_MAP_CS:
                    raise RuntimeError('Type "' + paramType + '" does not exist')
                elif 'ReadOnlySpan_or_IReadOnlyList' in TYPE_MAP_CS[paramType]:
                    params += TYPE_MAP_CS[paramType].replace('ReadOnlySpan_or_IReadOnlyList', 'ReadOnlySpan') + ' ' + paramName
                else:
                    params += TYPE_MAP_CS[paramType] + ' ' + paramName

                if paramDefaultValue is not None:
                    if paramType == 'string' and paramDefaultValue.startswith("'") and paramDefaultValue.startswith("'"):
                        params += ' = "' + paramDefaultValue[1:-1] + '"'
                    elif paramType == 'bool' and (paramDefaultValue == 'true' or paramDefaultValue == 'false'):
                        params += ' = ' + paramDefaultValue
                    elif paramType == 'int' or paramType == 'uint' or paramType == 'float':
                        params += ' = ' + paramDefaultValue
                    elif paramType == 'HorizontalAlignment':
                        params += ' = HorizontalAlignment.' + paramDefaultValue
                    elif paramType == 'VerticalAlignment':
                        params += ' = VerticalAlignment.' + paramDefaultValue
                    else:
                        raise RuntimeError('Default value "' + paramDefaultValue + '" for type "' + paramType + '" is not supported yet')

    bodyLines, unsafeCode = generateFunctionBodyCSharp(className, enums, segment.nameC, segment.static, segment.returnType, segment.params)
    returnType = generateReturnTypeCSharp(className, enums, segment.returnType)
    staticStr = 'static ' if segment.static else ''
    generatedLines.append('public ' + staticStr + ('unsafe ' if unsafeCode else '') + returnType + ' ' + segment.name[0].upper() + segment.name[1:] + '(' + params + ')')
    generatedLines.append('{')

    if segment.returnType.startswith('List<') or segment.returnType.startswith('Set<'):
        usingDeclarations.add('using System.Collections.Generic;')
    for param in segment.params:
        if param[0].startswith('List<') or param[0].startswith('Set<'):
            usingDeclarations.add('using System.Collections.Generic;')
        if param[0].startswith('Set<'):
            usingDeclarations.add('using System.Linq;')

    for line in bodyLines:
        if line:
            generatedLines.append('    ' + line)
        else:
            generatedLines.append('')

    generatedLines.append('}')
    return generatedLines, exportedSymbols, usingDeclarations


def generateSignalCSharp(className, segment):
    paramNames = segment.paramNames
    capitalizedParamNames = []
    for i in range(len(segment.paramNames)):
        capitalizedParamNames.append(paramNames[i][0].upper() + paramNames[i][1:])
        if paramNames[i] == 'value':
            paramNames[i] = 'val';
        if paramNames[i] == 'checked':
            paramNames[i] = 'isChecked'

    unsafeCode = False
    generatedLines = []
    if segment.signalType == '':
        paramTypes, paramsC, delegateType, funcNameC = [], '', 'UnmanagedCallback', 'tguiWidget_signalConnect'
        addHelperImpl = ['value(sender, EventArgs.Empty);']
    elif segment.signalType == 'Int':
        paramTypes, paramsC, delegateType, funcNameC = ['int'], 'int val', 'UnmanagedCallbackInt', 'tguiWidget_signalIntConnect'
        addHelperImpl = ['value(sender, new ' + segment.objName + 'EventArgs(val));']
    elif segment.signalType == 'UInt':
        paramTypes, paramsC, delegateType, funcNameC = ['uint'], 'uint val', 'UnmanagedCallbackUInt', 'tguiWidget_signalUIntConnect'
        addHelperImpl = ['value(sender, new ' + segment.objName + 'EventArgs(val));']
    elif segment.signalType == 'SizeT':
        paramTypes, paramsC, delegateType, funcNameC = ['UIntPtr'], 'UIntPtr val', 'UnmanagedCallbackNUInt', 'tguiWidget_signalSizeTConnect'
        addHelperImpl = ['value(sender, new ' + segment.objName + 'EventArgs(val));']
    elif segment.signalType == 'Bool':
        paramTypes, paramsC, delegateType, funcNameC = ['bool'], 'byte val', 'UnmanagedCallbackBool', 'tguiWidget_signalBoolConnect'
        addHelperImpl = ['value(sender, new ' + segment.objName + 'EventArgs(val != 0));']
    elif segment.signalType == 'Float':
        paramTypes, paramsC, delegateType, funcNameC = ['float'], 'float val', 'UnmanagedCallbackFloat', 'tguiWidget_signalFloatConnect'
        addHelperImpl = ['value(sender, new ' + segment.objName + 'EventArgs(val));']
    elif segment.signalType == 'Color':
        paramTypes, paramsC, delegateType, funcNameC = ['Color?'], 'ColorCTGUI color', 'UnmanagedCallbackColor', 'tguiWidget_signalColorConnect'
        addHelperImpl = ['value(sender, new ' + segment.objName + 'EventArgs(Util.GetColorFromC(color)));']
    elif segment.signalType == 'String':
        paramTypes, paramsC, delegateType, funcNameC = ['string'], 'IntPtr str', 'UnmanagedCallbackString', 'tguiWidget_signalStringConnect'
        addHelperImpl = ['value(sender, new ' + segment.objName + 'EventArgs(Util.GetStringFromC_UTF32(str)));']
    elif segment.signalType == 'Vector2f':
        paramTypes, paramsC, delegateType, funcNameC = ['Vector2f'], 'Vector2f vec', 'UnmanagedCallbackVector2f', 'tguiWidget_signalVector2fConnect'
        addHelperImpl = ['value(sender, new ' + segment.objName + 'EventArgs(vec));']
    elif segment.signalType == 'FloatRect':
        paramTypes, paramsC, delegateType, funcNameC = ['FloatRect'], 'FloatRect rect', 'UnmanagedCallbackFloatRect', 'tguiWidget_signalFloatRectConnect'
        addHelperImpl = ['value(sender, new ' + segment.objName + 'EventArgs(rect));']
    elif segment.signalType == 'BoolPtr':
        paramTypes, paramsC, delegateType, funcNameC = ['bool'], 'byte* ptr', 'UnmanagedCallbackBoolPtr', 'tguiWidget_signalBoolPtrConnect'
        unsafeCode = True
        addHelperImpl = [
            'var e = new ' + segment.objName + 'EventArgs(*ptr != 0);',
            'value(sender, e);',
            '*ptr = e.' + capitalizedParamNames[0] + ' ? (byte)1 : (byte)0;'
        ]
    elif segment.signalType == 'Range':
        paramTypes, paramsC, delegateType, funcNameC = ['float', 'float'], 'float val1, float val2', 'UnmanagedCallbackRange', 'tguiWidget_signalRangeConnect'
        addHelperImpl = ['value(sender, new ' + segment.objName + 'EventArgs(val1, val2));']
    elif segment.signalType == 'TabSelectionChanging':
        paramTypes, paramsC, delegateType, funcNameC = ['int', 'bool'], 'int index, byte* vetoed', 'UnmanagedCallbackTabSelectionChanging', 'tguiWidget_signalTabSelectionChangingConnect'
        unsafeCode = True
        addHelperImpl = [
            'var e = new ' + segment.objName + 'EventArgs(index, *vetoed != 0);',
            'value(sender, e);',
            '*vetoed = e.' + capitalizedParamNames[1] + ' ? (byte)1 : (byte)0;'
        ]
    elif segment.signalType == 'ChildWindow':
        paramTypes, paramsC, delegateType, funcNameC = ['ChildWindow'], 'IntPtr windowCPointer', 'UnmanagedCallbackChildWindow', 'tguiWidget_signalChildWindowConnect'
        addHelperImpl = [
            'using var childWindow = new ChildWindow(windowCPointer);'
            'value(sender, new ' + segment.objName + 'EventArgs(childWindow));'
        ]
    elif segment.signalType == 'Item':
        paramTypes, paramsC, delegateType, funcNameC = ['int'], 'int index', 'UnmanagedCallbackItem', 'tguiWidget_signalItemConnect'
        addHelperImpl = ['value(sender, new ' + segment.objName + 'EventArgs(index));']
    elif segment.signalType == 'PanelListBoxItem':
        paramTypes, paramsC, delegateType, funcNameC = ['int'], 'int index', 'UnmanagedCallbackPanelListBoxItem', 'tguiWidget_signalPanelListBoxItemConnect'
        addHelperImpl = ['value(sender, new ' + segment.objName + 'EventArgs(index));']
    elif segment.signalType == 'FileDialogPaths':
        paramTypes, paramsC, delegateType, funcNameC = ['string[]'], 'UIntPtr count, IntPtr* strings', 'UnmanagedCallbackFileDialogPaths', 'tguiWidget_signalFileDialogPathsConnect'
        unsafeCode = True
        addHelperImpl = [
            'string[] stringArray = new string[(int)count];',
            'for (int i = 0; i < (int)count; ++i)',
            '    stringArray[i] = Util.GetStringFromC_UTF32(strings[i]);',
            'value(sender, new ' + segment.objName + 'EventArgs(stringArray));'
        ]
    elif segment.signalType == 'ShowEffect':
        paramTypes, paramsC, delegateType, funcNameC = ['ShowEffectType', 'bool'], 'ShowEffectType type, byte show', 'UnmanagedCallbackShowEffect', 'tguiWidget_signalShowEffectConnect'
        addHelperImpl = ['value(sender, new ' + segment.objName + 'EventArgs(type, show != 0));']
    elif segment.signalType == 'AnimationType':
        paramTypes, paramsC, delegateType, funcNameC = ['AnimationType'], 'ShowEffectType type', 'UnmanagedCallbackAnimationType', 'tguiWidget_signalAnimationTypeConnect'
        addHelperImpl = ['value(sender, new ' + segment.objName + 'EventArgs(type));']
    elif segment.signalType == 'ItemHierarchy':
        unsafeCode = True
        paramTypes, paramsC, delegateType, funcNameC = ['string[]'], 'UIntPtr count, IntPtr* strings', 'UnmanagedCallbackItemHierarchy', 'tguiWidget_signalItemHierarchyConnect'
        addHelperImpl = [
            'string[] stringArray = new string[(int)count];',
            'for (int i = 0; i < (int)count; ++i)',
            '    stringArray[i] = Util.GetStringFromC_UTF32(strings[i]);',
            'value(sender, new ' + segment.objName + 'EventArgs(stringArray));'
        ]
    else:
        raise RuntimeError('Signal with type ' + segment.signalType + ' is not supported')

    if segment.signalType == '':
        eventType = 'EventHandler'
    else:
        assert(len(paramTypes) == len(paramNames))
        eventType = 'EventHandler<' + segment.objName + 'EventArgs>'
        generatedLines.append('public class ' + segment.objName + 'EventArgs : EventArgs')
        generatedLines.append('{')
        generatedLines.append('    public ' + segment.objName + 'EventArgs(')
        for i in range(len(paramTypes)):
            if i > 0:
                generatedLines[-1] += ', '
            generatedLines[-1] += paramTypes[i] + ' ' + paramNames[i]
        generatedLines[-1] += ')'
        generatedLines.append('    {')
        for i in range(len(paramTypes)):
            generatedLines.append('        ' + capitalizedParamNames[i] + ' = ' + segment.paramNames[i] + ';')
        generatedLines.append('    }')
        for i in range(len(paramTypes)):
            if segment.signalType == 'BoolPtr' or (segment.signalType == 'TabSelectionChanging' and i == 1):
                getOrSet = 'get; set;'
            else:
                getOrSet = 'get;'
            generatedLines.append('    public ' + paramTypes[i] + ' ' + capitalizedParamNames[i] + ' { ' + getOrSet + ' }')
        generatedLines.append('}')

    generatedLines.extend([
        'public ' + ('unsafe ' if unsafeCode else '') + 'event ' + eventType + ' On' + segment.objName,
        '{',
        '    add',
        '    {',
    ])

    addImplLines = [
        'var selfCPointer = CPointer;',
        'var selfType = GetType();',
        delegateType + ' func = (' + paramsC + ') => {',
        '    using var sender = Util.GetWidgetFromC(tguiWidget_addPointerReference(selfCPointer), selfType);'
    ]
    for line in addHelperImpl:
        addImplLines.append('    ' + line)
    addImplLines.extend([
        '};',
        'uint id = ' + funcNameC + '(CPointer, Util.ConvertStringForC_UTF32("' + segment.descName + '"), func);',
        'ConnectEventHandler(id, "' + segment.descName + '", value, func);'
    ])
    for line in addImplLines:
        generatedLines.append('        ' + line)

    generatedLines.extend([
        '    }',
        '    remove',
        '    {',
        '        DisconnectEventHandler("' + segment.descName + '", value);',
        '    }',
        '}'
    ])
    return generatedLines


def parseCustomFileCSharp(customFileCSharp):
    customUsingLines = []
    customClassLines = []
    customImportLines = []

    parsingCustomImport = False
    if customFileCSharp:
        extraFile = open(customFileCSharp, 'r')
        for line in extraFile.readlines():
            strippedLine = line.rstrip()
            if line.startswith('using '):
                customUsingLines.append(strippedLine)
            elif parsingCustomImport:
                customImportLines.append(strippedLine)
                if line.startswith('#endregion'):
                    parsingCustomImport = False
            else:
                if line.startswith('#region'):
                    parsingCustomImport = True
                    customImportLines.append(strippedLine)
                else:
                    if strippedLine or (customClassLines and customClassLines[-1]): # Don't add repeated empty lines
                        customClassLines.append(strippedLine)

    return customUsingLines, customClassLines, customImportLines


def generateWidgetFileCSharp(srcFile, destFile, className, customFileCSharp):
    segments = parseDescriptionFile(srcFile)
    customUsingLines, customClassLines, customImportLines = parseCustomFileCSharp(customFileCSharp)

    parentClassName = 'Widget'
    for segment in segments:
        if isinstance(segment, SegmentInherits):
            if segment.parentName not in ['ScrollbarChildInterface', 'DualScrollbarChildInterface']:
                parentClassName = segment.parentName

    if className == 'Widget':
        classDeclaration = 'public class Widget : ObjectBase'
        assert(parentClassName == 'Widget')
    else:
        classDeclaration = 'public class ' + className + ' : ' + parentClassName

    enums = {}
    enumLines = []
    abstractClass = False
    for segment in segments:
        if isinstance(segment, SegmentAbstractClass):
            abstractClass = True
        elif isinstance(segment, SegmentEnum):
            enums[segment.name] = segment.values
            enumLines.append('    public enum ' + className + segment.name)
            enumLines.append('    {')
            for value in segment.values:
                enumLines.append('        ' + value + ',')
            enumLines.append('    }')
            enumLines.append('')

    usingDeclarations = set(['using System;', 'using System.Security;', 'using System.Runtime.InteropServices;'])
    if customUsingLines:
        usingDeclarations.update(set(customUsingLines))

    generatedLines = [
        'namespace TGUI',
        '{',
    ]

    if enumLines:
        generatedLines.extend(enumLines)

    generatedLines.extend([
        '    /// <summary>',
        '    /// ' + className + ' widget',
        '    /// </summary>',
        '    ' + classDeclaration,
        '    {'
    ])

    generatedClassLines = []
    if not abstractClass:
        generatedClassLines.extend([
            '/// <summary>',
            '/// Default constructor',
            '/// </summary>',
            'public ' + className + '()',
            '    : base(tgui' + className + '_create())',
            '{',
            '}',
            ''
        ])

    generatedClassLines.extend([
        '/// <summary>',
        '/// Constructor that creates the object from its C pointer',
        '/// </summary>',
        '/// <param name="cPointer">Pointer to object in C code</param>',
        'protected internal ' + className + '(IntPtr cPointer)',
        '    : base(cPointer)',
        '{',
        '}',
        '',
        '/// <summary>',
        '/// Copy constructor',
        '/// </summary>',
        '/// <param name="copy">Object to copy</param>',
        'public ' + className + '(' + className + ' copy)',
        '    : base(copy)',
        '{',
        '}',
        ''
    ])

    if customClassLines:
        generatedClassLines.extend(customClassLines)
        if generatedClassLines[-1]:
            generatedClassLines.append('')

    exportedSymbols = []
    try:
        exportedSymbols = [
            'IntPtr tgui' + className + '_create()',
        ]
        for segment in segments:
            if isinstance(segment, SegmentInherits):
                if segment.parentName == 'ScrollbarChildInterface':
                    exportedSymbols.append('IntPtr tguiScrollbarChildInterface_getScrollbar(IntPtr cPointer)')
                    generatedClassLines.extend([
                        'public ScrollbarAccessor Scrollbar => new ScrollbarAccessor(tguiScrollbarChildInterface_getScrollbar(CPointer));'
                    ])
                elif segment.parentName == 'DualScrollbarChildInterface':
                    exportedSymbols.append('IntPtr tguiDualScrollbarChildInterface_getVerticalScrollbar(IntPtr cPointer)')
                    exportedSymbols.append('IntPtr tguiDualScrollbarChildInterface_getHorizontalScrollbar(IntPtr cPointer)')
                    generatedClassLines.extend([
                        'public ScrollbarAccessor VerticalScrollbar => new ScrollbarAccessor(tguiDualScrollbarChildInterface_getVerticalScrollbar(CPointer));',
                        'public ScrollbarAccessor HorizontalScrollbar => new ScrollbarAccessor(tguiDualScrollbarChildInterface_getHorizontalScrollbar(CPointer));'
                    ])
                else:
                    continue # Already handled earlier
            elif isinstance(segment, SegmentPropertyWidgetRenderer):
                rendererType = segment.type
                rendererNamePrefix = segment.prefix
                newStr = ' ' if rendererNamePrefix else ' new '
                generatedClassLines.extend([
                    'public' + newStr + rendererType + ' ' + rendererNamePrefix + 'Renderer',
                    '{',
                    '    get => new ' + rendererType + '(tguiWidget_getRenderer(CPointer));',
                    '    set => SetRenderer(value.Data);',
                    '}',
                    '',
                    'public' + newStr + rendererType + ' ' + rendererNamePrefix + 'SharedRenderer => new ' + rendererType + '(tguiWidget_getSharedRenderer(CPointer));'
                ])
            elif isinstance(segment, SegmentProperty):
                generatedLinesForProperty, exportedSymbolsForProperty = generatePropertyCSharp(className, segment, enums)
                exportedSymbols.extend(exportedSymbolsForProperty)
                generatedClassLines.extend(generatedLinesForProperty)
                if segment.type.startswith('List<') or segment.type.startswith('Set<'):
                    usingDeclarations.add('using System.Collections.Generic;')
            elif isinstance(segment, SegmentFunction):
                generatedLinesForFunction, exportedSymbolsForFunction, usingDeclarationsForFunction = generateFunctionCSharp(className, segment, enums)
                exportedSymbols.extend(exportedSymbolsForFunction)
                generatedClassLines.extend(generatedLinesForFunction)
                usingDeclarations.update(usingDeclarationsForFunction)
            elif isinstance(segment, SegmentSignal):
                generatedClassLines.extend(generateSignalCSharp(className, segment))
            elif isinstance(segment, SegmentAbstractClass) or isinstance(segment, SegmentEnum):
                continue # Already handled earlier
            else:
                raise RuntimeError('Unsupported instruction in description file')

            # Add a newline behind each segment
            generatedClassLines.append('')
    except RuntimeError as e:
        raise RuntimeError('Error generating "' + destFile + '": ' + str(e))

    if customImportLines:
        generatedClassLines.extend(customImportLines)
        if generatedClassLines[-1]:
            generatedClassLines.append('')

    generatedClassLines.append('#region GeneratedImports')
    generatedClassLines.append('')
    for exportedSymbol in exportedSymbols:
        generatedClassLines.append('[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]')
        if '*' in exportedSymbol:
            generatedClassLines.append('private static extern unsafe ' + exportedSymbol + ';')
        else:
            generatedClassLines.append('private static extern ' + exportedSymbol + ';')
        generatedClassLines.append('')
    generatedClassLines.append('#endregion')

    for line in generatedClassLines:
        if line:
            generatedLines.append('        ' + line)
        else:
            generatedLines.append('')

    generatedLines.extend([
        '    }',
        '}'
    ])

    generatedLines = ['// This file is generated, it should not be edited directly.', ''] + list(sorted(usingDeclarations)) + [''] + generatedLines

    outFile = open(destFile, 'w')
    for line in generatedLines:
        if line:
            outFile.write(line + '\n')
        else:
            outFile.write('\n')


def generateRendererFileCSharp(srcFile, destFile, className, customFileCSharp):
    segments = parseDescriptionFile(srcFile)

    assert(className.endswith('Renderer'))
    widgetName = className[:-8]

    parentClassName = 'WidgetRenderer'
    for segment in segments:
        if isinstance(segment, SegmentInherits):
            parentClassName = segment.parentName

    if className == 'WidgetRenderer':
        classDeclaration = 'public class WidgetRenderer : ObjectBase'
        assert(parentClassName == 'WidgetRenderer')
    else:
        classDeclaration = 'public class ' + className + ' : ' + parentClassName

    usingDeclarations = set(['using System;', 'using System.Security;', 'using System.Runtime.InteropServices;'])

    generatedLines = [
        'namespace TGUI',
        '{',
        '    /// <summary>',
        '    /// Renderer for ' + widgetName + ' widgets',
        '    /// </summary>',
        '    ' + classDeclaration,
        '    {',
        '        /// <summary>',
        '        /// Default constructor',
        '        /// </summary>',
        '        public ' + className + '()',
        '            : base(tgui' + className + '_create())',
        '        {',
        '        }',
        '',
        '        /// <summary>',
        '        /// Constructor that creates the object from its C pointer',
        '        /// </summary>',
        '        /// <param name="cPointer">Pointer to object in C code</param>',
        '        protected internal ' + className + '(IntPtr cPointer)',
        '            : base(cPointer)',
        '        {',
        '        }',
        '',
        '        /// <summary>',
        '        /// Copy constructor',
        '        /// </summary>',
        '        /// <param name="copy">Renderer object to copy</param>',
        '        public ' + className + '(' + className + ' copy)',
        '            : base(tgui' + className + '_copy(copy.CPointer))',
        '        {',
        '        }',
        ''
    ]

    try:
        exportedSymbols = [
            'IntPtr tgui' + className + '_create()',
            'IntPtr tgui' + className + '_copy(IntPtr cPointer)',
        ]
        for segment in segments:
            if isinstance(segment, SegmentInherits):
                continue # Already handled earlier
            elif isinstance(segment, SegmentPropertyWidgetRenderer):
                raise RuntimeError('"property-widget-renderer" is not supported in a renderer')
            elif isinstance(segment, SegmentProperty):
                generatedLinesForProperty, exportedSymbolsForProperty = generatePropertyCSharp(className, segment, {})
                exportedSymbols.extend(exportedSymbolsForProperty)
                for line in generatedLinesForProperty:
                    if line:
                        generatedLines.append('        ' + line)
                    else:
                        generatedLines.append('')
            elif isinstance(segment, SegmentFunction):
                # Functions are allowed in the base class, but not in the derived classes which are only allowed to use properties
                if className == 'WidgetRenderer':
                    generatedLinesForFunction, exportedSymbolsForFunction, usingDeclarationsForFunction = generateFunctionCSharp(className, segment, {})
                    exportedSymbols.extend(exportedSymbolsForFunction)
                    usingDeclarations.update(usingDeclarationsForFunction)
                    for line in generatedLinesForFunction:
                        if line:
                            generatedLines.append('        ' + line)
                        else:
                            generatedLines.append('')
                else:
                    raise RuntimeError('function is not supported in a renderer')
            elif isinstance(segment, SegmentAbstractClass):
                raise RuntimeError('abstract-class is not supported in a renderer')
            elif isinstance(segment, SegmentEnum):
                raise RuntimeError('enum is not supported in a renderer')
            else:
                raise RuntimeError('Unsupported instruction in description file')

            # Add a newline behind each segment
            generatedLines.append('')
    except RuntimeError as e:
        raise RuntimeError('Error generating "' + destFile + '": ' + str(e))

    if customFileCSharp:
        extraFile = open(customFileCSharp, 'r')
        for line in extraFile.readlines():
            if line.rstrip():
                generatedLines.append('        ' + line.rstrip())
            else:
                generatedLines.append('')
        generatedLines.append('')

    generatedLines.append('        #region GeneratedImports')
    generatedLines.append('')
    for exportedSymbol in exportedSymbols:
        generatedLines.extend([
            '        [DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]',
            '        private static extern ' + exportedSymbol + ';',
            ''
        ])
    generatedLines.append('        #endregion')
    generatedLines.extend([
        '    }',
        '}'
    ])

    generatedLines = ['// This file is generated, it should not be edited directly.', ''] + list(sorted(usingDeclarations)) + [''] + generatedLines

    outFile = open(destFile, 'w')
    for line in generatedLines:
        if line:
            outFile.write(line + '\n')
        else:
            outFile.write('\n')


def generateOtherFileCSharp(srcFile, destFile, className, customFileCSharp):
    segments = parseDescriptionFile(srcFile)
    customUsingLines, customClassLines, customImportLines = parseCustomFileCSharp(customFileCSharp)

    enums = {}
    enumLines = []
    parentClassName = 'ObjectBase'
    for segment in segments:
        if isinstance(segment, SegmentInherits):
            parentClassName = segment.parentName
        elif isinstance(segment, SegmentEnum):
            enums[segment.name] = segment.values
            enumLines.append('    public enum ' + className + segment.name)
            enumLines.append('    {')
            for value in segment.values:
                enumLines.append('        ' + value + ',')
            enumLines.append('    }')
            enumLines.append('')

    usingDeclarations = set(['using System;', 'using System.Security;', 'using System.Runtime.InteropServices;'])
    if customUsingLines:
        usingDeclarations.update(set(customUsingLines))

    generatedLines = [
        'namespace TGUI',
        '{',
    ]

    if enumLines:
        generatedLines.extend(enumLines)

    inheritsStr = ' : ' + parentClassName
    if className == 'ToolTip':
        inheritsStr = ''

    generatedLines.extend([
        '    public class ' + className + inheritsStr,
        '    {'
    ])

    generatedClassLines = []
    if className != 'Gui' and className != 'ToolTip':
        generatedClassLines.extend([
            '/// <summary>',
            '/// Constructor that creates the object from its C pointer',
            '/// </summary>',
            '/// <param name="cPointer">Pointer to object in C code</param>',
            'protected internal ' + className + '(IntPtr cPointer)',
            '    : base(cPointer)',
            '{',
            '}',
            ''
        ])

    if customClassLines:
        generatedClassLines.extend(customClassLines)
        if generatedClassLines[-1]:
            generatedClassLines.append('')

    exportedSymbols = []
    try:
        for segment in segments:
            if isinstance(segment, SegmentAbstractClass):
                raise RuntimeError('abstract-class is not supported here')
            if isinstance(segment, SegmentInherits):
                continue # Already handled earlier
            elif isinstance(segment, SegmentPropertyWidgetRenderer):
                raise RuntimeError('property-widget-renderer is not supported here')
            elif isinstance(segment, SegmentProperty):
                generatedLinesForProperty, exportedSymbolsForProperty = generatePropertyCSharp(className, segment, enums)
                exportedSymbols.extend(exportedSymbolsForProperty)
                generatedClassLines.extend(generatedLinesForProperty)
                if segment.type.startswith('List<') or segment.type.startswith('Set<'):
                    usingDeclarations.add('using System.Collections.Generic;')
            elif isinstance(segment, SegmentFunction):
                generatedLinesForFunction, exportedSymbolsForFunction, usingDeclarationsForFunction = generateFunctionCSharp(className, segment, enums)
                exportedSymbols.extend(exportedSymbolsForFunction)
                generatedClassLines.extend(generatedLinesForFunction)
                usingDeclarations.update(usingDeclarationsForFunction)
            elif isinstance(segment, SegmentSignal):
                if className != 'Widget':
                    raise RuntimeError('Signals are only implemented for widgets')
                generatedClassLines.extend(generateSignalCSharp(className, segment))
            elif isinstance(segment, SegmentEnum):
                continue # Already handled earlier
            else:
                raise RuntimeError('Unsupported instruction in description file')

            # Add a newline behind each segment
            generatedClassLines.append('')
    except RuntimeError as e:
        raise RuntimeError('Error generating "' + destFile + '": ' + str(e))

    if customImportLines:
        generatedClassLines.extend(customImportLines)
        if generatedClassLines[-1]:
            generatedClassLines.append('')

    generatedClassLines.append('#region GeneratedImports')
    generatedClassLines.append('')
    for exportedSymbol in exportedSymbols:
        generatedClassLines.append('[DllImport(Util.LibName, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]')
        if 'IntPtr*' in exportedSymbol:
            generatedClassLines.append('unsafe private static extern ' + exportedSymbol + ';')
        else:
            generatedClassLines.append('private static extern ' + exportedSymbol + ';')
        generatedClassLines.append('')
    generatedClassLines.append('#endregion')

    for line in generatedClassLines:
        if line:
            generatedLines.append('        ' + line)
        else:
            generatedLines.append('')

    generatedLines.extend([
        '    }',
        '}'
    ])

    generatedLines = ['// This file is generated, it should not be edited directly.', ''] + list(sorted(usingDeclarations)) + [''] + generatedLines

    outFile = open(destFile, 'w')
    for line in generatedLines:
        if line:
            outFile.write(line + '\n')
        else:
            outFile.write('\n')


def main():
    for filename in os.listdir(os.path.join(CTGUI_TEMPLATES_DIR, 'Renderers')):
        if filename.endswith('.desc'):
            className = filename[:-5]
            inputDescFile = os.path.join(CTGUI_TEMPLATES_DIR, 'Renderers', filename)
            outputFile = os.path.join('..', 'src', 'Renderers', className + '.cs')
            hasCustomFileC = os.path.isfile(os.path.join(CTGUI_TEMPLATES_DIR, 'Renderers', className + '.extra.cpp')) or os.path.isfile(os.path.join(CTGUI_TEMPLATES_DIR, 'Renderers', className + '.extra.h'))
            customFileCSharp = os.path.join('templates', 'Renderers', className + '.cs')
            hasCustomFileCSharp = os.path.isfile(customFileCSharp)
            assert(hasCustomFileC == hasCustomFileCSharp)
            if not hasCustomFileCSharp:
                customFileCSharp = None
            generateRendererFileCSharp(inputDescFile, outputFile, className, customFileCSharp)

    for filename in os.listdir(os.path.join(CTGUI_TEMPLATES_DIR, 'Widgets')):
        if filename.endswith('.desc'):
            className = filename[:-5]
            inputDescFile = os.path.join(CTGUI_TEMPLATES_DIR, 'Widgets', filename)
            outputFile = os.path.join('..', 'src', 'Widgets', className + '.cs')
            hasCustomFileC = os.path.isfile(os.path.join(CTGUI_TEMPLATES_DIR, 'Widgets', className + '.extra.cpp')) or os.path.isfile(os.path.join(CTGUI_TEMPLATES_DIR, 'Widgets', className + '.extra.h'))
            customFileCSharp = os.path.join('templates', 'Widgets', className + '.cs')
            hasCustomFileCSharp = os.path.isfile(customFileCSharp)
            assert(hasCustomFileC == hasCustomFileCSharp)
            if not hasCustomFileCSharp:
                customFileCSharp = None
            generateWidgetFileCSharp(inputDescFile, outputFile, className, customFileCSharp)

    for filename in os.listdir(os.path.join(CTGUI_TEMPLATES_DIR)):
        if filename.endswith('.desc'):
            className = filename[:-5]
            inputDescFile = os.path.join(CTGUI_TEMPLATES_DIR, filename)
            outputFile = os.path.join('..', 'src', className + '.cs')
            hasCustomFileC = os.path.isfile(os.path.join(CTGUI_TEMPLATES_DIR, className + '.cpp')) or os.path.isfile(os.path.join(CTGUI_TEMPLATES_DIR, className + '.h'))
            customFileCSharp = os.path.join('templates', className + '.cs')
            hasCustomFileCSharp = os.path.isfile(customFileCSharp)
            assert(hasCustomFileC == hasCustomFileCSharp)
            if not hasCustomFileCSharp:
                customFileCSharp = None
            generateOtherFileCSharp(inputDescFile, outputFile, className, customFileCSharp)

if __name__ == "__main__":
    main()
