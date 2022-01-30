#!/bin/sh

dotnet publish -c release -r osx-x64 --self-contained false /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true
cp bin/release/net5.0/osx-x64/publish/mdvcat ~/bin