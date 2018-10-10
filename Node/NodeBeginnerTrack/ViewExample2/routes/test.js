const test1 = (req, res) => {
    console.log('test 모듈 안에 있는 test1 호출됨.');

    res.writeHead('200', {'Content-Type':'text/html;charset=utf8'});

    // 뷰 템플릿을 이용하여 렌더링한 후 전송
    let context = {};
    req.app.render('test1_success', context, (err, html) => {
        if (err) {
            console.error('뷰 렌더링 중 오류 발생 : ' + err.stack);

            res.write('<h2>뷰 렌더링 중 오류 발생</h2>');
            res.write('<p>' + err.stack + '</p>');
            res.end();

            return;
        }
        console.log('rendered : ' + html);

        res.end(html);
    });
};

module.exports.test1 = test1;