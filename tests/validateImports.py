#!/usr/bin/env python3

import re
import os
import sys

def validateTypes(typeCS, typeC, returnType):
    if (typeCS == typeC) \
    or (typeCS == 'bool' and typeC == 'sfBool') \
    or (typeCS == 'uint' and typeC == 'unsigned int') \
    or (typeCS == 'uint' and typeC == 'size_t') \
    or (typeCS == 'Color' and typeC == 'sfColor') \
    or (typeCS == 'Time' and typeC == 'sfTime') \
    or (typeCS == 'Event' and typeC == 'sfEvent') \
    or (typeCS == 'Vector2f' and typeC == 'sfVector2f') \
    or (typeCS == 'PrimitiveType' and typeC == 'sfPrimitiveType') \
    or (typeCS == 'Text.Styles' and typeC == 'sfUint32') \
    or (typeCS == 'IntPtr' and typeC == 'sfTexture*') \
    or (typeCS == 'IntPtr' and typeC == 'sfRenderWindow*') \
    or (typeCS == 'IntPtr' and typeC == 'sfFont*') \
    or (typeCS == 'IntPtr' and typeC == 'sfShape*') \
    or (typeCS == 'IntPtr' and typeC == 'sfSprite*') \
    or (typeCS == 'IntPtr' and typeC == 'sfView*') \
    or (typeCS == 'IntPtr' and typeC == 'sfText*') \
    or (typeCS == 'IntPtr' and typeC == 'sfVertexArray*') \
    or (typeCS == 'ShowAnimationType' and typeC == 'tguiShowAnimationType') \
    or (typeCS == 'HorizontalAlignment' and typeC == 'tguiHorizontalAlignment') \
    or (typeCS == 'Alignment' and typeC == 'tguiAlignment') \
    or (typeCS == 'VerticalAlignment' and typeC == 'tguiVerticalAlignment') \
    or (typeCS == 'Direction' and typeC == 'tguiFillDirection') \
    or (typeCS == 'Direction' and typeC == 'tguiExpandDirection') \
    or (typeCS == 'ScrollbarPolicy' and typeC == 'tguiScrollbarPolicy') \
    or (typeCS == 'TitleButton' and typeC == 'unsigned int') \
    or (typeCS == 'IntPtr' and typeC == 'tguiRenderer*') \
    or (typeCS == 'IntPtr' and typeC == 'tguiRendererData*') \
    or (typeCS == 'IntPtr' and typeC == 'tguiWidget*') \
    or (typeCS == 'IntPtr' and typeC == 'tguiOutline*') \
    or (typeCS == 'IntPtr' and typeC == 'tguiLayout*') \
    or (typeCS == 'IntPtr' and typeC == 'tguiTheme*') \
    or (typeCS == 'IntPtr' and typeC == 'tguiGui*') \
    or (typeCS == 'IntPtr' and typeC == 'tguiLayout2d*') \
    or (typeCS == 'IntPtr' and typeC == 'tguiCustomWidgetForBindings*') \
    or (typeCS == 'IntPtr' and typeC == 'const char*') \
    or (typeCS == 'IntPtr' and typeC == 'const sfUint32*') \
    or (typeCS == 'IntPtr*' and typeC == 'const char**') \
    or (typeCS == 'IntPtr*' and typeC == 'const sfUint32**') \
    or (typeCS == 'IntPtr*' and typeC == 'tguiWidget**') \
    or (typeCS == 'Vertex*' and typeC == 'const sfVertex*') \
    or (typeCS == 'IntPtr[]' and typeC == 'const sfUint32**') \
    or (typeCS == 'out uint' and typeC == 'size_t*') \
    or (typeCS == 'out IntPtr' and typeC == 'const sfUint32**') \
    or (typeCS == 'out IntPtr' and typeC == 'const char**') \
    or (typeCS == 'out IntPtr' and typeC == 'tguiWidget**') \
    or (typeCS == 'ref RenderStatesMarshalData' and typeC == 'const sfRenderStates*') \
    or (typeCS == 'CallbackAction' and typeC == 'void (*function)()') \
    or (typeCS == 'CallbackActionVector2f' and typeC == 'void (*function)(sfVector2f)') \
    or (typeCS == 'CallbackActionString' and typeC == 'void (*function)(const sfUint32*)') \
    or (typeCS == 'CallbackActionInt' and typeC == 'void (*function)(sfBool)') \
    or (typeCS == 'CallbackActionInt' and typeC == 'void (*function)(int)') \
    or (typeCS == 'CallbackActionFloat' and typeC == 'void (*function)(float)') \
    or (typeCS == 'CallbackActionUInt' and typeC == 'void (*function)(unsigned int)') \
    or (typeCS == 'CallbackActionRange' and typeC == 'void (*function)(float, float)') \
    or (typeCS == 'CallbackActionItemSelected' and typeC == 'void (*function)(const sfUint32*, const sfUint32*)') \
    or (typeCS == 'CallbackActionAnimation' and typeC == 'void (*function)(tguiShowAnimationType, sfBool)') \
    or (typeCS == 'CallbackCustomWidgetVector2f' and typeC == 'void (*function)(sfVector2f)') \
    or (typeCS == 'CallbackCustomWidgetBool' and typeC == 'void (*function)(sfBool)') \
    or (typeCS == 'CallbackCustomWidgetVoid' and typeC == 'void (*function)(void)') \
    or (typeCS == 'CallbackCustomWidgetGetVector2f' and typeC == 'sfVector2f (*function)(void)') \
    or (typeCS == 'CallbackCustomWidgetGetBool' and typeC == 'sfBool (*function)(void)') \
    or (typeCS == 'CallbackCustomWidgetUpdate' and typeC == 'void (*function)(sfTime)') \
    or (typeCS == 'CallbackCustomWidgetMouseOnWidget' and typeC == 'sfBool (*function)(sfVector2f)') \
    or (typeCS == 'CallbackCustomWidgetKeyPressed' and typeC == 'void (*function)(sfKeyEvent)') \
    or (typeCS == 'CallbackCustomWidgetTextEntered' and typeC == 'void (*function)(sfUint32)') \
    or (typeCS == 'CallbackCustomWidgetMouseWheelScrolled' and typeC == 'sfBool (*function)(float, sfVector2f)') \
    or (typeCS == 'CallbackCustomWidgetRenderer' and typeC == 'sfBool (*function)(const char*)') \
    or (typeCS == 'CallbackCustomWidgetDraw' and typeC == 'void (*function)(sfRenderStates)'):
        return True
    else:
        # Try removing 'const' in the C type
        if len(typeC) > 6 and typeC[:6] == 'const ':
            return validateTypes(typeCS, typeC[6:], False)
        else:
            return False

if os.name == 'nt':
    ctguiLibDir = '..\\extlibs\\lib\\'
    ctguiIncludeDir = '../extlibs/CTGUI/include'
else:
    import getpass
    if getpass.getuser() == 'texus':
        ctguiLibDir = '../../CTGUI/build/src/CTGUI/'
        ctguiIncludeDir = '../../CTGUI/include'
    else:
        ctguiLibDir = '../extlibs/lib/'
        ctguiIncludeDir = '../extlibs/CTGUI/include'

# Extract function names from dll
if os.name == 'nt':
    os.system('dumpbin /exports ' + ctguiLibDir + 'ctgui-0.8.dll > tmp')
else:
    os.system('nm ' + ctguiLibDir + 'libctgui.so | grep "[a-z0-9]* T .*" | sed "s/[a-z0-9]* T \(.*\)/\\1/g" > tmp')

with open('tmp') as f:
    dump = f.read()

os.remove('tmp')

exportedFunctions = re.findall('tgui.*', dump)

# Extract function signatures from C# source code
importedFunctions = []
for root, subFolders, files in os.walk('../src'):
    for f in files:
        if len(f) > 3 and f[-3:] == '.cs':
            with open(os.path.join(root, f), 'r') as fin:
                dump = fin.read()

            importLines = re.findall('static extern.*', dump)
            for line in importLines:
                match = re.match('static extern (?:protected )?(?:private )?(?:internal )?(.*) (tgui\S*)\s*\((.*)\);', line)
                returnType = match.group(1)
                name = match.group(2)
                rawParams = match.group(3).split(',')

                params = []
                for param in rawParams:
                    param = param.strip()
                    if param == '':
                        param = 'void'
                    elif param != 'void':
                        # Remove the parameter name
                        param = param[:param.rfind(' ')]

                    # Remove the 'MarshalAs' part of callback function parameters
                    m = re.match('\[MarshalAs\(UnmanagedType.FunctionPtr\)\] (.*)', param);
                    if m:
                        param = m.group(1)

                    params += [param]

                importedFunctions += [(returnType, name, params)]

# Extract function signatures from C headers
declaredFunctions = []
for root, subFolders, files in os.walk(ctguiIncludeDir):
    for f in files:
        if len(f) > 3 and f[-2:] == '.h':
            with open(os.path.join(root, f), 'r') as fin:
                dump = fin.read()
            declarations = re.findall('CTGUI_API.*;', dump)
            for line in declarations:
                match = re.match('CTGUI_API (.*) (tgui\S*)\s*\((.*)\);', line)
                returnType = match.group(1)
                name = match.group(2)
                rawParams = match.group(3).split(',')

                # Remove the parameter name, unless the parameter is a function pointer
                params = []
                matchingFunction = False
                for param in rawParams:
                    param = param.strip()

                    # Function parameters may have a comma in their signature which incorrectly got detected as a different parameter
                    if matchingFunction:
                        if param[-1] == ')':
                            matchingFunction = False
                            params[-1] = params[-1] + ', ' + param
                            continue
                    if re.match('.*\(*function\).*', param):
                        if param[-1] != ')':
                            matchingFunction = True

                    if param == '':
                        param = 'void'
                    elif param != 'void' and not re.match('.*\(*function\).*', param):
                        param = param[:param.rfind(' ')]
                    params += [param]

                declaredFunctions += [(returnType, name, params)]

# Check if all declared functions in CTGUI are exported (which is not the case when the function definition is missing)
undefinedFunctions = []
for func in declaredFunctions:
    name = func[1]
    if name not in exportedFunctions:
        undefinedFunctions += [name]

# Check if all function imported in C# are exported by the CTGUI dll
unexportedFunctions = []
for func in importedFunctions:
    name = func[1]
    if name not in exportedFunctions:
        unexportedFunctions += [name]

# Verify that function signatures from C# and C code match
incompatibleFunctions = []
for funcCS in importedFunctions:
    returnTypeCS = funcCS[0]
    nameCS = funcCS[1]
    paramsCS = funcCS[2]

    for funcC in declaredFunctions:
        if funcC[1] == nameCS:
            returnTypeC = funcC[0]
            nameC = funcC[1]
            paramsC = funcC[2]
            break

    # Compare return types
    if not validateTypes(returnTypeCS, returnTypeC, True):
        incompatibleFunctions += [nameCS]
        continue

    if len(paramsCS) != len(paramsC):
        incompatibleFunctions += [nameCS]
        continue

    # Compare parameters
    for i in range(len(paramsCS)):
        paramCS = paramsCS[i]
        paramC = paramsC[i]
        if not validateTypes(paramCS, paramC, False):
            incompatibleFunctions += [nameCS]

# Provide an overview of all encountered errors
if len(undefinedFunctions) > 0 or len(unexportedFunctions) > 0 or len(incompatibleFunctions) > 0:
    if len(undefinedFunctions) > 0:
        print('ERROR: Encountered undefined functions in CTGUI!')
        for name in undefinedFunctions:
            print('- ' + name)

    if len(unexportedFunctions) > 0:
        print('ERROR: Not all imported functions are exported by CTGUI!')
        for name in unexportedFunctions:
            print('- ' + name)

    if len(incompatibleFunctions) > 0:
        print('ERROR: Incompatible function signatures detected!')
        for name in incompatibleFunctions:
            print('- ' + name)

    sys.exit(1)
