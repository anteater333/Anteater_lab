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
