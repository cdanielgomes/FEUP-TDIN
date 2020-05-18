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

    sendInfo(id, message) {
        try{
            const messageToSent =  JSON.stringify(message);
            this.clients[id].write("data:" + messageToSent +"\n\n")
            
        } catch (e){
            console.log(e)
        }
    }

}


module.exports = new Events()