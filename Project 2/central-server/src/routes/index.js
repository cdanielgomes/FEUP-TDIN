var express = require('express');
var router = express.Router();

router.get('/', (req, res) => {
    res.status(200).json({
        message: "The Central Server is running",
    });
});

module.exports = router;