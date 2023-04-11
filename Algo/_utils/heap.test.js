const Heap = require("./heap").default;

const testHeap = new Heap((X, Y) => X < Y);

console.log(testHeap);

testHeap.push(3);
testHeap.push(5);
testHeap.push(2);
testHeap.push(3);
testHeap.push(5);
testHeap.push(2);
testHeap.push(3);
testHeap.push(5);
testHeap.push(2);
testHeap.push(3);
testHeap.push(5);
testHeap.push(2);

console.log(testHeap);

console.log(testHeap.poll());
console.log(testHeap.poll());
console.log(testHeap.poll());

console.log(testHeap.poll());
console.log(testHeap.poll());
console.log(testHeap.poll());

console.log(testHeap.poll());
console.log(testHeap.poll());
console.log(testHeap.poll());

console.log(testHeap.poll());
console.log(testHeap.poll());
console.log(testHeap.poll());

console.log(testHeap.poll());
