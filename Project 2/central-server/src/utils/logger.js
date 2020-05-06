const info = (...args) => {
    console.log('\x1b[36m%s\x1b[0m', '[INFO]:', ...args);
}

const warn = (...args) => {
    console.log('\x1b[33m%s\x1b[0m', '[WARN]:', ...args);
}

const error = (...args) => {
    console.log('\x1b[31m%s\x1b[0m', '[ERROR]:', ...args);
}

const debug = (...args) => {
    console.log('\x1b[35m%s\x1b[0m', '[DEBUG]:', ...args);
}

module.exports = {
    info,
    warn,
    error,
    debug
};