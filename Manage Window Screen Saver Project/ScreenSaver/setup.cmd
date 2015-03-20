echo OFF
set bgfolder=%windir%\system32\oobe\Info\backgrounds\

REM Creates the backgrounds folder
md %bgfolder%

REM Creates the dummy background files
FOR %%f IN (@files@) DO echo 2> %bgfolder%%%f 1> NUL

REM Gives all authenticated users the right to write these files
FOR %%f IN (@files@) DO icacls %bgfolder%%%f /grant *S-1-5-11:(R,W,M)

REM Forces the use of the custom background permanently
reg add HKLM\Software\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background /v OEMBackground /t REG_DWORD /d 1 /f
reg add HKLM\SOFTWARE\Policies\Microsoft\Windows\System /v UseOEMBackground /t REG_DWORD /d 1 /f
