const fs = require('fs');

const inname = './output.txt'; 
const outname = './output2.txt'; 

fs.exists(outname, (exists) => {
    if(exists) {
        fs.unlink(outname, (err) => {
            if(err) throw err;
            console.log('기존 파일 [' + outname + '] 삭제함.');
        });
    }

    const infile = fs.createReadStream(inname, {flags: 'r'});
    const outfile = fs.createWriteStream(outname, {flags: 'w'});
    infile.pipe(outfile);
    console.log('파일 복사 [' + inname + '] -> [' + outname + ']');
});