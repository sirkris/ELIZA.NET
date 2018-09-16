@ECHO OFF

REM Run this batch file from the command-line in the dist directory to run the tests against the bundled DOCTOR script.

RunTests.exe ..\scripts\DOCTOR\tests\doctorTest.json
