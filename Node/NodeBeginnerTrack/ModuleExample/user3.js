// module.exports에 객체를 할당
const user = {
    getUser : () => {
        return {id : 'test01', name : '트와이스'};
    },
    group: {id : 'group01', name : '친구'}
}

module.exports = user;