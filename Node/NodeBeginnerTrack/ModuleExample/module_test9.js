// 사용 패턴 : new 연산자로 만든 인스턴스 객체를 module.exports에 할당한 후 그 인스턴스 객체의 함수를 호출함
const user = require('./user9').user;

user.printUser();