#!/bin/bash

# Publish for Windows
electronize build /target win


# Publish for Linux
electronize build /target linux


# Publish for OSX
electronize build /target osx
