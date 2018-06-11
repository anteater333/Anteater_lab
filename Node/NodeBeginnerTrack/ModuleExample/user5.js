// 우선순위 : module.exports > exports
// 서로 겹칠 경우 exports가 무시됨
module.exports = {
    getUser: () => {
        return {id : 'test01', name : '트와이스'};
    },
    group: {id : 'group01', name : '친구'}
};

exports.group = {id : 'group02', name : '가족'}; // 무시됨