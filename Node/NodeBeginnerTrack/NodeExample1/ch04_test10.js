// 크기 지정
const output = '안녕 1!';
const buffer1 = new Buffer(10);
const len = buffer1.write(output, 'utf8');
console.log('첫 번재 버퍼의 문자열 : %s', buffer1.toString());

// 문자열 이용
const buffer2 = new Buffer('안녕 2!', 'utf8');
console.log('두 번째 버퍼의 문자열 : %s', buffer2.toString());

console.log('버퍼 객체의 타입 : %s', Buffer.isBuffer(buffer1));

// 버퍼 객체에 들어있는 문자열 데이터를 문자열 변수로 변환
const byteLen = Buffer.byteLength(output);
const str1 = buffer1.toString('utf8', 0, byteLen);
const str2 = buffer2.toString('utf8');

// 첫 번째 버퍼 객체의 문자열을 두 번째 버퍼 객체로 복사
buffer1.copy(buffer2, 0, 0, len);
console.log('두 번째 버퍼에 복사한 후의 문자열 : %s', buffer2.toString('utf8'));

// 두 버퍼 병합
const buffer3 = Buffer.concat([buffer1, buffer2]);
console.log('두 개의 버퍼를 붙인 후의 문자열 : %s', buffer3.toString('utf8'));