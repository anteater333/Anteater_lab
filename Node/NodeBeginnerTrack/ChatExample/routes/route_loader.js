let route_loader = {};

let config = {};

route_loader.init = (app, router, route_config) => {
    console.log('route_loader.init 호출됨.');
    
    config = route_config;

    return initRoutes(app, router);
};

// config.route_info에 정의된 라우팅 정보 처리
function initRoutes(app, router) {
    let infoLen = config.route_info.length;
    console.log('설정에 정의된 라우팅 모듈의 수 : %d', infoLen);

    for (let i = 0; i < infoLen; i++) {
        let curItem = config.route_info[i];

        // 모듈 파일에서 모듈 불러옴
        let curModule = require(curItem.file);
        console.log('%s 파일에서 모듈정보를 읽어옴.', curItem.file);

        // 라우팅 처리
        if (curItem.type == 'get') {
            router.route(curItem.path).get(curModule[curItem.method]);
        } else if (curItem.type == 'post') {
            router.route(curItem.path).post(curModule[curItem.method]);
        } else {
            router.route(curItem.path).post(curModule[curItem.method]);
        }

        console.log('라우팅 모듈 [%s]이 설정됨.', curItem.method);
    }
    
    // 라우터 객체 등록
    app.use('/', router);
}

module.exports = route_loader;