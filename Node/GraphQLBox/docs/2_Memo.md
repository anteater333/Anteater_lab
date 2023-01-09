# 키워드 및 개념 정리

블로그에 적어야 되는 내용들인데, 일단 워낙 이것저것 동시에 만져보고 있기도 하고. 나중에 안까먹도록 간단하게 알게된 내용들을 적어두려함.

## Query & Mutation

React Query, GraphQL 둘 다 해당. 아무튼 둘 다 "질의Query" 를 위한 기술이니까.

### Query

질의. 데이터의 조회에 사용함. (Read)

### Mutation

변경. 데이터의 변경에 사용함. (Create, Update, Delete)

## GraphQL over HTTP

https://graphql.github.io/graphql-over-http/draft/#

우선은 express-graphql 사용하려다가 저장소에서 확인한 문구.

> express-graphql is DEPRECATED.

네 그렇답니다. 이렇게 말하면서 [GraphQL over HTTP](https://github.com/graphql/graphql-http) 라는 개념을 설명하던데.

> This specification details how GraphQL should be served and consumed over HTTP in order to maximize interoperability between clients, servers and tools.

사실 많이 혼란스럽긴 하다. 그냥 원래 HTTP로 전달하던거 아니였어?

> HTTP 전송은 GraphQL 스펙에 의해 커버되지 않습니다. (GraphQL over HTTP 저장소에서 논의가 진행되고는 있습니다) 그래도 express-graphql 이라는 표준 구현과 Serving over HTTP 문서에 의해 큰 틀은 정의가 되어 있습니다.  
> http://sixmen.com/ko/tech/page/2/

흥미로운 부분은 HTTP를 사용한 GraphQL 통신에서 HTTP Status code는 모두 200을 사용한다고. 심지어 오류까지. 이 사실로부터 GraphQL이 HTTP에 종속적인 것은 아니란 단서를 얻을 수 있지 않나..

아무튼, GraphQL over HTTP는 2022년 쯤 부터 막 논의되기 시작한 새로운 스펙인 것 같다. 말인 즉슨, 완전 새삥일 가능성이 크다. 바로 따르기엔 조금 위험할 수..도.. 있나..?

https://graphql.org/learn/serving-over-http/

스펙 문서에서 이 글을 언급하고 있다. 애초에 HTTP에 GraphQL이 종속적인건 아닌게 맞긴 한데 HTTP가 널리 사용되고 있는 것도 사실이다.

GraphQL over HTTP의 가장 큰 의의는 Best practice였던 HTTP를 통한 GQL 사용을 명세화 한다는 것에 있는듯.

GET 메소드는 어떤 식으로 사용되어야 하는가, 특히 Mutation에는 사용되지 말아야 한다던가, POST 메소드는 어떤 식으로 사용되어야 하는가 등등을 안내하고있다.

## GraphQL Mutation 설계하기

https://fe-developers.kakaoent.com/2022/220113-designing-graphql-mutation/
갓갓캇카오. 사실 대단한건 원문 작성한곳이 아닌가..

아무튼, Mutation이란 개념은 아직도 생소하다. 일단은 쿼리와 비슷하게 행동을 나타낸다고는 한다.

- 작명. mutation 이름에 동사를 먼저 작성한다. 가능하다면 그 뒤에 목적어나 명사를 사용한다. 그리고 camelCase를 사용한다.
- 명확성. 가능한 구체적으로 만든다. mutation은 사용자가 취할 수 있는 의미있는 행동을 나타내야 한다.
- 입력 객체. 클라이언트에서 더 쉽게 mutation을 실행할 수 있도록 단일하고(single), 필수적이고(required), 고유한(unique) 입력(input) 객체 타입으로 작성한다.
- 고유한 Payload 타입. 각 mutation에 맞는 고유한 payload 타입을 사용하고 mutation 결괏값을 payload 타입에 필드로 추가한다.
- 중첩. 가능하면 어디서든 중첩을 사용한다.

## `gql` Tagged Template Literal

### Tagged Template Literal

TTL이 뭐냐 일단.

```javascript
gql`
  query {
    ...
  }
`;
```

이거에요 이거.

Template Literal(``)에 이름을 달아놓은 문법.

```javascript
const person = "Mike";
const age = 28;

function myTag(strings, personExp, ageExp) {
  const str0 = strings[0]; // "That "
  const str1 = strings[1]; // " is a "
  const str2 = strings[2]; // "."

  const ageStr = ageExp > 99 ? "centenarian" : "youngster";

  // We can even return a string built using a template literal
  return `${str0}${personExp}${str1}${ageStr}${str2}`;
}

const output = myTag`That ${person} is a ${age}.`;

console.log(output);
// That Mike is a youngster.
```

실제론 이런 식으로 구현/사용 한다고함. 함수의 두 번째 인자를 `...args` 이렇게 만드는 방식이 많이 사용되는듯.

### graphql-tag

여기에 눈길을 주게 된 이유는, `graphql-js`로만 구현하는게 영 불만족스러워서. `GraphQLSchema` 클래스를 사용하는 방식 보단 좀 더 graphql 본연의 문법을 쓰고싶은데, `buildSchema`에 GQL 문자열을 넣으면 되긴 하지만... 뭔가 2% 부족한 느낌.

`Apollo`는 최대한 안쓰려고 하고 있지만(너무 이거까지 해버린다고? 싶어서), 얘네가 만든 `gql` tagged template은 조금 탐나더라. 그래서 그 기능만 떼서 만들어놓은 [`graphql-tag`](https://github.com/apollographql/graphql-tag)란걸 찾았다. 알아서 GQL AST를 반환해준다는데, 자세한건 써보고 더 적어보자.

`buildASTSchema` 라는것도 있었다. 생각보다 `graphql-js` 다채로운데, 문제는 이거 도큐먼트가 안보여... 이거 맞나 의심이 피어오르는중.

## Mutation을 사용하는 법

이렇게 쿼리를 정의하고

```graphql
mutation CreateTodo($input: CreateTodoInput!) {
  createTodo(input: $input) {
    todo {
      title
      content
    }
  }
}
```

이렇게 variable을 사용하면 된다.

```
{
    "input": {
        "title": "콤퓨타",
        "content": "좋은걸로"
    }
}
```

클라이언트에서 보내는 것에 variables라는게 또 있는줄은 몰랐음. 어떻게 저 $input에 값을 넣나 계속 고민중이었는데

## Type-Graphql

[Typescript w/ graphql](https://m.blog.naver.com/izure/222443538184)

Typescript에서 GraphQL은 거추장스럽다. Type 선언이 반복된다. 따라서 Type-GraphQL을 사용한다. 끝.

## GraphQL Client

혼자서 GraphQL을 배우겠다고 난리치고 있으니까 상당히 불편한게, 백엔드 만드는데 며칠 걸리고, 프론트엔드 만드는데 며칠 걸리고...

아무튼 조금이라도 공수를 줄여보고자 클라이언트는 유명 라이브러리를 써보고자 한다. Apollo(이건 서버/클라이언트 둘 다 있음)라는게 제일 유명한 것 같고, Relay도 아주 많이 들어봄. 일단은... Relay..?

문제는, React-Query와의 관계가 아주 애매해졌다. relay를 쓰던 apollo를 쓰던 어쨌든 얘네들이 요청에 대한 상태 관리도 해준다. 그럼 굳이 React-query를 들고 있을 이유가...?

> "뭐라고요?, GraphQL 은 서버의 쿼리 언어, Redux 는 클라이언트의 상태 관리 라이브러리잖아요. 어떻게 대체가 되죠?"

https://velog.io/@minsangk/%EB%B2%88%EC%97%AD-GraphQL%EC%9D%80-%EC%96%B4%EB%96%BB%EA%B2%8C-Redux%EB%A5%BC-%EB%8C%80%EC%B2%B4%ED%95%98%EB%8A%94%EA%B0%80-cijz6lfvf4

유익한 글이었다

근데 Relay 솔직히, 설치부터 머리 어프다. relay 라는 단일 라이브러리 하나만 받는게 아니라 뭐 많어...  
프레임워크 수준인데?

Relay is a JavaScript framework for building data ... - GitHub

아니 진짜 프레임워크인데?

일단 가능성을 정리해보자..

- 직접 GQL을 래핑한 Server Call 함수들 + react-query를 통해 상태 관리
  - 백엔드에서 구현한 방식과 가장 유사
  - 현재 스택(백엔드에선 express)에 딱 GQL만 최소로 올린 느낌
  - 기본 + 심플에 충실하자
  - 근데 그냥 REST API를 GQL로 거울 복제 하는 것과 다를바 없는게 아닌가 = GQL만의 장점을 학습하기 위해 또 학습을 많이 해야할지도
- Apollo Client
  - 일단 얘는 안찾아봐서 사이즈가 얼마만할지 모르겠다.
  - 적어도 프레임워크를 표방하고 있는 것 같진 않다.
  - useQuery를 주는등, react-query가 제공하는 기능을 대체.. 할 수 있나..?
    - REST API를 사용하는 부분은 남겨놔야해서 react-query도 남겨놓는다고 한들, 둘이 제공하는 메소드 이름이 똑같은데, 같이 쓰면 조금 난감할 수도 있겠다.
- Relay
  - React 만들고 GraphQL도 만든 페이스북이 만든 GraphQL 클라이언트 **프레임워크**
  - 프레임워크를 표방한다는 점에서 충격
    - 배울거 너무 많아짐
  - 자체적으로 무슨 컴파일러도 쓰고 있고, CR(elay)A도 있고
  - 좋아보이는 것과 별개로, 지금 이거 시작해버리면 또 1주 동안 이거 배우는데 시간 날릴 것 같다.
  - 괜히 레퍼런스들에서 Relay를 망설이는 분위기가 느껴지는게 아니었구나
    - 진짜 딱 "좋긴 한데, 지금 상황엔 너무 무겁지 않나.." 라는 느낌
