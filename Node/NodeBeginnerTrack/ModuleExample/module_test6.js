// 가상으로 require() 함수를 정의해 require()를 통한 모듈화 이해하기
var require = (path) => {   // let, const 모두 적용 안됨
    const exports = {
        getUser : () => {
            return {id : 'test01', name : '소녀시대'};
        },
        group : {id : 'group01', name : '친구'}
    };

    return exports;
}

const user = require('...');

function showUser() {
    return user.getUser().name + ', ' + user.group.name;
}

console.log('사용자 정보 : %s', showUser());