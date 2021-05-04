
노드 리액트 기초 강의
===

https://www.youtube.com/watch?v=fgoMqmNKE18&t=122s

react-native까지 발전할 수 있도록...  



reactjs. library. not framework. Components : 재사용성이 뛰어남.  

DOM  
 * real DOM : list 중 하나가 바뀌면 모두 바뀐다  
 * virtual DOM : list 중 하나가 바뀌면 하나만 바뀐다 (snapshot과의 차이 분석, diffing)

webpack(bundle 툴, 특) 복잡하다.), babel(javascript 문법 브라우저 지원 관련 to ES5)  
create-react-app 으로 설정  

```
npx create-react-app .
```

npx?  
npm -g 옵션으로 global로 사용하는 것이 아니라  
npx를 사용해 npm registry에서 해당 모듈을 찾아서 다운로드 없이 실행.  


webpack은 src/ 를 관리, public/ 은 관리하지 않음  
이미지 등 asset은 src/ 에 넣어야.  

props vs state  

props:  
 * property
 * 부모 => 자식 데이터 전달
 * Props are immutable
   * 부모가 1이라고 자식에게 줬으면 1은 변경불가.
   * 다시 부모 컴포넌트에서 새 값을 전달해줘야함.
```
<ChatMessages
    messages={messages}
    currentMemeber={member}
/>
```

state:  
 * 컴포넌트 안에서 데이터 교환.
 * State is mutable
 * state가 변경되면 re-render
```
state = {
    message: '',
    attachFile: undefined,
    openMenu: false,
};
```

redux는 전역 state storage(store)를 제공  

react component dispatches -> action -> reducer -> store -> subscribe to react component

 * action : 무엇이 발생하였는가 (ex. articleId:42의 좋아요 버튼이 눌렸다)  
 * reducer : action으로 인해 변해진 state를 리턴 (pure function)  
 * store : Application의 전체 state tree를 감싸고 있다.  


class component vs functional component  
많은 기능/긴 코드 vs 적은 기능/짧은 코드  

react hook의 등장으로 functional component에서 다양한 기능 제공 가능


higherOrderComponent(HOC)  
component 접근 제어.  