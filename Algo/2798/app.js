const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = 2; // 입력 갯수

rl.on('line', function(line) {
    input.push(line);
    if (input.length == inputNum) rl.close();
}).on('close', function() {
    
    algorila();

    process.exit();
});

function algorila() {
    const NM = input[0].split(' ').map((el) => parseInt(el));
    const N = NM[0];    // 카드의 개수
    const M = NM[1];    // 최대값

    let cards = input[1].split(' ').map((el) => parseInt(el));

    let sum = 0;
    let max = 0;

    const val = () => {    
        for (let i = 0; i < N; i++) {
            sum = cards[i];
            for (let j = 0; j < N; j++) {
                if (i == j) continue;
                sum += cards[j];
                if (sum > M) {
                    sum -= cards[j];
                    continue;
                }
                for (let k = 0; k < N; k++) {
                    if (i == k) continue;
                    else if (j == k) continue;
                    sum += cards[k];
                    if (sum > M) {
                        sum -= cards[k];
                        continue;
                    }
                    else if (sum == M) return sum;
                    else if (sum > max) max = sum;
                    sum -= cards[k];
                }
                sum -= cards[j];
            }
        }
        return max;
    }

    console.log(val());
}