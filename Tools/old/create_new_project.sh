#!/bin/bash

# Colors
# ----------------------------------
NOCOLOR='\033[0m'
RED='\033[0;31m'
GREEN='\033[0;32m'


successCounter=0
function checkStep() {
    if [ $? == 0 ]; then
        echo -e "${GREEN} $1 OK ${NOCOLOR}"
        ((successCounter=successCounter+1))
    else
        echo -e "${RED}ERROR EN EL PASO $1 ${NOCOLOR}"
	return -1
	exit 0    
    fi
}

function renameDirectory() {
    for d in $1 ; do
        if [[ -d "$d" ]]
            then
                rename -n $d caraculo.txt
                echo "$d"
                renameDirectory $d*/
        fi
    done
}

if [ "$#" -ne 1 ]; then
    echo "expected 1 argument"
    echo "example: $ ./create_new_project project-name"
    read -p "pulse cualquier tecla para cerrar"
    exit
fi

defaultProjectName=$1

rm -rf FirstDays/FirstDays/FirstDays/bin
rm -rf FirstDays/FirstDays/FirstDays/obj
checkStep "Clean base project"

rm -rf FirstDays/FirstDays/FirstDays.Android/bin
rm -rf FirstDays/FirstDays/FirstDays.Android/obj
checkStep "Clean base Android project"

rm -rf FirstDays/FirstDays/FirstDays.iOS/bin
rm -rf FirstDays/FirstDays/FirstDays.iOS/obj
checkStep "Clean base IOS project"

rm -rf FirstDays/FirstDays/FirstDays.UWP/bin
rm -rf FirstDays/FirstDays/FirstDays.UWP/obj
checkStep "Clean base UWP project"

rm -rf newProject
mkdir -p newProject
cp -r FirstDays newProject
checkStep "Create new project"

# rename project folder

# find and replace inside files
#grep -rlZ 'FirstDays' newProject | xargs -0 sed -i  "s/FirstDays/$defaultProjectName/g"
checkStep "Rename new project"

#renameDirectory newProject
checkStep "Rename directory"

if [[ $successCounter == 7 ]]; then
    echo "ok"
else 
    echo "error"
fi
read -p "pulse cualquier tecla para cerrar"
