class Events {

    constructor() {

        this.clients = {}
    }

    addClient(id, res) {
        const headers = {
            'Content-Type': 'text/event-stream',
            'Cache-Control': 'no-cache',
            'Connection': 'keep-alive'
        }
        res.writeHead(200, headers)
        res.flushHeaders()
        this.clients[id] = res
        ///  console.log(clients)
    }

    sendInfo(issue) {
        const { creator, assignee } = issue
        try {
            const messageToSent = JSON.stringify(issue);
            this.clients[creator].write("data:" + messageToSent + "\n\n")
            this.clients[assignee].write("data:" + messageToSent + "\n\n")
        } catch (e) {
            console.log(e)
        }
    }

}


module.exports = new Events()