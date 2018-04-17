const util = require('util');
const EventEmitter = require('events').EventEmitter;

const Calc = function() { // 람다식 사용시 에러 발생. 원인은 모르겠음.
    const self = this;

    this.on('stop', () => {
        console.log('Calc에 stop event 전달됨.');
    });
};

util.inherits(Calc, EventEmitter);

Calc.prototype.add = (a, b) => {
    return a + b;
};

module.exports = Calc;
module.exports.title = 'calculator';