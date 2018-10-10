// require() 메소드는 ./user3의 module.exports를 반환함
const user = require('./user3');

function showUser() {
    return user.getUser().name + ', ' + user.group.name;
}

console.log('사용자 정보 : %s', showUser());