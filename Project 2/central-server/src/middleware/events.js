class Events  {

    constructor(){

        this.clients = {}
    }
    
    addClient(id, res) {
        res.writeHead(200, {
            'Content-Type': 'text/event-stream',
            'Cache-Control': 'no-cache',
            'Connection': 'keep-alive'
          })
        this.clients[id] = res
    }

    sendInfo (id, message) {
        this.clients[id].write(JSON.stringify(message))
    }

}


module.exports = new Events()