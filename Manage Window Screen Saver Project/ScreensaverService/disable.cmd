echo OFF
set bgfolder=%windir%\system32\oobe\Info\backgrounds\

REM Removes all authenticated users the right to write these files
FOR %%f IN (@files@) DO icacls %bgfolder%%%f /remove *S-1-5-11
