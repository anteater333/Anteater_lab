#!/usr/bin/env zx

/**
 * zx, A tool for writing better scripts
 * https://github.com/google/zx
 */

/**
 * 220814 깃허브 탐방 중 신기해 보이는 것이 있어서.
 * 특히 내가 자주 쓰는 아이디 중 하나가 zx1056인데 친숙함이 느껴짐.
 */

/**
 * ex. Async
 */
await Promise.all([$`sleep 1; echo 1`, $`sleep 2; echo 2`, $`sleep 3; echo 3`]);

/**
 * ex. Importing module
 */

import { $ } from "zx";
// or
import "zx/globals";

await (async function () {
  await $`ls -la`;
})();

/**
 * ex. Event listener for stdout
 */

import chalk from "chalk";
const echoCmd = $`echo "Hello World";`;

echoCmd.stdout.on("data", (data) => {
  console.log(`echo : `, chalk.blue(data.toString()));
});
