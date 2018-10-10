const url = require('url');
// ES6 Expression
// import url from "url";

const curURL = url.parse('https://m.search.naver.com/search.naver?query=steve+fox&where=m&sm=mtp_hty');

const curStr = url.format(curURL);

console.log('주소 문자열 : %s', curStr);
console.dir(curURL);

const querystring = require('querystring');
const param = querystring.parse(curURL.query);

console.log('요청 파라미터 중 query의 값 : %s', param.query);
console.log('원본 요청 파라미터 : %s', querystring.stringify(param));