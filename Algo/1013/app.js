/* node.js */

const rl = require("readline").createInterface({
    input: process.stdin,
    output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = -1; // 입력 갯수

rl.on("line", function (line) {
    if (inputNum == -1) {
        inputNum = line;
    } else {
        input.push(line);
        if (input.length == inputNum) rl.close();
    }
}).on("close", function () {
    algorila();

    process.exit();
});

/** Automata */
const STATE = [
    0b0000000, 0b0000001, 0b0000010, 0b0000100, 0b0001000, 0b0010000, 0b0100000,
    0b1000000,
];

function algorila() {
    input.forEach((signal) => {
        const arrSignal = signal.split("").map((a) => +a);

        let prevState = STATE[2];

        for (let i = 0; i < arrSignal.length; i++) {}
    });
}

/**
 * (100+1+ | 01)+
 * @param {*} prevState
 * @returns
 */
function automataCompiler(prevState, currBit) {
    let nextState = STATE[0];
    if (prevState & STATE[1]) {
        // | 0
        if (currBit == 0) {
            nextState = STATE[0];
        } else if (currBit == 1) {
            nextState = STATE[2];
        }
    } else if (prevState & STATE[2]) {
        // | 01 / 초기 상태
        if (currBit == 0) {
            nextState = STATE[1];
        } else if (currBit == 1) {
            nextState = STATE[3];
        }
    } else if (prevState & STATE[3]) {
        // (1
        if (currBit == 0) {
            nextState = STATE[4];
        } else if (currBit == 1) {
            nextState = STATE[0];
        }
    } else if (prevState & STATE[4]) {
        // (10
        if (currBit == 0) {
            nextState = STATE[5];
        } else if (currBit == 1) {
            nextState = STATE[0];
        }
    } else if (prevState & STATE[5]) {
        // (100
        if (currBit == 0) {
            nextState = STATE[6];
        } else if (currBit == 1) {
            nextState = STATE[7];
        }
    } else if (prevState & STATE[6]) {
        // (100+
        if (currBit == 0) {
            nextState = STATE[6];
        } else if (currBit == 1) {
            nextState = STATE[7];
        }
    } else if (prevState & STATE[7]) {
        // (100+1+ |
        if (currBit == 0) {
        } else if (currBit == 1) {
        }
    }

    return nextState;
}
