// 사용 패턴 : module.exports에 프로토타입 객체를 정의한 후 할당함
const User = require('./user10');

const user = new User('test01', '트와이스');

user.printUser();