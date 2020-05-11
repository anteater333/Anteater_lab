const rl = require('readline').createInterface({
    input: process.stdin,
    output: process.stdout
});

let input = []; // 입력 큐
let inputNum = 0; // 입력 갯수

rl.on('line', function(line) {
    if (inputNum == 0) inputNum = parseInt(line);
    else {
        input.push(line);
        if (input.length == inputNum) rl.close();
    }
}).on('close', function() {
    
    algorila();

    process.exit();
});

function algorila() {
    let xy; let x; let y;
    let cxy; let cx; let cy;
    let rank;

    let ranks = [];

    for (let i = 0; i < inputNum; i++) {
        xy = input[i].split(' ');
        x = parseInt(xy[0]); y = parseInt(xy[1]);

        rank = 1;

        for (let j = 0; j < inputNum; j++) {
            if (i === j) continue;   // 자기 자신과의 비교는 패스

            cxy = input[j].split(' ');
            cx = parseInt(cxy[0]); cy = parseInt(cxy[1]);

            if (x < cx && y < cy) rank++;
        }
        
        ranks.push(rank);
    }
    
    ranks.forEach(rank => {
        process.stdout.write(rank + " ");
    });
}