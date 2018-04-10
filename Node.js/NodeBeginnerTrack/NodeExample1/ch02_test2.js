console.log('argv 속성의 파라미터 수 : ' + process.argv.length);
console.dir(process.argv);

if (process.argv.length > 2) {
    console.log('세 번째 파라미터의 값 : %s', process.argv[2]);
}

process.argv.forEach((item, index) => {
    console.log(index + ' : ', item);
}); // 람다식

console.dir(process.env);

console.log('OS 환경 변수의 값 : ' + process.env['OS']);