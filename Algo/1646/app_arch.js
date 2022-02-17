/* node.js */

const rl = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout,
});

let input = []; // 입력 큐
let inputNum = 1; // 입력 갯수

rl.on("line", function (line) {
  if (inputNum == -1) {
    inputNum = line;
  } else {
    input.push(line);
    if (input.length == inputNum) rl.close();
  }
}).on("close", function () {
  algorila();

  process.exit();
});

let N,
  start = -1,
  dest;
let startNode = null;

let counter = 1;
function getCounter() {
  return counter++;
}

function generatePeTree(N, parent) {
  const root = new peNode(parent);
  if (N == 0 || N == 1) {
    return root;
  }

  root.setLeft(generatePeTree(N - 2, root));
  root.setRight(generatePeTree(N - 1, root));

  return root;
}

function peNode(parentNode) {
  this.parent = parentNode;

  /** 시작점 미리 구해두기 */
  if (counter == start) {
    startNode = this;
  }
  this.id = getCounter();

  /** @type {peNode} */
  this.leftChild = null;
  /** @type {peNode} */
  this.rightChild = null;

  this.visited = false;

  this.setLeft = function (childNode) {
    this.leftChild = childNode;
  };
  this.setRight = function (childNode) {
    this.rightChild = childNode;
  };
}

/**
 *
 * @param {peNode} start
 * @returns
 */
function travel(start) {
  const root = start;
  const queue = [{ node: root, path: "" }];
  let head = 0;

  let path = "";
  while (head < queue.length) {
    const cursor = queue[head++];
    const cNode = cursor.node;
    const cPath = cursor.path;
    cNode.visited = true;

    if (cNode.id == dest) {
      path = cPath;
      break;
    }

    if (cNode.leftChild && !cNode.leftChild.visited) {
      queue.push({ node: cNode.leftChild, path: cPath + "L" });
    }
    if (cNode.rightChild && !cNode.rightChild.visited) {
      queue.push({ node: cNode.rightChild, path: cPath + "R" });
    }
    if (cNode.parent && !cNode.parent.visited) {
      queue.push({ node: cNode.parent, path: cPath + "U" });
    }
  }

  return path;
}

function algorila() {
  [N, start, dest] = input[0].split(" ").map((a) => +a);
  const peTree = generatePeTree(N, null);

  console.log(travel(startNode));
  console.log(process.memoryUsage().heapUsed / 1024 / 1024);
}
