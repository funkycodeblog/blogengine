
echo off

set dest=..\FunkyCode.Blog.WebApi\wwwroot\

echo (1/3) Removing content of %dest% (except \Playground) ...
del /q %dest%*
for /d %%i in (%dest%*) do if /i not "%%~nxi"=="Playground" rd /s /q "%%i"

echo (2/3) Building production backage ...
call npm run build

echo (3/3) Copy package to %dest%
xcopy .\build %dest%  /s /e /h

echo Finished!