const sendMessage = (type, body) => {
    window.external.sendMessage(JSON.stringify({Type:type, Body: body}))
}

const dispatcher = new Map()

const onMsg = (type, callback) => {
    dispatcher[type] = callback
}

window.external.receiveMessage(message => {
    const msg = JSON.parse(message)
    dispatcher[msg.Type](msg.Body)
})

window.addEventListener('load', (e)=> {
    const contentRoot = document.getElementById('content-root')

    onMsg("showMd", (mdhtml) => {
        contentRoot.innerHTML = mdhtml
    })

    sendMessage("notifyLoaded", "")
})

