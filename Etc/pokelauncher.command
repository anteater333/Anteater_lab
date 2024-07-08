echo "POKEROUGE SERVER RUNNING"

cd /Users/anteater/Sandbox/pokerogue

echo "CHECKING FOR UPDATES"

git fetch
git pull
npm ci

echo "\nPRESS "o" TO OPEN BROWSER"

npm run start
