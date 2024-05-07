@echo off

:parse
IF "%~1"=="" GOTO endparse
IF "%~1"=="-h" echo This help
IF "%~1"=="-b" echo Is B
echo "%~1"
SHIFT
GOTO parse
:endparse
REM ready for action!

FOR %%A IN (%*) DO (
      echo %%A
)

pause