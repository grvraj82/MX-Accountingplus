echo off

E:
cd E:\Projects\MXAccountingPlusCode\MX-AccountingPlus
set CVSROOT=:sspi::drajshekhar@172.29.240.66:2401:/solutions
rem cvs update
rem cvs update -P -d -C
REM cvs commit
cvs checkout PrintRover/Build.version
cvs commit -m "version updated" -f PrintRover/Build.version
pause