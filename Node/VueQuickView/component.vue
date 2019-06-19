<meta charset="UTF-8">

<div id="child">Hello {{ name }}, {{ nickname }}</div> <!-- 자식 컴포넌트 -->
<div id="app">
<Child nickname="Andy"></Child>
</div> <!-- 부모 컴포넌트 -->

<script src="https://cdn.jsdelivr.net/npm/vue/dist/vue.js"></script>
<script>
/**
컴포넌트란 조합화며 화면을 구성할 수 있는 블록을 의미한다.
재사용 가능해 생산성을 증가시켜준다. => 캡슐화
 */
Vue.component('Child', {
    el: '#child',
    data: function() {
        return {
            name: 'Andruw'
        }   // 컴포넌트는 재사용 가능하기 때문에 고유한 데이터를 가지고 있어야 한다.
    },
    props: ['nickname'], // 데이터는 부모 -> 자녀 방향으로만 전달할 수 있다. 자녀 컴포넌트의 프로퍼티를 이용해 데이터를 전달할 수 있다.
                         // 5번줄에서 부모 컴포넌트가 nickname 속성을 Andy로 전달함.
    methods: {  // 자녀 -> 부모 방향으로 데이터 전달은 불가능하나 이벤트 전달로 커뮤니케이션은 가능.
                // 버튼 엘레멘트가 클릭 이벤트를 발생하고 이를 상위 엘레멘트의 누군가 수신.
        onClick: function() {
            this.$emit('typed');
        }   // <Child v-on:typed="onTyped"></Child>
    }
});


var vm = new Vue({
    el: '#app'
});
</script>