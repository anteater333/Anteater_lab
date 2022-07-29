const TESTSIZE = 50000;

function consoleOutput() {
  for (let i = 0; i < TESTSIZE; i++) {
    console.log(i);
  }
}

function bufferOutput() {
  const buffer = [];
  for (let i = 0; i < TESTSIZE; i++) {
    buffer.push(i);
  }
  console.log(buffer.join("\n"));
}

function rawDebugOutput() {
  for (let i = 0; i < TESTSIZE; i++) {
    process._rawDebug(i);
  }
}

// console.time("consoleOutput");
// consoleOutput();
// console.timeEnd("consoleOutput");

// console.time("bufferOutput");
// bufferOutput();
// console.timeEnd("bufferOutput");

console.time("rawDebugOutput");
rawDebugOutput();
console.timeEnd("rawDebugOutput");
