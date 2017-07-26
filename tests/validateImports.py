import re
import os
import sys

os.system('dumpbin /exports ..\extlibs\lib\ctgui-0.8.dll > tmp')
with open('tmp') as f:
    dump = f.read()

availableFunctions = re.findall('tgui.*', dump)
importedFunctions = []

for root, subFolders, files in os.walk('..\src'):
    for f in files:
        if len(f) > 3 and f[-3:] == '.cs':
            with open(os.path.join(root, f), 'r') as fin:
                dump = fin.read()

            importLines = re.findall('static extern.*', dump)
            for line in importLines:
                functions = re.findall('tgui[^(\n]*\(', line)
                functions = map(lambda func: func[:-1].strip(), functions)
                importedFunctions += functions

error = False
for func in importedFunctions:
    if func not in availableFunctions:
        if not error:
            print('ERROR: Not all imported functions are exported by CTGUI!')

        error = True
        print('- ' + func)

if error:
    sys.exit(1)
