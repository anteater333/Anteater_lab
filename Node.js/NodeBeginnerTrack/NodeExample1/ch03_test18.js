function Person(name, age) {
    this.name = name;
    this.age = age;
}

Person.prototype.walk = function(speed) {
    console.log(speed + 'km/h 속도로 걸어갑니다.');
}

/*
// ES2015(ES6) Expression
class Person {
    constructor(name, age) {
        this.name = name;
        this.age = age;
    }
    walk(speed) {
        console.log(speed + 'km/h 속도로 걸어갑니다.');
    }
}
*/

const person01 = new Person('소녀시대', 20);
const person02 = new Person('걸스데이', 22);

console.log(person01.name + '객체의 walk(10)을 호출합니다.');
person01.walk(10);