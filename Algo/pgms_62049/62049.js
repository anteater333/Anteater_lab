function solution(n) {
    var answer = fold(n);
    return answer;
}

function fold(n) {
    if ( n === 1 )
        return [0];
    else {
        const front = fold(n-1);
        const back = toggle(front).reverse();
        return front.concat([0]).concat(back);
    }
}
    
function toggle(arr) {
    let rt = new Array(arr.length);
    for ( let i = 0; i < arr.length; i++) {
        if (arr[i] === 0) {
            rt[i] = 1;
        } else {
            rt[i] = 0;
        }
    }
    return rt;
}

const input = process.argv.slice(2)

console.log(`fold(${input})`);
console.log(`${solution(input)}`);