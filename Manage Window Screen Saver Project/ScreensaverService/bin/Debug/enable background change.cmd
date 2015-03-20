echo OFF
set bgfolder=%windir%\system32\oobe\Info\backgrounds\

REM Creates the backgrounds folder
md %bgfolder%

REM Creates the dummy background files
FOR %%f IN (backgroundDefault.jpg background1280x960.jpg background1024x768.jpg background1600x1200.jpg background1440x900.jpg background1920x1200.jpg background1280x768.jpg background1360x768.jpg background1024x1280.jpg background960x1280.jpg background900x1440.jpg background768x1280.jpg background768x1360.jpg) DO echo 2> %bgfolder%%%f 1> NUL

REM Gives all authenticated users the right to write these files
FOR %%f IN (backgroundDefault.jpg background1280x960.jpg background1024x768.jpg background1600x1200.jpg background1440x900.jpg background1920x1200.jpg background1280x768.jpg background1360x768.jpg background1024x1280.jpg background960x1280.jpg background900x1440.jpg background768x1280.jpg background768x1360.jpg) DO icacls %bgfolder%%%f /grant *S-1-5-11:(R,W,M)

REM Forces the use of the custom background permanently
reg add HKLM\Software\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background /v OEMBackground /t REG_DWORD /d 1 /f
reg add HKLM\SOFTWARE\Policies\Microsoft\Windows\System /v UseOEMBackground /t REG_DWORD /d 1 /f
