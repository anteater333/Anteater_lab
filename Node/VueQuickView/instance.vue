<meta charset="UTF-8">

<div id="app">
    {{ msg }}
    <br>
    <button v-on:click="onClick">Click!</button>
    <br>
    {{ greeting }}
</div>


<script src="https://cdn.jsdelivr.net/npm/vue/dist/vue.js"></script>
<script>
var vm = new Vue({
    el: '#app', // element - 뷰 인스턴스가 그려질 지점
    data: {  // 인스턴스의 상태를 저장하는 객체 == 템플릿에 바인딩할 데이터들의 집합
        msg: 'Hello world'
    },
    methods: { // 인스턴스 행동의 집합
        onClick: function(){ alert('Hello world'); }
    },
    computed: { // Methods와 비슷하지만 캐시가 있음. 함수가 사용하는 상태값에 변화가 없으면 함수를 수행하지 않고 저장한 캐쉬값을 바로 반환.
        greeting: function(){ return 'Hello ' + this.name; }
    }
});
</script>