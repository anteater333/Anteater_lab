let result = 0; // var 대신 let을 쓰는 습관을 가지도록 하자

console.time('duration_sum');   // js는 '와 "를 구분하지 않는다

for (let i = 1; i <= 1000; i++)
{
    result += i;
}

console.timeEnd('duration_sum');
console.log('1부터 1000까지 더한 결과물 : %d', result);

console.log('현재 실행한 파일의 이름 : %s', __filename);
console.log('현재 실행한 파일의 패스 : %s', __dirname);

let Person = {
    name: '트와이스',
    age: 20
};
console.dir(Person);