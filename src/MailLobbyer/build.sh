#!/bin/bash

# Publish for Windows
dotnet publish -c Release -r win-x64 --self-contained true -o ./builds/ML-win-x64

# Zip the Windows version
zip -r ./builds/ML-win-x64.zip ./builds/ML-win-x64

# Publish for Linux
dotnet publish -c Release -r linux-x64 --self-contained true -o ./builds/ML-linux-x64

# Zip the Linux version
zip -r ./builds/ML-linux-x64.zip ./builds/ML-linux-x64

# Publish for OSX
dotnet publish -c Release -r osx-x64 --self-contained true -o ./builds/ML-osx-x64

# Zip the OSX version
zip -r ./builds/ML-osx-x64.zip ./builds/ML-osx-x64
