// Reason : user2.js 파일에서 exports에 객체를 할당하였으므로,
//          require()를 호출할 때 자바스크립트에서 새로운 변수로 처리함
//          결국 아무 속성도 없는 { } 객체가 반환됨.
const user = require('./user2');

console.dir(user);

function showUser() {
    return user.getUser().name + ', ' + user.group.name;
}

console.log(showUser());