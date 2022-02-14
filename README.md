# mdvcat

Markdown viewer from command line using photino.

## Screenshot

![screenshot.png](https://github.com/karino2/mdvcat/raw/main/screenshot/screenshot.png)

## Install

For Mac, there is binary drop in [Releases](https://github.com/karino2/mdvcat/releases)

Also, there is homebrew tap for mdvcat.
You can install follwoing way.

```
$ brew tap karino2/tap
$ brew install karino2/tap/mdvcat
```

### Troubleshooting

If you see the error saying

```
A fatal error occurred. The required library libhostfxr.dylib could not be found.
```

You need to set DOTNET_ROOT like following (in .zshrc or .bashrc, etc.)

```
$ export DOTNET_ROOT="$(brew --prefix)/opt/dotnet/libexec"
```


## Usage

```
USAGE: mdvcat [--help] [--disablehtml] <path>

PATH:

    <path>                Markdown path.

OPTIONS:

    --disablehtml, -d     Disable HTML parsing (for unknown source md).
    --help                display this list of options.```
```

## Open source libraries

mdvcat uses following libraries.

- [Markdig](https://github.com/xoofx/markdig)
- [Photino.NET](https://www.nuget.org/packages/Photino.NET/)
- [bluma.css](https://bulma.io/)
- [Font awesome free 5 web](https://fontawesome.com/) 
- [Argu](https://www.nuget.org/packages/Argu/)
