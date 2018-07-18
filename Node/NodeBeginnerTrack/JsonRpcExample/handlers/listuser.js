const listuser = (params, callback) => {
    console.log('JSON-RPC listuser 호출됨.');
    console.dir(params);

    // 데이터베이스 객체 참조
    const database = global.database;
    if (database) {
        console.log('database 객체 참조됨.');
    } else {
        console.log('database 객체 참조 불가.');
        callback({
            code : 410
            , message : 'database 객체 참조 불가.'
        }, null);
        return;
    }

    if (database.db) {
        // 1. 모든 사용자 검색
        database.UserModel.findAll((err, results) => {
            if (results) {
                console.log('결과물 문서 데이터의 개수 : %d', results.length);

                let output = [];
                for (let i = 0; i < results.length; i++) {
                    let curId = results[i]._doc.email;
                    let curName = results[i]._doc.name;
                    output.push({id : curId, name : curName});
                }

                console.dir(output);
                callback(null, output);
            }
        });
    }
};

module.exports = listuser;