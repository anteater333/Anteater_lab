// Reason : exports는 속성으로, exports에 속서을 추가하면 모듈에서 접근하지만
//          exports에 객체를 지정하면 자바스크립트에서 새로운 변수로 처리함
exports = {
    getUser : () => {
        return {id : 'test01', name : '트와이스'};
    },
    group: {id : 'group01', name : '친구'}
}