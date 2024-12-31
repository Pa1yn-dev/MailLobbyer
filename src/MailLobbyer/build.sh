#!/bin/bash
# Publish for Linux
dotnet publish --os linux --output ./builds
zip -r publish_linux.zip ./builds