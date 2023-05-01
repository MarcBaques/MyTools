:: https://gist.github.com/nibro7778/0f41c9335af34f1cc08f5091ce4066de
:: Command to define alias: git config alias.(customWord) '!C:/git-scripts/customDeploy.bat'
:: Usage: customWord (list with spaces: switch ps4 ps5 steam xboxOne xboxSeries gog epic ws)
:: To fix:
:: -What happends with conflicts
:: -Define from branch
:: -Separate to 2 batch, deploy and multiple deployment

@echo off
setlocal enabledelayedexpansion

set DEPLOY_FROM=master
set PS4_BRANCH=platform/ps4
set PS5_BRANCH=platform/ps5
set SWITCH_BRANCH=platform/switch
set STEAM_BRANCH=platform/steam
set XBOXONE_BRANCH=platform/xbox_one
set XBOXSERIES_BRANCH=platform/xbox_series
set WINDOWS_STORE_BRANCH=platform/windows_store
set EPIC_BRANCH=platform/epic
set GOG_BRANCH=platform/gog

set PS4_KEY=ps4
set PS5_KEY=ps5
set SWITCH_KEY=switch
set STEAM_KEY=steam
set XBOX_ONE_KEY=xboxOne
set XBOX_SERIES_KEY=xboxSeries
set EPIC_KEY=epic
set GOG_KEY=gog
set WINDOWS_STORE_KEY=ws

::Fill array with arguments
set argCount=0
for %%x in (%*) do (
   set /A argCount+=1
   set "argVec[!argCount!]=%%~x"
)

::Check if command is help
if %argVec[1]%==help (goto HELP)
if %argVec[1]%==h (goto HELP)

::Safe current work
echo -------------------------------------------------
echo Stash with to "Pre deploy changes"
git stash push -m "Pre deploy changes %RANDOM%" -u 

::Check all remotes
echo -------------------------------------------------
echo Fetch to have last remotes
git fetch --all

echo.
echo -------------------------------------------------
echo Deploy from master to %argCount% platforms (%*)
echo -------------------------------------------------
echo.

for /L %%i in (1,1,%argCount%) do (
    CALL :deploy_to !argVec[%%i]!
)

echo -------------------------------------------------
echo Terminating script
echo -------------------------------------------------
EXIT /B 0

:: ////////////////FUNCTION_DEPLOY_TO///////////////////////////////////////////
:deploy_to
echo -------------------------------------------------
echo Deploy to %~1
echo -------------------------------------------------
set PLATFORM_BRANCH=default
if %~1==%SWITCH_KEY% (set PLATFORM_BRANCH=%SWITCH_BRANCH%)
if %~1==%STEAM_KEY% (set PLATFORM_BRANCH=%STEAM_BRANCH%)
if %~1==%PS4_KEY% (set PLATFORM_BRANCH=%PS4_BRANCH%)
if %~1==%PS5_KEY% (set PLATFORM_BRANCH=%PS5_BRANCH%)
if %~1==%XBOX_ONE_KEY% (set PLATFORM_BRANCH=%XBOXONE_BRANCH%)
if %~1==%XBOX_SERIES_KEY% (set PLATFORM_BRANCH=%XBOXSERIES_BRANCH%)
if %~1==%EPIC_KEY% (set PLATFORM_BRANCH=%EPIC_BRANCH%)
if %~1==%GOG_KEY% (set PLATFORM_BRANCH=%GOG_BRANCH%)
if %~1==%WINDOWS_STORE_KEY% (set PLATFORM_BRANCH=%WINDOWS_STORE_BRANCH%)
if %PLATFORM_BRANCH%==default  (echo Platform %~1 not supported & echo. & EXIT /B %ERRORLEVEL%)

git checkout %PLATFORM_BRANCH%
git merge --no-ff master
git push origin %PLATFORM_BRANCH%

echo.

EXIT /B %ERRORLEVEL%
:: ////////////////FUNCTION_DEPLOY_TO///////////////////////////////////////////

:: ////////////////FUNCTION_HELP///////////////////////////////////////////
:HELP
echo.
echo Usage: git deployc ^<platforms^>
echo Example: git deployc switch ps4 ps5
echo Suported platforms are:
echo   %SWITCH_KEY% - %SWITCH_BRANCH%
echo   %STEAM_KEY% - %STEAM_BRANCH%
echo   %PS4_KEY% - %PS4_BRANCH%
echo   %PS5_KEY% - %PS5_BRANCH%
echo   %XBOX_ONE_KEY% - %XBOXONE_BRANCH%
echo   %XBOX_SERIES_KEY% - %XBOXSERIES_BRANCH%
echo   %EPIC_KEY% - %EPIC_BRANCH%
echo   %GOG_KEY% - %GOG_BRANCH%
echo   %WINDOWS_STORE_KEY% - %WINDOWS_STORE_BRANCH%

EXIT /B 0 
:: ////////////////FUNCTION_HELP///////////////////////////////////////////