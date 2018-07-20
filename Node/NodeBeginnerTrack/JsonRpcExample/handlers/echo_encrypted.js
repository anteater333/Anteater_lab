const crypto = require('crypto-js');
const echo = (params, callback) => {
    console.log('JSON-RPC echo_encrypted 호출됨.');
    console.dir(params);
    try {
        // 복호화 테스트
//      var encrypted = params[0];
        const secret = 'my secret';
        const decrypted = crypto.AES.decrypt(params[0], secret).toString(crypto.enc.Utf8);

        console.log('복호화된 데이터 : ' + decrypted);

        // 암호화 테스트
        const encrypted = '' + crypto.AES.encrypt(decrypted + ' -> 서버에서 보냄.', secret);

        console.log(encrypted);
        params[0] = encrypted;
    } catch(err) {
        console.dir(err);
        console.log(err.stack);
    }

    callback(null, params);
};

module.exports = echo;