/* node.js */

const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

const line0 = "어느 한 컴퓨터공학과 학생이 유명한 교수님을 찾아가 물었다."
const line1 = "\"재귀함수가 뭔가요?\""
const line2 = "\"잘 들어보게. 옛날옛날 한 산 꼭대기에 이세상 모든 지식을 통달한 선인이 있었어."
const line3 = "마을 사람들은 모두 그 선인에게 수많은 질문을 했고, 모두 지혜롭게 대답해 주었지."
const line4 = "그의 답은 대부분 옳았다고 하네. 그런데 어느 날, 그 선인에게 한 선비가 찾아와서 물었어.\""
const line5 = "\"재귀함수는 자기 자신을 호출하는 함수라네\""
const lineLevel = "____"
const lineE = "라고 답변하였지."

let input = []; // 입력 큐
let inputNum = 1; // 입력 갯수

rl.on('line', function(line) {
    input.push(line);
    if (input.length == inputNum) rl.close();
}).on('close', function() {
    
    algorila();

    process.exit();
});

function algorila() {
    console.log(line0)

    OUAT(parseInt(input[0]), 0)
}

function OUAT(base, n) {
    let space = lineLevel.repeat(n)
    console.log(space + line1)
    if (n == base) {
        console.log(space + line5)
    }
    else {
        console.log(space + line2)
        console.log(space + line3)
        console.log(space + line4)
        OUAT(base, n + 1)
    }
    console.log(space + lineE)
}