const MAX = 10000;

let selfNum = new Array(MAX + 1).fill(0);

function d(n) {
    let val = n;
    while (n >= 10) {
        val += n % 10;
        n = Math.floor(n / 10);
    }

    val += n;

    return val;
}

function net(n) {
    let num = d(n);
    if (num > MAX) return;
    else if (selfNum[num] != 0) return;
    else {
        selfNum[num] = num;
        return net(num);
    }
}

selfNum[0] = -1;

for (let i = 1; i <= MAX; i++) net(i);

selfNum.forEach((el, index) => {
    if (el == 0) console.log(index);
});