open System
open System.Drawing
open PhotinoNET
open System.Text.Json
open System.Reflection
open System.IO
open Markdig

type Message = {Type: string; Body: string}

let sendMessage (wnd:PhotinoWindow) (message:Message) =
    let msg = JsonSerializer.Serialize(message)
    wnd.SendWebMessage(msg) |> ignore

let onMessage mdhtml (wnd:Object) (message:string) =
    let pwnd = wnd :?>PhotinoWindow
    let msg = JsonSerializer.Deserialize<Message>(message)
    match msg.Type with
    | "notifyLoaded" -> sendMessage pwnd {Type="showMd"; Body=mdhtml}
    | "notifyDeb" -> printfn "deb: %s" msg.Body
    | _ -> failwithf "Unknown msg type %s" msg.Type

let launchBrowser (mdhtml : string)  =
    let onFinish (results:string array) = ()
    let onCancel () = ()


    let asm = Assembly.GetExecutingAssembly()

    let load (url:string) (prefix:string) =
        let fname = url.Substring(prefix.Length)
        asm.GetManifestResourceStream($"mdvcat.assets.{fname}")

    let win = (new PhotinoWindow(null))

    win.LogVerbosity <- 0
    win.SetTitle("mdvcat")
        .SetUseOsDefaultSize(false)
        .Center()
        .RegisterCustomSchemeHandler("resjs",
            PhotinoWindow.NetCustomSchemeDelegate(fun sender scheme url contentType ->
                contentType <- "text/javascript"
                load url "resjs:"))
        .RegisterCustomSchemeHandler("rescss", 
            PhotinoWindow.NetCustomSchemeDelegate(fun sender scheme url contentType ->
                contentType <- "text/css"
                load url "rescss:")) |> ignore
                
    let asm = Assembly.GetExecutingAssembly()
    use stream = asm.GetManifestResourceStream("mdvcat.assets.index.html")
    use sr = new StreamReader(stream)
    let text = sr.ReadToEnd()
    // printfn "content: %s" text

    win.RegisterWebMessageReceivedHandler(System.EventHandler<string>(onMessage mdhtml))
        .Center()
        .SetSize(new Size(1200, 700))
        .LoadRawString(text)
        .WaitForClose()


[<EntryPoint>]
let main argv =
    if argv.Length <> 1 then
        printfn "Usage: mdvcat <filepath>"
        1
    else
        let path = argv.[0]
        let pipeline = MarkdownPipelineBuilder().Build()
        let md = File.ReadAllText path

        let html = Markdig.Markdown.ToHtml(md, pipeline)
        
        launchBrowser html
        // printfn "deb %s" html
        0 // return an integer exit code