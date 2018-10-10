// echo 오류 테스트
const echo_error = (params, callback) => {
    console.log('JSON-RPC echo_error 호출됨.');
    console.dir(params);

    // 파라미터 체크
    if (params.length < 2) {
        callback({ // error
            code : 400
            , message : 'Insufficient parameters'
        }, null);
        return;
    }

    const output = 'Success';
    callback(null, output);
};

module.exports = echo_error;