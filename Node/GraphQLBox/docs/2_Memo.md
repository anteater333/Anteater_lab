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
`
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