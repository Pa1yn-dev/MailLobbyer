#!/bin/bash
# Publish for Linux
dotnet publish --os linux --output ./builds/linux
zip -r ML-Linux-v1.2.4-Server.zip ./builds/linux