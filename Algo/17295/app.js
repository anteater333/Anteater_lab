const TITLE1 = Array.from("Avengers");
const MERCY = Array.from(": ");
const TITLE2 = Array.from("Endgame");

let SPOILER = [];
let LOSER = [];
for (let i = 0; i < TITLE1.length; ) {
  const randChar = String.fromCharCode(
    Math.floor(Math.random() * (123 - 65)) + 65
  );

  if (TITLE1[i] == randChar) {
    SPOILER.push(randChar);
    i++;
  } else {
    LOSER.push(randChar);
    LOSER = LOSER.sort();
  }
}

for (let i = 0; i < MERCY.length; i++) {
  SPOILER.push(MERCY[i]);
}

for (let i = 0; i < TITLE2.length; ) {
  const randChar = String.fromCharCode(
    Math.floor(Math.random() * (123 - 65)) + 65
  );

  if (TITLE2[i] == randChar) {
    SPOILER.push(randChar);
    i++;
  } else {
    LOSER.push(randChar);
    LOSER = LOSER.sort();
  }
}

console.log(SPOILER.join(""));
