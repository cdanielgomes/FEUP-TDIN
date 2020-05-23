class Events {

    constructor() {

        this.solver = {}
        this.worker = {}
    }

    removeClient(req) {
        const role = req.userRole
        const email = req.userEmail
        delete this[role][email]
    }


    addClient(req, res) {
        
        const headers = {
            'Content-Type': 'text/event-stream',
            'Cache-Control': 'no-cache',
            'Connection': 'keep-alive'
        }

        try {
            this[req.userRole] = { ...this[req.userRole], [req.userEmail]: res }
            req.on("end", () => {
                console.log("end")
                this.removeClient(req)
            })
            req.on("close", () => {
                console.log("close")
                this.removeClient(req)
            })
            res.writeHead(200, headers)
            res.flushHeaders()

        } catch (e) {
            console.log(e)
        }

    }

    sendInfo(type, issue, question) {
        try {
            switch (type) {

                case "issue":
                    console.log(this.solver)
                    for (let solver in this.solver) this.solver[solver].write(this.message({ type: "issue", issue }))
                    break;
                case "question":
                    this.solver[issue.assignee].write(this.message({ type: "question", question, issue }))
                    break;
                case "client":
                    this.worker[issue.creator].write(this.message(issue))
                    break;
                default:
                    console.log("SHOUDLNT BE HERE")
                    break;
            }

        } catch (e) {
            console.log(e)
        }
    }


    message(obj) {
        return "data: " + JSON.stringify(obj) + "\n\n";
    }

}


module.exports = new Events()