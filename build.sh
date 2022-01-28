#!/bin/sh

dotnet publish -c release -r osx-x64 /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true /p:PublishTrimmed=true
cp bin/release/net5.0/osx-x64/publish/mdvcat ~/bin