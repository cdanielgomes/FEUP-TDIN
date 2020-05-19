class Events {

    constructor() {

        this.solver = {}
        this.worker = {}
    }

    addClient(req, res) {
        const headers = {
            'Content-Type': 'text/event-stream',
            'Cache-Control': 'no-cache',
            'Connection': 'keep-alive'
        }
        res.writeHead(200, headers)
        res.flushHeaders()
        this[req.userRole] = { ...this[role], [req.userEmail]: res }
        req.on("close", function () {
            delete this[req.userRole][req.userEmail]
        })
        console.log(this.worker)
        console.log(this.solver)

    }

    sendInfo(type, issue, question) {
        try {
            switch (type) {

                case "issue":
                    for (let solver of this.solver) solver.write(this.message({ type: "issue", issue }))
                    break;
                case "question":
                    this.solver[issue.assignee].write({ type: "question", question, issue })
                    break;
                case "client":
                    this.client[issue.creator].write(issue)
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